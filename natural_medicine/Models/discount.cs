namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class discount
    {
        public int id { get; set; }

        [StringLength(100)]
        public string code { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public double? value { get; set; }

        public int? max_value { get; set; }

        [Column(TypeName = "date")]
        public DateTime? start_date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? end_date { get; set; }

        public DateTime? create_at { get; set; }
    }
}
