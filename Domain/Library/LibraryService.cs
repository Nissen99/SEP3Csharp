using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Util;
using Entities;
using TagLib;

namespace Domain.Library
{
    public class LibraryService : ILibraryService
    {
        private IList<byte[]> songsByte = new List<byte[]>();
        private IList<Song> songList = new List<Song>();
        private ILibraryNetworking libraryNetworking;
        
        public LibraryService(ILibraryNetworking libraryNetworking)
        {
            this.libraryNetworking = libraryNetworking;
        }
        public async Task<IList<byte[]>> GetAllMP3Async()
        {
            return await libraryNetworking.GetAllMP3();
        }

        public async Task SendSongListToDBAsync()
        {
            
            songsByte = await GetAllMP3Async();
            
            //Builder Pattern!!!! GO go implement
            foreach (byte[] MP3Byte in songsByte)
            {
                File file = File.Create(new FileBytesAbstraction("temp.mp3", MP3Byte));
                
                string title = file.Tag.Title;
                string albumName = file.Tag.Album;
                string[] artists = file.Tag.Performers[0].Split(",");
                uint year = file.Tag.Year;
                int duration = (int)file.Properties.Duration.TotalSeconds;
                
                Song song = new Song
                {
                    Title = title,
                    Album = new Entities.Album() {Title = albumName},
                    Artists = Enumerable.Range(0,artists.Length).Select(i => new Entities.Artist{Name = artists[i]}).ToList(),
                    Duration = duration,
                    ReleaseYear = (int)year,
                    Mp3 = MP3Byte
                };
                songList.Add(song);
            }
            
            await libraryNetworking.PostAllSongs(songList);
            Console.WriteLine("Post Done");

        }
        
    }
}
