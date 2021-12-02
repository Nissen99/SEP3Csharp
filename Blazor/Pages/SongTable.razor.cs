using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Blazor.Model;
using Blazor.Model.AudioTestModel;
using Blazor.Model.PlaylistManageModel;
using Blazor.Model.PlayModel;
using Blazor.Model.SongManagerModel;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class SongTable : ComponentBase
    {
        [Inject] public IAudioTestModel Model { get; set; }
        [Inject] public ISongManageModel SongManageModel { get; set; }
        [Inject] public IPlaylistManageModel PlaylistManageModel { get; set; }
        [Inject] public IPlayModel PlayModel { get; set; }
        [Inject] public IModalService ModalService { get; set; }
        [Parameter]
        public IList<Song> SongList { get; set; }
        
        [Parameter]
        public Entities.Playlist Playlist { get; set; }

        public Song CurrentSong;

        protected async override Task OnInitializedAsync()
        {
            if (Playlist != null)
            {
                Console.WriteLine("Vi bruger squ playlisten nu");
                SongList = Playlist.Songs;
            }
            
            
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

        private async Task remove(Song song)
        {
            if (Playlist != null)
            {
                await removeSongFromPlaylist(song);
                return;
            }

            await removeSongFromSystem(song);
        }

        private async Task removeSongFromPlaylist(Song song)
        {
            Console.WriteLine("Removing from playlist");
            var form = ModalService.Show<ConfirmChoice>($"Are you sure you want to remove \"{song.Title}\"");
            var result = await form.Result;

            if (!result.Cancelled)
            {
                await PlaylistManageModel.RemoveSongFromPlaylist(Playlist, song);
                SongList.Remove(song);
                StateHasChanged();
            }
            
        }


        private async Task removeSongFromSystem(Song song)
        {
            Console.WriteLine("Removing from muddafucka system");
            var form = ModalService.Show<ConfirmChoice>($"Are you sure you want to remove \"{song.Title}\"");
            var result = await form.Result;
         
            if (!result.Cancelled)
            {
                await SongManageModel.RemoveSongAsync(song);
                SongList.Remove(song);
                Console.WriteLine("Remove Song ");
                StateHasChanged();
            }
        }

        private async Task PerformAddToPlaylist(Song song)
        {
            var form = ModalService.Show<AddToPlaylist>("Choose a playlist to add this song to");
            var result = await form.Result;
            if (!result.Cancelled)
            {
                Entities.Playlist playlist = (Entities.Playlist)result.Data;
                await PlaylistManageModel.AddSongToPlaylist(playlist, song);
                Console.WriteLine($"Added {song.Title} to playlist: {playlist.Title}");
            }
        }
    }
}