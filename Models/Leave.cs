using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations.Schema;

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
		[Remote("CheckStaffIdExists", "Leave", AdditionalFields = "StaffId_clone", ErrorMessage = "Id does not exists in the context!", HttpMethod = "POST")]
		public string StaffId { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[Display(Name = "Leave Name")]
		public int LeaveTypeId { get; set; }
		public LeaveType LeaveType { get; set; }
		public string Name { get; set; }

	}
}
