using System.Threading.Tasks;

namespace Domain.Songs
{
    public interface ISongsNetworking
    {
        Task<string> GetAllSongs();
    }
}