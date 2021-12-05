using System;
using System.IO;
using System.Threading.Tasks;
using Blazor.Model.SongManagerModel;
using Blazored.Modal;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Blazor.Pages
{
    public partial class AddSong : ComponentBase{
        
        
        [Inject] public IModalService ModalService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public ISongManageModel SongManageModel { get; set; }
        private Song newSong = new Song();
        private Mp3 newMp3 = new Mp3();
        private int defaultYear = @DateTime.Now.Year;


        private async void LoadFile(InputFileChangeEventArgs e)
        {
            if (!e.File.Name.EndsWith(".mp3"))
            {
                resetMp3();
                ModalService.Show<Popup>("Not right File type, please select an mp3");
                return;
            }

            using MemoryStream ms = new MemoryStream(80000000);

            await e.File.OpenReadStream(80000000).CopyToAsync(ms);
            newMp3.Data = ms.ToArray();

        }

        private async Task AddNewSong()
        {
            newSong.ReleaseYear = defaultYear;

            if (string.IsNullOrEmpty(newSong.Title) || newSong.Album == null || newSong.Artists.Count == 0)
            {
                ModalService.Show<Popup>("Something not sat, please make sure everything is sat");
                newMp3.Data = Array.Empty<byte>();

                return;
            }

            Console.WriteLine($"Title: {newSong.Title}");
            await SongManageModel.AddNewSongAsync(newSong, newMp3);
            NavigationManager.NavigateTo("/Search");

        }

        private async Task AddArtist()
        {
            ModalParameters parameters = new ModalParameters();
            parameters.Add("Song", newSong);
            var form = ModalService.Show<NewOrExistingArtist>("Make new artist or choose existing", parameters);
            var result = await form.Result;

            if (!result.Cancelled)
            {
                Artist justCreated = (Artist) result.Data;
                newSong.Artists.Add(justCreated);
                StateHasChanged();
            }
        }

        private void removeArtist(Artist artist)
        {
            newSong.Artists.Remove(artist);
            StateHasChanged();
        }

        private async Task AddAlbum()
        {
            ModalParameters parameters = new ModalParameters();
            parameters.Add("Song", newSong);
            var form = ModalService.Show<NewOrExistingAlbum>("Make new album or choose existing", parameters);
            var result = await form.Result;

            if (!result.Cancelled)
            {
                Album justCreated = (Album) result.Data;
                newSong.Album = justCreated;
                StateHasChanged();
            }
        }

        private void resetMp3()
        {
            newMp3.Data = Array.Empty<byte>();
        }
    }
}