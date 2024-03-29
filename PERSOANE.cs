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

    public partial class PERSOANE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PERSOANE()
        {
            this.CONTRACTE = new HashSet<CONTRACTE>();
        }

        [Display(Name = "ID")]
        public int P_ID { get; set; }
        [Display(Name = "Nume")]
        public string P_NUME { get; set; }
        [Display(Name = "Prenume")]
        public string P_PRENUME { get; set; }
        [Display(Name = "CNP")]
        public string P_CNP { get; set; }
        [Display(Name = "Tip")]
        public Nullable<int> P_ID_TYPE { get; set; }
        [Display(Name = "Data Nasterii")]
        public Nullable<System.DateTime> P_DATA_NASTERII { get; set; }
        [Display(Name = "Adresa")]
        public string P_ADRESA { get; set; }
        [Display(Name = "Judet")]
        public string P_JUDET { get; set; }
        [Display(Name = "Oras")]
        public string P_ORAS { get; set; }
        [Display(Name = "Tel")]
        public string P_TEL { get; set; }
        [Display(Name = "Email")]
        public string P_EMAIL { get; set; }
        [Display(Name = "Numar")]
        public string P_ID_VALUE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTRACTE> CONTRACTE { get; set; }
        public virtual LIBRARIE LIBRARIE { get; set; }
    }
}
