using System;
using System.Threading.Tasks;
using Blazor.Model;
using Blazor.Model.PlayModel;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Blazor.Shared
{
    public partial class AudioPlayer : ComponentBase
    {
        [Inject] public IPlayModel Play { get; set; }
        [Inject] public IModalService ModalService { get; set; }

        public double progressValuePercentage { get; set; }
        public int pVP { get; set; }
        public int volume { get; set; } = 30;
        private string songTitle = "";
        private string artistTitle = "";
        private bool isPlaying = false;
        private ElementReference progress;
        
        private string currentDuration = "00:00:00";
        private double progressValue;
        private string totalDuration = "00:00:00";
        private Song currentSong;

        protected override async Task OnInitializedAsync()
        {
            Play.Context.UpdatePlayState = () => updatePlayState();
            Play.Context.ProgressBarUpdate = () => updateProgressBar();
        }
        private async Task TogglePlay()
        {
            await Play.PlayPauseToggleAsync();
        }
        private async Task previousSong()
        {
            await Play.PlayPreviousSong();
        }

        private async Task nextSong()
        {
            await Play.PlayNextSongAsync();
        }

       
        private async Task updatePlayState()
        { 
            isPlaying = Play.Context.IsPlaying;
            currentSong = await Play.GetCurrentSongAsync();
            songTitle = currentSong.Title;
            artistTitle = currentSong.Artists[0].Name; //Giver kun første artist på listen - skal flyttes ud i modellen.
            TimeSpan totalDurationSpan = new TimeSpan(0, currentSong.Duration / 60, currentSong.Duration % 60);
            totalDuration = totalDurationSpan.ToString();
            StateHasChanged();
            
        }
        private async Task updateProgressBar()
        {
            progressValue = await Play.Context.UpdateProgressBar();
            progressValuePercentage = progressValue / currentSong.Duration * 100;
            pVP = (int) progressValuePercentage;
            TimeSpan currentDurationSpan = new TimeSpan(0, (int)(currentSong.Duration * progressValuePercentage / 100 / 60), (int)(currentSong.Duration * progressValuePercentage / 100 % 60));
            currentDuration = currentDurationSpan.ToString();
            await InvokeAsync(() => StateHasChanged());
            
        }
        
        private async Task progressBarClicked(MouseEventArgs mouseEventArgs)
        {
            float returnHack = 1;
            returnHack = await JS.InvokeAsync<float>("getProgress", mouseEventArgs);
            await Play.PlayFromAsync(returnHack);
        }
        
        private async Task volumeClicked(MouseEventArgs arg)
        {
            volume = await JS.InvokeAsync<int>("getVolume", arg);
            await Play.SetVolumeAsync(volume);
        }


    }
}