using System.Threading.Tasks;
using Entities;

namespace Domain.SongManage
{
    public interface ISongManageService
    {
        Task AddNewSongAsync(Song newSong, Mp3 mp3);
        Task RemoveSongAsync(Song songToRemove);
    }
}