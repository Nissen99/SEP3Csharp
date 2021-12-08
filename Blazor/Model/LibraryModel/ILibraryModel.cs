using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.LibraryModel
{
    public interface ILibraryModel
    {
        Task<IList<Song>> GetAllSongs();

    }
}