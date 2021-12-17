using Entities;
 /*
  * Denne klasse er en hjælpe klasse som består af samtlige Valideringer. Alle metoder er statiske og returnerer en bool.
  */
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
        
        public static bool ValidateUser(Entities.User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return false;
            }

            return true;
        }


        public static bool CheckForAlbum(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return false;
            }

            return true;
        }

        public static bool CheckForArtist(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return true;
        }

        public static bool CheckPlaylistId(int playlistId)
        {
            if (playlistId <= 0)
            {
                return false;
            }

            return true;
        }

        public static bool CheckMp3(Mp3 mp3)
        {
            if (mp3 == null || mp3.Data == null || mp3.Data.Length == 0)
            {
               
                return false;
            }

            return true;
        }

        public static bool CheckFilterInput(string[] args)
        {
            if (args == null || args.Length != 2 || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
            {
                return false;
            }

            return true;
        }

        public static bool CheckForSongTitle(string songTitle)
        {
            if (string.IsNullOrEmpty(songTitle))
            {
                return false;
            }

            return true;
        }
    }
}