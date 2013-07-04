using System;
using System.Dynamic;
using System.IO;

namespace Dynamics.Demos
{
    internal class TrimPathApiDemo
    {
        public TrimPathApiDemo()
        {
            dynamic trimPathApi = new TrimPathApi();
            var driveLetter = trimPathApi.ExtractDriveLetter(@"C:\Dev\Ruby193\bin\ruby.exe");
            var pathRoot = trimPathApi.ExtractPathRoot(@"C:\Dev\Ruby193\bin\ruby.exe");
            var path = trimPathApi.ExtractPath(@"C:\Dev\Ruby193\bin\ruby.exe");
            var fileExtension = trimPathApi.ExtractFileExtension(@"C:\Dev\Ruby193\bin\ruby.exe");
            var fileName = trimPathApi.ExtractFileName(@"C:\Dev\Ruby193\bin\ruby.exe");
            var fileNameWithExtension = trimPathApi.ExtractFileNameWithExtension(@"C:\Dev\Ruby193\bin\ruby.exe");
        }
    }

    internal class TrimPathApi : DynamicObject
    {
        /*
         * C:\Dev\Ruby193\bin\ruby.exe
         * ExtractDriveLetter
         * ExtractPathRoot
         * ExtractFileName
         * ExtractFileNameWithExtension
         * ExtractFileExtension
        */

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var propertyOrMethodToGet = binder.Name;

            if (!propertyOrMethodToGet.StartsWith("Extract"))
                throw new Exception("Parsing did not end well...");

            propertyOrMethodToGet = propertyOrMethodToGet.Remove(0, "Extract".Length);

            if (propertyOrMethodToGet.StartsWith("DriveLetter"))
            {
                result = new Func<string, string>(filepath => filepath[0].ToString());
                return true;
            }
            if (propertyOrMethodToGet.StartsWith("PathRoot"))
            {
                result = new Func<string, string>(Path.GetPathRoot); //Equivalent to: "filename => Path.GetPathRoot(filename)"
                return true;
            }
            if (propertyOrMethodToGet.StartsWith("Path"))
            {
                result = new Func<string, string>(Path.GetDirectoryName);
                return true;
            }
            if (propertyOrMethodToGet.StartsWith("FileExtension"))
            {
                result = new Func<string, string>(Path.GetExtension);
                return true;
            }
            if (propertyOrMethodToGet.StartsWith("FileName"))
            {
                if (propertyOrMethodToGet.EndsWith("WithExtension"))
                    result = new Func<string, string>(Path.GetFileName);
                else
                    result = new Func<string, string>(Path.GetFileNameWithoutExtension);
                return true;
            }

            throw new Exception("Parsing did not end well...");
        }
    }
}