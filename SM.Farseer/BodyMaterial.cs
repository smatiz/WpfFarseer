﻿using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class BodyMaterial : IBodyMaterial
    {
        private Vector2 _originalPosition = Vector2.Zero;
        private Body _body;

        public virtual object Object { get { return _body; } }
        public string Id { get { return (string)_body.UserData; } }

        public BodyMaterial(World world)
        {
            _body = BodyFactory.CreateBody(world);
        }
        public BodyMaterial(Body body, string id)
        {
            _body = body;
            _body.UserData = id;
            _body.BodyType = FarseerPhysics.Dynamics.BodyType.Dynamic;
        }


        public void Build(string id, SM.BodyType bodyType)
        {
            _body.UserData = id;
            _body.BodyType = (FarseerPhysics.Dynamics.BodyType)bodyType;
            //_body.Position = originPosition;
            //_originalPosition = originPosition;
        }

        public rotoTranslation RotoTranslation
        {
            get
            {
                var q = _body.Position - _originalPosition; 
                return new rotoTranslation(new float2(q.X, q.Y), _body.Rotation);
            }
        }

        public void AddShape(IShapeView shape)
        {

            FarseerPhysics.Factories.FixtureFactory.AttachPolygon(shape.Points_X.ToFarseerVertices(), shape.Density_X, _body);
        }
    }
}
