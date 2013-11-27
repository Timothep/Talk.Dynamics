using System;
using System.Dynamic;
using System.Globalization;
using System.IO;
using NUnit.Framework;

namespace Dynamics.Demos
{
    internal class TrimPathApi : DynamicObject
    {
        /// <summary>
        ///   Function handling the first call on the dynamic object
        /// </summary>
        /// <param name="binder">A wrapper object containing the name of the member that was called "ExtractSomething"</param>
        /// <param name="result">An initialized MemberAccessWrapper on which to invoke the method</param>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            //Create a Wrapper and save the member name that was called "ExtractSomething.."
            result = new MemberAccessWrapper(binder.Name);
            return true;
        }
    }

    internal class MemberAccessWrapper : DynamicObject
    {
        private readonly string member;
        public string Value;

        /// <summary>
        ///   Constructor
        /// </summary>
        /// <param name="memberName">The name of the member called to come here (ExtractSomething)</param>
        /// <param name="result">The result of the parsing sofar (in case of chaining)</param>
        public MemberAccessWrapper(string memberName, string result = null)
        {
            member = memberName;
            Value = result;
        }

        /// <summary>
        ///   Handler for the commands
        /// </summary>
        /// <param name="binder">N/A</param>
        /// <param name="args">Contains the eventual parameters</param>
        /// <param name="result">Contains a MemberAccessWrapper object representing the state of the parsing</param>
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            var workingMember = member;
            if (workingMember.StartsWith("Extract"))
            {
                workingMember = workingMember.Remove(0, "Extract".Length);

                if (workingMember.StartsWith("DriveLetter"))
                {
                    result = new MemberAccessWrapper(member, (args[0].ToString())[0].ToString(CultureInfo.InvariantCulture));
                    return true;
                }
                if (workingMember.StartsWith("PathRoot"))
                {
                    result = new MemberAccessWrapper(member, Path.GetPathRoot(args[0].ToString()));
                    return true;
                }
                if (workingMember.StartsWith("Path"))
                {
                    result = new MemberAccessWrapper(member, Path.GetDirectoryName(args[0].ToString()));
                    return true;
                }
                if (workingMember.StartsWith("FileExtension"))
                {
                    result = new MemberAccessWrapper(member, Path.GetExtension(args[0].ToString()));
                    return true;
                }
                if (workingMember.StartsWith("FileName"))
                {
                    result = new MemberAccessWrapper(member, Path.GetFileName(args[0].ToString()));
                    return true;
                }
            }

            if (workingMember.StartsWith("Without"))
            {
                workingMember = workingMember.Remove(0, "Without".Length);
                if (workingMember.StartsWith("Extension"))
                {
                    result = new MemberAccessWrapper(member, Value.Remove(Value.LastIndexOf('.')));
                    return true;
                }
            }

            throw new Exception("Parsing failed");
        }

        /// <summary>
        ///   Function handling the subsequent/chained calls to dynamic objects
        /// </summary>
        /// <param name="binder">A wrapper object containing the name of the member that was called "WithoutExtension"</param>
        /// <param name="result">An initialized MemberAccessWrapper on which to invoke the method</param>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = new MemberAccessWrapper(binder.Name, Value);
            return true;
        }
    }

    [TestFixture]
    public class TestsDemo09
    {
        private const string PathToTrim = @"C:\Dev\Ruby193\bin\ruby.exe";
        private dynamic trimPathApi;

        [TestFixtureSetUp]
        public void TestsSetup()
        {
            trimPathApi = new TrimPathApi();
        }

        [Test]
        public void TestDemo09ExtractDriveLetter()
        {
            MemberAccessWrapper driveLetter = trimPathApi.ExtractDriveLetter(PathToTrim);
            Assert.AreEqual("C", driveLetter.Value);
        }

        [Test]
        public void TestDemo09ExtractFileExtension()
        {
            MemberAccessWrapper fileExtension = trimPathApi.ExtractFileExtension(PathToTrim);
            Assert.AreEqual(".exe", fileExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractFileName()
        {
            MemberAccessWrapper fileName = trimPathApi.ExtractFileName(PathToTrim);
            Assert.AreEqual("ruby.exe", fileName.Value);
        }

        [Test]
        public void TestDemo09ExtractFileNameWithoutExtension()
        {
            MemberAccessWrapper fileNameWithExtension = trimPathApi.ExtractFileName(PathToTrim).WithoutExtension();
            Assert.AreEqual("ruby", fileNameWithExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractPath()
        {
            MemberAccessWrapper path = trimPathApi.ExtractPath(PathToTrim);
            Assert.AreEqual(@"C:\Dev\Ruby193\bin", path.Value);
        }

        [Test]
        public void TestDemo09ExtractPathRoot()
        {
            MemberAccessWrapper pathRoot = trimPathApi.ExtractPathRoot(PathToTrim);
            Assert.AreEqual(@"C:\", pathRoot.Value);
        }
    }
}