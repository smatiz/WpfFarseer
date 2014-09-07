using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaType.Tests.Models;
using NUnit.Framework;

namespace MetaType.Tests
{
    [TestFixture]
    public class ExtendedTypeTests : EngineTestsBase
    {
        [Test]
        public void SameTypeInstanceShouldBeReturnedPerType()
        {
            var alphaType1 = Engine.GetType<Alpha>();
            var betaType1 = Engine.GetType<Beta>();
            var alphaType2 = Engine.GetType<Alpha>();
            var betaType2 = Engine.GetType<Beta>();

            Assert.That(alphaType1, Is.SameAs(alphaType2));
            Assert.That(betaType1, Is.SameAs(betaType2));
            Assert.That(alphaType1, Is.Not.SameAs(betaType1));
        }

        [Test]
        public void DifferentEnginesReturnDifferentExtendedTypes()
        {
            var engine2 = new MetaTypeEngine();
            var alphaType1 = Engine.GetType<Alpha>();
            var alphaType2 = engine2.GetType<Alpha>();
            Assert.That(alphaType1, Is.Not.SameAs(alphaType2));
        }
        
        [Test]
        public void BaseTypeReturnsSameInstance()
        {
            var gammaType = Engine.GetType<Gamma>();
            var betaType = Engine.GetType<Beta>();
            var objectType = Engine.GetType<Object>();

            Assert.That(gammaType, Is.Not.SameAs(betaType));
            Assert.That(gammaType.BaseType, Is.SameAs(betaType));
            Assert.That(gammaType.BaseType.BaseType, Is.SameAs(objectType));
            Assert.That(gammaType.BaseType.BaseType.BaseType, Is.Null);
        }

    }
}
