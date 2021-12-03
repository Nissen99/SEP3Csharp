using Entities;

namespace Domain.Util
{
    public class InputValidator
    {
        /*
         * ReleaseYear sat på bagrund af første lydoptagelse
         * (https://videnskab.dk/kultur-samfund/6-december-verdens-forste-lydoptagelse)
         */
        public static bool CheckSongValidWithoutMp3(Song song)
        {
            if (string.IsNullOrEmpty(song.Title) || song.ReleaseYear < 1869 || song.Album == null ||
                song.Artists == null || song.Artists.Count == 0)
            {
                return false;
            }

            return true;
        }


        public static bool CheckPlaylist(Entities.Playlist playlist)

        {
            if (string.IsNullOrEmpty(playlist.Title) || playlist.User == null)
            {
                return false;
            }

            return true;
        }
    }
}