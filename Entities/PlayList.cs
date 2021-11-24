﻿using System.Collections.Generic;

namespace Entities
{
    public class PlayList
    {
        public int PlaylistID { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public List<Song> Songs { get; set; }
    }
}