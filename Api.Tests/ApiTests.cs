using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using Api.DAL;
using Api.Models;
using Api.Services;
using Newtonsoft.Json;
using Ninject;

namespace Api.Tests
{
    public class ApiTests
    {
        private TestServer _server;
        private MockHelper _mockHelper;
        private DateTime _defaultApiTime;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            _mockHelper = new MockHelper();
            _server = TestServer.Create<Api.Startup>();

            _defaultApiTime = new DateTime(2005, 01, 01, 13, 11, 0, DateTimeKind.Utc);
        }

        [OneTimeTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            ResetDatabase();
            SetApiTime(_defaultApiTime);
        }

        private void ResetDatabase()
        {
            Startup.Container.Unbind<GameContext>();
            DbConnection effortConnection = Effort.DbConnectionFactory.CreateTransient();
            Startup.Container.Bind<GameContext>().ToSelf().WithConstructorArgument(effortConnection);
            //TODO Set times
            var context = Startup.Container.Get<GameContext>();
            var animals = context.Animals.ToList();
            foreach (Entities.Animal animal in animals)
            {
                animal.LastAction = _defaultApiTime;
            }
            context.SaveChanges();
        }

        private void SetApiTime(DateTime dateTime)
        {
            _mockHelper.MockOut<ITestableDateTime>().Setup(x => x.UtcNow()).Returns(dateTime);
        }

        [Test]
        public async Task GetUser()
        {
            var response = await _server.HttpClient.GetAsync("/api/user/1");
            var result = await response.Content.ReadAsAsync<User>();

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Lucifier", result.Name);
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

            var response = await _server.HttpClient.GetAsync($"/api/animal/user/{userId}");
            var result = await response.Content.ReadAsAsync<List<Animal>>();

            var animal1 = result[0];
            var animal2 = result[1];

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, animal1.Id);
            Assert.AreEqual(50, animal1.HappyLevel);
            Assert.AreEqual(1, animal1.HappyLevelChange);
            Assert.AreEqual(50, animal1.HungryLevel);
            Assert.AreEqual(4, animal1.HungryLevelChange);
            Assert.AreEqual(_defaultApiTime, animal1.LastAction);

            Assert.AreEqual(2, animal2.Id);
            Assert.AreEqual(50, animal2.HappyLevel);
            Assert.AreEqual(2, animal2.HappyLevelChange);
            Assert.AreEqual(50, animal2.HungryLevel);
            Assert.AreEqual(3, animal2.HungryLevelChange);
            Assert.AreEqual(_defaultApiTime, animal2.LastAction);
        }

        [Test]
        public async Task FeedAnimal()
        {
            int userId = 1;

            var response = await _server.HttpClient.GetAsync($"/api/animal/user/{userId}");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First();

            var testTime = _defaultApiTime.AddMinutes(5);
            SetApiTime(testTime);

            var response1 = await _server.HttpClient.PostAsJsonAsync("/api/animal/feed", animal);
            var fedAnimal = await response1.Content.ReadAsAsync<Animal>();

            //Take in consideration 5min natural increase/decrease
            int andjustedHungryLevel = animal.HungryLevel;
            andjustedHungryLevel += 5 * animal.HungryLevelChange;

            Assert.AreEqual(andjustedHungryLevel - animal.HungryLevelChange, fedAnimal.HungryLevel);
            Assert.AreEqual(testTime, fedAnimal.LastAction);
        }

        [Test]
        public async Task PetAnimal()
        {
            int userId = 1;

            var response = await _server.HttpClient.GetAsync($"/api/animal/user/{userId}");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First();

            var testTime = _defaultApiTime.AddMinutes(10);
            SetApiTime(testTime);

            var response1 = await _server.HttpClient.PostAsJsonAsync("/api/animal/pet", animal);
            var petAnimal = await response1.Content.ReadAsAsync<Animal>();

            //Take in consideration 10min natural increase/decrease
            int andjustedHappyLevel = animal.HappyLevel;
            andjustedHappyLevel -= 10 * animal.HappyLevelChange;

            Assert.AreEqual(andjustedHappyLevel + animal.HappyLevelChange, petAnimal.HappyLevel);
            Assert.AreEqual(testTime, petAnimal.LastAction);
        }

        [Test]
        public async Task TestDecreasingHappiness()
        {

            int userId = 1;

            var response = await _server.HttpClient.GetAsync($"/api/animal/user/{userId}");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First(x => x.Id == 1);


            DateTime secondDate = _defaultApiTime.AddMinutes(5);
            SetApiTime(secondDate);

            var response2 = await _server.HttpClient.GetAsync($"/api/animal/user/{userId}");
            var userAnimals2 = await response2.Content.ReadAsAsync<List<Animal>>();
            Animal animal2 = userAnimals2.First(x => x.Id == 1);

            Assert.AreEqual(animal.HappyLevel - 5 * animal.HappyLevelChange, animal2.HappyLevel);
        }

        [Test]
        public async Task TestIncreasingHungriness()
        {

            int userId = 1;

            var response = await _server.HttpClient.GetAsync($"/api/animal/user/{userId}");
            var userAnimals = await response.Content.ReadAsAsync<List<Animal>>();
            Animal animal = userAnimals.First(x => x.Id == 1);

            DateTime secondDate = _defaultApiTime.AddMinutes(5);
            _mockHelper.MockOut<ITestableDateTime>().Setup(x => x.UtcNow()).Returns(secondDate);


            var response2 = await _server.HttpClient.GetAsync($"/api/animal/user/{userId}");
            var userAnimals2 = await response2.Content.ReadAsAsync<List<Animal>>();
            Animal animal2 = userAnimals2.First(x => x.Id == 1);

            Assert.AreEqual(animal.HungryLevel + 5 * animal.HungryLevelChange, animal2.HungryLevel);
        }

    }
}
