using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using NAudio.Wave;
using SocketsT1_T2.Tier1;
using SocketsT1_T2.Tier1.Song;

namespace Blazor.Model.PlayModel
{
    public class PlayModel : IPlayModel
    {
        private IPlayNetworkClient playClient;
        private Mp3FileReader fileReader;
        private MemoryStream ms;
        private BufferedWaveProvider bufferedWaveProvider;
        private IWavePlayer waveOut;
        private IList<Song> previouslySongs = new List<Song>();
        private Song currentSong;

        public bool IsPlaying
        {
            get { return waveOut.PlaybackState == PlaybackState.Playing; }
        }

        public Action UpdatePlayState { get; set; }
        public Action ProgressBarUpdate { get; set; }
        public IList<Song> CurrentPlaylist { get; set; }

        public PlayModel(IPlayNetworkClient playClient)
        {
            this.playClient = playClient;
            waveOut = new WaveOutEvent();
            waveOut.Volume = 0.3f;

        }

        public async Task PlaySongAsync(Song song)
        {
            var watch = new Stopwatch();
            watch.Start();

            waveOut.Dispose();
            
            byte[] songToPlay = await playClient.PlaySong(song);
            watch.Stop();
            Console.WriteLine("Time taken to play song: " + watch.Elapsed.ToString(@"m\:ss\.fff"));
            MemoryStream ms = new MemoryStream(songToPlay);
            fileReader = new Mp3FileReader(ms);
            
            waveOut.Init(fileReader);
            waveOut.Play();
            currentSong = song;
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

            if (previouslySongs.Count == 0 || previouslySongs[^1].Id != song.Id)
            {
                previouslySongs.Add(song);
            }
            
        }

       
        

        public async Task PlayPauseToggleAsync()
        {
            Console.WriteLine("Current playstate: " + waveOut.PlaybackState);
            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Pause();
            }
            else
            {
                waveOut.Play();
            }

            UpdatePlayState.Invoke();
        }

        public async Task PlayFromAsync(float progress)
        {
            int sec = (int)(currentSong.Duration * (progress/100));
            waveOut.Stop();
            fileReader.CurrentTime = TimeSpan.FromSeconds(sec);
            waveOut.Play();
            UpdatePlayState.Invoke();
        }

        public async Task SetVolumeAsync(int percentage)
        {
            float toSet = (float) percentage / 100;
            waveOut.Volume = toSet;
        }
        
        public async Task PlayPreviousSong()
        {
            if (fileReader.CurrentTime.TotalSeconds < 5 && previouslySongs.Count >= 2)
            {
                previouslySongs.RemoveAt(previouslySongs.Count - 1);
                Console.WriteLine(previouslySongs.Count);
                await PlaySongAsync(previouslySongs[^1]);
            }
            else
            {
                await PlayFromAsync(0);
            }
        }

        
        
        public async Task PlayNextSongAsync()
        {
            int index = CurrentPlaylist.IndexOf(currentSong);
            if (index == CurrentPlaylist.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            Console.WriteLine(index);
            await PlaySongAsync(CurrentPlaylist[index]);
        }

        public string UpdateDisplay()
        {
            if (currentSong.Artists.Count < 2)
            {
                return currentSong.Title + " " + currentSong.Artists[0].Name + currentSong.Duration;
            }
            else
            {
                return currentSong.Title + " Various Artists";
            }
            
        }

        public void StopPlaying()
        {
            if (waveOut != null && fileReader != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                fileReader.Dispose();
            }
        }

        public async Task<double> UpdateProgressBar()
        {
            return fileReader.CurrentTime.TotalSeconds;
        }

        public async Task<Song> GetCurrentSongAsync()
        {
             return currentSong;
        }
    }
}