namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            orders_detail = new HashSet<orders_detail>();
        }

        public int id { get; set; }

        public int user_id { get; set; }

        public int? payment_method_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(100)]
        public string discount_code { get; set; }

        public int? subtotal { get; set; }

        public int? total { get; set; }

        public DateTime? create_at { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        public byte? status_id { get; set; }

        public int? user_address_id { get; set; }

        public virtual address address1 { get; set; }

        public virtual payment_methods payment_methods { get; set; }

        public virtual orders_status orders_status { get; set; }

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders_detail> orders_detail { get; set; }
    }
}
