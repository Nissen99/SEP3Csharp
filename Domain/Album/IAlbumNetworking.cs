using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Album
{
    public interface IAlbumNetworking
    {
        Task<IList<Entities.Album>> SearchForAlbums(string title);
    }
}