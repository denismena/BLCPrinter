using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BLCPrinter.Models
{
    public class IncasariList
    {
        public int C_ID { get; set; }
        public int C_NUMAR { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime C_DATA { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? C_AVANS_DATA { get; set; }
        public decimal? C_AVANS { get; set; }
        public string C_FACTURA { get; set; }
        public string C_CHITANTA { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? C_DATA_DIFERENTA { get; set; }
        public int P_ID { get; set; }
        public string P_NUME { get; set; }
        public string P_PRENUME { get; set; }
        public string P_TEL { get; set; }
    }
}