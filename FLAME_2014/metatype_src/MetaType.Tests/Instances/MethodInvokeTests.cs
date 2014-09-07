using System;
using MetaType.Tests.Models;
using NUnit.Framework;

namespace MetaType.Tests.Instances
{
    [TestFixture]
    public class MethodInvokeTests : EngineTestsBase
    {
        [Test]
        public void CallingMethodsWorksThroughClr()
        {
            var delta = Engine.New<Delta>();
            Assert.That(delta.Hello(), Is.EqualTo("World"));
        }

        [Test]
        public void CallingMethodsWorksThroughDlr()
        {
            dynamic delta = Engine.New<Delta>();
            Assert.That(delta.Hello(), Is.EqualTo("World"));
        }

        [Test]
        public void AssigningExistingMethodThroughDlrAsProperty()
        {
            dynamic delta = Engine.New<Delta>();
            delta.Hello = new Func<Delta, string>(self => "replacement");
            ExtendedObject instance = delta.GetExtendedObject();
            var temp = instance.GetExtendedType().FindMethod("Hello");
            Assert.That(temp, Is.InstanceOf<Delegate>());       
        }


        [Test]
        public void CallingThroughDlrToExistingMethod()
        {
            dynamic delta = Engine.New<Delta>();
            delta.Hello = new Func<Delta, string>(self => "replacement");

            Assert.That(delta.Hello(), Is.EqualTo("replacement"));
        }

        [Test]
        public void CallingThroughClrToExistingMethod()
        {
            dynamic delta = Engine.New<Delta>();
            delta.Hello = new Func<Delta, string>(self => "replacement");

            Delta clrDelta = delta;
            Assert.That(clrDelta.Hello(), Is.EqualTo("replacement"));
        }

        [Test]
        public void CallingExistingMethodNoSelfZeroArgs()
        {
            dynamic delta = Engine.New<Delta>();
            Delta clrDelta = delta;

            delta.Hello = new Func<string>(() => "replacement");

            Assert.That(delta.Hello(), Is.EqualTo("replacement"));
            Assert.That(clrDelta.Hello(), Is.EqualTo("replacement"));
        }

        [Test]
        public void CallingExistingMethodNoSelfHasArgs()
        {
            dynamic delta = Engine.New<Delta>();
            Delta clrDelta = delta;

            delta.HelloPlus = new Func<int, string, string>((x, y) => x + "/" + y);

            Assert.That(delta.HelloPlus(5, "foo"), Is.EqualTo("5/foo"));
            Assert.That(clrDelta.HelloPlus(5, "foo"), Is.EqualTo("5/foo"));
        }

        [Test]
        public void CallingExistingMethodHasSelfZeroArgs()
        {
            dynamic delta = Engine.New<Delta>();
            Delta clrDelta = delta;

            delta.Hello = new Func<Delta, string>(self => self.Extra +"replacement");

            delta.Extra = "-";
            Assert.That(delta.Hello(), Is.EqualTo("-replacement"));
            Assert.That(clrDelta.Hello(), Is.EqualTo("-replacement"));
        }

        [Test]
        public void CallingExistingMethodHasSelfHasArgs()
        {
            dynamic delta = Engine.New<Delta>();
            Delta clrDelta = delta;

            delta.HelloPlus = new Func<Delta, int, string, string>((self, x, y) => self.Extra + ":" + x + "/" + y);

            delta.Extra = "-";
            Assert.That(delta.HelloPlus(5, "foo"), Is.EqualTo("-:5/foo"));
            Assert.That(clrDelta.HelloPlus(5, "foo"), Is.EqualTo("-:5/foo"));
        }

        
        [Test]
        public void CallingDynamicMethodNoSelfZeroArgs()
        {
            dynamic delta = Engine.New<Delta>();

            delta.FakeMethod = new Func<string>(() => "replacement");

            Assert.That(delta.FakeMethod(), Is.EqualTo("replacement"));
        }

        [Test]
        public void CallingDynamicMethodNoSelfHasArgs()
        {
            dynamic delta = Engine.New<Delta>();

            delta.FakeMethod = new Func<int, string, string>((x, y) => x + "/" + y);

            Assert.That(delta.FakeMethod(5, "foo"), Is.EqualTo("5/foo"));
        }

        [Test]
        public void CallingDynamicMethodHasSelfZeroArgs()
        {
            dynamic delta = Engine.New<Delta>();

            delta.FakeMethod = new Func<Delta, string>(self => self.Extra +"replacement");

            delta.Extra = "-";
            Assert.That(delta.FakeMethod(), Is.EqualTo("-replacement"));
        }

        [Test]
        public void CallingDynamicMethodHasSelfHasArgs()
        {
            dynamic delta = Engine.New<Delta>();

            delta.FakeMethod = new Func<Delta, int, string, string>((self, x, y) => self.Extra + ":" + x + "/" + y);

            delta.Extra = "-";
            Assert.That(delta.FakeMethod(5, "foo"), Is.EqualTo("-:5/foo"));
        }
    }
}


