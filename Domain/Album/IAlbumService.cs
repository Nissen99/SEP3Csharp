using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Album
{
    public interface IAlbumService
    {
        Task<IList<Entities.Album>> SearchForAlbums(string title);
    }
}