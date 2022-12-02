namespace MusicApp
{
    internal class Program
    {
        static List<Track> musicLibrary = new List<Track>();
        static List<Playlist> playlists = new List<Playlist>();


        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ShowMainMenu();
        }

        private static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("********GEsys Music App********\n\n");
            Console.WriteLine($"Press 1 to view PlayList or Create new PlayList\n" +
                $"\nPress 2 to Enter Music Library\n");

            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    ShowPlaylists();
                    return;
                case '2':
                    ShowMusicLibrary();
                    return;
            }

        }
        
        private static void ShowPlaylists()
        {
            Console.Clear();
            if (playlists.Count == 0)
            {
                Console.WriteLine("You do not have any playlist");
            }
            else
            {
                Console.WriteLine("Select Playlist");
                Enumerate(playlists);
            }

            Console.WriteLine("\nEnter c to create new playlist\n" +
                "\nEnter b to create new playlist\n");
            string input = Console.ReadLine();
            switch (input)
            {
                case "c":
                    Console.WriteLine("\nEnter a name for the playlist");
                    playlists.Add(new Playlist
                    {
                        name = Console.ReadLine(),
                        tracks = new List<Track>()
                    });

                    ShowPlaylists();
                    return;
                case "b":
                    ShowMainMenu();
                    return;
                default:
                    int index;
                    if (TryParseIndex(input, playlists.Count, out index))
                    {
                        ShowPlaylist(playlists[index]);
                        return;
                    }

                    ShowPlaylists();
                    return;
            }
        }

        private static void ShowPlaylist(Playlist playlist)
        {
            Console.Clear();
            if (playlist.tracks.Count == 0)
            {
                Console.WriteLine("You have not added any music to this playlist");
            }
            else
            {
                Console.WriteLine("Select Music");
                Enumerate(playlist.tracks);
            }

            Console.WriteLine("\nEnter a to add music to this playlist from the music Library\n" +
                "\nEnter b to go back\n" +
                (playlist.tracks.Count > 0 ? ("\nEnter p to play all" +
                "\nEnter s to shuffle and play" +
                "\nEnter pa to play all in alphabetical order\n") : ""));

            string input = Console.ReadLine();
            switch (input)
            {
                case "a":
                    ShowAddMusicTrackToPlaylist(playlist);
                    return;
                case "p":
                    if (playlist.tracks.Count == 0) ShowPlaylist(playlist);
                    else ShowNowPlaying(playlist.tracks, playlist.name);
                    return;
                case "s":
                    if (playlist.tracks.Count == 0) ShowPlaylist(playlist);
                    else ShowNowPlaying(ShuffleTracks(playlist.tracks), playlist.name);
                    return;
                case "pa":
                    if (playlist.tracks.Count == 0) ShowPlaylist(playlist);
                    else ShowNowPlaying(SortTracks(playlist.tracks), playlist.name);
                    return;
                case "b":
                    ShowPlaylists();
                    return;
                default:
                    int index;
                    if (TryParseIndex(input, playlist.tracks.Count, out index))
                    {
                        ShowNowPlaying(playlist.tracks, playlist.name, index);
                        return;
                    }
                    ShowPlaylist(playlist);
                    return;
            }
        }

        private static IEnumerable<Track> SortTracks(IEnumerable<Track> tracks)
        {
            Track[] sortedTracks = tracks.ToArray();
            Array.Sort(sortedTracks, (track1, track2) => track1.name.CompareTo(track2.name));
            return sortedTracks;
        }

        static Random random = new Random();
        private static IEnumerable<Track> ShuffleTracks(IEnumerable<Track> tracks)
        {
            return tracks.ToArray().OrderBy(track => random.Next());
        }

        static string play = "\u25B6", pause = "\u23F8", next = "\u23ED", prev = "\u23EE ", stop = "\u23F9 ";
        private static void ShowNowPlaying(IEnumerable<Track> tracks, string listName, int startingFrom = 0, bool paused = false)
        {
            Console.Clear();
            Console.WriteLine(
                $"Now Playing From {listName}\n" +
                "\n" +
                $"Playing {tracks.ElementAt(startingFrom).name}\n" +
                $"By {tracks.ElementAt(startingFrom).artist}\n" +
                "\n" +
                $"Tracks in {listName}"
            );


            Enumerate(tracks, 0, startingFrom);
            Console.WriteLine($"{(paused ? pause : play)} {tracks.ElementAt(startingFrom).name}");
            Enumerate(tracks, startingFrom + 1);

            Console.WriteLine(
                "\nActions\n" +
                $"p {(paused ? play : pause)}\n" +
                $"> {next}\n" +
                $"< {prev}\n" +
                $"s {stop}\n" +
                "\n" +
                "select an action to take or a track to jump to"
            );

            var input = Console.ReadLine();
            switch (input)
            {
                case "p":
                    ShowNowPlaying(tracks, listName, startingFrom, !paused);
                    return;
                case ">":
                    if (startingFrom == tracks.Count() - 1) ShowNowPlaying(tracks, listName, startingFrom, paused);
                    ShowNowPlaying(tracks, listName, startingFrom + 1, paused);
                    return;
                case "<":
                    if (startingFrom == 0) ShowNowPlaying(tracks, listName, startingFrom, paused);
                    ShowNowPlaying(tracks, listName, startingFrom - 1, paused);
                    return;
                case "s":
                    ShowMainMenu();
                    return;
                default:
                    int index;
                    if (TryParseIndex(input, tracks.Count(), out index))
                    {
                        ShowNowPlaying(tracks, listName, index);
                        return;
                    }
                    ShowNowPlaying(tracks, listName, startingFrom, paused);
                    return;
            }
        }

        private static void ShowAddMusicTrackToPlaylist(Playlist playlist)
        {
            if (musicLibrary.Count == 0)
            {
                Console.WriteLine("\nYou do not have any music in your music library");
            }
            else
            {
                Console.WriteLine("\nSelect From Music Library");
                Enumerate(musicLibrary);
            }

            Console.WriteLine("\npress a to add a new music to your music library and this playlist\n" +
                "\nEnter b to create new playlist\n");

            var input = Console.ReadLine();
            switch (input)
            {
                case ("a"):
                    playlist.tracks.Add(ShowAddMusicToMusicLibrary());
                    ShowPlaylist(playlist);
                    return;
                case ("b"):
                    ShowPlaylist(playlist);
                    return;
                default:
                    int index;
                    if (TryParseIndex(input, musicLibrary.Count, out index))
                    {
                        playlist.tracks.Add(musicLibrary[index]);
                        ShowPlaylist(playlist);
                        return;
                    }
                    ShowAddMusicTrackToPlaylist(playlist);
                    return;
            }
        }

        private static Track ShowAddMusicToMusicLibrary()
        {
            Console.WriteLine("\nEnter the name of the music");
            var name = Console.ReadLine();
            Console.WriteLine("\nEnter the name of the artist");
            var track = new Track()
            {
                name = name,
                artist = Console.ReadLine(),
            };
            musicLibrary.Add(track);
            return track;
        }

        private static void ShowMusicLibrary()
        {
            Console.Clear();
            if (musicLibrary.Count == 0)
            {
                Console.WriteLine("You do not have any music in your music library\n");
            }
            else
            {
                Console.WriteLine("Music Library\n");
                Enumerate(musicLibrary);
            }

            Console.WriteLine("\nEnter a to add music" +
                (musicLibrary.Count > 0 ? 
                "\nEnter r to remove music" +
                "\nEnter e to edit music" : "") +
                "\n" +
                "\nEnter b to go back\n ");

            var input = Console.ReadLine();
            switch (input)
            {
                case ("a"):
                    ShowAddMusicToMusicLibrary();
                    ShowMusicLibrary();
                    return;
                case ("r"):
                    if (musicLibrary.Count != 0) {
                        Console.WriteLine("\nSelect Music to Remove");
                        int indexToRemove;
                        if (TryParseIndex(Console.ReadLine(), musicLibrary.Count, out indexToRemove))
                        {
                            var track = musicLibrary[indexToRemove];
                            musicLibrary.RemoveAt(indexToRemove);
                            playlists.ForEach(playlist => playlist.tracks.Remove(track));
                        }
                    }
                    ShowMusicLibrary();
                    return;
                case ("e"):
                    if (musicLibrary.Count != 0)
                    {
                        Console.WriteLine("\nSelect Music to Edit");
                        int indexToEdit;
                        if (TryParseIndex(Console.ReadLine(), musicLibrary.Count, out indexToEdit))
                        {
                            Console.WriteLine("\nEnter the new name");
                            musicLibrary[indexToEdit].name = Console.ReadLine();
                            Console.WriteLine("\nEnter the new artist's name");
                            musicLibrary[indexToEdit].artist = Console.ReadLine();
                        }
                    }
                    ShowMusicLibrary();
                    return;
                case ("b"):
                    ShowMainMenu();
                    return;
                default:
                    int startingIndex;
                    if (TryParseIndex(input, musicLibrary.Count, out startingIndex))
                    {
                        ShowNowPlaying(musicLibrary, "Music Library", startingIndex);
                        return;
                    }
                    ShowMusicLibrary();
                    return;
            }
        }

        private static bool TryParseIndex(string input, int limit, out int index)
        {
            if (int.TryParse(input, out index) && index != 0 && index <= limit)
            {
                index--;
                return true;
            }
            return false;
        }

        private static void Enumerate(IEnumerable<Track> tracks, int start = 0, int limit = int.MaxValue)
        {
            for (int i = start; i < Math.Min(tracks.Count(), limit); i++)
            {
                Console.WriteLine($"{i + 1} {tracks.ElementAt(i).name}");
            }
        }

        private static void Enumerate(IEnumerable<Playlist> playlists,int start = 0, int limit = int.MaxValue)
        {
            for (int i = start; i < Math.Min(playlists.Count(), limit); i++)
            {
                Console.WriteLine($"{i + 1} {playlists.ElementAt(i).name}");
            }
        }
    }
}