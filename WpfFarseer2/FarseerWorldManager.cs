using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
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
    // Unico detentore del oggetto World
    // quindi l'unico in grado di creare oggetti Farseer
    public class FarseerWorldManager
    {
        WorldWatch _worldWatch;
        private World _world;
        List<BodyManager> _bodyManagers = new List<BodyManager>();

        public FarseerWorldManager()
        {
            _world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            _worldWatch = new WorldWatch(dt => step(dt));
        }

        public void Update()
        {
            foreach (var x in _bodyManagers)
            {
                x.Update();
            }
        }

        public void AddBodyControl(BodyControl bodyControl)
        {
            var originPosition = WpfFarseerHelper.ToFarseer(bodyControl.TranslatePoint(new System.Windows.Point(0, 0), (System.Windows.UIElement)bodyControl.Parent));
            var body = BodyFactory.CreateBody(_world, Vector2.Zero);
            _bodyManagers.Add(new BodyManager(bodyControl, body, originPosition));
        }

        private void step(float dt)
        {
            _world.Step(dt);
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
            var settings = new JsonSerializerSettings()
            {
                MaxDepth = 1
            };

            File.WriteAllText(@"s:\aaa.json", JsonConvert.SerializeObject(from x in _world.ContactList select x.Manifold, settings));
            WorldSerializer.Serialize(_world, @"s:\aaa.xml");
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
            var contact = JsonConvert.DeserializeObject(File.ReadAllText(@"s:\aaa.json"));

            _world = WorldSerializer.Deserialize(@"s:\aaa.xml");
            _world.Step(0.000001f);
        }

        Body _findBody(BodyControl bodyControl)
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

        public void AddRopeJoint(TwoPointJointInfo jointInfo, RopeJointControl jointControl)
        {
            var j = JointFactory.CreateRopeJoint(_world, _findBody(jointInfo.BodyControlA), _findBody(jointInfo.BodyControlB), jointInfo.AnchorA.ToFarseer(), jointInfo.AnchorB.ToFarseer());
            j.UserData = jointControl.Name;
            j.CollideConnected = jointControl.CollideConnected;
            if (jointControl.MaxLength != -1)
            {
                j.MaxLength = jointControl.MaxLength;
            }
            else if (jointControl.MaxLengthFactor != -1)
            {
                j.MaxLength *= jointControl.MaxLengthFactor;
            }
        }

        public void AddWeldJoint(TwoPointJointInfo jointInfo, WeldJointControl jointControl)
        {
            var j = JointFactory.CreateWeldJoint(_world, _findBody(jointInfo.BodyControlA), _findBody(jointInfo.BodyControlB), jointInfo.AnchorA.ToFarseer(), jointInfo.AnchorB.ToFarseer());
            j.UserData = jointControl.Name; 
            j.CollideConnected = jointControl.CollideConnected;
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
            j.UserData = jointControl.Name; 
            j.CollideConnected = jointControl.CollideConnected;
        }
        public void AddDistanceJoint(TwoPointJointInfo jointInfo, DistanceJointControl jointControl)
        {
            var j = JointFactory.CreateDistanceJoint(_world, _findBody(jointInfo.BodyControlA), _findBody(jointInfo.BodyControlB), jointInfo.AnchorA.ToFarseer(), jointInfo.AnchorB.ToFarseer());
            j.UserData = jointControl.Name; 
            j.CollideConnected = jointControl.CollideConnected;
        }
    }

}
