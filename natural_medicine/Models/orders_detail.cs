namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class orders_detail
    {
        public int id { get; set; }

        public int? order_id { get; set; }

        public int? product_id { get; set; }

        public int? price { get; set; }

        public int? quantity { get; set; }

        public virtual order order { get; set; }

        public virtual product product { get; set; }
    }
}
