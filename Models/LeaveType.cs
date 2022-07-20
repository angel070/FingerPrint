using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
	public class LeaveType
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "This field is required")]
		public string Type { get; set; }
		[Required(ErrorMessage = "This field is required")]
		[Display(Name ="Short term")]
		public string ShortType { get; set; }
	}
}
