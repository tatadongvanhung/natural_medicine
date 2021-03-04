namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class review
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        [StringLength(255)]
        public string user_name { get; set; }

        public byte? rating { get; set; }

        [StringLength(255)]
        public string comment { get; set; }

        public int? product_id { get; set; }

        public DateTime? create_at { get; set; }

        public DateTime? update_at { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
