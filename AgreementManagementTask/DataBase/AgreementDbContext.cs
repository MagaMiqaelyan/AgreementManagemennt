using AgreementManagementTask.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AgreementManagementTask.DataBase
{
    public class AgreementDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
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

            var user = SeedUser();

            builder.Entity<User>().HasData(user);

            builder.Entity<ProductGroup>(productgroup => productgroup.HasIndex(x => x.GroupCode).IsUnique(true));

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

            SeedProduct(builder);
            SeedAgreement(builder, user);
        }

        private User SeedUser()
        {
            var user = new User()
            {
                Id = 1,
                UserName = "UserSample",
                Email = "user@gmail.com",
                LockoutEnabled = false, 
                PhoneNumber = "+37498780680"
            };

            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "3yu5?9RCqQfH!-M");
            return user;
        }
        private void SeedProduct(ModelBuilder builder)
        {
            builder.Entity<ProductGroup>().HasData(
               new ProductGroup
               {
                   Id = 1, 
                   Active = true,
                   GroupCode = 5,
                   GroupDescription = "groupOne",
               },
               new ProductGroup
               {
                   Id = 2,
                   Active = true,
                   GroupCode = 6,
                   GroupDescription = "groupTwo",
               },
               new ProductGroup
               {
                   Id = 3,
                   Active = true,
                   GroupCode = 8,
                   GroupDescription = "groupThree",
               });

            builder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Active = true,
                   Price = 100,
                   ProductGroupId = 2,
                   ProductNumber = 10,
                   ProductDescription = "First Product Description",
               },
               new Product
               {
                   Id = 2,
                   Active = true,
                   Price = 350,
                   ProductGroupId = 3,
                   ProductNumber = 23,
                   ProductDescription = "Second Product Description",
               }
               );
        }

        private void SeedAgreement(ModelBuilder builder, User user)
        {
            builder.Entity<Agreement>().HasData(
               new Agreement
               {
                   Id = 1,
                   EffectiveDate = DateTime.Now.AddDays(-4),
                   ExpirationDate = DateTime.Now.AddDays(4),
                   NewPrice = 150,
                   ProductPrice = 100,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,

               },
               new Agreement
               {
                   Id = 2,
                   EffectiveDate = DateTime.Now.AddDays(-2),
                   ExpirationDate = DateTime.Now.AddDays(4),
                   NewPrice = 250,
                   ProductPrice = 100,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,
               },
               new Agreement
               {
                   Id = 3,
                   EffectiveDate = DateTime.Now.AddDays(-1),
                   ExpirationDate = DateTime.Now.AddDays(5),
                   NewPrice = 350,
                   ProductPrice = 500,
                   ProductGroupId = 3,
                   ProductId = 1,
                   UserId = user.Id,

               },
               new Agreement
               {
                   Id = 4,
                   EffectiveDate = DateTime.Now,
                   ExpirationDate = DateTime.Now.AddDays(3),
                   NewPrice = 550,
                   ProductPrice = 600,
                   ProductGroupId = 2,
                   ProductId = 1,
                   UserId = user.Id,
               },
               new Agreement
               {
                   Id = 5,
                   EffectiveDate = DateTime.Now,
                   ExpirationDate = DateTime.Now.AddDays(7),
                   NewPrice = 160,
                   ProductPrice = 170,
                   ProductGroupId = 3,
                   ProductId = 1,
                   UserId = user.Id,

               },
               new Agreement
               {
                   Id = 6,
                   EffectiveDate = DateTime.Now.AddDays(-2),
                   ExpirationDate = DateTime.Now.AddDays(10),
                   NewPrice = 850,
                   ProductPrice = 340,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,
               });
        }
    }
}
