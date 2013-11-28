namespace Dynamics.Demos
{
    using System.Dynamic;
    using NUnit.Framework;

    internal class TimsDynamicObject : DynamicObject
    {
        public string SomeProperty { get; set; }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var propertyToSet = binder.Name;
            var valueToSet = value;

            SomeProperty = (string)value;

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var propertyOrMethodToGet = binder.Name;

            result = SomeProperty;
            return true;
        }
    }

    [TestFixture]
    internal class TestDemo4
    {
        [Test]
        public void TestDemo4DynamicvsDynamicObject()
        {
            //"dynamic" allows me to write anything after the "."
            dynamic notDynamicString = "";
            notDynamicString.SomeProperty = "42";
            notDynamicString.Title = "42";

            //DynamicObject can call "real properties" like a "normal object"
            TimsDynamicObject dynamicObject = new TimsDynamicObject();
            dynamicObject.SomeProperty = "42";
            //dynamicObject.Title = "42";

            //Combine the two to get both features
            dynamic fullDynamicObject = new TimsDynamicObject();
            fullDynamicObject.SomeProperty = "42";
            fullDynamicObject.Title = "42";
        }

        [Test]
        public void TestDemo4TrySetMember()
        {
            dynamic myDynamicObject = new TimsDynamicObject();
            myDynamicObject.Title = "Set DynamicObject Demo";
            Assert.AreEqual("Set DynamicObject Demo", myDynamicObject.SomeProperty);
        }

        [Test]
        public void TestDemo4TryGetMember()
        {
            dynamic myDynamicObject = new TimsDynamicObject
                {
                    SomeProperty = "Get DynamicObject Demo"
                };
            Assert.AreEqual("Get DynamicObject Demo", myDynamicObject.Title);
        }
    }
}