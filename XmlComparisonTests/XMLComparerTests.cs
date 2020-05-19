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
        private List<XmlFilters> filters;

        [SetUp]
        public void SetUp()
        {
            filters = new List<XmlFilters>();

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
            filters.Add(XmlFilters.Comments);
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

        [Test]
        public void WhiteSpaceShouldComeBackAsDifferent()
        {
            ///Note that the white space is within the nodes and not the structure of the file.
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\MessagesWhiteSpace.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test, filters);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void IgnoreWhiteSpaceFilter()
        {
            ///Note that the white space is within the nodes and not the structure of the file.
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\MessagesWhiteSpace.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            filters.Add(XmlFilters.WhiteSpace);
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test, filters);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void MultipleFilters()
        {
            ///Note that the white space is within the nodes and not the structure of the file.
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\MessagesCommentsWhitespace.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            filters.Add(XmlFilters.WhiteSpace);
            filters.Add(XmlFilters.Comments);
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test, filters);
            Assert.AreEqual(true, result);
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
