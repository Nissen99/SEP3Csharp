using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Blazor.Model;
using Blazor.Model.LibraryModel;
using Blazor.Model.PlaylistManageModel;
using Blazor.Model.PlayModel;
using Blazor.Model.SongManagerModel;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;
/*
 * Laver tabel over sange brugeren kan se og interagere med.
 *
 * Brugeren kan afspille sange.
 * Slette sange fra playlister (Hvis der er givet en playliste)
 * Slette sange fra musikbiblioteket (Hvis de er administrator)
 *
 * Tilføje sange til playlister (Hvis de er logget ind)
 */
namespace Blazor.Pages
{
    public partial class SongTable : ComponentBase
    {
        
        [Inject] public ISongManageModel SongManageModel { get; set; }
        [Inject] public IPlaylistManageModel PlaylistManageModel { get; set; }
        [Inject] public IPlayModel PlayModel { get; set; }
        [Inject] public IModalService ModalService { get; set; }
        [Parameter]
        public IList<Song> SongList { get; set; }
        
        [Parameter]
        public Playlist Playlist { get; set; }

        public Song CurrentSong;

        
        /**
         * Her tjekkes det om en playliste bliver brugt. Hvis den gør det opdateres songList.
         */
        protected async override Task OnInitializedAsync()
        {
            if (Playlist != null)
            {
                SongList = Playlist.Songs;
            }
            
            
            SongPlaying();
            PlayModel.Context.UpdatePlayState += () => SongPlaying();
            PlayModel.CurrentPlaylist = SongList;
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
            return PlayModel.Context.IsPlaying;
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

        private async Task AddSongToPlaylist(Song song)
        {
            try
            { 
                var form = ModalService.Show<AddToPlaylist>($"Choose a playlist to add \"{song.Title}\" to");
                var result = await form.Result;
                if (!result.Cancelled)
                {
                    Playlist playlist = (Playlist)result.Data;
                    await PlaylistManageModel.AddSongToPlaylist(playlist, song);
                    Console.WriteLine($"Added {song.Title} to playlist: {playlist.Title}");
                }
            }
            catch (Exception e)
            {
                ModalService.Show<Popup>(e.Message);
            }
        }
    }
}