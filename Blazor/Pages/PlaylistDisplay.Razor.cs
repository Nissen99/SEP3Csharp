using System.Threading.Tasks;
using Blazor.Model.PlaylistModel;
using Entities;
using Microsoft.AspNetCore.Components;
/*
 * Bruges til at fremvise en bestemt playliste brugeren ønsker at inspiserer.
 *
 * Playlisten der fremvises bliverbestemt gennem URI til siden.
 * Her søges der efter playlisten med et kald til modellen
 */
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