namespace Dynamics.Demos
{
    using NUnit.Framework;

    [TestFixture]
    public class TestsDemo09
    {
        private const string PathToTrim = @"C:\Dev\Ruby193\bin\ruby.exe";
        private readonly dynamic trimmer = new Trimmer();

        [TestFixtureSetUp]
        public void TestsSetup()
        {
        }

        [Test]
        public void TestDemo09ExtractDriveLetter()
        {
            MemberAccessWrapper driveLetter = trimmer.ExtractDriveLetter(PathToTrim);
            Assert.AreEqual("C", driveLetter.Value);
        }

        [Test]
        public void TestDemo09ExtractFileExtension()
        {
            MemberAccessWrapper fileExtension = trimmer.ExtractFileExtension(PathToTrim);
            Assert.AreEqual(".exe", fileExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractFileName()
        {
            MemberAccessWrapper fileName = trimmer.ExtractFileName(PathToTrim);
            Assert.AreEqual("ruby.exe", fileName.Value);
        }

        [Test]
        public void TestDemo09ExtractFileNameWithoutExtension()
        {
            MemberAccessWrapper fileNameWithoutExtension = trimmer.ExtractFileName(PathToTrim).WithoutExtension();
            Assert.AreEqual("ruby", fileNameWithoutExtension.Value);
        }

        [Test]
        public void TestDemo09ExtractPath()
        {
            MemberAccessWrapper path = trimmer.ExtractPath(PathToTrim);
            Assert.AreEqual(@"C:\Dev\Ruby193\bin", path.Value);
        }

        [Test]
        public void TestDemo09ExtractPathRoot()
        {
            MemberAccessWrapper pathRoot = trimmer.ExtractPathRoot(PathToTrim);
            Assert.AreEqual(@"C:\", pathRoot.Value);
        }
    }
}