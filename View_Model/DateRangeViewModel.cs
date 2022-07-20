using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.View_Model
{
    public class DateRangeViewModel
    {
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
		[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
		[DataType(DataType.Date)]
        public DateTime DateTo { get; set; }
        public string Name { get; set; }
		public string Staff_Id { get; set;}
		public string Branch { get; set;}
		public string Department { get; set;}

    }
}
