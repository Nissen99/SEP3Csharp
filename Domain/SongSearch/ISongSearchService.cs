using System.Threading.Tasks;
using Entities;

namespace Domain.SongSearch
{
    public interface ISongSearchService
    {
        
        Task<string> GetSongsByFilterJsonAsync(TransferObj tObj);

        
    }
}