using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace Dynamics.Demos
{
    /// <summary>
    ///     Demo heavily inspired by Alexandra Rusina's Blog post
    ///     http://blogs.msdn.com/b/csharpfaq/archive/2009/10/01/dynamic-in-c-4-0-introducing-the-expandoobject.aspx
    /// </summary>
    internal class ExpandoObjectDemo
    {
        public void BasicsDemo()
        {
            dynamic expando = new ExpandoObject();
            expando.Title = "ExpandoObject Demo";
            var title = expando.Title;
            expando.DoSomething = new Action(() => Console.WriteLine("DidSomething"));
            expando.DoSomething();
        }

        public void XmlvsExpandoDemo()
        {
            //####### XML #######
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

            //Print out something
            Console.WriteLine((string) contactXml.Element("Address").Element("State"));

            //####### EXPANDO #######

            dynamic contact = new ExpandoObject();
            contact.Name = "Patrick Hines";
            contact.Phone = "206-555-0144";
            contact.Address = new ExpandoObject();
            contact.Address.Street = "123 Main St";
            contact.Address.City = "Mercer Island";
            contact.Address.State = "WA";
            contact.Address.Postal = "68402";

            //Print out something
            Console.WriteLine(contact.Address.State);
        }

        public void LinqExpandoDemo()
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
        }

        public void ExpandoToXmlDemo()
        {
            dynamic contact = new ExpandoObject();
            contact.Name = "Patrick Hines";
            contact.Phone = "206-555-0144";
            contact.Address = new ExpandoObject();
            contact.Address.Street = "123 Main St";
            contact.Address.City = "Mercer Island";
            contact.Address.State = "WA";
            contact.Address.Postal = "68402";

            //Convert from Expando to XML
            var contactXml = ExpandoToXml(contact, "Contact");
        }

        /// <summary>
        ///     Converts an ExpandoObject to its XML representation
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
                    foreach (var element in (List<dynamic>) property.Value)
                        xmlNode.Add(ExpandoToXml(element, property.Key));
                else
                    xmlNode.Add(new XElement(property.Key, property.Value));
            }
            return xmlNode;
        }
    }
}