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
            set { throw new NotImplementedException(); }
        }
        
        public PausedPost5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            fileReader = playstateContext.FileReader;
            Init();
        }

        private void Init()
        {
            waveOut.Pause();
        }

        public async Task<bool> PlayPreviousSong()
        {
            fileReader.CurrentTime = TimeSpan.Zero;
            return false;
        }

        public async Task TogglePlayPause()
        {
            playstateContext.CurrentState = new PlayingPost5(playstateContext);
        }
    }
}