namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_images
    {
        public int id { get; set; }

        public int? product_id { get; set; }

        [Column(TypeName = "text")]
        public string image_url { get; set; }

        public virtual product product { get; set; }
    }
}
