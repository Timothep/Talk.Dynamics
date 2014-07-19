using Microsoft.CSharp.RuntimeBinder;

namespace Dynamics.Demos
{
    using NUnit.Framework;

    [TestFixture]
    public class TestsDemo09
    {
        private readonly dynamic trimmer = new Trimmer();
        private readonly dynamic easyTrimmer = new EasyTrimmer();

        private const string PathToTrim = @"C:\Dev\Ruby193\bin\ruby.exe";

        // EASY CASE, EASY-TRIMMER

        [Test]
        public void TestDemo09_EasyTrimmer_ExtractFileName()
        {
            string fileName = easyTrimmer.ExtractFileName(PathToTrim);
            Assert.AreEqual("ruby.exe", fileName);
        }

        [Test]
				[ExpectedException(typeof(RuntimeBinderException))]
        public void TestDemo09_EasyTrimmer_ExtractFileNameWithoutExtension()
        {
            var fileNameWithoutExtension = easyTrimmer.ExtractFileName(PathToTrim).WithoutExtension();
            Assert.AreEqual("ruby", fileNameWithoutExtension);
        }

        // ELABORATE CASE - TRIMMER

        [Test]
        public void TestDemo09ExtractDrive()
        {
            Wrapper drive = trimmer.ExtractDrive(PathToTrim);
            Assert.AreEqual(@"C:\", drive.Value);
        }

        [Test]
        public void TestDemo09ExtractDriveLetter()
        {
            Wrapper driveLetter = trimmer.ExtractDrive(PathToTrim).LetterOnly();
            Assert.AreEqual("C", driveLetter.Value);
        }

        [Test]
        public void TestDemo09ExtractFileExtension()
        {
            Wrapper fileExtension = trimmer.ExtractFileExtension(PathToTrim);
            Assert.AreEqual(".exe", fileExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractFileName()
        {
            Wrapper fileName = trimmer.ExtractFileName(PathToTrim);
            Assert.AreEqual("ruby.exe", fileName.Value);
        }

        [Test]
        public void TestDemo09ExtractFileNameWithoutExtension()
        {
            Wrapper fileNameWithoutExtension = trimmer.ExtractFileName(PathToTrim).WithoutExtension().GetFirstLetter();
            Assert.AreEqual("r", fileNameWithoutExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractPath()
        {
            Wrapper path = trimmer.ExtractPath(PathToTrim);
            Assert.AreEqual(@"C:\Dev\Ruby193\bin", path.Value);
        }

        [Test]
        public void TestDemo09ExtractPathRoot()
        {
            Wrapper pathRoot = trimmer.ExtractPathRoot(PathToTrim);
            Assert.AreEqual(@"C:\", pathRoot.Value);
        }
    }
}