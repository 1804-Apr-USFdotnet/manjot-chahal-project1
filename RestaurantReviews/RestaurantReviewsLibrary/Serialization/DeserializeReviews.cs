using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantReviewsLibrary
{
    //have this implement generic deserialization interface?
    public class DeserializeReviews
    {
        public static List<Review> Deserialize()
        {
            //string fileName = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString() + @"/RestaurantReviewsJSON/restaurantdata.json";
            //var jsonText = File.ReadAllText(@"C:\Users\Manjot\Downloads\revature\chahal-manjot-project0\RestaurantReviews\RestaurantReviewsJSON\restaurantdata.json");
            var jsonText = File.ReadAllText(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\RestaurantReviewsJSON\reviewdata.json")));
            var result = JsonConvert.DeserializeObject<List<Review>>(jsonText);
            return result;
            //foreach (var a in result)
            //{
            //    Console.WriteLine(a.getReview()+"\n");
            //}
        }
    }
}
