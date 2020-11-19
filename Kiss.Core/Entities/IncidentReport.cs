
using System;

namespace Kiss.Core.Entities
{
    public class IncidentReport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ReportedOn { get; set; }
        public Contact ReportedBy { get; set; }
    }
}
