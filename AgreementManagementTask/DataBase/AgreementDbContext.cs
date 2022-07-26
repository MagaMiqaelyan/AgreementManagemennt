using AgreementManagementTask.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AgreementManagementTask.DataBase
{
    public class AgreementDbContext : IdentityDbContext
    {
        public AgreementDbContext(DbContextOptions<AgreementDbContext> options)
          : base(options)
        {
        }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<User> AgreementUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            var user = new User()
            {
                Id = 1,
                UserName = "Admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123");


            builder.Entity<User>().HasData(user);

            builder.Entity<ProductGroup>(productgroup =>
            {
                productgroup.HasIndex(x => x.GroupCode).IsUnique(true);
            });


            builder.Entity<Agreement>()
                   .HasOne(b => b.Product)
                   .WithMany(a => a.Agreements)
                   .HasForeignKey(f => f.ProductId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<ProductGroup>()
                .HasIndex(x => x.GroupCode)
                .IsUnique();


            builder.Entity<Product>()
                .HasIndex(x => x.ProductNumber)
                .IsUnique();



            builder.Entity<ProductGroup>().HasData(
                 new ProductGroup
                 {
                     Id = 1,
                     Active = true,
                     GroupCode = 2,
                     GroupDescription = "test1",
                 },
                 new ProductGroup
                 {
                     Id = 2,
                     Active = true,
                     GroupCode = 3,
                     GroupDescription = "test3",
                 },
                 new ProductGroup
                 {
                     Id = 3,
                     Active = false,
                     GroupCode = 4,
                     GroupDescription = "test3",
                 });

            builder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Active = true,
                   Price = 300,
                   ProductGroupId = 2,
                   ProductNumber = 12,
                   ProductDescription = "TestProductDescription",
               });

            builder.Entity<Agreement>().HasData(
               new Agreement
               {
                   Id = 1,
                   EffectiveDate = DateTime.Now,
                   ExpirationDate = DateTime.Now.AddDays(2),
                   NewPrice = 200,
                   ProductPrice = 300,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,

               }, new Agreement
               {
                   Id = 2,
                   EffectiveDate = DateTime.Now,
                   ExpirationDate = DateTime.Now.AddDays(2),
                   NewPrice = 200,
                   ProductPrice = 300,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,
               });
        }
    }
}
