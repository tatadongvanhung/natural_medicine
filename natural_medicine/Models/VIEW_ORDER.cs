//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class VIEW_ORDER
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string discount_code { get; set; }
        public Nullable<int> subtotal { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<System.DateTime> create_at { get; set; }
        public string note { get; set; }
        public Nullable<int> user_address_id { get; set; }
        public string address { get; set; }
        public Nullable<byte> status_id { get; set; }
        public string status { get; set; }
        public Nullable<int> payment_method_id { get; set; }
        public string payment_method { get; set; }
    }
}
