using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface IArtistModel
    {
        Task<IList<Artist>> SearchForArtists(string name);
        Task<IList<Artist>> GetAllArtistAsync();
    }
}