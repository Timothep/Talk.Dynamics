namespace Dynamics.Demos
{
    using System.Dynamic;
    using NUnit.Framework;

    internal class MyDynamicObject : DynamicObject
    {
        public string MyTitle { get; set; }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var propertyToSet = binder.Name;
            var valueToSet = value;

            MyTitle = (string)value;

            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var propertyOrMethodToGet = binder.Name;

            result = MyTitle;
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
            //notDynamicString.MyTitle = "42";
            //notDynamicString.Title = "42";

            //DynamicObject can call "real properties" like a "normal object"
            MyDynamicObject dynamicObject = new MyDynamicObject();
            dynamicObject.MyTitle = "42";
            //dynamicObject.Title = "42";

            //Combine the two to get both features
            dynamic fullDynamicObject = new MyDynamicObject();
            fullDynamicObject.MyTitle = "42";
            fullDynamicObject.Title = "42";
        }

        [Test]
        public void TestDemo4TrySetMember()
        {
            dynamic myDynamicObject = new MyDynamicObject();
            myDynamicObject.Title = "DynamicObjectDemo";
        }

        [Test]
        public void TestDemo4TryGetMember()
        {
            dynamic myDynamicObject = new MyDynamicObject();
            var title = myDynamicObject.Title;
        }
    }
}