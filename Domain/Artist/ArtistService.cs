using System.Collections.Generic;
using System.Threading.Tasks;
/*
 * Denne klasse står for håndtering af Artist information
 */
namespace Domain.Artist
{
    public class ArtistService : IArtistService
    {
        private IArtistNetworking artistNetworking;
        public ArtistService(IArtistNetworking artistNetworking)
        {
            this.artistNetworking = artistNetworking;
        }

        public async Task<IList<Entities.Artist>> SearchForArtists(string name)
        {
            return await artistNetworking.SearchForArtists(name);

        }

        public async Task<IList<Entities.Artist>> GetAllArtistsAsync()
        {
            return await artistNetworking.GetAllArtistsAsync();
        }
    }
}