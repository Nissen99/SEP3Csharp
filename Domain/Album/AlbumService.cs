using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Album
{
    public class AlbumService : IAlbumService
    {
        private IAlbumNetworking albumNetworking;
        
        public AlbumService(IAlbumNetworking albumNetworking)
        {
            this.albumNetworking = albumNetworking;
        }
        public async Task<IList<Entities.Album>> SearchForAlbums(string title)
        {
            return await albumNetworking.SearchForAlbums(title);

        }
    }
}