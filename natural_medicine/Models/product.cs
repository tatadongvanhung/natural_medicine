namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            imports = new HashSet<import>();
            orders_detail = new HashSet<orders_detail>();
            product_images = new HashSet<product_images>();
            reviews = new HashSet<review>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(100)]
        public string ingredient { get; set; }

        [StringLength(100)]
        public string uses { get; set; }

        [StringLength(100)]
        public string dosage { get; set; }

        [StringLength(100)]
        public string unit { get; set; }

        public int? price { get; set; }

        public int? inventory_quantity { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [Column(TypeName = "text")]
        public string image_url { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        public byte? category_id { get; set; }

        public byte? publisher_id { get; set; }

        public virtual category category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<import> imports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders_detail> orders_detail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_images> product_images { get; set; }

        public virtual publisher publisher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<review> reviews { get; set; }
    }
}
