using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Songs
{
    public interface ISongsNetworking
    {
        Task<IList<Song>> GetAllSongs();
    }
}