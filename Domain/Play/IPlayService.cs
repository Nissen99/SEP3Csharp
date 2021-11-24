using System.Threading.Tasks;
using Entities;

namespace Domain.Play
{
    public interface IPlayService
    {
        Task<string> PlayAsync(Song song);

        Task<string> GetAllSongsAsJsonAsync();
        
    }
}