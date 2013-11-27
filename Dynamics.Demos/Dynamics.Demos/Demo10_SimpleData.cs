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
            const string sqlConnString = @"Data Source=.\SQLEXPRESS;Initial Catalog=MvcMusicStore;Integrated Security=True";
            var db = Database.OpenConnection(sqlConnString);

            var albums = db.albums //FROM Album
                            .FindAllByGenreId(1) //SELECT * + WHERE GenreId = 1
                            .OrderByAlbumIdDescending() //ORDER BY AlbumId DESC
                            .Take(10).Skip(10) //Paging
                            .ToList(); //Enumerate to fire the query

            Assert.Greater(albums.Count, 0);
            Assert.LessOrEqual(albums.Count, 10);
        }
    }
}