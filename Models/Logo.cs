using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
	public class Logo
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string FileName { get; set; }
		[Required]
		public string FilePath { get; set; }
	}
}
