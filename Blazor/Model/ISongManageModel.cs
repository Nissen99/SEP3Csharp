using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface ISongManageModel
    {
        Task AddNewSongAsync(Song newSong);
    }
}