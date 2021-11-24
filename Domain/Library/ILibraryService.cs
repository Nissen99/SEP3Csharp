using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Library
{
    public interface ILibraryService
    {
        Task<IList<byte[]>> GetAllMP3Async();
        Task SendSongListToDBAsync();
    }
}