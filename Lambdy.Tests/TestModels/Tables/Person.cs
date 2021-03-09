using System;

namespace Lambdy.Tests.TestModels.Tables
{
    public class Person
    {
        public Guid Id { get; set; }
        
        public Guid? PersonAddressId { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public int Age { get; set; }
        
        public double? Height { get; set; }
        
        public DateTime BirthDate { get; set; }
        
        public DateTime? LastVisit { get; set; }
        
    }
}