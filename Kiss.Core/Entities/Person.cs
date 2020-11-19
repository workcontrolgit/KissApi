﻿using System;

namespace Kiss.Core.Entities
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int Age { get; set; }
        public int NumberOfKids { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Contact EmergencyContact { get; set; }

    }
}
