using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MetaType.Tests.Models;
using NUnit.Framework;

namespace MetaType.Tests.ExtendedTypes
{
    [TestFixture]
    public class TypeMethodInvokeTests : EngineTestsBase
    {
        [Test]
        public void VirtualOverrideOnType()
        {
            Engine.GetType<Delta>().SetMethod("Hello", () => "asdf");
            
            var delta = Engine.New<Delta>();
            Assert.That(delta.Hello(), Is.EqualTo("asdf"));
        }

        [Test]
        public void VirtualOverrideOnBaseType()
        {
            Engine.GetType<Delta>().SetMethod("Hello", () => "asdf");
            
            var epsilon = Engine.New<Epsilon>();
            Assert.That(epsilon.Hello(), Is.EqualTo("asdf"));
        }

        [Test]
        public void VirtualOverrideOnDerivedType()
        {
            Engine.GetType<Epsilon>().SetMethod("Hello", () => "asdf");
            
            var epsilon = Engine.New<Delta>();
            Assert.That(epsilon.Hello(), Is.EqualTo("World"));
        }
    }
}
