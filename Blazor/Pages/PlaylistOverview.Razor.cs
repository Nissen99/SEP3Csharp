using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Authentication;
using Blazor.Model.PlaylistModel;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
/*
 * Bruges til at fremvise alle playlister brugeren har oprettet.
 *
 * Her søges efter disse playlister med et kald til modellen
 *
 * Hvis det ænskes at inspisere en playliste, kan dette gøres ved at åbne en ny side hvor den bestemte playlistes id
 * sendes med i URI'en
 */
namespace Blazor.Pages
{
    public partial class PlaylistOverview : ComponentBase
    {
        [Inject] public IPlayListModel PlaylistModel { get; set; }
        [Inject] private IModalService ModalService { get; set; }
        public IList<Entities.Playlist> Playlists { get; set; }

        
        protected override async Task OnInitializedAsync()
        {
            Playlists = await PlaylistModel.GetAllPlaylistsForUserAsync(CustomAuthenticationStateProvider.cachedUser);
        }

        public async Task PerformCreatePlaylist()
        {
            var form = ModalService.Show<CreatePlaylist>("Create new Playlist");
            var result = await form.Result;
            
            if (!result.Cancelled)
            {
                Entities.Playlist justCreated = (Entities.Playlist) result.Data;
                justCreated.User = CustomAuthenticationStateProvider.cachedUser;

                if (string.IsNullOrEmpty(justCreated.Title))
                {
                    ModalService.Show<Popup>("Please type a playlist Title");
                    return;
                }
                await PlaylistModel.CreateNewPlaylistAsync(justCreated);
                Playlists= await PlaylistModel.GetAllPlaylistsForUserAsync(CustomAuthenticationStateProvider.cachedUser);
                StateHasChanged();
                
                

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


   
        private void LoadPlaylist(Entities.Playlist playlist)
        {
            NavigationManager.NavigateTo($"/PlaylistDisplay/{playlist.Id}");
        }
    }
}