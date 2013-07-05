using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Diagnostics;

namespace ConsoleApplication1
{
	public class CustomBootstrapper: Nancy.DefaultNancyBootstrapper
	{
		protected override DiagnosticsConfiguration DiagnosticsConfiguration
		{
			get { return new DiagnosticsConfiguration { Password = @"nancy" }; }
		}
	}
}
