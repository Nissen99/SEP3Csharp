using System.Threading.Tasks;
using Entities;

namespace Domain.Songs
{
    public interface ISongManageService
    {
        Task AddNewSongAsync(Song newSong);
        Task RemoveSongAsync(Song songToRemove);
    }
}