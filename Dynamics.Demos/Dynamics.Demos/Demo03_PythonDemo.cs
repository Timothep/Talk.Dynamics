namespace Dynamics.Demos
{
    using IronPython.Hosting;
    using NUnit.Framework;

    [TestFixture]
    internal class TestDemo3
    {
        [Test]
        public void TestDemo3PythonDemo()
        {
            var pythonRuntime = Python.CreateRuntime();
            dynamic pythonScript = pythonRuntime.UseFile("Demo03_script.py");
            var result = pythonScript.add(10, 20);
            Assert.AreEqual(30, result);
        }
    }
}