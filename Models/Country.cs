using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FingerPrint.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required (ErrorMessage="Country is required" )]
        public string Name { get; set; }



    }
}