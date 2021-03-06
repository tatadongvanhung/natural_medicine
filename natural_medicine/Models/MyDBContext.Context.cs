﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace natural_medicine.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<discount> discounts { get; set; }
        public virtual DbSet<import> imports { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<orders_detail> orders_detail { get; set; }
        public virtual DbSet<orders_status> orders_status { get; set; }
        public virtual DbSet<payment_methods> payment_methods { get; set; }
        public virtual DbSet<product_images> product_images { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<publisher> publishers { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<VIEW_IMPORT_PRODUCT> VIEW_IMPORT_PRODUCT { get; set; }
        public virtual DbSet<VIEW_ORDER> VIEW_ORDER { get; set; }
        public virtual DbSet<VIEW_ORDER_DETAIL> VIEW_ORDER_DETAIL { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<news> news { get; set; }
    
        public virtual ObjectResult<IMPORTS_REPORT_Result> IMPORTS_REPORT(Nullable<System.DateTime> start_date, Nullable<System.DateTime> end_date)
        {
            var start_dateParameter = start_date.HasValue ?
                new ObjectParameter("start_date", start_date) :
                new ObjectParameter("start_date", typeof(System.DateTime));
    
            var end_dateParameter = end_date.HasValue ?
                new ObjectParameter("end_date", end_date) :
                new ObjectParameter("end_date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<IMPORTS_REPORT_Result>("IMPORTS_REPORT", start_dateParameter, end_dateParameter);
        }
    
        public virtual ObjectResult<EXPORTS_REPORT_Result> EXPORTS_REPORT(Nullable<System.DateTime> start_date, Nullable<System.DateTime> end_date)
        {
            var start_dateParameter = start_date.HasValue ?
                new ObjectParameter("start_date", start_date) :
                new ObjectParameter("start_date", typeof(System.DateTime));
    
            var end_dateParameter = end_date.HasValue ?
                new ObjectParameter("end_date", end_date) :
                new ObjectParameter("end_date", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EXPORTS_REPORT_Result>("EXPORTS_REPORT", start_dateParameter, end_dateParameter);
        }
    }
}
