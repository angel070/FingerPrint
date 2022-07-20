using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        [Display(Name= "Holiday Name")]
		[Required(ErrorMessage = "This field is required")]
		public string Name { get; set; }
        [DataType(DataType.Date)]
		[Required(ErrorMessage = "This field is required")]
		public DateTime DateFrom { get; set; }
        [DataType(DataType.Date)]
		[Required(ErrorMessage = "This field is required")]
		public DateTime DateTo { get; set; }
    }
}
