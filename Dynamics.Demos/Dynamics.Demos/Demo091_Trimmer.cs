using System;
using System.Dynamic;
using System.Globalization;
using System.IO;

namespace Dynamics.Demos
{
    internal class EasyTrimmer : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var workingMember = binder.Name;
            if (workingMember.StartsWith("Extract"))
            {
                workingMember = workingMember.Remove(0, "Extract".Length);

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
    
    internal class Trimmer : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Parser.Parse(binder.Name, args[0].ToString());
            return true;
        }
    }

    internal static class Parser
    {
        public static Wrapper Parse(string how, string what)
        {
            if (how.StartsWith("Extract"))
            {
                how = how.Remove(0, "Extract".Length);

                if (how.StartsWith("Drive"))
                    return new Wrapper(what.Substring(0, 3));
                if (how.StartsWith("PathRoot"))
                    return new Wrapper(Path.GetPathRoot(what));
                if (how.StartsWith("Path"))
                    return new Wrapper(Path.GetDirectoryName(what));
                if (how.StartsWith("FileExtension"))
                    return new Wrapper(Path.GetExtension(what));
                if (how.StartsWith("FileName"))
                    return new Wrapper(Path.GetFileName(what));
            }

            if (how.StartsWith("Without"))
            {
                how = how.Remove(0, "Without".Length);
                if (how.StartsWith("Extension"))
                    return new Wrapper(what.Remove(what.LastIndexOf('.')));
            }

            if (how.StartsWith("LetterOnly"))
                return new Wrapper(what[0].ToString(CultureInfo.InvariantCulture));

            throw new Exception("Parsing failed");
        }
    }
    
    internal class Wrapper: DynamicObject
    {
        public string Value { get; set; }

        public Wrapper(string value)
        {
            Value = value;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Parser.Parse(binder.Name, this.Value);
            return true;
        }
    }
}
