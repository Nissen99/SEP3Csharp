using System;
using System.Threading.Tasks;
using Entities;
using NAudio.Wave;

namespace Blazor.Util.Playstate
{
    public interface IPlaystateContext
    {
        IPlaystate CurrentState { get; set; }
        IWavePlayer WaveOut { get; set; }
        public Task PlaySong(Song song);
        public Task PlayFromAsync(int sec);
        public Task<bool> PlayPreviousAsync();
        public Task TogglePlayPause();

        void StopPlaying();
        
        Action UpdatePlayState { get; set; } 
        Action ProgressBarUpdate { get; set; }

        Task<double> UpdateProgressBar();
    }
}