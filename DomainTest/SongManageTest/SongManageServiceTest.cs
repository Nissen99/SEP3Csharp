using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Album;
using Domain.Artist;
using Domain.Play;
using Domain.SongManage;
using Domain.SongSearch;
using Entities;
using NUnit.Framework;
using RestT2_T3;


/*
 * Denne test klasse fylder databasen med Test artister og albums
 */
namespace DomainTest.SongManageTest
{
    public class SongManageServiceTest
    {
        private ISongManageService songManageService = new SongManageService(new SongManageRestClient());
        private ISongSearchService songSearchService = new SongSearchService(new SongSearchRestClient());
        private IArtistService artistService = new ArtistService(new ArtistRestClient());
        private IAlbumService albumService = new AlbumService(new AlbumRestClient());
        private IPlayService playService = new PlayService(new PlayRestClient());

        private string songTitle;
        private Album album;
        private IList<Artist> artists;
        private int releaseYear;
        private string mp3;
        private byte[] data;


        [SetUp]
        public void SetUp()
        {
            songTitle = "Test";
            album = new Album() {Title = "TestAlbum"};
            artists = new List<Artist>() {new Artist() {Name = "Test"}};
            releaseYear = 2020;
            mp3 = @"C:\Users\mathi\RiderProjects\SEP3Csharp\DomainTest\audio\test.mp3";
        }


        [TearDown]
        public async Task TearDown()
        {
            try
            {
                IList<Song> allSongsOnFilterAfter =
                    await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", "Test"});
                if (allSongsOnFilterAfter.Count != 0)
                {
                    await songManageService.RemoveSongAsync(allSongsOnFilterAfter[0]);
                }
            }
            catch (Exception e)
            {
            }
        }


        /*
         * ZERO TESTS
         */
        [Test]
        public async Task AddSongWithEmptyTitle()
        {
            songTitle = "";
            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
        }

        [Test]
        public async Task AddSongWithNullTitle()
        {
            songTitle = null;
            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
        }


        [Test]
        public async Task AddSongWithNullAlbum()
        {
            album = null;
            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
        }

        [Test]
        public async Task AddSongWithNullArtists()
        {
            artists = null;
            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
        }

        [Test]
        public async Task AddSongWithEmptyArtistsList()
        {
            artists = new List<Artist>();
            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
        }

        // [Test]
        // public async Task AddSongWithEmptyMp3()
        // {
        //     mp3 = Array.Empty<byte>();
        //     Assert.ThrowsAsync<InvalidDataException>( CreateNewSongAndSave);
        // }
        //
        // [Test]
        // public async Task AddSongWithWrongFileType()
        // {
        //     mp3 = File.ReadAllBytes(mp3);
        //     Assert.ThrowsAsync<InvalidDataException>(CreateNewSongAndSave);
        // }

        [Test]
        public async Task AddSongWithNullMp3()
        {
            mp3 = null;
            Assert.ThrowsAsync<ArgumentNullException>(CreateNewSongAndSave);
        }

        [Test]
        public async Task AddSongWithReleaseYear0()
        {
            releaseYear = 0;
            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
        }

        [Test]
        public async Task RemoveSongNotInDatabase()
        {
            int countBefore = (await playService.GetAllSongsAsync()).Count;
            Song notRealSong = new Song()
            {
                Id = -500,
                Title = "This is not a real song in database",
                Artists = artists,
                Album = album,
                ReleaseYear = releaseYear
            };
            
            await songManageService.RemoveSongAsync(notRealSong);
            int countAfter = (await playService.GetAllSongsAsync()).Count;
            Assert.AreEqual(countAfter, countBefore);
        }


        /*
         * ONE TESTS
         */

        [Test]
        public async Task AddSongNewAlbumAndArtist()
        {
            IList<Song> allSongsOnFilterBefore =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", songTitle});

            Song newSong = await CreateNewSongAndSave();

            IList<Song> allSongsOnFilterAfter =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", newSong.Title});
            Assert.AreNotEqual(allSongsOnFilterBefore, allSongsOnFilterAfter);
        }

        [Test]
        public async Task AddSongOldAlbumAndNewArtist()
        {
            album = (await albumService.GetAllAlbumsAsync())[0];

            IList<Song> allSongsOnFilterBefore =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", songTitle});

            Song newSong = await CreateNewSongAndSave();

            IList<Song> allSongsOnFilterAfter =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", newSong.Title});
            Assert.AreNotEqual(allSongsOnFilterBefore, allSongsOnFilterAfter);
        }

        [Test]
        public async Task AddSongOldAlbumAndOldArtist()
        {
            album = (await albumService.GetAllAlbumsAsync())[0];
            artists = new List<Artist>()
            {
                (await artistService.GetAllArtistsAsync())[0]
            };

            IList<Song> allSongsOnFilterBefore =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", songTitle});

            Song newSong = await CreateNewSongAndSave();

            IList<Song> allSongsOnFilterAfter =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", newSong.Title});
            Assert.AreNotEqual(allSongsOnFilterBefore, allSongsOnFilterAfter);
        }


        [Test]
        public async Task AddSongNewAlbumAndOldArtist()
        {
            artists = new List<Artist>()
            {
                (await artistService.GetAllArtistsAsync())[0]
            };

            IList<Song> allSongsOnFilterBefore =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", songTitle});

            Song newSong = await CreateNewSongAndSave();

            IList<Song> allSongsOnFilterAfter =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", newSong.Title});
            Assert.AreNotEqual(allSongsOnFilterBefore, allSongsOnFilterAfter);
        }


        [Test]
        public async Task AddSongOnlyAddsOneSongToDatabase()
        {
            IList<Song> allSongsBefore = await playService.GetAllSongsAsync();

            await CreateNewSongAndSave();

            IList<Song> allSongsAfter = await playService.GetAllSongsAsync();

            Assert.AreEqual(allSongsBefore.Count + 1, allSongsAfter.Count);
        }

        [Test]
        public async Task RemoveSongRemovesOneSongFromDatabase()
        {
            Song newSong = await CreateNewSongAndSave();
            Song newSongWithId = (await songSearchService.GetSongsByFilterJsonAsync(new string[] {"Title", newSong.Title}))[0];

            int countBefore = (await playService.GetAllSongsAsync()).Count;
            
            await songManageService.RemoveSongAsync(newSongWithId);

            int countAfter = (await playService.GetAllSongsAsync()).Count;
            
            Assert.AreEqual(countAfter, countBefore - 1 );
        }
        
        [Test]
        public async Task RemoveSongRemovesRightSongFromDatabase()
        {
            Song newSong = await CreateNewSongAndSave();
            Song newSongWithId = (await songSearchService.GetSongsByFilterJsonAsync(new string[] {"Title", newSong.Title}))[0];

            
            await songManageService.RemoveSongAsync(newSongWithId);

            IList<Song> allSongs = await playService.GetAllSongsAsync();
                        
            Assert.False(allSongs.Any(song => song.Id == newSongWithId.Id));
        }

        /*
         * Many Tests
         */
        [Test]
        public async Task AddSongWithMultipleArtists()
        {
            artists = new List<Artist>()
            {
                (await artistService.GetAllArtistsAsync())[0],
                new Artist() {Name = "New Test Artist"}
            };

            Song newSong = await CreateNewSongAndSave();

            IList<Song> allSongsOnFilterAfter =
                await songSearchService.GetSongsByFilterJsonAsync(new[] {"Title", newSong.Title});

            Song song = allSongsOnFilterAfter.First(song1 => song1.Title.Equals(songTitle));

            if (song.Artists.Count != artists.Count)
            {
                Assert.Fail("Song.Artist and artists not same count");
            }

            for (int i = 0; i < artists.Count; i++)
            {
                if (!song.Artists.Any(artist => artist.Name.Equals(artists[i].Name)))
                {
                    Assert.Fail("Song.Artists.Any did not contain");
                }
            }
        }
        
        
        /*
         * Boundary Tests
         */

        [Test]
        public void SongWithReleaseYearUnderLowerBound()
        {
            releaseYear = 1868;

            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
        }
        
        [Test]
        public void SongWithReleaseYearOnLowerBound()
        {
            releaseYear = 1869;

            Assert.DoesNotThrowAsync(CreateNewSongAndSave);
        }

        [Test]
        public void SongWithReleaseYearBothSidesOfLowerBound()
        {
            releaseYear = 1569;
            Assert.ThrowsAsync<ArgumentException>(CreateNewSongAndSave);
            
            releaseYear = 2000;
            Assert.DoesNotThrowAsync(CreateNewSongAndSave);
        }
   
        
        private async Task<Song> CreateNewSongAndSave()
        {
            Song newSong = new Song()
            {
                Title = songTitle,
                Album = album,
                ReleaseYear = releaseYear,
                Artists = artists,
                Mp3 = mp3
            };

            //await songManageService.AddNewSongAsync(newSong);
            return newSong;
        }
    }
}