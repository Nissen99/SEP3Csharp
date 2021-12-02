using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Authentication;
using Blazor.Model.PlaylistModel;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class PlaylistOverview : ComponentBase
    {
        [Inject] public IPlayListModel PlaylistModel { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        public IList<Entities.Playlist> Playlists { get; set; }


        public async Task PerformCreatePlaylist()
        {
            var form = ModalService.Show<CreatePlaylist>("Create new Playlist");
            var result = await form.Result;

            if (!result.Cancelled)
            {
                Entities.Playlist justCreated = (Entities.Playlist) result.Data;
                justCreated.User = CustomAuthenticationStateProvider.cachedUser;
                StateHasChanged();
                await PlaylistModel.CreateNewPlatListAsync(justCreated);
            }
        }

        private async Task RemovePlaylist(Entities.Playlist playlist)
        {
            var form = ModalService.Show<ConfirmChoice>($"Are you sure you want to remove \"{playlist.Title}\"");
            var result = await form.Result;

            if (!result.Cancelled)
            {
                await PlaylistModel.RemovePlayListAsync(playlist);

                Playlists.Remove(playlist);
                Console.WriteLine("Playlist removed ");
                StateHasChanged();
            }
        }


        protected async override Task OnInitializedAsync()
        {
            Playlists = await PlaylistModel.GetAllPlaylistsForUserAsync(CustomAuthenticationStateProvider.cachedUser);
            // StateHasChanged();
        }

        private void LoadPlaylist(Entities.Playlist playlist)
        {
            PlaylistModel.CurrentPlaylist = playlist;
            NavigationManager.NavigateTo("/Playlist");
        }
    }
}