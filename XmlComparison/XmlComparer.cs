using Org.XmlUnit.Builder;
using Org.XmlUnit.Diff;

namespace XmlComparison
{
    public static class XmlComparer
    {
        public static bool AreXMLDocumentsTheSame(string control, string test)
        {
            Diff myDiff = DiffBuilder.Compare(control).WithTest(test).Build();
            return !myDiff.HasDifferences();
        }
    }
}
