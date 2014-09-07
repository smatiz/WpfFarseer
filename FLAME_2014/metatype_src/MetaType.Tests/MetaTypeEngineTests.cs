using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MetaType.Tests.Models;
using NUnit.Framework;

namespace MetaType.Tests
{
    [TestFixture]
    public class MetaTypeEngineTests : EngineTestsBase
    {
        [Test]
        public void CreatingNewObjectShouldBeSubclass()
        {
            var alpha = Engine.New<Alpha>();
            Assert.That(alpha, Is.Not.Null);
            Assert.That(alpha, Is.InstanceOf<Alpha>());
            Assert.That(alpha, Is.Not.TypeOf<Alpha>());
        }

        [Test]
        public void CreatingNewObjectWithParameters()
        {
            var beta = Engine.New<Beta>(42);
            Assert.That(beta, Is.Not.Null);
            Assert.That(beta.Foo, Is.EqualTo(42));
        }

        [Test]
        public void ProxyObjectShouldHaveDynamicInterface()
        {
            var alpha = Engine.New<Alpha>();
            Assert.That(alpha, Is.InstanceOf<IDynamicMetaObjectProvider>());
        }

        [Test]
        public void DynamicMetaObjectShouldBeFromMetaTypeAssembly()
        {
            var alpha = Engine.New<Alpha>();
            var provider = (IDynamicMetaObjectProvider) alpha;
            var metaObject = provider.GetMetaObject(Expression.Constant(alpha));
            Assert.That(metaObject, Is.InstanceOf<InstanceMetaObject>());
        }

        [Test]
        public void ProxyObjectShouldHaveInstanceProvider()
        {
            var alpha = Engine.New<Alpha>();
            Assert.That(alpha, Is.InstanceOf<IExtendedObjectProvider>());
        }

        [Test]
        public void InstanceShouldBeFromMetaTypeAssembly()
        {
            var alpha = Engine.New<Alpha>();
            var provider = (IExtendedObjectProvider)alpha;
            var instance = provider.GetExtendedObject();
            Assert.That(instance, Is.InstanceOf<ExtendedObject>());
        }
    }
}
