using System.Dynamic;
using AmazedSaint.Elastic.Lib;
using NUnit.Framework;

namespace Dynamics.Demos
{
    [TestFixture]
    internal class TestDemo6
    {
        [Test]
        public void TestDemo6ExpandovsElasticDemo()
        {
            //Arrange
            dynamic contactExpando = new ExpandoObject();
            contactExpando.Name = "Patrick Hines";
            contactExpando.Phone = "206-555-0144";
            contactExpando.Address = new ExpandoObject();
            contactExpando.Address.Street = "123 Main St";
            contactExpando.Address.City = "Mercer Island";
            contactExpando.Address.State = "WA";
            contactExpando.Address.Postal = "68402";

            dynamic contactElastic = new ElasticObject();
            contactElastic.Name = "Patrick Hines";
            contactElastic.Phone = "206-555-0144";
            contactElastic.Address.Street = "123 Main St";
            contactElastic.Address.City = "Mercer Island";
            contactElastic.Address.State = "WA";
            contactElastic.Address.Postal = "68402";

            //Act
            var elasticState = contactElastic.Address.State;

            //Assert
            Assert.AreEqual("WA", elasticState);
        }

        [Test]
        public void TestDemo6ElasticToXmlDemo()
        {
            //Arrange
            dynamic contact = new ElasticObject("Contact");
            contact.Name = "Patrick Hines";
            contact.Phone = "206-555-0144";
            contact.Address.Street = "123 Main St";
            contact.Address.City = "Mercer Island";
            contact.Address.State = "WA";
            contact.Address.Postal = "68402";

            //Act
            var contactXml = contact > FormatType.Xml;
            var addressXml = contactXml.Element("Address");
            var xmlState = addressXml.Attribute("State");

            //Assert
            Assert.IsTrue(contactXml.HasElements);
            Assert.AreEqual("WA", xmlState.Value);
        }

        //EXPANDO
        //<Contact>
        //  <Name>Patrick Hines</Name>
        //  <Phone>206-555-0144</Phone>
        //  <Address>
        //    <Street>123 Main St</Street>
        //    <City>Mercer Island</City>
        //    <State>WA</State>
        //    <Postal>68402</Postal>
        //  </Address>
        //</Contact>

        //ELASTIC
        //<Contact Name="Patrick Hines" Phone="206-555-0144">
        //  <Adress Street="123 Main St" City="Mercer Island" State="WA" Postal="68402" />
        //</Contact>
    }
}