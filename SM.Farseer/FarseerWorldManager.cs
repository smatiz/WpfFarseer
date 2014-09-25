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

namespace WpfFarseer
{

  interface  IJointObject
  {
      bool CollideConnected {
  }

    

    public class LoopCoroutine : BasicCoroutine
    {
        private Func<float, IEnumerator<BasicCoroutine>> _func;
        float _dt;
        public LoopCoroutine(Func<float, IEnumerator<BasicCoroutine>> func)
        {
            _func = func;
        }

        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            return _func(_dt);
        }

        public void Do(float dt)
        {
            _dt = dt;
            Do();
        }
    }


    // Unico detentore del oggetto World
    // quindi l'unico in grado di creare oggetti Farseer
    public class FarseerWorldManager
    {
        private WorldWatch _worldWatch;
        private World _world;
        private List<BodyManager> _bodyManagers = new List<BodyManager>();
        private List<Joint> _joints = new List<Joint>();
        private List<LoopCoroutine> _loopCoroutine = new List<LoopCoroutine>();

        public FarseerWorldManager()
        {
            _world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            _worldWatch = new WorldWatch(dt => step(dt));
        }

        public object Find(string name)
        { 
            foreach(var x in _bodyManagers)
            {
                if(((string)x.Body.UserData) == name)
                {
                    return x.Body;
                }
            }

            foreach (var x in _joints)
            {
                if (((string)x.UserData) == name)
                {
                    return x;
                }
            }

            return null;
        }
        public void AddFarseerBehaviour(IFarseerBehaviour x)
        {
            _loopCoroutine.Add(new LoopCoroutine(x.Loop));
        }
        public void AddBodyControl(BodyControl bodyControl)
        {
            var originPosition = WpfFarseerHelper.ToFarseer(bodyControl.TranslatePoint(new System.Windows.Point(0, 0), (System.Windows.UIElement)bodyControl.Parent));


            var body = BodyFactory.CreateBody(_world, Vector2.Zero);
            body.UserData = bodyControl.Id;
            CodeGenerator.AddCode(String.Format("var {0} = BodyFactory.CreateBody(World, Vector2.Zero);", body.g()));
            //
            _bodyManagers.Add(new BodyManager(bodyControl, body, originPosition));
        }
        public void Update()
        {
            foreach (var x in _bodyManagers)
            {
                x.Update();
            }
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
        
        private Body _findBody(BodyControl bodyControl)
        {
            foreach (var x in _bodyManagers)
            {
                if (x.BodyControl == bodyControl)
                {
                    return x.Body;
                }
            }
            return null;
        }
        private void step(float dt)
        {
            _world.Step(dt);

            foreach (var c in _loopCoroutine)
            {
                c.Do(dt);
            }
            //foreach (var c in _farseerBehaviours)
            //{
            //    //if (!c.MoveNext())
            //    //{
            //    //    c.Reset();
            //    //    c.MoveNext();
            //    //}
            //    c.Loop(dt);
            //}
        }
        private void addJoint(Joint j, IJointObject jointControl)
        {
            j.UserData = jointControl.Id;
            j.CollideConnected = jointControl.CollideConnected;
            CodeGenerator.AddCode(String.Format("{1}.CollideConnected = {0};", j.g(),  jointControl.CollideConnected.g()));
            _joints.Add(j);
        }
        private Joint addJoint(TwoPointJointInfo jointInfo, BasicJointControl jointControl, Func<World, Body, Body, Vector2, Vector2, Joint> func, string name)
        {
            var bA = _findBody(jointInfo.BodyControlA);
            var bB = _findBody(jointInfo.BodyControlB);

            var pA = jointInfo.AnchorA.ToFarseer();
            var pB = jointInfo.AnchorB.ToFarseer();

            var j = JointFactory.CreateRopeJoint(_world, bA, bB, pA, pB);
            addJoint(j, jointControl);
            CodeGenerator.AddCode(String.Format("var {4} = JointFactory.{5}(_world, {0}, {1}, {2}, {3});", bA.g(), bB.g(), pA.g(), pB.g(), j.g(), name));
            return j;

        }

        public void AddRopeJoint(TwoPointJointInfo jointInfo, RopeJointControl jointControl)
        {

            var j = (RopeJoint)addJoint(jointInfo, jointControl, (w, bA, bB, pA, pB) => JointFactory.CreateRopeJoint(w, bA, bB, pA, pB), "CreateRopeJoint");

            /*var bA =  _findBody(jointInfo.BodyControlA);
            var bB = _findBody(jointInfo.BodyControlB);

            var pA = jointInfo.AnchorA.ToFarseer();
            var pB = jointInfo.AnchorB.ToFarseer();

            var j = JointFactory.CreateRopeJoint(_world, bA, bB, pA, pB);
            addJoint(j, jointControl);
            CodeGenerator.AddCode(String.Format("var {4} = JointFactory.CreateRopeJoint(_world, {0}, {1}, {2}, {3});", bA.g(), bB.g(), pA.g(), pB.g(), j.g()));
           */
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
        public void AddWeldJoint(TwoPointJointInfo jointInfo, WeldJointControl jointControl)
        {
            var j = JointFactory.CreateWeldJoint(_world, _findBody(jointInfo.BodyControlA), _findBody(jointInfo.BodyControlB), jointInfo.AnchorA.ToFarseer(), jointInfo.AnchorB.ToFarseer());

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
        public void AddRevoluteJoint(TwoPointJointInfo jointInfo, RevoluteJointControl jointControl)
        {
            var j = JointFactory.CreateWeldJoint(_world, _findBody(jointInfo.BodyControlA), _findBody(jointInfo.BodyControlB), jointInfo.AnchorA.ToFarseer(), jointInfo.AnchorB.ToFarseer());

            addJoint(j, jointControl);
        }
        public void AddDistanceJoint(TwoPointJointInfo jointInfo, DistanceJointControl jointControl)
        {
            var j = JointFactory.CreateDistanceJoint(_world, _findBody(jointInfo.BodyControlA), _findBody(jointInfo.BodyControlB), jointInfo.AnchorA.ToFarseer(), jointInfo.AnchorB.ToFarseer());

            addJoint(j, jointControl);
        }
    }
}
