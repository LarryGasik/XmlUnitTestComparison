using NUnit.Framework;


namespace XmlComparisonTests
{
    [TestFixture()]
    public class XMLComparerTests
    {
        

        [Test]
        public void ShouldPass()
        {
            string control = "<a><b attr1=\"abc\" attr2=\"def\"></b></a>";
            string test = "<a><b attr1=\"uvw\" attr2=\"xyz\"></b></a>";

            var result = XmlComparison.XmlComparer.AreXMLDocumentsTheSame(control, test);
            Assert.AreEqual(false, result);
        }
    }
}
