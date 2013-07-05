using System.Dynamic;
using NUnit.Framework;

namespace Dynamics.Demos
{
    [TestFixture]
    internal class TestDemo4
    {
        [Test]
        [Ignore]
        public void TestDemo4TrySetMember()
        {
            dynamic myDynamicObject = new MyDynamicObject();
            myDynamicObject.Title = "DynamicObjectDemo";
        }

        [Test]
        [Ignore]
        public void TestDemo4TryGetMember()
        {
            dynamic myDynamicObject = new MyDynamicObject();
            var title = myDynamicObject.Title;
        }
    }

    internal class MyDynamicObject : DynamicObject
    {
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var propertyToSet = binder.Name;
            var valueToSet = value;
            return base.TrySetMember(binder, value);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var propertyOrMethodToGet = binder.Name;
            return base.TryGetMember(binder, out result);
        }
    }
}