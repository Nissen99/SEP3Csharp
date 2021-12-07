using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Library
{
    public interface ILibraryService
    {
        Task<IList<Song>> GetAllSongsAsync();

    }
}