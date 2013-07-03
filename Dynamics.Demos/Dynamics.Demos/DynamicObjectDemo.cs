using System.Dynamic;

namespace Dynamics.Demos
{
    internal class DynamicObjectDemo
    {
        public DynamicObjectDemo()
        {
            dynamic myDynamicObject = new MyDynamicObject();
            //myDynamicObject.Title = "DynamicObjectDemo";
            //var title = myDynamicObject.Title;
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