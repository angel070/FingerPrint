using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Models
{
    public class TimeInAndOut
    {
        public int Id { get; set; }
		[Remote("CheckDaysExists", "TimeInAndOut", ErrorMessage = "This day is already exists!")]
		[Required(ErrorMessage = "This field is required")]
		public DayOfWeek Days { get; set; }
        [DataType(DataType.Time)]
		[Required(ErrorMessage = "This field is required")]
		public DateTime TimeIn { get; set; }
        [DataType(DataType.Time)]
		[Required(ErrorMessage = "This field is required")]
		public DateTime TimeOut { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public decimal WorkingHours { get; set; }
		[Display(Name ="Department")]
		public int DepartmentId { get; set; }
		public Department Department { get; set; }
		[Display(Name = "Branch")]
		[Required(ErrorMessage = "This field is required")]
		public int BranchId { get; set;}
		public Branch Branch { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Created Date")]
		public Nullable<DateTime> CreatedDate { get; set; }
		
    }
}
