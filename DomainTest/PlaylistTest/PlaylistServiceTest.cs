using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Playlist;
using Domain.Users;
using Entities;
using NUnit.Framework;
using RestT2_T3;

namespace DomainTest.PlaylistTest
{
    public class PlaylistServiceTest
    {
        private IPlayListService PlayListService = new PlayListService(new PlaylistRestClient());
        private IUserService UserService = new UserService(new UserRestClient());

        private string playlistTitle;
        private User user;

        [SetUp]
        public async Task SetUp()
        {
            user = new User() {Username = "Admin", Password = "Admin", Role = "Admin"};
            playlistTitle = "TestPlaylist";
        }

        private async Task<Playlist> CreateNewPlaylist()
        {
            Playlist newPlaylist = new Playlist() {Title = playlistTitle, User = await UserService.ValidateUser(user)};
            
            await PlayListService.CreateNewPlaylistAsync(newPlaylist);
            return newPlaylist;
        }

        [Test]
        public async Task AddPlaylistWithTitle()
        {
            Assert.AreEqual(playlistTitle, CreateNewPlaylist().Result.Title);
        }

        [Test]
        public async Task AddPlaylistWithEmptyTitle()
        {
            playlistTitle = "";
            Assert.ThrowsAsync<ArgumentException>(CreateNewPlaylist);
        }

        [Test]
        public async Task AddPlaylistWithNull()
        {
            playlistTitle = null;
            Assert.ThrowsAsync<ArgumentException>(CreateNewPlaylist);
        }

        [Test]
        public async Task AddPlaylistWithoutUser()
        {
            Playlist newPlaylist = new Playlist();
            newPlaylist.Title = "Testplaylist";
            Assert.ThrowsAsync<ArgumentException>(() => PlayListService.CreateNewPlaylistAsync(newPlaylist));
        }


        [Test]
        public async Task AddPlaylistOnlyAddsOnePlaylistToDatabase()
        {
            IList<Playlist> allPlaylistsBefore = PlayListService.GetAllPlaylistsForUserAsync(user).Result;

            await CreateNewPlaylist();

            IList<Playlist> allPlaylistsAfter = PlayListService.GetAllPlaylistsForUserAsync(user).Result;

            Assert.AreEqual(allPlaylistsBefore.Count + 1, allPlaylistsAfter.Count);
        }


        [Test]
        public async Task RemovePlaylistNotInDatabase()
        {
            int countBefore = PlayListService.GetAllPlaylistsForUserAsync(user).Result.Count;
            Playlist notRealPlaylist = new Playlist()
            {
                Title = "This is not a real playList in database",
                User = await UserService.ValidateUser(user)
            };

            await PlayListService.DeletePlayListAsync(notRealPlaylist);
            int countAfter = PlayListService.GetAllPlaylistsForUserAsync(user).Result.Count;
            Assert.AreEqual(countAfter, countBefore);
        }


        [Test]
        public async Task RemovePlaylistRemovesOnePlaylistFromDatabase()
        {
            Playlist newPlaylistWithId = (await PlayListService.GetAllPlaylistsForUserAsync(user))[0];

            int countBefore = (await PlayListService.GetAllPlaylistsForUserAsync(user)).Count;

            await PlayListService.DeletePlayListAsync(newPlaylistWithId);

            int countAfter = (await PlayListService.GetAllPlaylistsForUserAsync(user)).Count;

            Assert.AreEqual(countAfter, countBefore - 1);
        }
    }
}