namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("address")]
    public partial class address
    {
        public int id { get; set; }

        public int? user_id { get; set; }

        [Column("address", TypeName = "text")]
        public string address1 { get; set; }

        public DateTime? create_at { get; set; }

        public DateTime? update_at { get; set; }

        public virtual user user { get; set; }
    }
}
