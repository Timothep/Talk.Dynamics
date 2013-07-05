using NUnit.Framework;
using Nancy;
using Nancy.Testing;

namespace DemoNancy
{
    public class Module : NancyModule
    {
        public Module()
        {
            Get["/greet/{name}"] = x =>
            {
                return string.Concat("Hello ", x.name);
            };
        }
    }

    //[TestFixture]
    //class TestDemo7
    //{
    //    [Test]
    //    public void TestDemo7Something()
    //    {
    //        var bootstrapper = new DefaultNancyBootstrapper();
    //        var browser = new Browser(bootstrapper);
    //        var result = browser.Get("/blahblah", with => with.HttpRequest());
    //        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
    //    }
    //}
}
