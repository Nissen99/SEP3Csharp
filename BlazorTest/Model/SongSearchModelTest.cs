using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Model.LibraryModel;
using Blazor.Model.SongSearchModel;
using Entities;
using NUnit.Framework;
using SocketsT1_T2.Tier1.Library;
using SocketsT1_T2.Tier1.Song;

namespace BlazorTest.Model
{
    public class SongSearchModelTest
    {
        private ILibraryModel libraryModel = new LibraryModel(new LibraryTcpClient());
        private ISongSearchModel songSearchModel = new SongSearchModel(new SongSearchTcpClient());


        [Test]
        public async Task TestIfCorrectSongIsFoundByTitle()
        {
            int songNumberTest = 0;

            IList<Song> listOfAllSongs = await libraryModel.GetAllSongs();
            string songTitleTest = listOfAllSongs[songNumberTest].Title;

            IList<Song> songList = await songSearchModel.GetSongsByFilterAsync("Title", songTitleTest);
            
            Assert.That(songList.Any(song => song.Id == listOfAllSongs[songNumberTest].Id));
        }
        
        [Test]
        public async Task TestIfCorrectSongIsFoundByArtistName()
        {
            int songNumberTest = 0;

            IList<Song> listOfAllSongs = await libraryModel.GetAllSongs();
            string artistName = listOfAllSongs[songNumberTest].Artists[0].Name;

            IList<Song> songList = await songSearchModel.GetSongsByFilterAsync("Artist", artistName);
            
            Assert.That(songList.Any(song => song.Id == listOfAllSongs[songNumberTest].Id));
        }
        
        
        [Test]
        public async Task SearchBySomethingNotImplemented()
        {

            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("Handsize","HandsizeSearchFiels"));
        }
        
        [Test]
        public async Task SearchByNull()
        {
            int songNumberTest = 0;

            IList<Song> listOfAllSongs = await libraryModel.GetAllSongs();
            string albumTitle = listOfAllSongs[songNumberTest].Album.Title;
            
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync(null,albumTitle));

        }
        
        [Test]
        public async Task SearchByEmptyString()
        
        {
            int songNumberTest = 0;

            IList<Song> listOfAllSongs = await libraryModel.GetAllSongs();
            string albumTitle = listOfAllSongs[songNumberTest].Album.Title;
        
        
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("",albumTitle));
        }
        
        [Test]
        public void SongTitleIsEmpty()
        {
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("Title",""));
        }
        
        [Test]
        public void SongTitleIsNull()
        {
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("Title",null));

        }
        
        [Test]
        public async Task SongTitleNotFound()
        {
          
            
            Assert.AreEqual(0, songSearchModel.GetSongsByFilterAsync("Title","NotAValidTitlejnjadnf").Result.Count);
        }
        
        [Test]
        public void ArtistNameIsEmpty()
        {
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("Artist",""));

        }
        
        [Test]
        public void ArtistNameIsNull()
        {
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("Artist",null));

        }
        
        [Test]
        public async Task ArtistNameNotFound()
        {

            Assert.AreEqual(0, songSearchModel.GetSongsByFilterAsync("Artist","NotaValidtArtistNamejasdf").Result.Count);
        }
        
        [Test]
        public void AlbumTitleIsEmpty()
        {
           
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("Album",""));
        }
        
        [Test]
        public void AlbumTitleIsNull()
        {
            Assert.ThrowsAsync<Exception>(() => songSearchModel.GetSongsByFilterAsync("Album",null));
        }
        
        [Test]
        public async Task AlbumTitleNotFound()
        {

            Assert.AreEqual(0,songSearchModel.GetSongsByFilterAsync("Album","NotAValidAlbumTitlekjnjkasdf").Result.Count);
        }
    }
}