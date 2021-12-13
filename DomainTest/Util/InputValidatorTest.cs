using Domain.Util;
using Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DomainTest.Util
{
    public class InputValidatorTest
    {

        
        [Test]
        public void ValidateUserSunny()
        {
            User validUser = new User()
            {
                Username = "ValidUsername",
                Password = "ValidPassword",
                Role = "StandardUser"
            };
            
            Assert.True(InputValidator.ValidateUser(validUser));
        }

        [Test]
        public void ValidateUserUsernameNull()
        {
            User notValidUser = new User()
            {
                Username = null,
                Password = "ValidPassword",
                Role = "StandardUser"
            };
            
            Assert.False(InputValidator.ValidateUser(notValidUser));
        }
        
        
        [Test]
        public void ValidateUserUsernameEmpty()
        {
            User notValidUser = new User()
            {
                Username = "",
                Password = "ValidPassword",
                Role = "StandardUser"
            };
            
            Assert.False(InputValidator.ValidateUser(notValidUser));
        }
    }
}