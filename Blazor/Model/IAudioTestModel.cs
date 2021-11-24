using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface IAudioTestModel
    {
       
        Task<IList<Song>> GetAllSongs();
    }
}