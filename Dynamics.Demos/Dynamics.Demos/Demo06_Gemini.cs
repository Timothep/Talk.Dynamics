namespace Dynamics.Demos
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Oak;

    [TestFixture]
    internal class TestDemo6
    {
        [Test]
        public void TestDemo6MembersOnTheFly()
        {
            dynamic gemini = new Gemini(new { FirstName = "Jane", LastName = "Doe" });
            gemini.MiddleInitial = "L";
            gemini.SayHello = new Func<string>(() => "Hello");

            Assert.AreEqual("Jane", gemini.FirstName);
            Assert.AreEqual("L", gemini.MiddleInitial);
            Assert.AreEqual("Hello", gemini.SayHello());
        }

        [Test]
        public void TestDemo6GeminiRespondsTo()
        {
            dynamic gemini = new Gemini(new {FirstName = "Jane", LastName = "Doe"});

            Assert.IsTrue(gemini.RespondsTo("FirstName"));
            Assert.IsFalse(gemini.RespondsTo("IDontExist"));
        }

        [Test]
        public void TestDemo6Introspection()
        {
            dynamic gemini = new Gemini(new {FirstName = "Jane", LastName = "Doe"});
            gemini.SayHello = new Func<string>(() => "Hello");
            
            IDictionary<string, object> properties = gemini.HashOfProperties();
            Assert.AreEqual(2, properties.Count);
        }
    }
}