using AndreTurismoApp.AddressService.Controllers;
using AndreTurismoApp.AddressService.Data;
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
    public class UnitTestAddress
    {
        private DbContextOptions<AndreTurismoAppAddressServiceContext> options;

        private void InitializeDatabase()
        {
            //Create a temporary database
            options = new DbContextOptionsBuilder<AndreTurismoAppAddressServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                context.Address.Add(new Address { Id = 1, Street = "Street 1", Number = 7, CEP = "123456789", 
                    City = new City { Id = 1, Description = "City1" }, Complement = "Na esquina", Neighborhood = "Aquela lá" });
                context.Address.Add(new Address { Id = 2, Street = "Street 2", Number = 9, CEP = "987654321", 
                    City = new City { Id = 2, Description = "City2" }, Complement = "Na esquina", Neighborhood = "Aquela lá" });
                context.Address.Add(new Address { Id = 3, Street = "Street 3", Number = 2, CEP = "543216789", 
                    City = new City { Id = 3, Description = "City3" }, Complement = "Na esquina", Neighborhood = "Aquela lá" });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDatabase();

            // Use a clean instance of the context to run the test
            using ( var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, null);
                IEnumerable<Address> addresses = addressController.GetAddress().Result.Value;

                Assert.Equal(3, addresses.Count());
            }
        }

        [Fact]
        public void GetById()
        {
            InitializeDatabase();

            using (var context  = new AndreTurismoAppAddressServiceContext(options))
            {
                int addressId = 2;
                AddressesController addressController = new AddressesController(context, null);
                Address address = addressController.GetAddress(addressId).Result.Value;
                Assert.Equal(2, address.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDatabase();

            Address address = new Address()
            { 
                Id = 4, 
                Street = "Street 4",
                Number = 3,
                CEP = "14804300",
                City = new City { Id = 4, Description = "City4" },
                Complement = "Na esquina",
                Neighborhood = "Aquela lá"
            };

            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, new PostOfficeService());
                Address ad = addressController.PostAddress(address).Result.Value;
                Assert.Equal("Avenida Alberto Benassi", ad.Street);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDatabase();

            Address address = new Address()
            {
                Id = 3,
                Street = "Rua 10 alterada",
                City = new() { Id = 10, Description = "City Alterada" }

            };

            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, null);
                Address ad = addressController.PutAddress(3, address).Result.Value;
                Assert.Equal("Rua 10 alterada", ad.Street);
                Assert.Equal("City Alterada", ad.City.Description);
            }        
        }

        [Fact]
        public void Delete()
        {
            InitializeDatabase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                AddressesController addressController = new AddressesController(context, null);
                Address address = addressController.DeleteAddress(2).Result.Value;
                Assert.Null(address);
            }
        }
    }
}
