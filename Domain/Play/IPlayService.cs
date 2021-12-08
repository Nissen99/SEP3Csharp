using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Play
{
    public interface IPlayService
    {
        Task<byte[]> PlayAsync(Song song);
        
        
    }
}