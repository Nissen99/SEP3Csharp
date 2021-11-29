using System;
using System.IO;
using System.Threading.Tasks;
using Entities;
using NAudio.Wave;

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
            using MemoryStream ms = new MemoryStream(newSong.Mp3);
            using Mp3FileReader fileReader = new Mp3FileReader(ms);
            
            
            int duration = (int) fileReader.TotalTime.TotalSeconds;
            Console.WriteLine("Duration read in int: " + duration);

            newSong.Duration = duration;
            
            Console.WriteLine($"(Song Service Title: {newSong.Title}");

            await songManageNetworking.AddNewSongAsync(newSong);
        }

        public async Task RemoveSongAsync(Song songToRemove)
        {
            await songManageNetworking.RemoveSongAsync(songToRemove);
        }
    }
}