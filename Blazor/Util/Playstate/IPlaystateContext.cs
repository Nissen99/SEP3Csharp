using System;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Util.Playstate
{
    public interface IPlaystateContext
    {
        Action UpdatePlayState { get; set; } 
        Action ProgressBarUpdate { get; set; }
        bool IsPlaying { get; }
        //IPlaystate CurrentState { get; set; }
        //IWavePlayer WaveOut { get; set; }
        public Task PlaySong(Song song);
        public Task PlayFromAsync(int sec);
        public Task<bool> PlayPreviousAsync();
        public Task TogglePlayPause();
        void StopPlaying();
        Task<double> UpdateProgressBar();
        Task SetVolume(int percentage);
    }
}