using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantReviewsLibrary;
using System.Collections.Generic;
using RestaurantReviewsLibrary.Repositories;
using Moq;
using System.Data.Entity;
using System.Linq;
using RestaurantReviewsData;
using Restaurant = RestaurantReviewsData.Restaurant;
using Review = RestaurantReviewsData.Review;

namespace RestaurantReviewsTest
{
    [TestClass]
    public class TestRestaurantRepository
    {
        //[TestMethod]
        //public void GetRestaurantsByName()
        //{
        //    mockRepo.Setup(x => x.GetRestaurantsAlphabetical()).Returns(rests.ToString());
        //    mockRepo.Object.GetRestaurantsAlphabetical();
        //}

        //[TestMethod]
        //public void GetRestaurantDetailsRepo()
        //{
        //    var rest = new Restaurant { name = "Subway" };
        //    var mockRepo = new Mock<IRestaurantRepository<Restaurant>>();
        //    mockRepo.Setup(x => x.GetDetails(rest.name)).Returns(new Restaurant());
        //    mockRepo.Setup(x => x.GetDetails(rest.name)).Returns(rest);
        //    var rest = mockRepo
        //    //mockRepo.Verify(x => x.GetDetails(rest.name), Times.Once);
        //    //mockRepo.Object.GetRestaurantsAlphabetical();

        //    //var employee = new Employee { EmployeeID = 1, Code = "Code", FirstName = "MyFirstName", LastName = "MyName" };

        //    //var employeeRepository = new Mock<IEmployeeRepository>();
        //    //employeeRepository.Setup(x => x.Add(employee)).Verifiable();

        //    var employeeService = new EmployeeService(employeeRepository.Object);
        //    var empl = employeeService.GetById(1);

        //    Assert.IsNotNull(empl);
        //}

        //[TestMethod]
        //public void AddRestaurant()
        //{
        //    var mockSet = new Mock<DbSet<RestaurantReviewsData.Restaurant>>();

        //    var mockContext = new Mock<RestaurantReviewsEntities>();
        //    mockContext.Setup(m => m.Restaurants).Returns(mockSet.Object);

        //    var service = new RestaurantRepository(mockContext.Object);
        //    service.AddRestaurant("Subway", "123 That Way", "1233453423");

        //    mockSet.Verify(m => m.Add(It.IsAny<Restaurant>()), Times.Once());
        //    mockContext.Verify(m => m.SaveChanges(), Times.Once());
        //}

        [TestMethod]
        public void GetRestaurantsByName()
        {
            var data = new List<Restaurant>
            {
                new Restaurant {name = "Subway"},
                new Restaurant {name = "Qdoba"},
                new Restaurant {name = "Wingstop"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<RestaurantReviewsData.Restaurant>>();
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RestaurantReviewsEntities>();
            mockContext.Setup(c => c.Restaurants).Returns(mockSet.Object);

            var service = new RestaurantRepository(mockContext.Object);
            var rests = service.GetRestaurantsAlphabetical();

            //mockSet.Verify(m => m.Add(It.IsAny<Restaurant>()), Times.Once());
            //mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(3, rests.Count());
            Assert.AreEqual("Qdoba", rests.ElementAt(0).Name);
            Assert.AreEqual("Subway", rests.ElementAt(1).Name);
            Assert.AreEqual("Wingstop", rests.ElementAt(2).Name);
        }

        [TestMethod]
        public void GetRestaurantDetails()
        {
            var data = new List<Restaurant>
            {
                new Restaurant {name = "Subway", address = "123 Revature Street", phone = "1234561234"},
                new Restaurant {name = "Qdoba"},
                new Restaurant {name = "Wingstop"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<RestaurantReviewsData.Restaurant>>();
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RestaurantReviewsEntities>();
            mockContext.Setup(c => c.Restaurants).Returns(mockSet.Object);

            var service = new RestaurantRepository(mockContext.Object);
            var rest = service.GetDetails("Subway");

            //mockSet.Verify(m => m.Add(It.IsAny<Restaurant>()), Times.Once());
            //mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual("Subway", rest.Name);
            Assert.AreEqual("123 Revature Street", rest.Address);
            Assert.AreEqual("1234561234", rest.Phone);
        }

        [TestMethod]
        public void GetRestaurantReviews()
        {
            var data = new List<Restaurant>
            {
                new Restaurant {name = "Subway", Reviews = new List<Review>(){new Review(){rating = 5, review = "Ok", user="newguy"}}},
                new Restaurant {name = "Qdoba"},
                new Restaurant {name = "Wingstop"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<RestaurantReviewsData.Restaurant>>();
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Restaurant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<RestaurantReviewsEntities>();
            mockContext.Setup(c => c.Restaurants).Returns(mockSet.Object);

            var service = new RestaurantRepository(mockContext.Object);
            var rev = service.GetReviews("Subway");

            //mockSet.Verify(m => m.Add(It.IsAny<Restaurant>()), Times.Once());
            //mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.AreEqual(5, rev.ElementAt(0).Rating);
            Assert.AreEqual("Ok", rev.ElementAt(0).review);
            Assert.AreEqual("newguy", rev.ElementAt(0).user);
        }

    }
}
