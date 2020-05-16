using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using NUnit.Framework;
using XmlComparison;


namespace XmlComparisonTests
{
    [TestFixture()]
    public class XmlComparerTests
    {
        private List<XMLFilters> filters;

        [SetUp]
        public void SetUp()
        {
            filters = new List<XMLFilters>();

        }

        [Test]
        public void TheSameFileShouldBeMarkedTheSame()
        {
            string fileName = @"SampleFiles\BreakfastMenu.xml";
            string control = LoadXmlIntoStringFromFile(fileName);
            string test = control;
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test, filters);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DifferentFilesShouldBeMarkedAsDifferent()
        {
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\BreakfastMenu.xml");
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test, filters);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CheckToSeeIfWeCanIgnoreComments()
        {
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\MessagesComments.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            filters.Add(XMLFilters.Comments);
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test, filters);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void CommentsShouldComeBackAsDifferent()
        {
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\MessagesComments.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test, filters);
            Assert.AreEqual(false, result);
        }


        /// <summary>
        /// Load XML File and get a string back for comparison
        /// </summary>
        /// <param name="filepath">File Path to the XML on disk</param>
        /// <returns>Contents of XML in string format</returns>
        private static string LoadXmlIntoStringFromFile(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            return doc.InnerXml;
        }


    }
}
