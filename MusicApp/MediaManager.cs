namespace MusicApp
{
    class Playlist
    {
        internal string name;
        internal List<Track> tracks;
    }

    internal class MediaManager
    {
        List<Playlist> genres = new List<Playlist>();
        public void GetPlayList()
        {
            genres.Add(new Playlist
            {
                Category = "Gospel",
                Ref = 1
            });

            genres.Add(new Playlist
            {
                Category = "Afro Sound ",
                Producer = "Den",
                Ref = 2
            });
            genres.Add(new Playlist
            {
                Category = "Hip-Hop",
                Producer = "Abel",
                Ref = 3
            });


            foreach (Playlist song in genres)
            {
                Console.WriteLine("\nName of Music: {0}", song.Category);
                Console.WriteLine("Producer of Music: {0}", song.Producer);
                Console.WriteLine("Music id: {0}", song.Ref);
                Console.WriteLine("*****************************************\n");
            }

            Console.WriteLine("press 1 to create a playlist or 2 to enter music libray");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Create New PlayLis");
                    string name = Console.ReadLine();
/*                    Console.WriteLine("enter producer name");
                    string producer = Console.ReadLine();
                    Console.WriteLine("enter year");
                    int year = Convert.ToInt32(Console.ReadLine());*/


                    genres.Add(new Playlist
                    {
                        Category = name,
/*                        Producer = producer,
                        Year = year*/
                    });
                    Console.WriteLine("successfull");
                    //Thread(20000);
                    foreach (Playlist song in genres)
                    {
                        Console.WriteLine("\nName of Music: {0}", song.Category);
/*                        Console.WriteLine("Producer of Music: {0}", song.Producer);
                        Console.WriteLine("Music id: {0}", song.Year);*/
                        Console.WriteLine("*****************************************\n");
                    }
                    break;
                case "2":
                    SoundPlayer myMusic = new SoundPlayer();
                    myMusic.GetMusic();
                    break;
            }
        }
        public void AddMusic()
        {

            Console.WriteLine("enter music name");
            string name = Console.ReadLine();
            Console.WriteLine("enter producer name");
            string producer = Console.ReadLine();
            Console.WriteLine("enter year");
            int year = Convert.ToInt32(Console.ReadLine());


            genres.Add(new Playlist
            {
                Category = name,
                Producer = producer,
                Ref = year
            });

            foreach (Playlist song in genres)
            {
                Console.WriteLine("\nName of Music: {0}", song.Category);
                Console.WriteLine("Producer of Music: {0}", song.Producer);
                Console.WriteLine("Music id: {0}", song.Ref);
                Console.WriteLine("*****************************************\n");
            }
        }

        public void control()
        {
            Console.WriteLine("press 1 to remove music");
            Console.WriteLine("press 2 to find music");
            Console.WriteLine("press 3 to edit music");
            string option = Console.ReadLine();
            //Console.WriteLine("enter id");
            //int id = Convert.ToInt32(Console.ReadLine());

            /*  int counter = 0;
              List<Music> stream = new List<Music>();
              while(counter> 20)
              {
                  stream.Add(new Music
                  {
                      Name = name,
                      Producer = producer,
                      id = id
                  });
                  counter++;
              }*/

            switch (option)
            {
                case "1": //remove
                    {
                        Console.WriteLine("music to remove");
                        //int removing = 0;
                        //while (removing < 5)
                        //{
                        Console.WriteLine("enter id of music to remove");
                        var name = Console.ReadLine();
                        genres.RemoveAll(music => music.Category == name);

                        //foreach (Music sub in stream)
                        //{
                        Console.WriteLine("this music: {0} has been removed from the playlist", name);
                        //}
                        // removing++;
                        //}

                        foreach (Playlist song in genres)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.Category);
                            Console.WriteLine("Producer of Music: {0}", song.Producer);
                            Console.WriteLine("Music id: {0}", song.Year);
                            Console.WriteLine("*****************************************\n");
                        }
                        break;

                    }


                case "2": //find
                    {
                        Console.WriteLine("to find music by producer");
                        Console.WriteLine("enter producer name");
                        string author = Console.ReadLine();
                        // stream.Contains(author);
                        genres.ForEach(music =>
                        {
                            if (music.Producer == author) Console.WriteLine(music.Category);
                        });


                        break;
                    }

                case "3": //edit
                    {
                        //Console.WriteLine("to find music by producer");
                        Console.WriteLine("enter Music name to edit");
                        string author = Console.ReadLine();
                        var IndexToEdit = genres.FindIndex(music => music.Category == author);
                        Console.WriteLine("enter new name");
                        genres[IndexToEdit].Producer = Console.ReadLine();
                        Console.WriteLine("enter new producer");
                        genres[IndexToEdit].Category = Console.ReadLine();
                        Console.WriteLine("enter new year");
                        genres[IndexToEdit].Year = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("{0} has been edited", IndexToEdit);

                        foreach (Playlist song in genres)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.Category);
                            Console.WriteLine("Producer of Music: {0}", song.Producer);
                            Console.WriteLine("Music id: {0}", song.Year);
                            Console.WriteLine("*****************************************\n");
                        }

                        /*  stream.ForEach(music =>
                          {
                              if (music.Producer == author) Console.WriteLine(music.Name);
                          });*/
                        break;
                    }
            }

        }

        public void Manager()
        {
            Console.WriteLine("press 1 to play music in order");
            Console.WriteLine("press 2 to play music alphabetically");
            Console.WriteLine("press 3 to shulffle");
            Console.WriteLine("press 4 for next");
            Console.WriteLine("press 5 for previous");
            string option = Console.ReadLine();
            //Console.WriteLine("enter id");
            //int id = Convert.ToInt32(Console.ReadLine());

            /*  int counter = 0;
              List<Music> stream = new List<Music>();
              while(counter> 20)
              {
                  stream.Add(new Music
                  {
                      Name = name,
                      Producer = producer,
                      id = id
                  });
                  counter++;
              }*/

            switch (option)
            {
                case "1": //queue
                    {

                        Queue<Playlist> stream = new Queue<Playlist>();
                        //stream.Dequeue(stream);
                        foreach (Playlist music in genres)
                        {
                            Console.WriteLine(music);

                        }
                        break;
                    }


                case "2": //Alphabet
                    {
                        genres.Sort((x, y) => string.Compare(x.Category, y.Category));
                        foreach (Playlist song in genres)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.Category);
                            Console.WriteLine("Producer of Music: {0}", song.Producer);
                            Console.WriteLine("Music id: {0}", song.Year);
                            Console.WriteLine("*****************************************\n");
                        }
                        break;
                    }

                case "3": //shulffle
                    {
                        var rnd = new Random();
                        var randomized = genres.OrderBy(item => rnd.Next());


                        foreach (var value in randomized)
                        {
                            Console.WriteLine(value.Category);
                        }

                        /*foreach (Music song in stream)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.Name);
                            Console.WriteLine("Producer of Music: {0}", song.Producer);
                            Console.WriteLine("Music id: {0}", song.Year);
                            Console.WriteLine("*****************************************\n");
                        }
                        */
                        break;
                    }
            }
        }
    }

    /* internal class MediaManager
     {
     }*/
}
