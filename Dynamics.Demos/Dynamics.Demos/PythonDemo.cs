using System;
using IronPython.Hosting;

namespace Dynamics.Demos
{
    internal class PythonDemo
    {
        public PythonDemo()
        {
            var pythonRuntime = Python.CreateRuntime();
            dynamic pythonScript = pythonRuntime.UseFile("script.py");
            var result = pythonScript.add(100, 200);
            Console.WriteLine(result);
        }
    }
}