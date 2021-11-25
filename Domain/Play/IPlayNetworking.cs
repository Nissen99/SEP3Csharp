using System.Threading.Tasks;
using Entities;

namespace Domain.Play
{
    public interface IPlayNetworking
    {
        Task<string> GetSongWithMP3(Song song);
        Task<string> GetAllSongs();
    }
}