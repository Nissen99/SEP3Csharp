using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1.Library;


/*
 * Klassen fungere gateway fra mvvm del til resten af systemet, her gennem kald til Client
 */

namespace Blazor.Model.SongManagerModel
{
    public class SongManageModel : ISongManageModel
    {
        private ILibraryNetworkClient libraryClient;

        public SongManageModel(ILibraryNetworkClient libraryClient)
        {
            this.libraryClient = libraryClient;
        }

        public async Task AddNewSongAsync(Song newSong, Mp3 mp3)
        {
            await libraryClient.AddNewSongAsync(newSong, mp3);
        }

        public async Task RemoveSongAsync(Song song)
        {
            await libraryClient.RemoveSongAsync(song);
        }
    }
}