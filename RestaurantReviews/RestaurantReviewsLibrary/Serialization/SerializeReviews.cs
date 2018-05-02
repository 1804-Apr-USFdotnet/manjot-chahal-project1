using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestaurantReviewsLibrary.Serialization
{
    class SerializeReviews
    {
        public static string Serialize(List<Review> list)
        {
            return JsonConvert.SerializeObject(list);
        }
    }
}
