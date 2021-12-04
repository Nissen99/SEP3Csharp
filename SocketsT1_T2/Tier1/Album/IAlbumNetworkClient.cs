using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Album
{
    public interface IAlbumNetworkClient
    {
        Task<IList<Entities.Album>> SearchForAlbums(string title);

        Task<IList<Entities.Album>> GetAllAlbumsAsync();

    }
}