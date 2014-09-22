﻿using FarseerPhysics.Common;
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
            foreach(var bm in _bodyManagers)
            {
                bm.Update();
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



       /* public JointManager CreateAngleJoint(BodyManager b1, BodyManager b2)
        {
            return new JointManager(JointFactory.CreateAngleJoint(_world, b1._body, b2._body));
        }


        public JointManager CreateRopeJoint( BodyManager b1, BodyManager b2, Vector2 v1, Vector2 v2)
        {
            return new JointManager(JointFactory.CreateRopeJoint(_world, b1._body, b2._body, v1, v2, true));
        }*/


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
            //_world.ContactList[0].

            //_world.ClearForces();

            /*foreach (var body in _world.BodyList)
            {
                var bodycontrol = Find((string)body.UserData);
                if (bodycontrol != null)
                {
                    bodycontrol.SetBody(body);
                }
            }*/
        }

    }
}