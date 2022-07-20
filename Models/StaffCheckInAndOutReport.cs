using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class StaffCheckInAndOutReport
    {
        public int Id { get; set; }
        public string StaffId { get; set; }
	
        public string firstName { get; set; }
        public string LastName { get; set; }
        public int Department { get; set; }
        public string DepartmentStr { get; set; }
        public int Branch { get; set; }
        public string BranchStr { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat( DataFormatString = "{0:HH:mm}")]
        public Nullable<DateTime> TimeIn { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public Nullable<DateTime> TimeOut { get; set; }

        public string status { get; set;}
        public Nullable<TimeSpan> TotalStaffHours{ get; set; }  
        public Nullable<decimal> PercentageHours{ get; set; }  
        public Nullable<decimal> OverTimeHours{ get; set; }  
         
    }
}
