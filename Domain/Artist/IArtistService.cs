using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Artist
{
    public interface IArtistService
    {
        Task<IList<Entities.Artist>> SearchForArtists(string name);
        Task<IList<Entities.Artist>> GetAllArtistsAsync();
    }
}
