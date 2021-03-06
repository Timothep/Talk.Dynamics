﻿namespace Dynamics.Demos
{
    using System;
    using System.Reflection;
    using NUnit.Framework;

    /// <summary>
    ///   Demo heavily inspired by Scott Hanselmann's Blog post
    ///   http://www.hanselman.com/blog/C4AndTheDynamicKeywordWhirlwindTourAroundNET4AndVisualStudio2010Beta1.aspx
    /// </summary>
    internal class Calculator : ICalc
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
    internal interface ICalc
    {
        int Add(int x, int y);
    }

    [TestFixture]
    public class TestDemo2
    {
        [Test]
        public void TestDemo2Static()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(30, calc.Add(10, 20));
        }


        [Test]
        public void TestDemo2Object()
        {
            object calc = new Calculator();
            //Assert.AreEqual(30, calc.Add(10, 20));
        }


        [Test]
        public void TestDemo2Reflection()
        {
            object reflectionCalc = new Calculator();
            var calcType = reflectionCalc.GetType();
            object res = calcType.InvokeMember(
                "Add",
                BindingFlags.InvokeMethod,
                null,
                Activator.CreateInstance(calcType),
                new object[] {10, 20});
            int sum = Convert.ToInt32(res);
            Assert.AreEqual(30, sum);
        }


        [Test]
        public void TestDemo2Itf()
        {
            ICalc calc = new Calculator();
            Assert.AreEqual(30, calc.Add(10, 20));
        }


        [Test]
        public void TestDemo2Dynamic()
        {
            dynamic calc = new Calculator();
            Assert.AreEqual(30, calc.Add(10, 20));
        }
    }
}