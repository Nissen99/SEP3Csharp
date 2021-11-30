using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.ArtistModel
{
    public interface IArtistModel
    {
        Task<IList<Artist>> SearchForArtists(string name);
    }
}