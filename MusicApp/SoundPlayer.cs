using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MusicApp
{
    class Track
    {
        public string name;
        public string artist;
    }
    internal class SoundPlayer
    {
            List<Music> stream = new List<Music>();
        public void GetMusic()
        {
            stream.Add(new Track
            {
                name = "who am I",
                Producer = "Kim",
                Category = "Gospel",
                Year = 1
            });

            stream.Add(new Track
            {
                name = "run down ",
                Producer = "Den",
                Category = "Afro Sound ",
                Year = 2
            });
            stream.Add(new Track
            {
                name = "Can he walk",
                Producer = "Abel",
                Category = "Hip-Hop",
                Year = 3
            });


            foreach (Track song in stream) {
                Console.WriteLine("\nName of Music: {0}", song.name);
                Console.WriteLine("\nCategory of Music: {0}", song.Category);
                Console.WriteLine("Producer of Music: {0}", song.Producer);
                Console.WriteLine("Music id: {0}", song.Year);
                Console.WriteLine("*****************************************\n");
            }

            Console.WriteLine("press 1 to add music");
            Console.WriteLine("press 2 to remove music");
            Console.WriteLine("press 3 to find music by Producer");
            Console.WriteLine("press 4 to edit music");
            string option = Console.ReadLine();


            switch (option)
            {
                case "1": //remove
                    {
                        Console.WriteLine("enter music name");
                        string name = Console.ReadLine();
                        Console.WriteLine("enter music category");
                        string category = Console.ReadLine();
                        Console.WriteLine("enter producer name");
                        string producer = Console.ReadLine();
                        Console.WriteLine("enter year");
                        int year = Convert.ToInt32(Console.ReadLine());


                        stream.Add(new Track
                        {
                            name = name,
                            Producer = producer,
                            Category = category,
                            Year = year
                        });

                        foreach (Track song in stream)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.name);
                            Console.WriteLine("\nName of Music: {0}", song.Category);
                            Console.WriteLine("Producer of Music: {0}", song.Producer);
                            Console.WriteLine("Music id: {0}", song.Year);
                            Console.WriteLine("*****************************************\n");
                        }
                        break;
                    }

                case "2": //remove
                    {
                        Console.WriteLine("music to remove");
                        //int removing = 0;
                        //while (removing < 5)
                        //{
                        Console.WriteLine("enter id of music to remove");
                        var name = Console.ReadLine();
                        stream.RemoveAll(music => music.name == name);

                        //foreach (Music sub in stream)
                        //{
                        Console.WriteLine("this music: {0} has been removed from the playlist", name);
                        //}
                        // removing++;
                        //}

                        foreach (Track song in stream)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.name);
                            Console.WriteLine("Producer of Music: {0}", song.Producer);
                            Console.WriteLine("Music id: {0}", song.Year);
                            Console.WriteLine("*****************************************\n");
                        }
                        break;

                    }


                case "3": //find
                    {
                        Console.WriteLine("to find music by producer");
                        Console.WriteLine("enter producer name");
                        string author = Console.ReadLine();
                        // stream.Contains(author);
                        stream.ForEach(music =>
                        {
                            if (music.Producer == author) Console.WriteLine(music.name);
                        });


                        break;
                    }

                case "4": //edit
                    {
                        //Console.WriteLine("to find music by producer");
                        Console.WriteLine("enter Music name to edit");
                        string author = Console.ReadLine();
                        var IndexToEdit = stream.FindIndex(music => music.name == author);
                        Console.WriteLine("enter new name");
                        stream[IndexToEdit].Producer = Console.ReadLine();
                        Console.WriteLine("enter new producer");
                        stream[IndexToEdit].name = Console.ReadLine();
                        Console.WriteLine("enter new year");
                        stream[IndexToEdit].Year = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("{0} has been edited", IndexToEdit);

                        foreach (Track song in stream)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.name);
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

                        Queue<Track> stream = new Queue<Track>();
                        //stream.Dequeue(stream);
                        foreach (Track music in stream)
                        {
                            Console.WriteLine(music);

                        }
                        break;
                    }


                case "2": //Alphabet
                    {
                        stream.Sort((x, y) => string.Compare(x.name, y.name));
                        foreach (Track song in stream)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.name);
                            Console.WriteLine("Producer of Music: {0}", song.Producer);
                            Console.WriteLine("Music id: {0}", song.Year);
                            Console.WriteLine("*****************************************\n");
                        }
                        break;
                    }

                case "3": //shulffle
                    {
                        var rnd = new Random();
                        var randomized = stream.OrderBy(item => rnd.Next());


                        foreach (var value in randomized)
                        {
                            Console.WriteLine(value.name);
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



        /* public void AddMusic()
         {

             Console.WriteLine("enter music name");
             string name = Console.ReadLine();
             Console.WriteLine("enter music category");
             string category = Console.ReadLine();
             Console.WriteLine("enter producer name");
             string producer = Console.ReadLine();
             Console.WriteLine("enter year");
             int year = Convert.ToInt32(Console.ReadLine());


             stream.Add(new Music
             {
                 Name = name,
                 Producer = producer,
                 Category = category,
                 Year = year
             });

             foreach (Music song in stream)
             {
                 Console.WriteLine("\nName of Music: {0}", song.Name);
                 Console.WriteLine("\nName of Music: {0}", song.Category);
                 Console.WriteLine("Producer of Music: {0}", song.Producer);
                 Console.WriteLine("Music id: {0}", song.Year);
                 Console.WriteLine("*****************************************\n");
             }
         }*/


        public void control() {
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
                            stream.RemoveAll(music => music.name == name);

                            //foreach (Music sub in stream)
                            //{
                                Console.WriteLine("this music: {0} has been removed from the playlist", name);
                        //}
                        // removing++;
                        //}

                        foreach (Track song in stream)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.name);
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
                            stream.ForEach(music =>
                            {
                                if (music.Producer == author) Console.WriteLine(music.name);
                            });

                       
                        break;
                    }

                case "3": //edit
                    {
                        //Console.WriteLine("to find music by producer");
                        Console.WriteLine("enter Music name to edit");
                        string author = Console.ReadLine();
                        var IndexToEdit = stream.FindIndex(music => music.name == author);
                        Console.WriteLine("enter new name");
                        stream[IndexToEdit].Producer = Console.ReadLine();
                        Console.WriteLine("enter new producer");
                        stream[IndexToEdit].name = Console.ReadLine();
                        Console.WriteLine("enter new year");
                        stream[IndexToEdit].Year = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("{0} has been edited", IndexToEdit);

                        foreach (Track song in stream)
                        {
                            Console.WriteLine("\nName of Music: {0}", song.name);
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

       
    }

}
