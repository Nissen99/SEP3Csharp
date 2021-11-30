using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model.ArtistModel
{
    public class ArtistModel : IArtistModel
    {
        private IClient client;

        public ArtistModel(IClient client)
        {
            this.client = client;
        }

        public async Task<IList<Artist>> SearchForArtists(string name)
        {
            return await client.SearchForArtists(name);
        }
    }
}