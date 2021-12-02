using System.Threading.Tasks;
using Entities;

namespace Domain.SongManage
{
    public interface ISongManageNetworking
    {
        Task AddNewSongAsync(Song newSong);
        Task RemoveSongAsync(Song songToRemove);
    }
}