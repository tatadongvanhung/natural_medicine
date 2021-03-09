namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VIEW_IMPORT_PRODUCT
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? product_id { get; set; }

        public byte? publisher_id { get; set; }

        public int? quantity { get; set; }

        public int? price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? import_date { get; set; }

        public DateTime? create_at { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        [StringLength(100)]
        public string product_name { get; set; }

        [Column(TypeName = "text")]
        public string image_url { get; set; }

        [StringLength(255)]
        public string publisher_name { get; set; }
    }
}
