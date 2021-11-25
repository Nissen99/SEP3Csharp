using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace SocketsT1_T2.Tier1
{
    public interface IClient
    {
        Task<IList<Song>> GetAllSongs();
        Task<Song> PlaySong(Song song);

        
        Task<IList<Song>> GetSongsByFilterAsync(string[] filterOptions);
        
        Task RegisterUser(User user);
        Task<User> validateUser(User user);
        
    }
}