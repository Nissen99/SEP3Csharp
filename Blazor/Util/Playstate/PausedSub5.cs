using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Model.PlayModel;
using Entities;
using NAudio.Wave;

namespace Blazor.Util.Playstate
{
    public class PausedSub5 : IPlaystate
    {
        private IWavePlayer waveOut;
        
        public PlaybackState State
        {
            get { return PlaybackState.Paused; }
            set { throw new NotImplementedException(); }
        }

        private PlaystateContext playstateContext;

        public PausedSub5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            Init();
        }

        private void Init()
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