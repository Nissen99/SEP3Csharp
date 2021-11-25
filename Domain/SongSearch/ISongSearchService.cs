using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.SongSearch
{
    public interface ISongSearchService
    {
        
        Task<IList<Song>> GetSongsByFilterJsonAsync(string[] args);

        
    }
}