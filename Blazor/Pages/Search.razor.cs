using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Model;
using Blazor.Model.AudioTestModel;
using Blazor.Model.PlayModel;
using Blazor.Model.SongSearchModel;
using Entities;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class Search : ComponentBase
    {
        [Inject] private IAudioTestModel AudioTestModel { get; set; }
        [Inject] private ISongSearchModel SongSearchModel { get; set; }
        [Inject] private IPlayModel PlayModel { get; set; }
        
        
        public bool ShowTrack { get; set; }


        
        private IList<Song> songsToShow;
        private string filterOption = "Title";
        private string searchField = "";
        
        protected override async Task OnInitializedAsync()
        {
            songsToShow = await AudioTestModel.GetAllSongs();
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
                songsToShow = await AudioTestModel.GetAllSongs();
            }
            
            PlayModel.CurrentPlaylist = songsToShow;
            StateHasChanged();
        }
    }
}