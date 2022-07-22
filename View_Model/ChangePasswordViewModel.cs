using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.View_Model
{
	public class ChangePasswordViewModel
	{
		[Display(Name ="Old Password")]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }
		[Display(Name = "New Password")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }
		[Display(Name = "Re-Enter Password")]
		[DataType(DataType.Password)]
		public string reEnterPassword { get; set; }
	}
}
