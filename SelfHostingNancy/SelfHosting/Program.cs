using System;
using Nancy;
using Nancy.Hosting.Self;

namespace SelfHostingNancy
{
	class Program
	{
		private const string ServerUri = "http://localhost:1234";

		static void Main(string[] args)
		{
			var host = new NancyHost(new Uri(ServerUri));
			Console.WriteLine("Listening on " + ServerUri);
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
			Get["/"] = input => "Hello World";

            Get["/{page}/{subpage?}"] = parameters => //Ex URI=localhost:1234/about/contact
                {
                    //Input
                    var pageBody =  "You are on the '" + parameters.page + "' page";
                    if (parameters.subpage != null)
                        pageBody += " and the subpage is '" + parameters.subpage + "'.";
                    //return pageBody;
                    
                    //Output
                    //return HttpStatusCode.OK;
                    //return "Hello World";
                    //return View["myView"];
                    //return Response.AsRedirect("/");
                    //return Negotiate.WithModel(something);
                };
		}
	}
}
