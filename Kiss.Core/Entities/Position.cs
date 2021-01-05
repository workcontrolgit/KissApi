using System;

namespace Kiss.Core.Entities
{
    public class Position
    {
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

    public class PositionExport
    {
        public PositionNumber PositionNumber;
        public Email Email;
        public ReportsToPositionNumber ReportsToPositionNumber;
        public FullName FullName;
        public OfficePhone OfficePhone;
        public Bureau Bureau;
        public OrgAbbreviation OrgAbbreviation;
        public OrgCode OrgCode;
        public PositionTitle PositionTitle;
        public PositionPayPlan PositionPayPlan;
        public PositionGrade PositionGrade;
        public PositionSeries PositionSeries;
    }
    public class Email
    {
        public string value;
        public string name;
    }
    public class PositionNumber
    {
        public string value;
        public string name;
    }
    public class ReportsToPositionNumber
    {
        public string value;
        public string name;
    }

    public class FullName
    {
        public string value;
        public string name;
    }
    public class OfficePhone
    {
        public string value;
        public string name;
    }
    public class Bureau
    {
        public string value;
        public string name;
    }
    public class OrgAbbreviation
    {
        public string value;
        public string name;
    }
    public class OrgCode
    {
        public string value;
        public string name;
    }
    public class PositionTitle
    {
        public string value;
        public string name;
    }
    public class PositionPayPlan
    {
        public string value;
        public string name;
    }
    public class PositionGrade
    {
        public string value;
        public string name;
    }
    public class PositionSeries
    {
        public string value;
        public string name;
    }
}
