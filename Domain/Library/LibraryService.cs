﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Library;
using Entities;
using NAudio.Wave;


    public class LibraryService : ILibraryService
    {
        private IList<byte[]> songsByte = new List<byte[]>();
        private IList<Song> songList = new List<Song>();
        private ILibraryNetworking libraryNetworking;
        
        public LibraryService(ILibraryNetworking libraryNetworking)
        {
            this.libraryNetworking = libraryNetworking;
        }
        public async Task<IList<byte[]>> GetAllMP3Async()
        {
            return await libraryNetworking.GetAllMP3();
        }

        public async Task SendSongListToDBAsync()
        {
            string path = @"../../../../Domain/Audio/tempfile.mp3";
            
            songsByte = await GetAllMP3Async();
            
            //Builder Pattern!!!! GO go implement
            foreach (byte[] MP3Byte in songsByte)
            {
                using (FileStream byteToMp3 = File.Create(path))
                {
                    await byteToMp3.WriteAsync(MP3Byte, 0, MP3Byte.Length);
                }
                
                TagLib.File file = TagLib.File.Create(path);
                string title = file.Tag.Title;
                string albumName = file.Tag.Album;
                string[] artistName = file.Tag.Performers;
                uint year = file.Tag.Year;
                int duration = (int)file.Properties.Duration.TotalSeconds;
                
                Artist artist = new Artist();
                artist.Name = artistName[0]; //Lav det her om, lige nu får den artistName som string
                
                Song song = new Song()
                {
                    Title = title,
                    Album = new Album() {Title = albumName},
                    Artists = new List<Artist>() {artist},
                    Duration = duration,
                    ReleaseYear = (int)year,
                    Mp3 = MP3Byte
                };
                
                songList.Add(song);
                
            }
            
            await libraryNetworking.PostAllSongs(songList);
            Console.WriteLine("Post Done");

        }
    }
