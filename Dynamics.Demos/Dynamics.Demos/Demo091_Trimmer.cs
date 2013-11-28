using System;
using System.Dynamic;
using System.Globalization;
using System.IO;

namespace Dynamics.Demos
{
    internal class Trimmer : DynamicObject
    {
        /// <summary>
        ///   Function handling the first call on the dynamic object
        /// </summary>
        /// <param name="binder">A wrapper object containing the name of the member that was called "ExtractSomething"</param>
        /// <param name="result">An initialized DynamicWrapper on which to invoke the method</param>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //Create a Wrapper and save the member name that was called "ExtractSomething.."
            result = new DynamicWrapper(binder.Name);
            return true;
        }
    }

    internal class EasyTrimmer : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var workingMember = binder.Name;
            if (workingMember.StartsWith("Extract"))
            {
                workingMember = workingMember.Remove(0, "Extract".Length);

                if (workingMember.StartsWith("DriveLetter"))
                {
                    result = (args[0].ToString())[0].ToString(CultureInfo.InvariantCulture);
                    return true;
                }
                if (workingMember.StartsWith("FileName"))
                {
                    result = Path.GetFileName(args[0].ToString());
                    return true;
                }
            }

            result = null;
            return false;
        }
    }

    internal class DynamicWrapper : DynamicObject
    {
        private readonly string member;
        public string Value;

        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name="memberName">The name of the member called to come here (ExtractSomething)</param>
        /// <param name="result">The result of the parsing sofar (in case of chaining)</param>
        public DynamicWrapper(string memberName, string result = null)
        {
            member = memberName;
            Value = result;
        }

        /// <summary>
        ///   Handler for the commands
        /// </summary>
        /// <param name="binder">N/A</param>
        /// <param name="args">Contains the eventual parameters</param>
        /// <param name="result">Contains a DynamicWrapper object representing the state of the parsing</param>
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            var workingMember = member;
            if (workingMember.StartsWith("Extract"))
            {
                workingMember = workingMember.Remove(0, "Extract".Length);

                if (workingMember.StartsWith("DriveLetter"))
                {
                    result = new DynamicWrapper(member, (args[0].ToString())[0].ToString(CultureInfo.InvariantCulture));
                    return true;
                }
                if (workingMember.StartsWith("PathRoot"))
                {
                    result = new DynamicWrapper(member, Path.GetPathRoot(args[0].ToString()));
                    return true;
                }
                if (workingMember.StartsWith("Path"))
                {
                    result = new DynamicWrapper(member, Path.GetDirectoryName(args[0].ToString()));
                    return true;
                }
                if (workingMember.StartsWith("FileExtension"))
                {
                    result = new DynamicWrapper(member, Path.GetExtension(args[0].ToString()));
                    return true;
                }
                if (workingMember.StartsWith("FileName"))
                {
                    result = new DynamicWrapper(member, Path.GetFileName(args[0].ToString()));
                    return true;
                }
            }

            if (workingMember.StartsWith("Without"))
            {
                workingMember = workingMember.Remove(0, "Without".Length);
                if (workingMember.StartsWith("Extension"))
                {
                    result = new DynamicWrapper(member, Value.Remove(Value.LastIndexOf('.')));
                    return true;
                }
            }

            throw new Exception("Parsing failed");
        }

        /// <summary>
        ///   Function handling the subsequent/chained calls to dynamic objects
        /// </summary>
        /// <param name="binder">A wrapper object containing the name of the member that was called "WithoutExtension"</param>
        /// <param name="result">An initialized DynamicWrapper on which to invoke the method</param>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new DynamicWrapper(binder.Name, Value);
            return true;
        }
    }
}
