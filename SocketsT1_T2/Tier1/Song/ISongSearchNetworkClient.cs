using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Song
{
    public interface ISongSearchNetworkClient
    {
        
        Task<IList<Entities.Song>> GetSongsByFilterAsync(string[] filterOptions);

    }
}