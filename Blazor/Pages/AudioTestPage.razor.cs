using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Model;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class AudioTestPage : ComponentBase
    {
        [Inject] public IAudioTestModel Model { get; set; }
        [Inject] public IPlayModel Play { get; set; }
        [Inject] public IModalService ModalService { get; set; }
        private IList<Song> songs;
        private Song currentSong;
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;
            
            songs = await Model.GetAllSongs();
                Play.CurrentPlaylist = songs;
                Console.WriteLine("DONE");
                StateHasChanged();
            
        }

        private async Task playSong(ChangeEventArgs e)
        {
            try
            {
                //Quick fix, skal aligevel udskiftes, gør intet når man vælger "Select Below"
                if ("Select Below".Equals((string) e.Value))
                {
                    return;
                }

                currentSong = songs.First(t => t.Id == int.Parse((string) e.Value));
                await Play.PlaySongAsync(currentSong);
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                ModalService.Show<Popup>("Error");
            }
        }
    }
}