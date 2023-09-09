using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Domain.Models.Contacts;
using PhoneBook.Domain.Models.User;
using System.Reflection;

namespace PhoneBook.Infrastructure.EFCore.Context
{
    public class PhoneBookDbContext : IdentityDbContext<ApplicationUser>
    {

        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {

        }

        #region contact
        public DbSet<Contact> Contacts { get; set; }
        #endregion


        #region on model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
          
            base.OnModelCreating(modelBuilder);
            #region ChangeTableName(Identity)
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            #endregion
        }
        #endregion
    }
}