using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Util;

/*
 * Denne klasse står for håndtering af Album information
 */
namespace Domain.Album
{
    public class AlbumService : IAlbumService
    {
        private IAlbumNetworking albumNetworking;
        
        public AlbumService(IAlbumNetworking albumNetworking)
        {
            this.albumNetworking = albumNetworking;
        }
        public async Task<IList<Entities.Album>> SearchForAlbums(string title)
        {
            if (!InputValidator.CheckForAlbum(title)) throw new ArgumentException("No property found");
            return await albumNetworking.SearchForAlbums(title);

        }

        public async Task<IList<Entities.Album>> GetAllAlbumsAsync()
        {
            return await albumNetworking.GetAllAlbumsAsync();
        }
    }
}