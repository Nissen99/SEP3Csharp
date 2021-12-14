using System.Threading.Tasks;
using NAudio.Wave;

namespace Blazor.Util.Playstate
{
    public interface IPlaystate
    {
        PlaybackState State { get; }

        Task<bool> PlayPreviousSong();
        Task TogglePlayPause();
    }
}