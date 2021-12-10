using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    [Serializable]

    public class Song
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public IList<Artist> Artists { get; set; }
        public Album Album { get; set; }
        public int ReleaseYear { get; set; }
        
        public Song()
        {
            Artists = new List<Artist>();
        }
    }
}