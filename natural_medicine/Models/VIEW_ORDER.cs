namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VIEW_ORDER
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int user_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(100)]
        public string discount_code { get; set; }

        public int? subtotal { get; set; }

        public int? total { get; set; }

        public DateTime? create_at { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        public int? user_address_id { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        public byte? status_id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string status { get; set; }

        public int? payment_method_id { get; set; }

        [StringLength(255)]
        public string payment_method { get; set; }
    }
}
