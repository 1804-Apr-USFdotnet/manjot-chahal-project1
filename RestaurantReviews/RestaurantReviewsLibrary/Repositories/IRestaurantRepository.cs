using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsLibrary.Repositories
{
    public interface IRestaurantRepository<T>
    {
        IEnumerable<T> GetRestaurants();
        IEnumerable<T> GetRestaurantsAlphabetical();
        IEnumerable<T> GetRestaurantsByRating();
        IEnumerable<T> GetTop3RestaurantsByRating();
        T GetDetails(string a);
        void AddRestaurant(string name, string address, string phone);
        void AddReview(string restname, int rating, string comment, string user);
        void RemoveRestaurant(string name);
        void RemoveReview(string restname, string user, int rating);
        void UpdateRestaurant(string name, string update);
        IEnumerable<T> Search(string search);

    }
}
