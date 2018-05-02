using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsLibrary
{
    public class Review
    {
        public int id;
        public int Rating { get; set; }
        public string review;
        public string user;
        public DateTime date;
        public int Restaurantid { get; set; }

        public Review() { }

        public Review(int id, string user, string review, int rating, DateTime date, int restaurantid)
        {
            this.id = id;
            this.user = user;
            this.review = review;
            this.Rating = rating;
            this.date = date;
            this.Restaurantid = restaurantid;
        }

        public string GetReview()
        {
            //String a = "Name: " + this.reviewer + "\nReview: " + this.review + "\nRating: " + this.rating + "\nDate: " + date;
            //string a = $"ID: {id}\nUser: {user}\nReview: {review}\nRating: {Rating}\nDate {date}\nRestaurantID: {Restaurantid}";
            string a = $"User: {user}\nReview: {review}\nRating: {Rating}\nDate {date}";
            return a;
        }
    }
}
