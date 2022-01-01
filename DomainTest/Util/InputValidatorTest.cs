using System;
using System.IO;
using Domain.Util;
using Entities;
using NUnit.Framework;


namespace DomainTest.Util
{
    public class InputValidatorTest
    {


        /*
         * Playlist
         */
        [Test]
        public void ValidatePlaylistSunny()
        {
            User userForPlaylist = new User()
            {
                Username = "ValidUserName",
                Password = "ValidPassword"
            };
            Playlist validPlaylist = new Playlist()
            {
                Title = "ValidPlaylistTitle",
                User = userForPlaylist,
            };
            
            Assert.True(InputValidator.CheckPlaylist(validPlaylist));
        }
        
        [Test]
        public void ValidatePlaylistNoUser()
        {
          
            Playlist validPlaylist = new Playlist()
            {
                Title = "ValidPlaylistTitle"
            };
            
            Assert.False(InputValidator.CheckPlaylist(validPlaylist));
        }
        
        [Test]
        public void ValidatePlaylistNullTitle()
        {
          
            Playlist validPlaylist = new Playlist()
            {
                Title = null
            };
            
            Assert.False(InputValidator.CheckPlaylist(validPlaylist));
        }
        
            
        [Test]
        public void ValidatePlaylistEmptyTitle()
        {
          
            Playlist validPlaylist = new Playlist()
            {
                Title = ""
            };
            
            Assert.False(InputValidator.CheckPlaylist(validPlaylist));
        }
        
        
        /*
         * Mp3
         */
        [Test]
        public void CheckMp3Sunny()
        {
            byte[] mp3AsByteArray = File.ReadAllBytes(@"C:\Users\Mikkel\RiderProjects\SEP3Csharp\DomainTest\audio\test.mp3");
            Mp3 validMp3 = new Mp3()
            {
                Data = mp3AsByteArray
            };
            
            Assert.True(InputValidator.CheckMp3(validMp3));
        }
        
        [Test]
        public void CheckMp3ArrayNull()
        {
            Mp3 validMp3 = new Mp3();
            
            Assert.False(InputValidator.CheckMp3(validMp3));
        }
        
        [Test]
        public void CheckMp3ArrayEmpty()
        {
            Mp3 validMp3 = new Mp3()
            {
                Data = Array.Empty<Byte>()
            };
            
            Assert.False(InputValidator.CheckMp3(validMp3));
        }
        
        /*
         * Check Filter Input
         */
        [Test]
        public void CheckInputFilterSunny()
        {
            String[] stringsForArg = new[] {"Title", "ValidSongTitle"};
            
            Assert.True(InputValidator.CheckFilterInput(stringsForArg));
        }
        
        [Test]
        public void CheckInputFilterNull()
        {
            String[] stringsForArg = null;
            
            Assert.False(InputValidator.CheckFilterInput(stringsForArg));
        }
        
        [Test]
        public void CheckInputFilterEmptyStrings()
        {
            String[] stringsForArg = new[] {"", ""};
            String[] stringsForOneEmpty = new[] {"Title", ""};
            String[] stringsForArgEmpty = new[] {"", "ValidSeatch"};
            
            Assert.False(InputValidator.CheckFilterInput(stringsForArg));
            Assert.False(InputValidator.CheckFilterInput(stringsForOneEmpty));
            Assert.False(InputValidator.CheckFilterInput(stringsForArgEmpty));
        }
        
        [Test]
        public void CheckInputFilterMoreThan2Strings()
        {
            String[] stringsForArg = new[] {"Title", "ValidSongTitle", "AnotherOne"};
            
            Assert.False(InputValidator.CheckFilterInput(stringsForArg));
        
        }
      
        
        /*
         * User
         */
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