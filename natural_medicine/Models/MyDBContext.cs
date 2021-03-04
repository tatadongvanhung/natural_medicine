using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace natural_medicine.Models
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext")
        {
        }

        public virtual DbSet<address> addresses { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<discount> discounts { get; set; }
        public virtual DbSet<import> imports { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<orders_detail> orders_detail { get; set; }
        public virtual DbSet<orders_status> orders_status { get; set; }
        public virtual DbSet<payment_methods> payment_methods { get; set; }
        public virtual DbSet<product_images> product_images { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<publisher> publishers { get; set; }
        public virtual DbSet<review> reviews { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<address>()
                .Property(e => e.address1)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.products)
                .WithOptional(e => e.category)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<discount>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .Property(e => e.discount_code)
                .IsUnicode(false);

            modelBuilder.Entity<order>()
                .HasMany(e => e.orders_detail)
                .WithOptional(e => e.order)
                .HasForeignKey(e => e.order_id);

            modelBuilder.Entity<orders_status>()
                .HasMany(e => e.orders)
                .WithOptional(e => e.orders_status)
                .HasForeignKey(e => e.status_id);

            modelBuilder.Entity<payment_methods>()
                .HasMany(e => e.orders)
                .WithOptional(e => e.payment_methods)
                .HasForeignKey(e => e.payment_method_id);

            modelBuilder.Entity<product_images>()
                .Property(e => e.image_url)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.imports)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<product>()
                .HasMany(e => e.orders_detail)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_images)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<product>()
                .HasMany(e => e.reviews)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<publisher>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<publisher>()
                .HasMany(e => e.imports)
                .WithOptional(e => e.publisher)
                .HasForeignKey(e => e.publisher_id);

            modelBuilder.Entity<publisher>()
                .HasMany(e => e.products)
                .WithOptional(e => e.publisher)
                .HasForeignKey(e => e.publisher_id);

            modelBuilder.Entity<user>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.addresses)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.reviews)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);
        }
    }
}
