using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

/*
 * Denne klasse står for håndering af at modtage musik biblioteket i systemet.
 */
namespace Domain.Library
{
    public class LibraryService : ILibraryService
    {
        private ILibraryNetworking libraryNetworking;

        public LibraryService(ILibraryNetworking libraryNetworking)
        {
            this.libraryNetworking = libraryNetworking;
        }

        public async Task<IList<Song>> GetAllSongsAsync()
        {
            return await libraryNetworking.GetAllSongsAsync();

        }
    }
}