using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;


public interface IArtistService
    {
        Task<IList<Artist>> SearchForArtists(string name);
    }
