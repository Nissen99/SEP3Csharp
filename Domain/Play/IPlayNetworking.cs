using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Play
{
    public interface IPlayNetworking
    {
        Task<Song> GetSongWithMP3(Song song);
        Task<IList<Song>> GetAllSongs();
    }
}