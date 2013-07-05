using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace Dynamics.Demos
{
    /// <summary>
    ///   Demo heavily inspired by Alexandra Rusina's Blog post
    ///   http://blogs.msdn.com/b/csharpfaq/archive/2009/10/01/dynamic-in-c-4-0-introducing-the-expandoobject.aspx
    /// </summary>
    [TestFixture]
    internal class TestDemo5
    {
        /// <summary>
        ///   Converts an ExpandoObject to its XML representation
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static XElement ExpandoToXml(dynamic node, String nodeName)
        {
            var xmlNode = new XElement(nodeName);

            foreach (var property in (IDictionary<String, Object>) node)
            {
                if (property.Value is ExpandoObject)
                    xmlNode.Add(ExpandoToXml(property.Value, property.Key));

                else if (property.Value.GetType() == typeof (List<dynamic>))
                {
                    foreach (var element in (List<dynamic>) property.Value)
                        xmlNode.Add(ExpandoToXml(element, property.Key));
                }
                else
                    xmlNode.Add(new XElement(property.Key, property.Value));
            }
            return xmlNode;
        }

        [Test]
        public void TestDemo5Basics()
        {
            dynamic expando = new ExpandoObject();

            expando.Title = "ExpandoObject Demo";
            expando.DoSomething = new Func<string>(() => "DidSomething");

            Assert.AreEqual("ExpandoObject Demo", expando.Title);
            Assert.AreEqual("DidSomething", expando.DoSomething());
        }

        [Test]
        public void TestDemo5XmlvsExpandoDemo()
        {
            //Arrange
            var contactXml =
                new XElement("Contact",
                             new XElement("Name", "Patrick Hines"),
                             new XElement("Phone", "206-555-0144"),
                             new XElement("Address",
                                          new XElement("Street1", "123 Main St"),
                                          new XElement("City", "Mercer Island"),
                                          new XElement("State", "WA"),
                                          new XElement("Postal", "68042")
                                 )
                    );

            dynamic contact = new ExpandoObject();
            contact.Name = "Patrick Hines";
            contact.Phone = "206-555-0144";
            contact.Address = new ExpandoObject();
            contact.Address.Street = "123 Main St";
            contact.Address.City = "Mercer Island";
            contact.Address.State = "WA";
            contact.Address.Postal = "68402";

            //Act
            var xmlState = contactXml.Element("Address").Element("State").ToString();
            var expandoState = contact.Address.State;

            //Assert
            Assert.AreEqual("<State>WA</State>", xmlState);
            Assert.AreEqual("WA", expandoState);
        }

        [Test]
        public void TestDemo5LinqExpandoDemo()
        {
            dynamic contacts = new List<dynamic>();

            contacts.Add(new ExpandoObject());
            contacts[0].Name = "Patrick Hines";
            contacts[0].Phone = "206-555-0144";

            contacts.Add(new ExpandoObject());
            contacts[1].Name = "Ellen Adams";
            contacts[1].Phone = "206-555-0155";

            //var phonesXML = from c in contactsXML.Elements("Contact")
            //                where c.Element("Name").Value == "Patrick Hines"
            //                select c.Element("Phone").Value;

            var phones = from c in (contacts as List<dynamic>)
                         where c.Name == "Patrick Hines"
                         select c.Phone;

            Assert.AreEqual(1, phones.Count());
            Assert.AreEqual("206-555-0144", phones.FirstOrDefault());
        }

        [Test]
        public void TestDemo5ExpandoToXmlDemo()
        {
            //Arrange
            dynamic contact = new ExpandoObject();
            contact.Name = "Patrick Hines";
            contact.Phone = "206-555-0144";
            contact.Address = new ExpandoObject();
            contact.Address.Street = "123 Main St";
            contact.Address.City = "Mercer Island";
            contact.Address.State = "WA";
            contact.Address.Postal = "68402";

            //Act
            //Convert from Expando to XML
            var contactXml = ExpandoToXml(contact, "Contact");
            var xmlState = contactXml.Element("Address").Element("State").ToString();

            //Assert
            Assert.IsTrue(contactXml.HasElements);
            Assert.AreEqual("<State>WA</State>", xmlState);
        }
    }
}