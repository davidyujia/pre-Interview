using System;
using System.Collections.Generic;

namespace EmployeeTaskManagement.Models
{
    public class EmployeeViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime HiredDate { get; set; }
        
        public IList<EmployeeTaskViewModel> Tasks { get; set; }
    }

    public class EmployeeTaskViewModel
    {
        public string TaskName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime Deadline { get; set; }
    }
}