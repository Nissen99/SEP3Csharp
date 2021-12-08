using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Util.Playstate;
using Entities;
using NAudio.Wave;
using SocketsT1_T2.Tier1;
using SocketsT1_T2.Tier1.Song;

namespace Blazor.Model.PlayModel
{
    public class PlayModel : IPlayModel
    {
        public IList<Song> previouslySongs = new List<Song>();
        private Song currentSong;
        
        
        public IList<Song> CurrentPlaylist { get; set; }
        
        public IPlaystateContext Context { get; set; }

        public PlayModel(IPlayNetworkClient client)
        {
            Context = new PlaystateContext(client);
        }

        public IList<Song> GetPreviouslySongs()
        {
            return previouslySongs;
        }
        
        
        public async Task PlaySongAsync(Song song)
        {
            currentSong = song;
            
            await Context.PlaySong(song);
            

            if (previouslySongs.Count == 0 || previouslySongs[^1].Id != song.Id)
            {
                previouslySongs.Add(song);
            }
        }

        public async Task PlayPauseToggleAsync()
        {
            await Context.TogglePlayPause();
        }
        

        public async Task PlayFromAsync(float progress)
        {
            if (currentSong == null) return;
            int sec = (int)(currentSong.Duration * (progress/100));
            await Context.PlayFromAsync(sec);
        }

        public async Task SetVolumeAsync(int percentage)
        {
            await Context.SetVolume(percentage);
            
        }
        
        public async Task PlayPreviousSong()
        {
            bool result = await Context.PlayPreviousAsync();
            if (result)
            {
                previouslySongs.RemoveAt(previouslySongs.Count - 1);
                await PlaySongAsync(previouslySongs[^1]);
            }
        }
        
        public async Task PlayNextSongAsync()
        {
            int index = CurrentPlaylist.IndexOf(currentSong);
            if (index == CurrentPlaylist.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            await PlaySongAsync(CurrentPlaylist[index]);
        }

        public string UpdateDisplay()
        {
            if (currentSong.Artists.Count < 2)
            {
                return currentSong.Title + " " + currentSong.Artists[0].Name + currentSong.Duration;
            }
            else
            {
                return currentSong.Title + " Various Artists";
            }
            
        }
        
        public async Task<Song> GetCurrentSongAsync()
        {
             return currentSong;
        }
    }
}