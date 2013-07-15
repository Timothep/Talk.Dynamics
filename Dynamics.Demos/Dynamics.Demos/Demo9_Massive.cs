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
            dynamic table = new Albums();
            var allAlbums = table.All();

            foreach (var album in allAlbums)
                Console.WriteLine(album.Title);
        }

        [Test]
        public void TestDemo9MassiveFind()
        {
            dynamic table = new Albums();
            var albumsGenreId1 = table.Find(GenreId: 1);

            foreach (var album in albumsGenreId1)
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