using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLCPrinter.Models
{
    public class SelectedServiceContract
    {
        public int serviceId { get; set; }
        public string serviceTitle { get; set; }
        public bool selected { get; set; }
    }
}