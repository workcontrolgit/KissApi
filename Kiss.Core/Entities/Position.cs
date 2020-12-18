using System;

namespace Kiss.Core.Entities
{
    public class Position
    {
        public Guid Id { get; set; }
        public string PositionNumber { get; set; }
        public string Email { get; set; }
        public string ReportsToPositionNumber { get; set; }
        public string FullName { get; set; }
        public string OfficePhone { get; set; }
        public string Bureau { get; set; }
        public string OrgAbbreviation { get; set; }
        public string OrgCode { get; set; }
        public string PositionTitle { get; set; }
        public string PositionPayPlan { get; set; }
        public string PositionGrade { get; set; }
        public string PositionSeries { get; set; }

    }
}
