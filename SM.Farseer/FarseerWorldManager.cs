using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
//using Newtonsoft.Json;
using SM;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SM.Farseer
{
    // Unico detentore del oggetto World
    // quindi l'unico in grado di creare oggetti Farseer
    public class FarseerWorldManager
    {
        private WorldWatch _worldWatch;
        private World _world;

        private Dictionary<string, IFarseerId> _map = new Dictionary<string, IFarseerId>();

        private List<LoopCoroutine> _loopCoroutine = new List<LoopCoroutine>();

        public FarseerWorldManager()
        {
            _world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            _worldWatch = new WorldWatch(dt => step(dt));
        }

        private void _addFarseerObject(IFarseerId obj)
        {
            _map.Add(obj.Id, obj);
        }

        public object Find(string name)
        {
            if (_map.ContainsKey(name))
                return _map[name];
            // WARNING
            return null;
        }

        public T Find<T>(string name) where T : class
        {
            var x = Find(name);
            if (typeof(T) != x.GetType()) return null;
            return (T)x;
        }

        public T FindObject<T>(string name) where T : class
        {
            var x = Find(name) as IFarseerObject;
            if(x == null) return null;
            var y = x.Object;
            if (typeof(T) != y.GetType()) return null;
            return (T)y;
        }

        public void AddFarseerBehaviour(IFarseerBehaviour x)
        {
            _loopCoroutine.Add(new LoopCoroutine(x.Loop));
        }
        public void AddBodyControl(IBodyControl bodyControl, Vector2 originPosition)
        {
            var body = BodyFactory.CreateBody(_world, Vector2.Zero);
            CodeGenerator.AddCode(String.Format("var {0} = BodyFactory.CreateBody(World, Vector2.Zero);", body.g()));
            _addFarseerObject(new BodyUpdater(bodyControl, body, originPosition));
            foreach(var p in bodyControl.Points)
            {
                _addFarseerObject(p);
            }
        }

        //public void AddBreakableBodyControl(IBreakableBodyControl bodyControl, Vertices vertices, Vector2 originPosition)
        //{
        //    var body = BodyFactory.CreateBreakableBody(_world, vertices, Const.Density);
        //    _addFarseerObject(new BreakableBodyUpdater(bodyControl, body, originPosition));
        //}

        public void AddBreakableBodyControl(IBodyControl bodyControl,  IEnumerable<IBodyControl> bodyPartControl, IEnumerable<Shape> shapes, Vector2 originPosition)
        {
            var body = BodyFactory.CreateBreakableBody(_world, shapes);
            var bbu = new BreakableBodyUpdater(bodyControl, body, originPosition);
            bbu.OnBroke += () =>
            {
                foreach (var a in bodyPartControl.Zip(body.Parts, (x, y) => new { BodyPartControl = x, Part = y }))
                {
                    _addFarseerObject(new BodyUpdater(a.BodyPartControl, a.Part.Body, body.MainBody.Position));
                }
            };
            _addFarseerObject(bbu);
        }

        public void Update()
        {
            //foreach (var x in from x in _map.Values select x as BreakableBodyUpdater)
            //{
            //    var y = x.Object as BreakableBody;
            //    if (y != null && y.Broken)
            //    {
            //        y.Parts
            //   //   var  _bodyControlParts = (from x in y.Parts select _bodyControl.Get(BodyFactory.CreateBody(null, _originalPosition), _originalPosition)).ToArray();
            //    }
            //}
            int n = _map.Count;
            for (int i = 0; i < n; i++ )
            {
                var x = _map.Values.ElementAt(i) as IUpdatable;
                if (x != null)
                {
                    x.Update();
                }
            }

                //foreach (var x in from x in _map.Values select x as IUpdatable)
                //{
                //    if (x != null)
                //    {
                //        x.Update();
                //    }
                //}
        }

        public void Play()
        {
            _worldWatch.Play();
        }
        public void Pause()
        {
            _worldWatch.Pause();
        }
        public void Back()
        {
        }

        #region unfinished

        public bool Savable
        {
            get
            {
                return true;

                if (_world == null) return false;
                if (_world.ContactList.Count == 0) return true;
                //  _world.ContactList[0].

                return (from x in _world.ContactList where !x.IsTouching select x).Count() == 0;
            }
        }
        public void Save()
        {
            //var settings = new JsonSerializerSettings()
            //{
            //    MaxDepth = 1
            //};

            //File.WriteAllText(@"s:\aaa.json", JsonConvert.SerializeObject(from x in _world.ContactList select x.Manifold, settings));
            //WorldSerializer.Serialize(_world, @"s:\aaa.xml");
        }
        Dictionary<string, Stream> _savedStatesMap = new Dictionary<string, Stream>();
        public void Save(string name)
        {
            var ms = new MemoryStream();
            WorldXmlSerializer.Serialize(_world, ms);
            _savedStatesMap.Add(name, ms);
        }
        public void Load(string name)
        {
            /*var ms = _savedStatesMap[name];
           _world = WorldXmlSerializer.DeSerialize(ms);

            _savedStatesMap.Add(name, ms);


            foreach (var body in _world.BodyList)
            {
                var bodycontrol = Find((string)body.UserData);
                if (bodycontrol != null)
                {
                    bodycontrol.SetBody(body);
                }
            }*/
        }
        public void Load()
        {
            //var contact = JsonConvert.DeserializeObject(File.ReadAllText(@"s:\aaa.json"));

            //_world = WorldSerializer.Deserialize(@"s:\aaa.xml");
            //_world.Step(0.000001f);
        }
        #endregion
     
        private void step(float dt)
        {
            _world.Step(dt);

            foreach (var c in _loopCoroutine)
            {
                c.Do(dt);
            }
        }

        #region Joint

        private void addJoint(Joint j, IJointControl jointControl)
        {
            j.UserData = jointControl.Id;
            j.CollideConnected = jointControl.CollideConnected;
            CodeGenerator.AddCode(String.Format("{0}.CollideConnected = {1};", j.g(),  jointControl.CollideConnected.g()));
            _addFarseerObject(new RopeJointObject(jointControl, ( RopeJoint)j));
        }
        private Joint addJoint(TwoPointJointInfo jointInfo, IJointControl jointControl, Func<World, Body, Body, Vector2, Vector2, Joint> func, string name)
        {
            var bA = jointInfo.BodyControlA;
            var bB = jointInfo.BodyControlB;

            var pA = jointInfo.AnchorA;
            var pB = jointInfo.AnchorB;

            var j = JointFactory.CreateRopeJoint(_world, bA, bB, pA, pB);
            addJoint(j, jointControl);
            CodeGenerator.AddCode(String.Format("var {4} = JointFactory.{5}(_world, {0}, {1}, {2}, {3});", bA.g(), bB.g(), pA.g(), pB.g(), j.g(), name));
            return j;
        }
        public void AddRopeJoint(IRopeJointControl jointControl)
        {

            var xA = (IPointControl)Find(jointControl.TargetNameA);
            var xB = (IPointControl)Find(jointControl.TargetNameB);
            var _bA = (Body)((BodyUpdater)Find(xA.ParentId)).Object;
            var _bB = (Body)((BodyUpdater)Find(xB.ParentId)).Object;
            var _aA = new Vector2(xA.X, xA.Y);
            var _aB = new Vector2(xB.X, xB.Y);
            TwoPointJointInfo jointInfo = new TwoPointJointInfo(_bA, _bB, _aA, _aB);
            var j = (RopeJoint)addJoint(jointInfo, jointControl, (w, bA, bB, pA, pB) => JointFactory.CreateRopeJoint(w, bA, bB, pA, pB), "CreateRopeJoint");

            if (jointControl.MaxLength != -1)
            {
                j.MaxLength = jointControl.MaxLength;
            }
            else if (jointControl.MaxLengthFactor != -1)
            {
                j.MaxLength *= jointControl.MaxLengthFactor;
            }
            CodeGenerator.AddCode(String.Format("{1}.MaxLength = {0};", j.MaxLength, j.g()));
        }
        /*public void AddWeldJoint(TwoPointJointInfo jointInfo, IWeldJointControl jointControl)
        {
            var j = JointFactory.CreateWeldJoint(_world, jointInfo.BodyControlA, jointInfo.BodyControlB, jointInfo.AnchorA, jointInfo.AnchorB);

            addJoint(j, jointControl);
            //CodeGenerator.AddCode(String.Format("var {4} = JointFactory.CreateRopeJoint(_world, {0}, {1}, {2}, {3});", bA.g(), bB.g(), pA.g(), pB.g(), j.g()));

            if (jointControl.ReferenceAngle != -1)
            {
                j.ReferenceAngle = jointControl.ReferenceAngle;
            }
            if (jointControl.FrequencyHz != -1)
            {
                j.FrequencyHz = jointControl.FrequencyHz;
            }
            if (jointControl.DampingRatio != -1)
            {
                j.DampingRatio = jointControl.DampingRatio;
            }
        }
        public void AddRevoluteJoint(TwoPointJointInfo jointInfo, IRevoluteJointControl jointControl)
        {
            var j = JointFactory.CreateWeldJoint(_world, jointInfo.BodyControlA, jointInfo.BodyControlB, jointInfo.AnchorA, jointInfo.AnchorB);

            addJoint(j, jointControl);
        }
        public void AddDistanceJoint(TwoPointJointInfo jointInfo, IDistanceJointControl jointControl)
        {
            var j = JointFactory.CreateDistanceJoint(_world, jointInfo.BodyControlA, jointInfo.BodyControlB, jointInfo.AnchorA, jointInfo.AnchorB);

            addJoint(j, jointControl);
        }*/
        #endregion



    }
}
