using System;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Blazor.Util.Playstate
{
    public class PlayingSub5 : IPlaystate
    {
        private IWavePlayer waveOut;
        private Mp3FileReader fileReader;
        public PlaybackState State
        {
            get { return PlaybackState.Playing; }
        }
        private PlaystateContext playstateContext;
        public PlayingSub5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            fileReader = playstateContext.FileReader;
            init();
            Task.Factory.StartNew(elapsedTime);
        }

        private async Task init()
        {
            if(waveOut != null) waveOut.Dispose();
            waveOut.Init(fileReader);
            waveOut.Play();
        }

        private void elapsedTime()
        {
            double time;
            do
            {
                time = fileReader.CurrentTime.TotalSeconds;
                Thread.Sleep(100);
            } while (time < 5);
            playstateContext.CurrentState = new PlayingPost5(playstateContext);
        }

       

        public async Task<bool> PlayPreviousSong()
        {
            return true;
        }

        public async Task TogglePlayPause()
        {
            playstateContext.CurrentState = new PausedSub5(playstateContext);
        }
    }
}