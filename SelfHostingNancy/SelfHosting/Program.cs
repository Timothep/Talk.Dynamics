using System;
using Nancy;
using Nancy.Hosting.Self;

namespace SelfHostingNancy
{
	class Program
	{
		private const string serverUri = "http://localhost:1234";

		static void Main(string[] args)
		{
			var host = new NancyHost(new Uri(serverUri));
			Console.WriteLine("Listening on " + serverUri);
			host.Start();
			Console.ReadLine();
			Console.WriteLine("You are about to close the application.");
			host.Stop();
		}
	}

	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = _ => "Hello World";

            Get["/{something}"] = parameters => //URI={something}?option=default -> parameters.option
                {
                    var something = parameters.something;
                    return HttpStatusCode.OK;
                    //return "Hello World";
                    //return View["myView"];
                    //return Response.AsRedirect("/");
                    //return Negotiate.WithModel(something);
                };
		}
	}
}
