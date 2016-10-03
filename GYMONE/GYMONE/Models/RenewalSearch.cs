using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYMONE.Models
{
    public class RenewalSearch
    {
        public string SearchButton { get; set; }
        public string RenewalSearchID { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public string Exactdate { get; set; }
    }
}