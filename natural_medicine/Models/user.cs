namespace natural_medicine.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            addresses = new HashSet<address>();
            orders = new HashSet<order>();
            reviews = new HashSet<review>();
        }

        public int id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        public int user_type { get; set; }

        public DateTime? last_login_at { get; set; }

        public DateTime? create_at { get; set; }

        public DateTime? update_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<address> addresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<review> reviews { get; set; }
    }
}
