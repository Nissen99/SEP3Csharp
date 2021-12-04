using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Song
{
    public interface IPlayNetworkClient
    {
        Task<byte[]> PlaySong(Entities.Song song);

    }
}