using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MetaType.Tests.Models;
using NUnit.Framework;

namespace MetaType.Tests.Instances
{

    [TestFixture]
    public class InstanceMissingMethodTests : EngineTestsBase
    {
        [Test]
        public void MissingMethodOnTypeCalledForInvokeViaDlr()
        {
            dynamic alpha = Engine.New<Alpha>();

            alpha.MissingMethod = new Func<Alpha, MissingMethodCall, object>(
                (self, call) => "You called " + call.Name + " with " + string.Concat(call.Arguments));

            Assert.That(alpha.Foo(), Is.EqualTo("You called Foo with "));
            Assert.That(alpha.Bar(1, 2, 3), Is.EqualTo("You called Bar with 123"));
        }

        [Test]
        public void MissingMethodCanActAsPropertyGet()
        {
            dynamic alpha = Engine.New<Alpha>();

            alpha.MissingMethod = new Func<Alpha, MissingMethodCall, object>(
                (self, call) =>
                {
                    if (call.Name == "get_Hello")
                        return "World";

                    return call.Default();
                });

            Assert.That(alpha.Hello, Is.EqualTo("World"));
        }

        [Test]
        public void InstanceAndTypeMethodsCascade()
        {
            Engine.SetMissingMethod<Alpha>(
                (self, call) =>
                {
                    if (call.Name == "get_Another")
                        return "BaseThing";
                    return call.Default();
                });

            dynamic alpha = Engine.New<Alpha>();

            alpha.MissingMethod = new Func<Alpha, MissingMethodCall, object>(
                (self, call) =>
                {
                    if (call.Name == "get_Hello")
                        return "World";
                    return call.Default();
                });

            Assert.That(alpha.Hello, Is.EqualTo("World"));
            Assert.That(alpha.Another, Is.EqualTo("BaseThing"));
        }
    }

}
