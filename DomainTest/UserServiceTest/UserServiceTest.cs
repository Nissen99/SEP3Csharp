using System;
using System.Threading.Tasks;
using Domain.Users;
using Entities;
using NUnit.Framework;
using RestT2_T3;


namespace DomainTest.UserServiceTest
{
    public class UserServiceTest
    {
        private IUserService userService = new UserService(new UserRestClient());
        
        
        private string username;
        private string password;
        private string role;
        private int i = 0;
        
        [SetUp]
        public void Setup()
        {
            username = "TestUser";
            password = "TestPassword";
            role = "StandardUser";
        }
        
        /*
         * Zero tests
         */
        
        [Test]
        public async Task ValidateUserNullUsername()
        {
            username = null;
            User user = CreateUser();
            Assert.ThrowsAsync<ArgumentException>(()=>userService.ValidateUser(user));
        }

        [Test]
        public async Task ValidateUserNullPassword()
        {
            password = null;
            User user = CreateUser();
            Assert.ThrowsAsync<ArgumentException>(()=>userService.ValidateUser(user));        
        }
        
        
        //Det her er vel ikke en zero test? 
        [Test]
        public async Task ValidateUserNotInDatabase()
        {
            User user = CreateUser();
            Assert.ThrowsAsync<Exception>(()=>userService.ValidateUser(user));        
        }

        [Test]
        public async Task RegisterUserNullUsername()
        {
            username = null;
            User user = CreateUser();
            Assert.ThrowsAsync<ArgumentException>(()=>userService.RegisterUser(user));
        }
        
        [Test]
        public async Task RegisterUserNullPassword()
        {
            username = null;
            User user = CreateUser();
            Assert.ThrowsAsync<ArgumentException>(()=>userService.RegisterUser(user));
        }
        
        /*
         * One tests
         */
        [Test]
        public async Task RegisterUser()
        {
            Random rng = new Random();
            int n = rng.Next(0, 10000);
            username = $"TestUser{n}";
            User user = CreateUser();
            Assert.DoesNotThrowAsync(()=>userService.RegisterUser(user));
        }

        [Test]
        public async Task ValidateUser()
        {
            username = "Admin";
            password = "Admin";
            role = "Admin";
            User user = CreateUser();
            
            Assert.DoesNotThrowAsync(()=>userService.ValidateUser(user));

        }
        private User CreateUser()
        {
            return new User() {Username = username, Password = password, Role = role};
        }
    }
    
    
}