using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using Api.Services;
using Moq;

namespace Api.Tests
{
    public class ApiTests
    {
        private TestServer _server;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            _server = TestServer.Create<Api.Startup>();
        }

        [OneTimeTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [Test]
        public async Task GetUser()
        {
            //TODO mockout
            //var m = new MockHelper();
            //m.MockOut<IUserService>().Setup(x => x.GetUser(It.IsAny<int>())).Returns(new User(2));

            var expectedUser = new User(1);

            var response = await _server.HttpClient.GetAsync("/api/user/1");
            var result = await response.Content.ReadAsAsync<User>();

            Assert.AreEqual(expectedUser.Id, result.Id);
        }

        [Test]
        public async Task GetNonExistantUser()
        {
            var response = await _server.HttpClient.GetAsync("/api/user/0");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task GetUserAnimals()
        {
            int userId = 1;
            var expectedAnimal = new Animal(1, 50, 1, 50, 1);

            var response = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
            var result = await response.Content.ReadAsAsync<List<Animal>>();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(expectedAnimal.Id, result.First().Id);
        }

        [Test]
        public async Task FeedAnimal()
        {
            int userId = 1;
            var expectedAnimal = new Animal(1, 50, 1, 50, 1);

            var response = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First();

            //TODO post?
            var response1 = await _server.HttpClient.PostAsJsonAsync("/api/animal/feed", animal);
            var fedAnimal = await response1.Content.ReadAsAsync<Animal>();

            Assert.AreEqual(49, fedAnimal.HungryLevel);

        }

        [Test]
        public async Task PetAnimal()
        {
            int userId = 1;
            var expectedAnimal = new Animal(1, 50, 1, 50, 1);

            var response = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First();

            var response1 = await _server.HttpClient.PostAsJsonAsync("/api/animal/pet", animal);
            var petAnimal = await response1.Content.ReadAsAsync<Animal>();

            Assert.AreEqual(51, petAnimal.HappyLevel);
        }

        //[Test]
        //public async Task TestDecreasingHappiness()
        //{
        //    int userId = 1;
        //    var expectedAnimal = new Animal(1, 50, 1, 50, 1);

        //    var response = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
        //    var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
        //    Animal animal = userAnimals.First(x => x.Id == 1);

        //    //Inject time adaptor to fake time utc
        //    var response2 = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
        //    var userAnimals2 = await response.Content.ReadAsAsync<List<Animal>>();
        //    Animal animal2 = userAnimals.First(x => x.Id == 1);

        //    Assert.AreEqual(50, animal.HappyLevel);
        //    Assert.AreEqual(49, animal2.HappyLevel);
        //}

        //[Test]
        //public async Task TestIncreasingHungriness()
        //{
        //    int userId = 1;
        //    var expectedAnimal = new Animal(1, 50, 1, 50, 1);

        //    var response = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
        //    var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
        //    Animal animal = userAnimals.First(x => x.Id == 1);

        //    //Inject time adaptor to fake time utc
        //    var response2 = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
        //    var userAnimals2 = await response.Content.ReadAsAsync<List<Animal>>();
        //    Animal animal2 = userAnimals.First(x => x.Id == 1);

        //    Assert.AreEqual(50, animal.HungryLevel);
        //    Assert.AreEqual(55, animal2.HungryLevel);
        //}

    }
}
