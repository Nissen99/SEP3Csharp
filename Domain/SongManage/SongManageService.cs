using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Library;
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
        public async Task AddNewSongAsync(Song newSong, Mp3 mp3)
        {
            if (!InputValidator.CheckSongValidWithoutMp3(newSong)) throw new ArgumentException("Some Property not found");
            
            using MemoryStream ms = new MemoryStream(mp3.Data);
           
            using Mp3FileReader fileReader = new Mp3FileReader(ms);

            Console.WriteLine("længe af mp3 " + mp3.Data.Length);

            int duration = (int) fileReader.TotalTime.TotalSeconds;
            newSong.Duration = duration;
            
            Song newSongWithCorrectId = await songManageNetworking.AddNewSongAsync(newSong);
            
            await songManageNetworking.UploadMp3(newSongWithCorrectId, mp3);
        }
        

        public async Task RemoveSongAsync(Song songToRemove)
        {
            await songManageNetworking.RemoveSongAsync(songToRemove);
        }
    }
}