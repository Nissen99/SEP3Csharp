using System;
using System.Threading.Tasks;
using Blazor.Model.PlayModel;
using NAudio.Wave;

/*
 * Denne klasse er en af tilstandene for musikafspilleren.
 */
namespace Blazor.Util.Playstate
{
    public class PlayingPost5 : IPlaystate
    {
        private IPlayModel playModel;
        private IWavePlayer waveOut;
        private Mp3FileReader fileReader;
        
        public PlaybackState State
        {
            get { return PlaybackState.Playing; }
        }
        private PlaystateContext playstateContext;
        public PlayingPost5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            fileReader = playstateContext.FileReader;
            init();
        }

        private void init()
        {
            waveOut.Play();
        }

        
        public async Task<bool> PlayPreviousSong()
        {
            fileReader.CurrentTime = TimeSpan.Zero;
            playstateContext.CurrentState = new PlayingSub5(playstateContext);
            return false;
        }

        public async Task TogglePlayPause()
        {
            playstateContext.CurrentState = new PausedPost5(playstateContext);
        }
    }
}