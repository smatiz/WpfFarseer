using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaType.Tests.Models;
using NUnit.Framework;

namespace MetaType.Tests.MissingMethods
{
    [TestFixture]
    public class MissingMethodOnCompiledType : EngineTestsBase
    {
        [Test]
        public void CompiledHandlerCatchesMethodCall()
        {
            dynamic zeta = Engine.New<Zeta>();
            Assert.That(zeta.NoSuchMethod(), Is.EqualTo("Missing method provided"));
        }

        [Test]
        public void CompiledHandlerCatchesPropertyGet()
        {
            dynamic zeta = Engine.New<Zeta>();
            Assert.That(zeta.NoSuchProperty, Is.EqualTo("Missing property provided"));
        }

        public class First
        {
            public object MissingMethod(MissingMethodCall call)
            {
                return call.Default() + "F";
            }
        }
        public class Second : First
        {
            public new object MissingMethod(MissingMethodCall call)
            {
                return call.Default() + "S";
            }
        }

        [Test]
        public void MissingMethodCallsHappenFromOutermostOrder()
        {
            Engine.SetMissingMethod<object>(call => call.Name + ":o");
            Engine.SetMissingMethod<First>(call => call.Default() + "f");
            Engine.SetMissingMethod<Second>(call => call.Default() + "s");

            dynamic second = Engine.New<Second>();
            second.MissingMethod = new Func<MissingMethodCall, object>(call => call.Default() + "i");

            Assert.That(second.FakeProperty, Is.EqualTo("get_FakeProperty:oFfSsi"));
            Assert.That(second.FakeMethod(), Is.EqualTo("FakeMethod:oFfSsi"));
        }

        public class FirstV
        {
            public virtual object MissingMethod(MissingMethodCall call)
            {
                return call.Default() + "F";
            }
        }
        public class SecondV : FirstV
        {
            public override object MissingMethod(MissingMethodCall call)
            {
                return call.Default() + "S";
            }
        }

        [Test]
        public void OutermostOrderWorksWithVirtualAndOverrideAlso()
        {
            Engine.SetMissingMethod<object>(call => call.Name + ":o");
            Engine.SetMissingMethod<FirstV>(call => call.Default() + "f");
            Engine.SetMissingMethod<SecondV>(call => call.Default() + "s");

            dynamic second = Engine.New<SecondV>();
            second.MissingMethod = new Func<MissingMethodCall, object>(call => call.Default() + "i");

            Assert.That(second.FakeProperty, Is.EqualTo("get_FakeProperty:oFfSsi"));
            Assert.That(second.FakeMethod(), Is.EqualTo("FakeMethod:oFfSsi"));
        }
    }
}
