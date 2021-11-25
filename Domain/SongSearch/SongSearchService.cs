using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;

namespace Domain.SongSearch
{
    public class SongSearchService : ISongSearchService
    {
        private ISongSearchNetworking songSearchNetworking;

        public SongSearchService(ISongSearchNetworking songSearchNetworking)
        {
            this.songSearchNetworking = songSearchNetworking;
        }

        public async Task<IList<Song>> GetSongsByFilterJsonAsync(string[] args)
        {
            switch (args[0])
            {
                case "Title":
                    return await getSongFromTitle(args[1]);

                case "Artist":
                    return await getSongFromArtist(args[1]);

                case "Album":
                    return await getSongFromAlbum(args[1]);
                
                default:
                    throw new Exception("You have tried to search " + args[0] + " which is not valid");
            }
            
        }

        private async Task<IList<Song>> getSongFromArtist(string artistName)
        {
            return await songSearchNetworking.GetSongsByArtistNameAsync(artistName);
        }

        private async Task<IList<Song>>  getSongFromAlbum(string albumTitle)
        {
            return await songSearchNetworking.GetSongsByAlbumTitleAsync(albumTitle);
            
        }

        private async Task<IList<Song>> getSongFromTitle(string songTitle)
        {
            return await songSearchNetworking.GetSongsByTitleAsync(songTitle);
            
        }
    }
}