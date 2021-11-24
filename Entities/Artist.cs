using System;

namespace Entities
{
    [Serializable]
    public class Artist
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
    }
}