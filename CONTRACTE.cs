//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BLCPrinter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CONTRACTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONTRACTE()
        {
            this.SERVICII_CONTRACT = new HashSet<SERVICII_CONTRACT>();
        }

        [Key]
        [Display(Name = "ID")]
        public int C_ID { get; set; }

        [Required(ErrorMessage = "Va rugam completati numarul contractului!")]
        [Display(Name = "Numar")]
        public int C_NUMAR { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public System.DateTime C_DATA { get; set; }

        [Required(ErrorMessage = "Va rugam completati numele clientului!")]
        [Display(Name = "Client")]
        public int C_PERSOANA_ID { get; set; }

        [Required(ErrorMessage = "Va rugam completati numarul de persoane!")]
        [Display(Name = "Nr. persoane")]
        public Nullable<int> C_NR_PERS { get; set; }

        [Display(Name = "Nr. adulti")]
        public Nullable<int> C_NR_ADULTI { get; set; }

        [Display(Name = "Nr. copii")]
        public Nullable<int> C_NR_COPII { get; set; }

        [Display(Name = "Tara")]
        public string C_TARA { get; set; }

        [Display(Name = "Oras")]
        public string C_ORAS { get; set; }

        [Display(Name = "De la data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> C_DE_LA_DATA { get; set; }

        [Display(Name = "Pana la data")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> C_PANA_LA_DATA { get; set; }

        [Display(Name = "Hotel")]
        public string C_HOTEL { get; set; }

        [Display(Name = "Nr. stele")]
        public Nullable<int> C_HOTEL_STELE { get; set; }

        [Display(Name = "Mentiuni")]
        public string C_MENTIUNI { get; set; }

        [Display(Name = "Pret")]
        public Nullable<decimal> C_PRET { get; set; }

        [Display(Name = "Moneda")]
        public Nullable<int> C_MONEDA { get; set; }

        [Display(Name = "Incasat")]
        public Nullable<decimal> C_AVANS { get; set; }

        [Display(Name = "Data diferenta")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> C_DATA_DIFERENTA { get; set; }

        [Display(Name = "Factura")]
        public string C_FACTURA { get; set; }

        [Display(Name = "Chitanta")]
        public string C_CHITANTA { get; set; }

        [Display(Name = "Data")]
        public Nullable<System.DateTime> C_AVANS_DATA { get; set; }

        [Display(Name = "Data")]
        public Nullable<System.DateTime> C_AVANS2_DATA { get; set; }

        [Display(Name = "Incasat")]
        public Nullable<decimal> C_AVANS2 { get; set; }

        [Display(Name = "Factura")]
        public string C_FACTURA2 { get; set; }

        [Display(Name = "Chitanta")]
        public string C_CHITANTA2 { get; set; }

        [Display(Name = "Data diferenta")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> C_DATA_DIFERENTA2 { get; set; }

        [Display(Name = "Data")]
        public Nullable<System.DateTime> C_AVANS3_DATA { get; set; }

        [Display(Name = "Incasat")]
        public Nullable<decimal> C_AVANS3 { get; set; }

        [Display(Name = "Factura")]
        public string C_FACTURA3 { get; set; }

        [Display(Name = "Chitanta")]
        public string C_CHITANTA3 { get; set; }

        [Display(Name = "Data diferenta")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public Nullable<System.DateTime> C_DATA_DIFERENTA3 { get; set; }

        public Nullable<decimal> REST { get { return C_PRET - ((C_AVANS ?? 0) + (C_AVANS2 ?? 0) + (C_AVANS3 ?? 0)); } }


        public virtual LIBRARIE LIBRARIE { get; set; }
        public virtual PERSOANE PERSOANE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICII_CONTRACT> SERVICII_CONTRACT { get; set; }
    }
}
