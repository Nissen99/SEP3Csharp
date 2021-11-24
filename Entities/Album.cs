using System;

namespace Entities
{
    [Serializable]
    public class Album
    {
        public int Id { get; set; }
        public string AlbumTitle { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}