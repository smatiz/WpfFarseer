using System;
using System.Reflection;
using MetaType.Tests.Models;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;

namespace MetaType.Tests.ExtendedTypes
{
    [TestFixture]
    public class TypeMissingMethodTests : EngineTestsBase
    {
        [Test]
        public void MissingMethodOnTypeCalledForInvokeViaDlr()
        {
            Engine.SetMissingMethod<Alpha>(
                 call => "You called " + call.Name + " with " + string.Concat(call.Arguments));

            dynamic alpha = Engine.New<Alpha>();

            Assert.That(alpha.Foo(), Is.EqualTo("You called Foo with "));
            Assert.That(alpha.Bar(1, 2, 3), Is.EqualTo("You called Bar with 123"));
        }

        [Test, ExpectedException]
        public void NormalMissingMethodInvokeStillFails()
        {
            dynamic alpha = Engine.New<Alpha>();

            Assert.That(alpha.Foo(), Is.EqualTo("You called Foo with "));
        }

        [Test, ExpectedException(typeof(TargetInvocationException))]
        public void MissingMethodCanThrowException()
        {
            Engine.SetMissingMethod<Alpha>(
                call => { throw new MissingMethodException("Alpha", "name"); });

            dynamic alpha = Engine.New<Alpha>();

            Assert.That(alpha.Foo(), Is.EqualTo("You called Foo with "));
        }

        [Test, ExpectedException(typeof(RuntimeBinderException))]
        public void DefaultMessingMethodActionThrowsException()
        {
            Engine.SetMissingMethod<Alpha>((self, call) => call.Default());

            dynamic alpha = Engine.New<Alpha>();

            Assert.That(alpha.Foo(), Is.EqualTo("You called Foo with "));
        }

    }
}


