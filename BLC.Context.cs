﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BLCEntities : DbContext
    {
        public BLCEntities()
            : base("name=BLCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CONTRACTE> CONTRACTEs { get; set; }
        public virtual DbSet<LIBRARIE> LIBRARIEs { get; set; }
        public virtual DbSet<PERSOANE> PERSOANEs { get; set; }
        public virtual DbSet<SERVICII_CONTRACT> SERVICII_CONTRACT { get; set; }
        public virtual DbSet<Utilizatori> UTILIZATORIs { get; set; }
        public virtual DbSet<Incasari> Incasaris { get; set; }
    }
}
