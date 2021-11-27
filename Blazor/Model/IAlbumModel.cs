using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface IAlbumModel
    {
        Task<IList<Album>> SearchForAlbums(string title);
    }
}