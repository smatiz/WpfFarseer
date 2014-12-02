using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMS;

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
}
