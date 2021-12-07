using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Library
{
    public interface ILibraryNetworking
    {
        Task<IList<Song>> GetAllSongsAsync();

    }
}