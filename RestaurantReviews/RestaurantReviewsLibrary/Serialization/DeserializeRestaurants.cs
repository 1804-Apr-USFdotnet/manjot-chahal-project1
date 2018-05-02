using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantReviewsLibrary
{
    //have this implement generic deserialize interface?
    public class DeserializeRestaurants
    {
        public static List<Restaurant> Deserialize()
        {
            //string fileName = System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString() + @"/RestaurantReviewsJSON/restaurantdata.json";
            //var jsonText = File.ReadAllText(@"C:\Users\Manjot\Downloads\revature\chahal-manjot-project0\RestaurantReviews\RestaurantReviewsJSON\restaurantdata.json");

            var jsonText = File.ReadAllText(Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\RestaurantReviewsJSON\restaurantdata.json")));
            var result = JsonConvert.DeserializeObject<List<Restaurant>>(jsonText);
            return result;
            //foreach (var a in result)
            //{
            //    Console.WriteLine(a.getInfo() + "\n");
            //}  
        }

        public static List<Restaurant> Deserialize(string serialized)
        {
            var result = JsonConvert.DeserializeObject<List<Restaurant>>(serialized);
            return result;
        }
    }
}
