using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviewsLibrary;
using System.Collections.Generic;

namespace RestaurantReviewsTest
{
    [TestClass]
    public class TestSerializeDeserialize
    {
        [TestMethod]
        public void TestSerializeDeserializeTogether()
        {
            List<Restaurant> expected = new List<Restaurant>();
            expected.Add(new Restaurant(1, "Subway", "03 Rusk Lane", "264-535-6950"));
            expected.Add(new Restaurant(2, "Wingstop", "874 Logan Park", "903-610-5694"));
            expected.Add(new Restaurant(3, "Qdoba", "26 Kinsman Crossing", "125-414-8122"));
            string expectedstr = "";
            foreach (var a in expected)
            {
                expectedstr += a.GetRestaurantInfo();
            }

            string serialized = SerializeRestaurants.Serialize(expected);
            List<Restaurant> result = DeserializeRestaurants.Deserialize(serialized);
            string resultstr = "";
            foreach (var b in result)
            {
                resultstr += b.GetRestaurantInfo();
            }

            Assert.AreEqual(expectedstr, resultstr);
        }

        [TestMethod]
        public void TestSerialize()
        {
            List<Restaurant> list = new List<Restaurant>();
            list.Add(new Restaurant(1, "Subway", "03 Rusk Lane", "264-535-6950"));
            string expected = "[{\"id\":1,\"name\":\"Subway\",\"address\":\"03 Rusk Lane\",\"phone\":\"264-535-6950\"}]";

            string serialized = SerializeRestaurants.Serialize(list);

            Assert.AreEqual(expected, serialized);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            List<Restaurant> expected = new List<Restaurant>();
            expected.Add(new Restaurant(1, "Subway", "03 Rusk Lane", "264-535-6950"));

            List<Restaurant> result = DeserializeRestaurants.Deserialize("[{\"id\":1,\"name\":\"Subway\",\"address\":\"03 Rusk Lane\",\"phone\":\"264-535-6950\"}]");

            Assert.AreEqual(expected[0].GetRestaurantInfo(), result[0].GetRestaurantInfo());
        }
    }
}
