using System.Threading.Tasks;
using Blazor.Model.PlaylistModel;
using Entities;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class PlaylistDisplay
    {
        [Inject]
        public IPlayListModel PlaylistModel { get; set; }
        [Parameter]
        public string PlaylistToDisplay { get; set; }

        private Playlist currentPlaylist;
    
        protected override async Task OnInitializedAsync()
        {
            currentPlaylist = await PlaylistModel.GetPlaylistFromIdAsync(int.Parse(PlaylistToDisplay));
            StateHasChanged();
        }
    }
}