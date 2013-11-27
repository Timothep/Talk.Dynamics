using System.Diagnostics;

namespace Dynamics.Demos
{
    using System;
    using System.Collections.Generic;
    using Massive;
    using NUnit.Framework;

    public class Albums : DynamicModel
    {
        public Albums()
            : base("SqlServer", tableName: "Album", primaryKeyField: "AlbumId")
        {
        }
    }

    [TestFixture]
    internal class TestDemo8
    {
        [Test]
        public void TestDemo8MassiveGetAll()
        {
            dynamic albumTable = new Albums();
            IEnumerable<dynamic> allAlbums = albumTable.All();

            foreach (var album in allAlbums)
                Debug.WriteLine(album.Title);
        }

        [Test]
        public void TestDemo8MassiveFind()
        {
            dynamic table = new Albums();
            IEnumerable<dynamic> albumsWithGenreId1 = table.FindBy(GenreId: 1);

            foreach (var album in albumsWithGenreId1)
            {
                Assert.AreEqual("For Those About To Rock We Salute You", album.Title);
                break;
            }
        }
    }
}