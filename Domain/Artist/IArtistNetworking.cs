using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Artist
{
    public interface IArtistNetworking
    {
        Task<IList<Entities.Artist>> SearchForArtists(string name);
    }
}