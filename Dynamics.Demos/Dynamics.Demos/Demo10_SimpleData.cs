namespace Dynamics.Demos
{
    using NUnit.Framework;
    using Simple.Data;

    [TestFixture]
    internal class TestDemo10
    {
        [Test]
        public void TestDemo10Album()
        {
            const string sqlConnString = 
                @"Data Source=.\SQLEXPRESS;Initial Catalog=MvcMusicStore;Integrated Security=True";
            dynamic db = Database.OpenConnection(sqlConnString);

            var albums = db.albums											//FROM Album
                            .FindAllByGenreId(1)				//SELECT * WHERE GenreId = 1
                            .OrderByAlbumIdDescending() //ORDER BY AlbumId DESC
														.Skip(10).Take(10)					//Paging
                            .ToList();									//Enumerate to fire the query

            Assert.AreEqual(10, albums.Count);
        }
    }
}