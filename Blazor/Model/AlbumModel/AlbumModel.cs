using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model.AlbumModel
{
    public class AlbumModel : IAlbumModel
    {
        
        private IClient client;

        public AlbumModel(IClient client)
        {
            this.client = client;
        }
        public async Task<IList<Album>> SearchForAlbums(string title)
        {
            return await client.SearchForAlbums(title);
        }
    }
}