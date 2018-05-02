using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewsLibrary
{
    public class Restaurant
    {
        [JsonProperty (PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonIgnore]
        public List<Review> reviews;
        int reviewCount;
        [JsonIgnore]
        public double AverageRating { get; set; }

        public Restaurant() { }

        public Restaurant(int id, string name, string address, string phone)
        {
            this.ID = id;
            this.Name = name;
            this.Address = address;
            this.Phone = phone;
            reviews = new List<Review>();
            reviewCount = 0;
            AverageRating = 0;
        }

        public void PopulateReviews(Review review)
        {
            reviews.Add(review);
            AverageRating = (AverageRating * reviewCount + review.Rating) / (reviewCount + 1);
            reviewCount++;
        }

        public string SearchInfo()
        {
            //string a = $"{id}\n{Name}\n{address}\n{phone}\n{AverageRating}";
            string a = $"{Name}\n{Address}\n{Phone}\n{AverageRating.ToString("0.##")}";
            return a;
        }

        public string GetRestaurantInfo()
        {
            //string a = "Name: " + this.name + "\nAddress: " + this.address + "\nPhone: " + this.phone + "\nRating: " + averageRating;
            //string a = $"Name: {name}\nAddress: {address}\nPhone: {phone}\nRating: {averageRating}";
            //string a = $"ID: {id}\nname: {name}\nAddress: {address}\nPhone: {phone}\nRating: {AverageRating}";
            string a = $"Name: {Name}\nAddress: {Address}\nPhone: {Phone}\nRating: {AverageRating.ToString("0.##")}";
            return a;
        }

        public string GetReviews()
        {
            String a = "";
            foreach (Review r in reviews)
            {
                a += r.GetReview() + "\n\n";
            }
            return a;
        }
    }
}
