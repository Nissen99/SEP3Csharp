using System;
using System.IO;
using System.Threading.Tasks;
using Entities;

namespace Domain.Songs
{
    public class SongManageService : ISongManageService
    {
        private ISongManageNetworking songManageNetworking;

        public SongManageService(ISongManageNetworking songManageNetworking)
        {
            this.songManageNetworking = songManageNetworking;
        }

        //TODO Input checks
        public async Task AddNewSongAsync(Song newSong)
        {
            string path = @"../../../../Domain/Audio/tempfileForSaving.mp3";

            using (FileStream byteToMp3 = File.Create(path))
            {
                await byteToMp3.WriteAsync(newSong.Mp3, 0, newSong.Mp3.Length);
            }

            TagLib.File file = TagLib.File.Create(path);
            
            int duration = (int) file.Properties.Duration.TotalSeconds;

            newSong.Duration = duration;
            
            Console.WriteLine($"(Song Service Title: {newSong.Title}");

            await songManageNetworking.AddNewSongAsync(newSong);
        }
    }
}