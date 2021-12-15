using System;
using System.IO;
using System.Threading.Tasks;
using Domain.Util;
using Entities;
using NAudio.Wave;
/*
 * Den klasse sørger for håndtering sange i systemet.
 * Her sikres at længden på en sang bliver tilføjet til Song objektet.
 * Der bliver både sendt et object til skal lægges i databasen og et object der skal gemmes som en fil.
 */
namespace Domain.SongManage
{
    public class SongManageService : ISongManageService
    {
        private ISongManageNetworking songManageNetworking;

        public SongManageService(ISongManageNetworking songManageNetworking)
        {
            this.songManageNetworking = songManageNetworking;
        }

        //Denne metode bærer præg af at vide at dataserver både gemmer i en database og gemmer en fil. 
        //I tre tier burde dette ikke ske.
        public async Task AddNewSongAsync(Song newSong, Mp3 mp3)
        {
            if (!InputValidator.CheckSongValidWithoutMp3(newSong) || !InputValidator.CheckMp3(mp3)) 
                throw new ArgumentException("Some Property not found");
            
            using MemoryStream ms = new MemoryStream(mp3.Data);
           
            using Mp3FileReader fileReader = new Mp3FileReader(ms);

            Console.WriteLine("længe af mp3 " + mp3.Data.Length);

            int duration = (int) fileReader.TotalTime.TotalSeconds;
            newSong.Duration = duration;
            
            Song newSongWithCorrectId = await songManageNetworking.AddNewSongAsync(newSong);
            
            await songManageNetworking.UploadMp3(newSongWithCorrectId, mp3);
        }
        

        public async Task RemoveSongAsync(Song songToRemove)
        {
            if (!InputValidator.CheckSongValidWithoutMp3(songToRemove))
                throw new ArgumentException("No property found");
            await songManageNetworking.RemoveSongAsync(songToRemove);
        }
    }
}