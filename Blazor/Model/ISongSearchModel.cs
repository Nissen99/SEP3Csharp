using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface ISongSearchModel
    {
        Task<IList<Song>> GetSongsByFilterAsync(string filterOption, string searchField);

    }
}