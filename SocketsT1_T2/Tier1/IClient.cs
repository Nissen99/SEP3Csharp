using System.Threading.Tasks;
using Entities;

namespace SocketsT1_T2.Tier1
{
    public interface IClient
    {
        Task<string> GetAllSongs(string transforObject);
        Task<Song> PlaySong(string tansfAsJson);
        Task<string> GetSongsByFilter(string transString);
        
    }
}