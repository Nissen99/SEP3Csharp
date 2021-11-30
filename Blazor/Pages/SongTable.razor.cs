using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Model;
using Blazor.Model.AudioTestModel;
using Blazor.Model.PlayModel;
using Blazor.Model.SongManagerModel;
using Entities;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class SongTable : ComponentBase
    {
        [Inject] public IAudioTestModel Model { get; set; }
        [Inject] public ISongManageModel SongManageModel { get; set; }
        [Inject] public IPlayModel PlayModel { get; set; }
        [Parameter]
        public IList<Song> SongList { get; set; }

        public Song CurrentSong;

        protected async override Task OnInitializedAsync()
        {
            SongPlaying();
            PlayModel.UpdatePlayState += () => SongPlaying();
            StateHasChanged();
        }

        private string generateArtists(Song song)
        {
            IList<Artist> artists = song.Artists;
            string toReturn = artists[0].Name;
            int count = artists.Count;
            switch (count)
            {
                case 1:
                    break;
                case 2:
                    toReturn += ", " + artists[1].Name;
                    break;
                case 3:
                    toReturn += ", " + artists[1].Name + ", " + artists[2].Name;
                    break;
                default:
                    toReturn += ", " + artists[1].Name + " and various more"; 
                    break;
            }
            return toReturn;
           
            }

        private async void SongPlaying()
        {
            CurrentSong = await PlayModel.GetCurrentSongAsync();
            StateHasChanged();
        }

        private async void PlaySong(Song song)
        {
           await PlayModel.PlaySongAsync(song);
        }

        private bool IsPlaying()
        {
            return PlayModel.IsPlaying;
        }
        private void TogglePlay()
        {
            PlayModel.PlayPauseToggleAsync();
        }

        private string songDurationDisplay(Song song)
        {
            string timestamp = "";

            int minutes = song.Duration / 60;

            if (minutes < 10)
            {
                timestamp += "0";
            }

            timestamp += minutes + ":";

            int seconds = song.Duration % 60;

            if (seconds < 10)
            {
                timestamp += "0";
            }

            timestamp += seconds;

            return timestamp;

        }

        private async Task removeSong(Song song)
        {
            await SongManageModel.RemoveSongAsync(song);
            Console.WriteLine("Remove Song ");
            StateHasChanged();
        }
    }
}