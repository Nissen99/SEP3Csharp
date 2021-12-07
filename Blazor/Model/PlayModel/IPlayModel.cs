using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Util.Playstate;
using Entities;
using NAudio.Wave;

namespace Blazor.Model.PlayModel
{
    public interface IPlayModel
    {
        IPlaystateContext Context { get; set; }
        Task PlaySongAsync(Song song);
        Task PlayPauseToggleAsync();
        Task PlayFromAsync(float progress);
        Task SetVolumeAsync(int percentage);
        Task PlayPreviousSong();
        Task PlayNextSongAsync();

         bool IsPlaying { get; }
        
        IList<Song> CurrentPlaylist { get; set; }
        IList<Song> GetPreviouslySongs();
        string UpdateDisplay();

        Task<Song> GetCurrentSongAsync();
    }
}