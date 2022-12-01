using System.Collections.Generic;
using System.IO;
using System.Media;
namespace MusicApp
{
    internal class Program
    {
        static List<Track> musicLibrary = new List<Track>();
        static List<Playlist> playlists = new List<Playlist>();


        static void Main(string[] args)
        {
            ShowMainMenu();
        }

        private static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("********GEsys Music App********\n\n");
            Console.WriteLine($"enter 1 to view PlayList or Create new PlayList\n" +
                $"{Environment.NewLine}enter 2 to enter Music Library");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ShowPlaylists();
                    return;
                case "2":
                    ShowMusicLibrary();
                    return;
            }

        }
        
        private static void ShowPlaylists()
        {
            Console.Clear();
            if (playlists.Count == 0)
            {
                Console.WriteLine("you have not created any playlist");
            }
            else
            {
                Console.Write("Select Playlist");
                for (int i = 0; i <= playlists.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {playlists[i].name}");
                }
            }

            Console.WriteLine("enter c to create new playlist");
            string input = Console.ReadLine();
            switch (input)
            {
                case "c":
                {
                    Console.WriteLine("enter a name for the playlist");
                    playlists.Add(new Playlist
                    {
                        name = Console.ReadLine(),

                    });

                    ShowPlaylists();
                    return;
                }
                default:
                {
                    int index;
                    if (int.TryParse(input, out index) && index != 0 && index <= playlists.Count)
                    {
                        ShowPlaylist(playlists[index]);
                        return;
                    }

                    ShowPlaylists();
                    return;
                }
            }
        }

        private static void ShowPlaylist(Playlist playlist)
        {
            Console.Clear();
            if (playlist.tracks.Count == 0)
            {
                Console.WriteLine("you have not added any music");
            }
            else
            {
                Console.Write("Select Music");
                for (int i = 0; i < playlist.tracks.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {playlist.tracks[i].name}");
                }
            }

            Console.WriteLine("enter a to add music to this playlist from the music Library" +
                "\nenter p to play all" +
                "\nenter s to shuffle and play" +
                "\nenter pa to play all in alphabetical order");

            string input = Console.ReadLine();
            switch (input)
            {
                case "a":
                {
                    ShowAddMusicTrackToPlaylist(playlist);
                    return;
                }
                case "p":
                {
                    ShowNowPlaying(playlist.tracks, playlist.name);
                    return;
                }
                case "s":
                {
                    ShowNowPlaying(ShuffleTracks(playlist.tracks), playlist.name);
                    return;
                }
                case "pa":
                {
                    ShowNowPlaying(SortTracks(playlist.tracks), playlist.name);
                    return;
                }
                default:
                {
                    int index;
                    if (int.TryParse(input, out index) && index != 0 && index <= playlist.tracks.Count)
                    {
                        ShowNowPlaying(playlist.tracks, playlist.name, index - 1);
                        return;
                    }

                    ShowPlaylists();
                    return;
                }
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

            for (int i = 0; i < startingFrom; i++)
            {
                Console.WriteLine($"{i + 1} {tracks.ElementAt(i).name}");
            }
            Console.WriteLine($"{(paused ? "⏸" : "▷")} {tracks.ElementAt(startingFrom).name}");
            for (int i = startingFrom + 1; i < tracks.Count(); i++)
            {
                Console.WriteLine($"{i + 1} {tracks.ElementAt(i).name}");
            }

            Console.WriteLine(
                "\nActions\n" +
                $"p {(paused ? "▷" : "⏸")}\n" +
                "> ⏭\n" +
                "< ⏮\n" +
                "s ⏹\n" +
                "\n" +
                "select an action to take or a track to jump to"
            );

            var input = Console.ReadKey();
            switch (input.KeyChar)
            {
                case 'p':
                    ShowNowPlaying(tracks, listName, startingFrom, !paused);
                    return;
                case '>':
                    ShowNowPlaying(tracks, listName, startingFrom + 1, paused);
                    return;
                case '<':
                    ShowNowPlaying(tracks, listName, startingFrom - 1, paused);
                    return;
                case 's':
                    ShowMainMenu();
                    return;
                default:
                    int index;
                    if (int.TryParse(input.KeyChar.ToString(), out index) && index != 0 && index <= tracks.Count())
                    {
                        ShowNowPlaying(tracks, listName, index - 1);
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
                Console.WriteLine("You do not have any music in your music library");
            }
            else
            {
                Console.WriteLine("Select From Music Library");
                for (int i = 0; i < musicLibrary.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {musicLibrary[i].name}");
                }
            }

            Console.WriteLine("press a to add a new music to your music library and this playlist");

            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case ('a'):
                    playlist.tracks.Add(ShowAddMusicToMusicLibrary());
                    ShowPlaylist(playlist);
                    return;
                default:
                    int index;
                    if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out index) && index != 0 && index <= musicLibrary.Count)
                    {
                        playlist.tracks.Add(musicLibrary[index - 1]);
                        ShowPlaylist(playlist);
                        return;
                    }
                    ShowAddMusicTrackToPlaylist(playlist);
                    return;
            }
        }

        private static Track ShowAddMusicToMusicLibrary()
        {
            Console.WriteLine("Enter the name of the music");
            var name = Console.ReadLine();
            Console.WriteLine("Enter the name of the artist");
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
            if (musicLibrary.Count == 0)
            {
                Console.WriteLine("You do not have any music in your music library");
            }
            else
            {
                Console.WriteLine("Music Library\n");
                for (int i = 0; i < musicLibrary.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {musicLibrary[i].name}");
                }
            }

            Console.WriteLine("press a to add music");
            Console.WriteLine("press r to remove music");
            Console.WriteLine("press e to edit music");

            var input = Console.ReadKey().KeyChar;
            switch (input)
            {
                case ('a'):
                    ShowAddMusicToMusicLibrary();
                    ShowMusicLibrary();
                    return;
                case ('r'):
                    Console.Clear();
                    Enumerate(musicLibrary);
                    return;
                default:
                    int index;
                    if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out index) && index != 0 && index <= musicLibrary.Count)
                    {
                        musicLibrary.Add(musicLibrary[index - 1]);
                        //ShowPlaylist(playlist);
                        return;
                    }
                    //ShowAddMusicTrackToPlaylist(playlist);
                    return;
            }
        }

        private static void Enumerate(List<Track> musicLibrary)
        {
            throw new NotImplementedException();
        }
    }
}