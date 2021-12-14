using System;
using System.Threading.Tasks;

using NAudio.Wave;

namespace Blazor.Util.Playstate
{
    public class PausedSub5 : IPlaystate
    {
        private IWavePlayer waveOut;
        
        public PlaybackState State
        {
            get { return PlaybackState.Paused; }
        }

        private PlaystateContext playstateContext;

        public PausedSub5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            init();
        }

        private void init()
        {
            waveOut.Pause();
        }
        

        public async Task<bool> PlayPreviousSong()
        {
            return true;
        }

        public async Task TogglePlayPause()
        {
            playstateContext.CurrentState = new PlayingSub5(playstateContext);
        }
    }
}