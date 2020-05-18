using System.Collections.Generic;
using Org.XmlUnit.Builder;
using Org.XmlUnit.Diff;

namespace XmlComparison
{
    public static class XmlComparer
    {
        public static bool AreXMLDocumentsTheSame(string control, string test, List<XmlFilters> filters)
        {
            DiffBuilder DifBuilder = DiffBuilder.Compare(control).WithTest(test);

            foreach (XmlFilters thisFilter in filters)
            {
                switch (thisFilter)
                {
                    case XmlFilters.Comments:
                        IgnoreComments(DifBuilder);
                        break;
                    case XmlFilters.WhiteSpace:
                        IgnoreWhiteSpace(DifBuilder);
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

        public static DiffBuilder IgnoreWhiteSpace(DiffBuilder builder)
        {
            return builder.IgnoreWhitespace();
        }
    }
}
