using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerPrint.View_Model
{
    public class BranchCountryViewModel
    {
        public Country _country { get; set; }
        public Branch _branch { get; set; }
        public Department _department { get; set; }
        public UserRoles _userRoles { get; set; }
        public IEnumerable<Branch> branch { get; set; }
        public IEnumerable<Department> department { get; set; }
        public IEnumerable<Country> country { get; set; }
        public IEnumerable<UserRoles> userRoles { get; set; }
        public IEnumerable<StaffCheckInAndOutReport> Reports { get; set; }
    }
}