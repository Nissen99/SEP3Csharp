using System.Collections.Generic;

namespace Entities
{
    public class Playlist
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public IList<Song> Songs { get; set; }


        public Playlist()
        {
            Songs = new List<Song>();
        }
    }
    
    
    
}