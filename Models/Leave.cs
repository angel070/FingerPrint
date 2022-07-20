using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
	public class Leave
	{
		public int Id { get; set; }
		public double TotalDays { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[DataType (DataType.Date)]
		public DateTime DateFrom { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[DataType (DataType.Date)]
		public DateTime DateTo { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public string StaffId { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[Display(Name = "Leave Name")]
		public int LeaveTypeId { get; set; }
		public LeaveType LeaveType { get; set; }
	}
}
