using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    public class MonthwiseModel
    {
        public string MonthID { get; set; }
        public IEnumerable<SelectListItem> MonthNameList { get; set; }
    }
}