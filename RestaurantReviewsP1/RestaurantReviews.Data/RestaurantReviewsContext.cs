using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data.Models;

namespace RestaurantReviews.Data
{
    //class Class1
    //{
    //    static void Main(string[] args)
    //    {
    //        Restaurant restaurant = new Restaurant(){Name="Subway",Street="2602 E Fletcher Ave",City="Tampa",State="Florida",Country="USA",Zipcode = "33612",Phone = "8139779288",Website = "www.subway.com"};
    //        Console.WriteLine(  "Creating");
    //        RestaurantReviewsContext db = new RestaurantReviewsContext();

    //        db.Restaurants.Add(restaurant);
    //        db.SaveChanges();
    //    }
    //}

    public class RestaurantReviewsContext : DbContext, IDbContext
    {
        public RestaurantReviewsContext() : base("RestaurantReviewsDb")
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Review> Reviews { get; set; }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();

            AddedEntities.ForEach(E =>
            {
                E.Property("Created").CurrentValue = DateTime.Now;
                E.Property("Modified").CurrentValue = DateTime.Now;
            });

            var ModifiedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();

            ModifiedEntities.ForEach(E =>
            {
                E.Property("Modified").CurrentValue = DateTime.Now;
            });
            return base.SaveChanges();
        }
    }
}
