namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class import
    {
        public int id { get; set; }

        public int? product_id { get; set; }

        public byte? publisher_id { get; set; }

        public int? quantity { get; set; }

        public int? price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? import_date { get; set; }

        public DateTime? create_at { get; set; }

        [StringLength(1)]
        public string note { get; set; }

        public virtual product product { get; set; }

        public virtual publisher publisher { get; set; }
    }
}
