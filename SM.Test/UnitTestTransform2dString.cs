using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SM.Test
{
    [TestClass]
    public class UnitTestTransform2dString
    {
        private void testEquality(string t, float x, float y, float a, float s)
        {
            var r = Transform2dString.GetTransform2d(t);
            Assert.AreEqual(r.RotoTranslation.Translation.X, x);
            Assert.AreEqual(r.RotoTranslation.Translation.Y, y);
            Assert.AreEqual(r.RotoTranslation.DegreeAngle, a);
            Assert.AreEqual(r.Scale, s);
        }
        private void testEqualityRadiant(string t, float x, float y, float a, float s)
        {
            var r = Transform2dString.GetTransform2d(t);
            Assert.AreEqual(r.RotoTranslation.Translation.X, x);
            Assert.AreEqual(r.RotoTranslation.Translation.Y, y);
            Assert.AreEqual(r.RotoTranslation.Angle, a);
            Assert.AreEqual(r.Scale, s);
        }

        [TestMethod]
        public void TestParse()
        {
            testEquality("1,2,3,4", 1, 2, 3, 4);
            testEquality("1,2,3", 1, 2, 3, 1);
            testEquality("1,2", 1, 2, 0, 1);
            testEquality("5", 0, 0, 5, 1);
            testEquality("s5", 0, 0, 0, 5);
            testEquality("x5.654", 5.654f, 0, 0, 1);
            testEquality("y5.6544", 0, 5.6544f, 0, 1);
            testEquality("R5", 0, 0, 5, 1);
            testEqualityRadiant("r5", 0, 0, 5, 1);
            testEquality("R1,s5,y2,x3", 3, 2, 1, 5);
        }
    }

    [TestClass]
    public class UnitTestTransform2d
    {
        [TestMethod]
        public void TestEquality()
        {
            var r = new Random(DateTime.Now.TimeOfDay.Milliseconds);
            var t1 = new transform2d((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());
            var t2 = t1;

            Assert.IsTrue(t1 == t2);

        }

        [TestMethod]
        public void TestAngle()
        {
            var r = new Random(DateTime.Now.TimeOfDay.Milliseconds);
            var p1 = (float)r.NextDouble() * r.Next(10 ^ 5);
            var p2 = p1 + r.Next(10 ^ 5) * 2f * (float)Math.PI;
            var t1 = new transform2d(0, 12, p1, 345);
            var t2 = new transform2d(0, 12, p2, 345);
            Assert.IsTrue(t1 == t2);


        }


        private void testTransform2d(float x, float y, float r, float s)
        {
            var t = new transform2d(x, y, r, s);
            var m = t.ToMatrix();
            var t2 = m.ToTransform2d();
            var m2 = t2.ToMatrix();
            Assert.IsTrue(t == t2);
            Assert.IsTrue((m - m2).MaxAbs() < Consts.Epsilon);
        }

        [TestMethod]
        public void TestMatrix()
        {
            var anglesFullTurnPercentage = Enumerable.Range(-200, 300).Select(i => i * 0.01f);
            var anglesRadiant = anglesFullTurnPercentage.Select(f => f * (float)Math.PI * 2f);

            foreach (var a in anglesRadiant)
                testTransform2d(0, 0, a, 134);


            var r = new Random(DateTime.Now.TimeOfDay.Milliseconds);
            testTransform2d((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble());


        }
    }
}
