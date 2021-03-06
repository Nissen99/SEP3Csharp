using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1.Artist;

/*
 * Klassen fungere gateway fra mvvm del til resten af systemet, her gennem kald til Client
 */
namespace Blazor.Model.ArtistModel
{
    public class ArtistModel : IArtistModel
    {
        private IArtistNetworkingClient artistNetworkClient;

        public ArtistModel(IArtistNetworkingClient artistNetworkClient)
        {
            this.artistNetworkClient = artistNetworkClient;
        }

        public async Task<IList<Artist>> SearchForArtists(string name)
        {
            return await artistNetworkClient.SearchForArtists(name);
        }

        public async Task<IList<Artist>> GetAllArtistAsync()
        {
            return await artistNetworkClient.GetAllArtistsAsync();
        }
    }
}