using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestaurantReviewsData;

namespace RestaurantReviewsLibrary.Repositories
{
    public class RestaurantRepository : IRestaurantRepository<Restaurant>
    {
        private RestaurantReviewsEntities db;

        public RestaurantRepository()
        {
            db = new RestaurantReviewsEntities();
        }

        public RestaurantRepository(RestaurantReviewsEntities context)
        {
            db = context;
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            IEnumerable<Restaurant> result;
                var dataList = db.Restaurants.ToList();
                result = dataList.Select(x => DataToLibrary(x)).ToList();
            return result;
        }

        public IEnumerable<Restaurant> GetRestaurantsAlphabetical()
        {
            IEnumerable<Restaurant> result;
                var dataList = db.Restaurants.OrderBy(x => x.name).ToList();
                result = dataList.Select(x => DataToLibrary(x)).ToList();
            return result;
        }

        public IEnumerable<Restaurant> GetRestaurantsByRating()
        {
            IEnumerable<Restaurant> result;
                var dataList = db.Restaurants.ToList();
                result = dataList.Select(x => DataToLibrary(x)).OrderByDescending(x => x.AverageRating).ToList();
            return result;
        }

        public IEnumerable<Restaurant> GetTop3RestaurantsByRating()
        {
            IEnumerable<Restaurant> result;
                var dataList = db.Restaurants.ToList();
                result = dataList.Select(x => DataToLibrary(x)).OrderByDescending(x => x.AverageRating).Take(3).ToList();
            return result;
        }

        public Restaurant GetDetails(string a)
        {
            Restaurant result;
                var rest = db.Restaurants.SingleOrDefault(x => x.name == a);
                if (rest != null)
                {
                    result = DataToLibrary(rest);
                    return result;
                }
                return null;
        }

        public IEnumerable<Review> GetReviews(string a)
        {
            IEnumerable<Review> result;
                var rest = db.Restaurants.SingleOrDefault(x => x.name == a);
                if (rest != null)
                {
                    var rev = rest.Reviews.ToList();
                    result = rev.Select(x => DataToLibrary(x)).ToList();
                    return result;
                }
                return null;
        }

        public void AddRestaurant(string name, string address, string phone)
        {
                var rest = new Restaurant()
                {
                    Name = name,
                    Address = address,
                    Phone = phone
                };

                db.Restaurants.Add(LibraryToData(rest));
                db.SaveChanges();
        }

        public void AddReview(string restname, int rating, string comment, string user)
        {
            var rest = db.Restaurants.SingleOrDefault(x => x.name == restname);
            if (rest != null)
            {
                var rev = new Review()
                {
                    Rating = rating,
                    review = comment,
                    user = user,
                    date = DateTime.Now
                };
                var datarev = LibraryToData(rev);
                datarev.Restaurant = rest;
                datarev.restaurantid = rest.id;
                db.Reviews.Add(datarev);
                db.SaveChanges();
            }
        }

        public void RemoveRestaurant(string name)
        {
                var rest = db.Restaurants.SingleOrDefault(x => x.name == name);
                if (rest != null)
                {
                    int id = rest.id;
                    db.Reviews.RemoveRange(db.Reviews.Where(x => x.restaurantid == id));
                    db.Restaurants.Remove(rest);
                    db.SaveChanges();
                }
        }

        public void RemoveReview(string restname, string user, int rating)
        {
                var rest = db.Restaurants.SingleOrDefault(x => x.name == restname);
                int id = rest.id;
                var rev = db.Reviews.SingleOrDefault(x => x.user == user && x.rating == rating && x.restaurantid == id);
                if (rev != null)
                {
                    db.Reviews.Remove(rev);
                    db.SaveChanges();
                }
        }

        public void UpdateRestaurant(string name, string update)
        {
                var rest = db.Restaurants.SingleOrDefault(x => x.name == name);
                if (rest != null)
                {
                    rest.name = update;
                    db.SaveChanges();
                }
        }

        public IEnumerable<Restaurant> Search(string search)
        {
            IEnumerable<Restaurant> result;
                var dataList = db.Restaurants.Where(x => x.name.Contains(search) || x.address.Contains(search) || x.phone.Contains(search)).ToList();
                result = dataList.Select(x => DataToLibrary(x)).ToList();
            return result;
        }

        public static Restaurant DataToLibrary(RestaurantReviewsData.Restaurant dataModel)
        {
            double rating = 0;
            using (var db = new RestaurantReviewsEntities())
            {
                var reviews = db.Reviews.Where(r => r.restaurantid == dataModel.id);
                if (reviews.Count() != 0)
                    rating = reviews.Average(r => r.rating);
            }

            var libModel = new Restaurant()
            {
                ID = dataModel.id,
                Name = dataModel.name,
                Address = dataModel.address,
                Phone = dataModel.phone,
                AverageRating = rating
                //reviews = (List<Review>) dataModel.Reviews
            };
            return libModel;
        }

        public static RestaurantReviewsData.Restaurant LibraryToData(Restaurant libModel)
        {
            var dataModel = new RestaurantReviewsData.Restaurant()
            {
                name = libModel.Name,
                address = libModel.Address,
                phone = libModel.Phone
            };
            return dataModel;
        }

        public static Review DataToLibrary(RestaurantReviewsData.Review dataModel)
        {
            var libModel = new Review()
            {
                id = dataModel.id,
                Rating = dataModel.rating,
                review = dataModel.review,
                user = dataModel.user,
                date = dataModel.date,
                Restaurantid = dataModel.restaurantid
            };
            return libModel;
        }

        public static RestaurantReviewsData.Review LibraryToData(Review libModel)
        {
            var dataModel = new RestaurantReviewsData.Review()
            {
                rating = libModel.Rating,
                review = libModel.review,
                user = libModel.user,
                date = libModel.date
            };
            return dataModel;
        }
    }
}
