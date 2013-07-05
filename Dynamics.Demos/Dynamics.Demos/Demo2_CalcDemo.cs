using System;
using System.Reflection;
using NUnit.Framework;

namespace Dynamics.Demos
{
    /// <summary>
    ///   Demo heavily inspired by Scott Hanselmann's Blog post
    ///   http://www.hanselman.com/blog/C4AndTheDynamicKeywordWhirlwindTourAroundNET4AndVisualStudio2010Beta1.aspx
    /// </summary>
    internal class Calculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }

    [TestFixture]
    public class TestDemo2
    {
        [Test]
        public void TestDemo2Dynamic()
        {
            dynamic calc = new Calculator();
            Assert.AreEqual(30, calc.Add(10, 20));
        }

        [Test]
        [Ignore]
        public void TestDemo2Object()
        {
            object calc = new Calculator();
            //Assert.AreEqual(30, calc.Add(10, 20));
            Assert.Fail();
        }

        [Test]
        public void TestDemo2Reflection()
        {
            object reflectionCalc = new Calculator();
            var calcType = reflectionCalc.GetType();
            var res = calcType.InvokeMember(
                "Add",
                BindingFlags.InvokeMethod,
                null,
                Activator.CreateInstance(calcType),
                new object[] {10, 20});
            var sum2 = Convert.ToInt32(res);
            Assert.AreEqual(30, sum2);
        }

        [Test]
        public void TestDemo2Static()
        {
            var calc = new Calculator();
            Assert.AreEqual(30, calc.Add(10, 20));
        }
    }
}