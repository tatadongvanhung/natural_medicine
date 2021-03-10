namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VIEW_ORDER_DETAIL
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? order_id { get; set; }

        public int? product_id { get; set; }

        public int? price { get; set; }

        public int? quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? create_at { get; set; }

        [Column(TypeName = "text")]
        public string image_url { get; set; }

        [StringLength(100)]
        public string name { get; set; }
    }
}
