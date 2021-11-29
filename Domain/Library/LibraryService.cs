using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities;

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
            string path = @"../../../../Domain/Audio/tempfile.mp3";
            
            songsByte = await GetAllMP3Async();
            
            //Builder Pattern!!!! GO go implement
            foreach (byte[] MP3Byte in songsByte)
            {
                using (FileStream byteToMp3 = File.Create(path))
                {
                    await byteToMp3.WriteAsync(MP3Byte, 0, MP3Byte.Length);
                }
                
                TagLib.File file = TagLib.File.Create(path);
                string title = file.Tag.Title;
                string albumName = file.Tag.Album;
                string[] artists = TagSplitter(file.Tag.Performers[0]);
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
        
        private string[] TagSplitter(string toSplit)
        {
            return toSplit.Split(",");
        }
    }
}
