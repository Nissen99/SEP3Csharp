using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Util;
using Entities;
using NAudio.Wave;

namespace Domain.SongManage
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
            if (!InputValidator.CheckSongValidWithoutMp3(newSong)) throw new ArgumentException("Some Property not found");

            
            
            using MemoryStream ms = new MemoryStream(newSong.Mp3);
            using Mp3FileReader fileReader = new Mp3FileReader(ms);
            
            
            int duration = (int) fileReader.TotalTime.TotalSeconds;

            newSong.Duration = duration;
            
            await songManageNetworking.AddNewSongAsync(newSong);
        }

        public async Task RemoveSongAsync(Song songToRemove)
        {
            await songManageNetworking.RemoveSongAsync(songToRemove);
        }
    }
}