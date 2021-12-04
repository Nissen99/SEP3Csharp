using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;
using SocketsT1_T2.Tier1.Album;

namespace Blazor.Model.AlbumModel
{
    public class AlbumModel : IAlbumModel
    {
        
        private IAlbumNetworkClient albumNetworkClient;

        public AlbumModel(IAlbumNetworkClient albumNetworkClient)
        {
            this.albumNetworkClient = albumNetworkClient;
        }
        public async Task<IList<Album>> SearchForAlbumsAsync(string title)
        {
            return await albumNetworkClient.SearchForAlbums(title);
        }

        public async Task<IList<Album>> GetAllAlbumsAsync()
        {
            return await albumNetworkClient.GetAllAlbumsAsync();
        }
    }
}