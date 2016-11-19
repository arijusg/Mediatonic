using System;
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
        private MockHelper _mockHelper;
        private DateTime _testDateTime;
        private Animal _testNAnimal;
        [OneTimeSetUp]
        public void FixtureInit()
        {
            _mockHelper = new MockHelper();
            _server = TestServer.Create<Api.Startup>();

            _testDateTime = new DateTime(2005, 01, 01, 13, 11, 0, DateTimeKind.Utc);

            _testNAnimal = new Animal(1, 50, 1, 50, 1);
            _testNAnimal.LastProcess = _testDateTime;
            _mockHelper.MockOut<IAnimalFactory>().Setup(x => x.GetAnimal()).Returns(_testNAnimal);
            
            _mockHelper.MockOut<ITestableDateTime>().Setup(x => x.UtcNow()).Returns(_testDateTime);
        }

        [OneTimeTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public async Task GetUser()
        {
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

        [Test]
        public async Task TestDecreasingHappiness()
        {
          
            int userId = 1;
            var expectedAnimal = new Animal(1, 50, 1, 50, 1);

            var response = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First(x => x.Id == 1);


            DateTime secondDate = _testDateTime.AddMinutes(5);
            _mockHelper.MockOut<ITestableDateTime>().Setup(x => x.UtcNow()).Returns(secondDate);

            var response2 = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
            var userAnimals2 = await response2.Content.ReadAsAsync<List<Animal>>();
            Animal animal2 = userAnimals2.First(x => x.Id == 1);

           // Assert.AreEqual(start, animal.LastProcess);

            Assert.AreEqual(50, animal.HappyLevel);
            Assert.AreEqual(45, animal2.HappyLevel);
        }

        [Test]
        public async Task TestIncreasingHungriness()
        {

            int userId = 1;

            var response = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First(x => x.Id == 1);

            //Inject time adaptor to fake time utc

            DateTime secondDate = _testDateTime.AddMinutes(5);
            _mockHelper.MockOut<ITestableDateTime>().Setup(x => x.UtcNow()).Returns(secondDate);


            var response2 = await _server.HttpClient.GetAsync($"/api/user/{userId}/animals");
            var userAnimals2 = await response2.Content.ReadAsAsync<List<Animal>>();
            Animal animal2 = userAnimals2.First(x => x.Id == 1);

            Assert.AreEqual(50, animal.HungryLevel);
            Assert.AreEqual(55, animal2.HungryLevel);
        }

    }
}
