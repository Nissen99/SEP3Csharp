using System;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Blazor.Util.Playstate
{
    public class PausedPost5 : IPlaystate
    {
        
        private IWavePlayer waveOut;
        private Mp3FileReader fileReader;
        private PlaystateContext playstateContext;
        public PlaybackState State
        {
            get { return PlaybackState.Paused; }
        }
        
        public PausedPost5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            fileReader = playstateContext.FileReader;
            init();
        }

        private void init()
        {
            waveOut.Pause();
        }

        public async Task<bool> PlayPreviousSong()
        {
            fileReader.CurrentTime = TimeSpan.Zero;
            playstateContext.CurrentState = new PlayingSub5(playstateContext);
            return false;
        }

        public async Task TogglePlayPause()
        {
            playstateContext.CurrentState = new PlayingPost5(playstateContext);
        }
    }
}