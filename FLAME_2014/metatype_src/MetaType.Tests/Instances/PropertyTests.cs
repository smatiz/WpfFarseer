using System;
using MetaType.Tests.Models;
using NUnit.Framework;

namespace MetaType.Tests.Instances
{
    [TestFixture]
    public class PropertyTests : EngineTestsBase
    {
        [Test]
        public void PropertyAssignmentShouldWorkNormally()
        {
            var beta = Engine.New<Beta>(42);
            Assert.That(beta.Foo, Is.EqualTo(42));
            Assert.That(beta._foo, Is.EqualTo(42));
            beta.Foo = 7;
            Assert.That(beta.Foo, Is.EqualTo(7));
            Assert.That(beta._foo, Is.EqualTo(7));
        }

        [Test]
        public void DynamicAssignmentShouldPassThroughToNormalProperties()
        {
            dynamic beta = Engine.New<Beta>(42);
            Assert.That(beta.Foo, Is.EqualTo(42));
            Assert.That(beta._foo, Is.EqualTo(42));
            beta.Foo = 7;
            Assert.That(beta.Foo, Is.EqualTo(7));
            Assert.That(beta._foo, Is.EqualTo(7));

            Beta clrBeta = beta;
            Assert.That(clrBeta.Foo, Is.EqualTo(7));
            Assert.That(clrBeta._foo, Is.EqualTo(7));
        }

        [Test]
        public void AssigningNewPropertyWillSetInstanceValue()
        {
            dynamic alpha = Engine.New<Alpha>();
            alpha.Bar = 7;
            ExtendedObject instance = alpha.GetExtendedObject();
            object temp;
            Assert.That(instance.TryGet(alpha, "Bar", out temp));
            Assert.That(temp, Is.EqualTo(7));
        }

        [Test]
        public void GettingPropertyWillUsePreviouslySetValues()
        {
            dynamic alpha = Engine.New<Alpha>();
            alpha.Bar = 7;
            Assert.That(alpha.Bar, Is.EqualTo(7));
        }

        [Test]
        public void GettingNonExistantPropertyWorksLikeNonProxyDynamic()
        {
            dynamic alpha1 = Engine.New<Alpha>();
            Exception ex1 = null;
            try { var x = alpha1.Bar; }
            catch (Exception ex) { ex1 = ex; }

            dynamic alpha2 = new Alpha();
            Exception ex2 = null;
            try { var x = alpha2.Bar; }
            catch (Exception ex) { ex2 = ex; }

            Assert.That(ex1.GetType(), Is.SameAs(ex2.GetType()));

            // need to forgive the different class name...
            var ex2Message = ex2.Message.Replace(alpha2.GetType().FullName, alpha1.GetType().FullName);
            Assert.That(ex1.Message, Is.EqualTo(ex2Message));
        }

        [Test, ExpectedException]
        public void DynamicAssignmentOfExistingPropertyIsTypeSensitive()
        {
            dynamic beta = Engine.New<Beta>(5);
            beta.Foo = "Should throw an error";
        }

        [Test]
        public void PropertyGetMayBeReplaced()
        {
            dynamic beta = Engine.New<Beta>(5);
            Beta clrBeta = beta;

            Assert.That(beta.Foo, Is.EqualTo(5));
            Assert.That(clrBeta.Foo, Is.EqualTo(5));

            beta.Foo = 6;
            Assert.That(beta.Foo, Is.EqualTo(6));
            Assert.That(clrBeta.Foo, Is.EqualTo(6));

            beta.get_Foo = new Func<int>(() => 7);
            Assert.That(beta.Foo, Is.EqualTo(7));
            Assert.That(clrBeta.Foo, Is.EqualTo(7));

            beta.Foo = 8;
            Assert.That(beta.Foo, Is.EqualTo(7));
            Assert.That(clrBeta.Foo, Is.EqualTo(7));
        }

        [Test]
        public void BackingPropertyCanBeEmulated()
        {
            dynamic beta = Engine.New<Beta>(5);
            Beta clrBeta = beta;

            beta.get_Foo = new Func<dynamic, int>(self => self._foo);
            beta.set_Foo = new Action<dynamic, int>((self, value) => { self._foo = value; });

            beta.Foo = 6;
            Assert.That(beta.Foo, Is.EqualTo(6));
            Assert.That(clrBeta.Foo, Is.EqualTo(6));
            Assert.That(beta._foo, Is.EqualTo(6));
        }

        [Test]
        public void DynamicAccessorMayBeUsedAsProperty()
        {
            dynamic alpha = Engine.New<Alpha>();

            int count = 0;
            alpha.get_Foo = new Func<int>(() => ++count);

            Assert.That(alpha.Foo, Is.EqualTo(1));
            Assert.That(alpha.Foo, Is.EqualTo(2));
            Assert.That(alpha.Foo, Is.EqualTo(3));
            Assert.That(count, Is.EqualTo(3));
        }
    }
}


