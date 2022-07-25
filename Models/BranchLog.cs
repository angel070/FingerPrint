using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
	public class BranchLog
	{
		public string Branch { get; set; }
		public int Id { get; set; }
		public int StaffPresent { get; set; }
		public int StaffAbsent { get; set; }
	}
}
