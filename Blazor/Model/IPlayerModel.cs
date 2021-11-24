using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface IPlayerModel
    {
        Task PlaySongAsync(Song song);
        Task PlayPauseToggleAsync();
        Task PlayFromAsync(float progress);
        Task SetVolumeAsync(int percentage);
        Task PlayPreviousSong();
        Task PlayNextSongAsync();

        bool IsPlaying { get; }
        Action UpdatePlayState { get; set; } 
        Action ProgressBarUpdate { get; set; }
        IList<Song> CurrentPlaylist { get; set; }
        string UpdateDisplay();
        void StopPlaying();
        Task<double> UpdateProgressBar();
        Task<Song> GetCurrentSongAsync();
    }
}