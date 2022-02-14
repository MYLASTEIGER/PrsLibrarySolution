using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrsLibrary.Models {
    public class PrsDbContext : DbContext {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }


        public PrsDbContext() { }
        public PrsDbContext(DbContextOptions<PrsDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if (!builder.IsConfigured) {
                builder.UseSqlServer("server=localhost\\sqlexpress;database=TestPrsDb;trusted_connection=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            //unique value identifier  makes username in User unique, can reuse exact formula for other unique properties, and just need to
            //change the User and Username names

            builder.Entity<User>(e => {
                e.HasIndex(p => p.Username).IsUnique(true);
            });

            builder.Entity<Vendor>(e => {
                e.HasIndex(p => p.Code).IsUnique(true);
            });
            builder.Entity<Product>(e => {
                e.HasIndex(p => p.PartNbr).IsUnique(true);
            });

        }
    }
}
