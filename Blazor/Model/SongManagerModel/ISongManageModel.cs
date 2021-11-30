using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.SongManagerModel
{
    public interface ISongManageModel
    {
        Task AddNewSongAsync(Song newSong);
        Task RemoveSongAsync(Song song);
    }
}