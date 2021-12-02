using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.SongManage
{
    public interface ISongsNetworking
    {
        Task<IList<Song>> GetAllSongs();
    }
}