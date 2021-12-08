using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class NewOrExistingAlbum: ComponentBase
    {
        
        [CascadingParameter] public BlazoredModalInstance BlazoredModal { get; set; }

    
        [Parameter]
        public Song Song { get; set; }

        private string albumTitle = "";
        private string searchTitle = "";
        private string displayToUser = "Search for Albums";

        private IList<Album> searchedForAlbums = new List<Album>();

        private async Task addNewAlbum()
        {
            Album justCreatedAlbum = new Album() {Title = albumTitle};
            await BlazoredModal.CloseAsync(ModalResult.Ok(justCreatedAlbum));
        }

        private async Task chooseExistingAlbum(Album album)
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(album));
        }

        private async Task searchForAlbum()
        {
            if (!string.IsNullOrEmpty(searchTitle))
            {
                searchedForAlbums = await AlbumModel.SearchForAlbumsAsync(searchTitle);
                if (searchedForAlbums.Count == 0)
                {
                    displayToUser = "No albums found matching";
                }
            }
            else
            {
                searchedForAlbums = await AlbumModel.GetAllAlbumsAsync();
            }
            Console.WriteLine(searchedForAlbums.Count);
            StateHasChanged();
        }

        private async Task Cancel()
        {
            await BlazoredModal.CancelAsync();
        }

    }
}