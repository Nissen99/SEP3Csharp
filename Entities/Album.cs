using System;

namespace Entities
{
    [Serializable]
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }

    }
}