using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Model.PlayModel;
using Entities;
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
            set { throw new NotImplementedException(); }
        }
        private PlaystateContext playstateContext;
        public PlayingSub5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            fileReader = playstateContext.FileReader;
            Init();
            Task.Factory.StartNew(elapsedTime);
        }

        private async Task Init()
        {
            fileReader.CurrentTime = TimeSpan.Zero;

            if(waveOut != null) waveOut.Dispose();
            waveOut.Init(fileReader);
            waveOut.Play();

        }

        private async Task elapsedTime()
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