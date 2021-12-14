using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;

/*
 * popup som håndtere bruger input når der skal findes artist
 *
 * Her kan brugeren enden vælger et eksiterende artist eller lave et nyt
 */ 

namespace Blazor.Pages
{
    public partial class NewOrExistingArtist
    {
        
        [CascadingParameter] public BlazoredModalInstance BlazoredModal { get; set; }

        
        private string artistName = "";
        private string searchName = "";
        private string displayToUser = "Search for Artist";


        private IList<Artist> searchedForArtists = new List<Artist>();

        private async Task addNewArtist()
        {
            Artist justCreatedArtist = new Artist() {Name = artistName};
            await BlazoredModal.CloseAsync(ModalResult.Ok(justCreatedArtist));
        }

        private async Task chooseExistingArtist(Artist artist)
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(artist));
        }

        private async Task searchForArtist()
        {
            if (!string.IsNullOrEmpty(searchName))
            {
                searchedForArtists = await ArtistModel.SearchForArtists(searchName);
                if (searchedForArtists.Count == 0)
                {
                    displayToUser = "No artist found matching";
                }
            }
            else
            {
                searchedForArtists = await ArtistModel.GetAllArtistAsync();
            }
            Console.WriteLine(searchedForArtists.Count);

            StateHasChanged();
        }

        private async Task Cancel()
        {
            await BlazoredModal.CancelAsync();
        }

        
    }
}