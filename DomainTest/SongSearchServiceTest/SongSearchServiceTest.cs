using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Library;
using Domain.Play;
using Domain.SongSearch;
using Entities;
using NUnit.Framework;
using RestT2_T3;

namespace DomainTest.SongSearchServiceTest
{
    public class SongSearchServiceTest
    {
        private ISongSearchService songSearchService = new SongSearchService(new SongSearchRestClient());
        private ILibraryService libraryService = new LibraryService(new LibraryRestClient());
         

        [Test]
        public async Task TestIfCorrectSongIsFoundByTitle()
        {
            int songNumberTest = 0;

            var listOfAllSongs = await GetListOfAllSongs();

            string songTitleTest = listOfAllSongs[songNumberTest].Title;

            string[] args = {"Title", songTitleTest};

            var listOfDeserilizedSong = ListOfSongSearchFor(args);


            Assert.That(listOfDeserilizedSong.Any(song => song.Id == listOfAllSongs[songNumberTest].Id));
        }

        [Test]
        public async Task TestIfCorrectSongIsFoundByArtistName()
        {
            int songNumberTest = 0;

            var listOfAllSongs = await GetListOfAllSongs();

            string songArtistName = listOfAllSongs[songNumberTest].Artists[0].Name;

            string[] args = {"Artist", songArtistName};

            var listOfDeserilizedSong = ListOfSongSearchFor(args);

            Assert.That(listOfDeserilizedSong.Any(song => song.Id == listOfAllSongs[songNumberTest].Id));
        }


        [Test]
        public async Task TestIfCorrectSongIsFoundByAlbum()
        {
            int songNumberTest = 0;
            var listOfAllSongs = await GetListOfAllSongs();

            string songAlbumTitle = listOfAllSongs[songNumberTest].Album.Title;

            string[] args = {"Album", songAlbumTitle};

            var listOfDeserilizedSong = ListOfSongSearchFor(args);

            Assert.That(listOfDeserilizedSong.Any(song => song.Id == listOfAllSongs[songNumberTest].Id));
        }


        [Test]
        public async Task SearchBySomethingNotImplemented()
        {
            string[] args = {"HandSize", "HandSizeObject"};
            
            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }


        [Test]
        public async Task SearchByNull()

        {
            int songNumberTest = 0;
            var listOfAllSongs = await GetListOfAllSongs();

            string songAlbumTitle = listOfAllSongs[songNumberTest].Album.Title;
            string[] args = {null, songAlbumTitle};
            


            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }


        [Test]
        public async Task SearchByEmptyString()

        {
            int songNumberTest = 0;
            IList<Song> listOfAllSongs = await GetListOfAllSongs();

            string songAlbumTitle = listOfAllSongs[songNumberTest].Album.Title;
            string[] args = {"", songAlbumTitle};

            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }

        [Test]
        public void SongTitleIsEmpty()
        {
            string[] args = {"Title", ""};

            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }

        [Test]
        public void SongTitleIsNull()
        {
            string[] args = {"Title", null};

            


            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }

        [Test]
        public async Task SongTitleNotFound()
        {
            int songNumberTest = 0;

            string[] args = {"Title", "asdNotTitleOfSongasdf"};

            var listOfDeserilizedSong = ListOfSongSearchFor(args);

            Assert.AreEqual(0, listOfDeserilizedSong.Count);
        }

        
        [Test]
        public void ArtistNameIsEmpty()
        {
            string[] args = {"Artist", ""};
            
            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }

        [Test]
        public void ArtistNameIsNull()
        {
            string[] args = {"Artist", null};

            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }

        [Test]
        public async Task ArtistNameNotFound()
        {
            string[] args = {"Artist", "asdNotAnArtistNamesdf"};

            var listOfDeserilizedSong = ListOfSongSearchFor(args);

            Assert.AreEqual(0, listOfDeserilizedSong.Count);
        }

        
        
        [Test]
        public void AlbumTitleIsEmpty()
        {
            string[] args = {"Album", ""};

            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }

        
        [Test]
        public void AlbumTitleIsNull()
        {
            string[] args = {"Album", null};

            Assert.ThrowsAsync<Exception>(() => songSearchService.GetSongsByFilterJsonAsync(args));
        }

        [Test]
        public async Task AlbumTitleNotFound()
        {
            int songNumberTest = 0;

            string[] args = {"Album", "asdNotAnAlbumTitleasdf"};

            var listOfDeserilizedSong = ListOfSongSearchFor(args);

            Assert.AreEqual(0, listOfDeserilizedSong.Count);
        }

        

        //Help Method
        private IList<Song> ListOfSongSearchFor(string[] args)
        {
            IList<Song> songWithTitle = songSearchService.GetSongsByFilterJsonAsync(args).Result;
            
            return  songWithTitle;
        }

            //Help Method
        private async Task<IList<Song>> GetListOfAllSongs()
        { 
            return await libraryService.GetAllSongsAsync();
            
        }
    }
}