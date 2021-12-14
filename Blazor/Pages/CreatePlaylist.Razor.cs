using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

/*
 * Popup brugt til at lave playlister, brugeren udfylder playliste title
 * Hvis brugeren vælger Confirm returneres playlisten
 * Hvis brugeren cancler returneres intet
 */

namespace Blazor.Pages
{
    public partial class CreatePlaylist:ComponentBase
    {
        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }

        private string Title;
        private string errorMessage;
        private Entities.Playlist Playlist = new();


        public async Task PerformCreatePlaylist()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(Playlist));
        }
    
        private async Task Cancel()
        {
            await BlazoredModal.CancelAsync();
        }
    }
}