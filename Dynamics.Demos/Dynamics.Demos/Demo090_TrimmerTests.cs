namespace Dynamics.Demos
{
    using NUnit.Framework;

    [TestFixture]
    public class TestsDemo09
    {
        private const string PathToTrim = @"C:\Dev\Ruby193\bin\ruby.exe";
        private readonly dynamic trimmer = new Trimmer();
        private readonly dynamic easyTrimmer = new EasyTrimmer();

        // EASY CASE, ONE LEVEL

        [Test]
        public void TestDemo09_EasyTrimmer_ExtractDriveLetter()
        {
            string driveLetter = easyTrimmer.ExtractDriveLetter(PathToTrim);
            Assert.AreEqual("C", driveLetter);
        }

        [Test]
        [Ignore]
        public void TestDemo09_EasyTrimmer_ExtractFileNameWithoutExtension()
        {
            var fileNameWithoutExtension = easyTrimmer.ExtractFileName(PathToTrim).WithoutExtension();
            Assert.AreEqual("ruby", fileNameWithoutExtension);
        }

        // ELABORATE CASE

        [Test]
        public void TestDemo09ExtractDriveLetter()
        {
            DynamicWrapper driveLetter = trimmer.ExtractDriveLetter(PathToTrim);
            Assert.AreEqual("C", driveLetter.Value);
        }

        [Test]
        public void TestDemo09ExtractFileExtension()
        {
            DynamicWrapper fileExtension = trimmer.ExtractFileExtension(PathToTrim);
            Assert.AreEqual(".exe", fileExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractFileName()
        {
            DynamicWrapper fileName = trimmer.ExtractFileName(PathToTrim);
            Assert.AreEqual("ruby.exe", fileName.Value);
        }

        [Test]
        public void TestDemo09ExtractFileNameWithoutExtension()
        {
            DynamicWrapper fileNameWithoutExtension = trimmer.ExtractFileName(PathToTrim).WithoutExtension();
            Assert.AreEqual("ruby", fileNameWithoutExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractPath()
        {
            DynamicWrapper path = trimmer.ExtractPath(PathToTrim);
            Assert.AreEqual(@"C:\Dev\Ruby193\bin", path.Value);
        }

        [Test]
        public void TestDemo09ExtractPathRoot()
        {
            DynamicWrapper pathRoot = trimmer.ExtractPathRoot(PathToTrim);
            Assert.AreEqual(@"C:\", pathRoot.Value);
        }
    }
}