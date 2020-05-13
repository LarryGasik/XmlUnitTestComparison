using System.IO;
using System.Xml;
using NUnit.Framework;


namespace XmlComparisonTests
{
    [TestFixture()]
    public class XmlComparerTests
    {
        [Test]
        public void TheSameFileShouldBeMarkedTheSame()
        {
            string fileName = @"SampleFiles\BreakfastMenu.xml";
            string  control = LoadXmlIntoStringFromFile(fileName);
            string test = control;
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DifferentFilesShouldBeMarkedTheSame()
        {
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\BreakfastMenu.xml");
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CommentsShouldBeMarkedAsDifferent()
        {
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\MessagesComments.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void CheckToSeeIfTheSameIgnoringComments()
        {
            string test = LoadXmlIntoStringFromFile(@"SampleFiles\MessagesComments.xml");
            string control = LoadXmlIntoStringFromFile(@"SampleFiles\Messages.xml");
            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test);
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
