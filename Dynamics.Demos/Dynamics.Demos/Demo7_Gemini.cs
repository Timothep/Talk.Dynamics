using System.Collections.Generic;
using NUnit.Framework;
using Oak;

namespace Dynamics.Demos
{
    [TestFixture]
    internal class TestDemo7
    {
        [Test]
        public void TestDemo7MembersOnTheFly()
        {
            dynamic gemini = new Gemini(new { FirstName = "Jane", LastName = "Doe" });
            gemini.MiddleInitial = "J";

            Assert.AreEqual("Jane", gemini.FirstName);
            Assert.AreEqual("J", gemini.MiddleInitial);
        }

        [Test]
        public void TestDemo7MethodsOnTheFly()
        {
            dynamic gemini = new Gemini(new { FirstName = "Jane", LastName = "Doe" });
            gemini.SayHello = new DynamicFunction(() => "Hello");

            //calling method
            Assert.AreEqual("Hello", gemini.SayHello());
        }

        [Test]
        public void TestDemo7GeminiObjectGraph()
        {
            dynamic gemini = new Gemini(new {FirstName = "Jane", LastName = "Doe"});
            Assert.AreEqual("this (Gemini)\r\n  FirstName (String): Jane\r\n  LastName (String): Doe\r\n  SetMembers (DynamicFunctionWithParam)\r\n",
                            gemini.ToString());
        }

        [Test]
        public void TestDemo7GeminiRespondsTo()
        {
            dynamic gemini = new Gemini(new {FirstName = "Jane", LastName = "Doe"});

            Assert.IsTrue(gemini.RespondsTo("FirstName"));
            Assert.IsFalse(gemini.RespondsTo("IDontExist"));
        }

        [Test]
        public void TestDemo7Introspection()
        {
            dynamic gemini = new Gemini(new {FirstName = "Jane", LastName = "Doe"});
            IDictionary<string, object> members = gemini.HashExcludingDelegates();

            Assert.AreEqual("Doe", gemini.GetMember("LastName"));
            Assert.AreEqual(2, members.Count);
        }
    }
}