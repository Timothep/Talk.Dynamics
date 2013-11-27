using IronPython.Hosting;
using NUnit.Framework;

namespace Dynamics.Demos
{
    [TestFixture]
    internal class TestDemo3
    {
        [Test]
        public void TestDemo3PythonDemo()
        {
            var pythonRuntime = Python.CreateRuntime();
            dynamic pythonScript = pythonRuntime.UseFile("Demo03_script.py");
            var result = pythonScript.add(100, 200);
            Assert.AreEqual(300, result);
        }
    }
}