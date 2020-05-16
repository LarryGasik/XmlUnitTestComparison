using System.Collections.Generic;
using Org.XmlUnit.Builder;
using Org.XmlUnit.Diff;

namespace XmlComparison
{
    public static class XmlComparer
    {
        public static bool AreXMLDocumentsTheSame(string control, string test, List<XMLFilters> filters)
        {
            DiffBuilder DifBuilder = DiffBuilder.Compare(control).WithTest(test);

            foreach (XMLFilters thisFilter in filters)
            {
                switch (thisFilter)
                {
                    case XMLFilters.Comments:
                        IgnoreComments(DifBuilder);
                        break;
                }
            }
            Diff mydiff = DifBuilder.Build();
            return !mydiff.HasDifferences();
        }


        public static DiffBuilder IgnoreComments(DiffBuilder builder)
        {
            return builder.IgnoreComments();
        }
    }
}
