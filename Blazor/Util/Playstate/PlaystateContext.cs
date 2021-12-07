using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Model.PlayModel;
using Entities;
using NAudio.Wave;
using SocketsT1_T2.Tier1;
using SocketsT1_T2.Tier1.Song;

namespace Blazor.Util.Playstate
{
    public class PlaystateContext : IPlaystateContext
    {
        public Mp3FileReader FileReader { get; private set; }
        private MemoryStream ms;
        //private BufferedWaveProvider bufferedWaveProvider;
        public IWavePlayer WaveOut { get; set; }
        private IPlayNetworkClient client;
        public IPlaystate CurrentState { get; set; }
        
        
        public Action UpdatePlayState { get; set; }
        public Action ProgressBarUpdate { get; set; }
        public PlaystateContext(IPlayNetworkClient client)
        {
            WaveOut = new WaveOutEvent();
            WaveOut.Volume = 0.3f;
            this.client = client;
        }
        
        public void StopPlaying()
        {
            if (WaveOut != null && FileReader != null)
            {
                WaveOut.Stop();
                WaveOut.Dispose();
                FileReader.Dispose();
            }
        }
        
        public async Task<double> UpdateProgressBar()
        {
            return FileReader.CurrentTime.TotalSeconds;
        }
        
        public async Task PlaySong(Song song)
        {
            //State = new Stopped(this);
            var watch = new Stopwatch();
            watch.Start();
            byte[] songToPlay = await client.PlaySong(song);
            watch.Stop();
            Console.WriteLine("Time taken to play song: " + watch.Elapsed.ToString(@"m\:ss\.fff"));
            MemoryStream ms = new MemoryStream(songToPlay);
            FileReader = new Mp3FileReader(ms);
            Console.WriteLine(FileReader == null);
            CurrentState = new PlayingSub5(this);
            UpdatePlayState.Invoke();
            Thread t1 = new Thread(() =>
            {
                while (true)
                {
                    ProgressBarUpdate.Invoke();
                    Thread.Sleep(500);
                }
            });
            t1.Start();
            

        }

        public async Task PlayFromAsync(int sec)
        {
            FileReader.CurrentTime = TimeSpan.FromSeconds(sec);
            if (sec < 5)
            {
                CurrentState = new PlayingSub5(this);
            }
            else
            {
                CurrentState = new PlayingPost5(this);
            }
            UpdatePlayState.Invoke();
        }

        public async Task<bool> PlayPreviousAsync()
        {
            return await CurrentState.PlayPreviousSong();
        }

        public async Task TogglePlayPause()
        {
            await CurrentState.TogglePlayPause();
            UpdatePlayState.Invoke();
        }
    }
}