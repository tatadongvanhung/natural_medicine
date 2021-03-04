namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class publisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public publisher()
        {
            imports = new HashSet<import>();
            products = new HashSet<product>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<import> imports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
    }
}
