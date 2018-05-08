using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantReviews.Data;
using RestaurantReviews.Library.Models;
using RestaurantReviews.Library.Repositories;

namespace RestaurantReviews.Library
{
    public class Service
    {
        private RestaurantReviewsContext db;
        private RestaurantRepository restrepo;
        private ReviewRepository revrepo;

        public Service()
        {
            db = new RestaurantReviewsContext();
            restrepo = new RestaurantRepository(db);
            revrepo = new ReviewRepository(db);
        }

        public Restaurant GetRestaurantById(object id)
        {
            return restrepo.GetById(id);
        }

        public void InsertRestaurant(Restaurant entity)
        {
            restrepo.Insert(entity);
        }

        public void UpdateRestaurant(Restaurant entity)
        {
            restrepo.Update(entity);
        }

        public void DeleteRestaurant(Restaurant entity)
        {
            restrepo.Delete(entity);
        }

        public virtual IEnumerable<Restaurant> RestaurantTable()
        {
            return restrepo.Table;
        }

        public Review GetReviewById(object id)
        {
            return revrepo.GetById(id);
        }

        public void InsertReview(Review entity)
        {
            revrepo.Insert(entity);
        }

        public void UpdateReview(Review entity)
        {
            revrepo.Update(entity);
        }

        public void DeleteReview(Review entity)
        {
            revrepo.Delete(entity);
        }

        public virtual IEnumerable<Review> ReviewTable()
        {
            return revrepo.Table;
        }
    }
}
