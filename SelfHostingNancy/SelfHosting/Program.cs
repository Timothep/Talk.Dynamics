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
                            //Func<dynamic, dynamic>
			Get["/"] = _ => "Hello World";

            Get["/{page}"] = parameters => //Ex URI=www.mysite.com/about?lang=de
                {
                    //Input
                    var page = parameters.page; //"about"
                    var language = parameters.lang; //"de"

                    //Output
                    return HttpStatusCode.OK;
                    //return "Hello World";
                    //return View["myView"];
                    //return Response.AsRedirect("/");
                    //return Negotiate.WithModel(something);
                };
		}
	}
}
