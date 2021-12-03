using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.SongManagerModel
{
    public interface ISongManageModel
    {
        Task AddNewSongAsync(Song newSong, Mp3 mp3);
        Task RemoveSongAsync(Song song);
    }
}