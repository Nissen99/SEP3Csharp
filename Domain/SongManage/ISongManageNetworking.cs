using System;
using System.Threading.Tasks;
using Entities;

namespace Domain.SongManage
{
    public interface ISongManageNetworking
    {
        Task<Song> AddNewSongAsync(Song newSong);
        Task RemoveSongAsync(Song songToRemove);
        Task UploadMp3(Song newSongWithCorrectId, Mp3 mp3);
    }
}