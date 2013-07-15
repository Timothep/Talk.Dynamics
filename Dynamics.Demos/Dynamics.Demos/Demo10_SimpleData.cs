﻿using NUnit.Framework;
using Simple.Data;

namespace Dynamics.Demos
{
    [TestFixture]
    internal class TestDemo10
    {
        [Test]
        public void TestDemo10Album()
        {
            const string sqlConnString = @"Data Source=.\SQLEXPRESS;Initial Catalog=MvcMusicStore;Integrated Security=True";
            var db = Database.OpenConnection(sqlConnString);

            var album = db.Albums.Get(392);

            Assert.AreEqual("Nevermind", album.Title);
        }
    }
}