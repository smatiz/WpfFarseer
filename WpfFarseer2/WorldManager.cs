using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
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
    public class WorldManager
    {

        enum Status { Stopped, Play, Pause }
        //const float Dt = 0.04f;
        const long Interval = 400;//(long)(Dt * 1000);
        private World _world;
        private Timer _timer;
        private Status _status = Status.Stopped;
        private Stopwatch _realWatch = new Stopwatch();
        private double _lastElapsedTotalSeconds = 0;


       
        double _elapsedStep = 0;
        public WorldManager()
        {
            _world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            _timer = new Timer(new TimerCallback(callback), null, Interval, Timeout.Infinite);
        }

        Stopwatch _watch = new Stopwatch();
        private void callback(Object state)
        {
            _watch.Start();
            var lastElapsedTotalSeconds = _realWatch.Elapsed.TotalSeconds;
            step((float)(lastElapsedTotalSeconds - _lastElapsedTotalSeconds));
            _timer.Change(Math.Max(0, Interval - _watch.ElapsedMilliseconds), Timeout.Infinite);
            _lastElapsedTotalSeconds = lastElapsedTotalSeconds;
        }

       
        public void Clear()
        {
            _world = new World(new Microsoft.Xna.Framework.Vector2(0, 10));
            _elapsedStep = 0;
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
            var settings = new  JsonSerializerSettings()
            {
                 MaxDepth = 1
            };

            File.WriteAllText(@"s:\aaa.json", JsonConvert.SerializeObject(from x in _world.ContactList select x.Manifold, settings));
            WorldSerializer.Serialize(_world, @"s:\aaa.xml");
        }



        public void Load()
        {

            var contact= JsonConvert.DeserializeObject(File.ReadAllText(@"s:\aaa.json"));


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
 

        private void step(float dt)
        {
            if (_status != Status.Play) return;
            if (dt >= 0)
            {
                _elapsedStep += dt;
                _world.Step(dt);
            }
            else
            {
                /*
                var step = (float)(_elapsedStep + dt);
                if (step >= 0)
                {
                    clear();
                    while (step > 100)
                    {
                        Step(100);
                        step -= 100;
                    }
                    Step(step);
                }*/
            }
        }

       public void Play()
        {
            _status = Status.Play;
            _realWatch.Start();
        }

       public void Pause()
       {
           _status = Status.Pause;
           _realWatch.Stop();
        }

       public void Back()
        {
        }

        



        public Body CreateBody(Microsoft.Xna.Framework.Vector2 vector2)
        {
            return BodyFactory.CreateBody(_world, Vector2.Zero);
        }
    }
}
