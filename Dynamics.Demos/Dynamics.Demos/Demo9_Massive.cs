using System;
using Massive;
using NUnit.Framework;

namespace Dynamics.Demos
{
    [TestFixture]
    internal class Demo9_Massive
    {
        [Test]
        public void TestDemo9MassiveGetAll()
        {
            var table = new Albums();
            var albums = table.All();
            //var albums = table.Find(GenreId: 1);

            foreach (var album in albums)
                Console.WriteLine(album.Title);
        }
    }

    public class Albums : DynamicModel
    {
        public Albums() : base("SqlServer", tableName: "Album", primaryKeyField: "AlbumId")
        {
        }
    }
}