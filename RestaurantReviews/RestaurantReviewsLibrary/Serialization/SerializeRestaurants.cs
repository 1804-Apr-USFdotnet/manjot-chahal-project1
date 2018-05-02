using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace RestaurantReviewsLibrary
{
    public class SerializeRestaurants
    {
        public static string Serialize(List<Restaurant> list)
        {
            return JsonConvert.SerializeObject(list);
        }
    }
}
