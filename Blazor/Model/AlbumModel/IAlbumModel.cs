using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.AlbumModel
{
    public interface IAlbumModel
    {
        Task<IList<Album>> SearchForAlbums(string title);
    }
}