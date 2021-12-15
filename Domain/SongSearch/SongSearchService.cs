using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Util;
using Entities;
/*
 * Denne klasse håndterer sang søgnings relaterede handlinger.
 */
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
            if (!InputValidator.CheckFilterInput(args)) throw new ArgumentException("No property found");
            switch (args[0])
            {
                case "Title":
                    return await GetSongFromTitle(args[1]);

                case "Artist":
                    return await GetSongFromArtist(args[1]);

                case "Album":
                    return await GetSongFromAlbum(args[1]);
                
                default:
                    throw new Exception("You have tried to search " + args[0] + " which is not valid");
            }
            
        }

        private async Task<IList<Song>> GetSongFromArtist(string artistName)
        {
            if (!InputValidator.CheckForArtist(artistName))throw new ArgumentException("No property found");
            return await songSearchNetworking.GetSongsByArtistNameAsync(artistName);
        }

        private async Task<IList<Song>>  GetSongFromAlbum(string albumTitle)
        {
            if (!InputValidator.CheckForAlbum(albumTitle)) throw new ArgumentException("No property found");
            return await songSearchNetworking.GetSongsByAlbumTitleAsync(albumTitle);
            
        }

        private async Task<IList<Song>> GetSongFromTitle(string songTitle)
        {
            if (!InputValidator.CheckForSongTitle(songTitle))throw new ArgumentException("No property found");
            return await songSearchNetworking.GetSongsByTitleAsync(songTitle);
            
        }
    }
}