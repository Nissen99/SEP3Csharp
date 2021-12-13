using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Model.PlayModel;
using Entities;
using NAudio.Wave;

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
            set { throw new NotImplementedException(); }
        }
        private PlaystateContext playstateContext;
        public PlayingPost5(PlaystateContext playstateContext)
        {
            this.playstateContext = playstateContext;
            waveOut = playstateContext.WaveOut;
            fileReader = playstateContext.FileReader;
            Init();
        }

        private void Init()
        {
            waveOut.Play();
        }

        
        public async Task<bool> PlayPreviousSong()
        {
            playstateContext.CurrentState = new PlayingSub5(playstateContext);

            return false;
        }

        public async Task TogglePlayPause()
        {
            playstateContext.CurrentState = new PausedSub5(playstateContext);
        }
    }
}