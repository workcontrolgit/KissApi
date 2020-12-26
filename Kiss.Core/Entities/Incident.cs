
using System;

namespace Kiss.Core.Entities
{
    public class Incident
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime ReportedOn { get; set; }
        public Contact ReportedBy { get; set; }
    }
}
