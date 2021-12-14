using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1.Library;

/*
 * Klassen fungere gateway fra mvvm del til resten af systemet, her gennem kald til Client
 */
namespace Blazor.Model.LibraryModel
{
    public class LibraryModel : ILibraryModel
    {
        private ILibraryNetworkClient libraryNetworkClient;


        public LibraryModel(ILibraryNetworkClient libraryNetworkClient)
        {
            this.libraryNetworkClient = libraryNetworkClient;
        }

        public async Task<IList<Song>> GetAllSongs()
        {
            return await libraryNetworkClient.GetAllSongs();
        }

    }
}