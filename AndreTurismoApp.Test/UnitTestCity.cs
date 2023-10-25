using AndreTurismoApp.AddressService.Controllers;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.CityService.Controllers;
using AndreTurismoApp.CityService.Data;
using AndreTurismoApp.Models;
using AndreTurismoApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismoApp.Test
{
    public class UnitTestCity
    {
        private DbContextOptions<AndreTurismoAppCityServiceContext> options;

        private void InitializeDatabase()
        {
            //Create a temporary database
            options = new DbContextOptionsBuilder<AndreTurismoAppCityServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                context.City.Add(new City
                {
                    Id = 1,
                    Description = "Araraquara",
                    RegisterDate = DateTime.Now
                });
                context.City.Add(new City
                {
                    Id = 2,
                    Description = "São Carlos",
                    RegisterDate = DateTime.Now
                });
                context.City.Add(new City
                {
                    Id = 3,
                    Description = "Ibaté",
                    RegisterDate = DateTime.Now
                });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDatabase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController cityController = new (context);
                IEnumerable<City> cities = cityController.GetCity().Result.Value;

                Assert.Equal(3, cities.Count());
            }
        }

        [Fact]
        public void GetById()
        {
            InitializeDatabase();

            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                int cityId = 2;
                CitiesController cityController = new (context);
                City city = cityController.GetCity(cityId).Result.Value;
                Assert.Equal(2, city.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDatabase();

            City city = new City()
            {
                Id = 4,
                Description = "Matão",
                RegisterDate = DateTime.Now
            };

            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController cityController = new CitiesController(context);
                City c = cityController.PostCity(city).Result.Value;
                Assert.Equal("Matão", c.Description);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDatabase();

            City city = new City()
            {
                Id = 3,
                Description = "City Alterada"
            };

            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController cityController = new CitiesController(context);
                City c = cityController.PutCity(3, city).Result.Value;
                Assert.Equal("City Alterada", c.Description);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDatabase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                CitiesController cityController = new CitiesController(context);
                City city = cityController.DeleteCity(2).Result.Value;
                Assert.Null(city);
            }
        }
    }
}
