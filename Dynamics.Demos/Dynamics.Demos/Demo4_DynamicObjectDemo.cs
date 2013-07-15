using System;
using System.Dynamic;
using NUnit.Framework;

namespace Dynamics.Demos
{
    [TestFixture]
    internal class TestDemo4
    {
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

    internal class MyDynamicObject : DynamicObject
    {
        public string MyTitle { get; set; }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var propertyToSet = binder.Name;
            var valueToSet = value;

            MyTitle = (string)value;

            value = null;
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var propertyOrMethodToGet = binder.Name;

            result = MyTitle;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return base.TryInvokeMember(binder, args, out result);
        }
    }
}