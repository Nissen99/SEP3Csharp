using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Authentication;
using Blazor.Model.PlaylistModel;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class AddToPlaylist : ComponentBase
    {
        [Inject] private IPlayListModel PlayListModel { get; set; }
        private IList<Entities.Playlist> playlists;
        [CascadingParameter] public BlazoredModalInstance BlazoredModal { get; set; }


        protected async override Task OnInitializedAsync()
        {
            playlists = await PlayListModel.GetAllPlaylistsForUserAsync(CustomAuthenticationStateProvider.cachedUser);
            StateHasChanged();
        }

        private async void choosePlaylist(Entities.Playlist playlist)
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(playlist));
        }

        private async Task onCancel()
        {
            await BlazoredModal.CancelAsync();
        }
    }
}