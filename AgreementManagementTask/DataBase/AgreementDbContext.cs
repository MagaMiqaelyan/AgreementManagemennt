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
            user.PasswordHash = passwordHasher.HashPassword(user, "user111");
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
               });

            builder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   Active = true,
                   Price = 100,
                   ProductGroupId = 2,
                   ProductNumber = 10,
                   ProductDescription = "SampleProductDescription",
               });
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
                   EffectiveDate = DateTime.Now.AddDays(-4),
                   ExpirationDate = DateTime.Now.AddDays(4),
                   NewPrice = 250,
                   ProductPrice = 100,
                   ProductGroupId = 1,
                   ProductId = 1,
                   UserId = user.Id,
               });
        }
    }
}
