using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    //public class APIDbContext : IdentityDbContext<ApplicationUser>
    public class APIDbContext : DbContext
    {
        

        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }
        public DbSet<Department> Departments
        {
            get;
            set;
        }
        public DbSet<Employee> Employees
        {
            get;
            set;
        }

        public virtual DbSet<Userdetails> Userdetails { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { 
                modelBuilder.Entity<Userdetails>(entity => { entity.ToTable("userdetails"); 
                entity.Property(e => e.Email).HasMaxLength(50).IsUnicode(false); 
                entity.Property(e => e.Mobile).HasMaxLength(50).IsUnicode(false); 
                entity.Property(e => e.Name).HasMaxLength(50).IsUnicode(false); 
                entity.Property(e => e.Password).HasMaxLength(50).IsUnicode(false); }); }

    }
}
