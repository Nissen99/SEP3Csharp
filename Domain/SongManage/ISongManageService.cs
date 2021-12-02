using System.Threading.Tasks;
using Entities;

namespace Domain.SongManage
{
    public interface ISongManageService
    {
        Task AddNewSongAsync(Song newSong);
        Task RemoveSongAsync(Song songToRemove);
    }
}