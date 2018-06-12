using System.Collections.Generic;

namespace CSDocNet.Markdown
{
    public readonly struct MDSettings
    {
        public readonly IReadOnlyList<MDSection> Sections;
        public readonly IReadOnlyList<MDSummarySection> SummarySections;
        public readonly IReadOnlyList<MDMemberSection> MemberSections;
        public readonly IReadOnlyList<MDMemberInfoSection> MemberInfoSections;

        public MDSettings(
            IReadOnlyList<MDSection> sections,
            IReadOnlyList<MDSummarySection> summarySections,
            IReadOnlyList<MDMemberSection> memberSections,
            IReadOnlyList<MDMemberInfoSection> memberInfoSections
        )
        {
            this.Sections = sections;
            this.SummarySections = summarySections;
            this.MemberSections = memberSections;
            this.MemberInfoSections = memberInfoSections;
        }
    }
}