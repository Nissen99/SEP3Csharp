using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Artist
{
    public interface IArtistNetworkingClient
    {
        Task<IList<Entities.Artist>> SearchForArtists(string name);
        Task<IList<Entities.Artist>> GetAllArtistsAsync();
    }
}