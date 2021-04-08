using Database.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public interface IEntities : IDisposable
    {
        DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<webpages_Membership> webpages_Membership { get; set; }
        DbSet<webpages_Roles> webpages_Roles { get; set; }

        int SaveChanges();
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
