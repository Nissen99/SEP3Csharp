using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1.Song;

/*
 * Klassen fungere gateway fra mvvm del til resten af systemet, her gennem kald til Client
 */
namespace Blazor.Model.SongSearchModel
{
    public class SongSearchModel :ISongSearchModel
    {
        private ISongSearchNetworkClient searchClient;

        public SongSearchModel(ISongSearchNetworkClient searchClient)
        {
            this.searchClient = searchClient;
        }

        public async Task<IList<Song>> GetSongsByFilterAsync(string filterOption, string searchField)
        {
            string[] args = {filterOption, searchField};

            return await searchClient.GetSongsByFilterAsync(args);
            
        }
    }
}
