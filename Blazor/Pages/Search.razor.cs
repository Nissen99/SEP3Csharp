using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Model.LibraryModel;
using Blazor.Model.PlayModel;
using Blazor.Model.SongSearchModel;
using Entities;
using Microsoft.AspNetCore.Components;
/*
 * Brugt til at modtage bruger input, her giver brugeren input om hvad der skal søges på, og hvilken katagoi af søgning
 * det skal være. Dette sendes til Modellen hvor det håndteres
 */
namespace Blazor.Pages
{
    public partial class Search : ComponentBase
    {
        [Inject] private ILibraryModel LibraryModel { get; set; }
        [Inject] private ISongSearchModel SongSearchModel { get; set; }
        [Inject] private IPlayModel PlayModel { get; set; }
        
        
        private IList<Song> songsToShow;
        private string filterOption = "Title";
        private string searchField = "";
        
        protected override async Task OnInitializedAsync()
        {
            songsToShow = await LibraryModel.GetAllSongs();
            PlayModel.CurrentPlaylist = songsToShow;
        }

        private async void Filter()
        {
            songsToShow = null;
            if (!string.IsNullOrEmpty(searchField))
            {
                songsToShow = await SongSearchModel.GetSongsByFilterAsync(filterOption, searchField);
            }
            else
            {
                songsToShow = await LibraryModel.GetAllSongs();
            }
            
            PlayModel.CurrentPlaylist = songsToShow;
            StateHasChanged();
        }
    }
}