using CommandLine;
using CommandLine.Text;
using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;

namespace PNGtoCLF
{
    class Program
    {
        public string nopath;
        public bool dohelp = false;
        private static string pngpath;
        private static string mapname;
        private static string mapauthor;
        private static bool pngpathe;
        private static bool mapnamee;
        private static bool mapauthore;
        private static bool colorInArray;

        public class aOptions
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose
            {
                get;
                set;
            }

            [Option('p', "pngpath", Required = false, HelpText = "Set this as the path to the original level PNG")]
            public string pngpath
            {
                get;
                set;
            }

            [Option('n', "mapname", Required = false, HelpText = "This is the level name in meta.")]
            public string mapname
            {
                get;
                set;
            }

            [Option('a', "mapauthor", Required = false, HelpText = "This is the map author in meta.")]
            public string mapauthor
            {
                get;
                set;
            }
        }

        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<aOptions>(args).WithParsed<aOptions>(o => {
                pngpath = o.pngpath;
                mapname = o.mapname;
                mapauthor = o.mapauthor;
                if (!String.IsNullOrEmpty(pngpath))
                {
                    pngpathe = true;
                }
                else
                {
                    pngpathe = false;
                }
                if (!String.IsNullOrEmpty(mapname))
                {
                    mapnamee = true;
                }
                else
                {
                    mapnamee = false;
                }
                if (!String.IsNullOrEmpty(mapauthor))
                {
                    mapauthore = true;
                }
                else
                {
                    mapauthore = false;
                }
            });

            if (result.Tag == ParserResultType.NotParsed)
            {
                // Help text requested, or parsing failed. Exit.
                Environment.Exit(1);
            }

            Program P = new Program(); // grab program

            Console.WriteLine("PNGtoCLF 1.2.4 - CLF FORMAT 0.3.1 | Team Cubey 2020\n\n"); // copyright
            string path;
            if (!pngpathe)
            {
                Console.WriteLine("Enter PNG location"); // ask for png location
                path = Console.ReadLine(); // grab the path they wanted
            }
            else
            {
                path = pngpath;
            }

            if (path.Contains(@":\"))
            {
                path = path; // if the path is absolute we'll just set up our variables like usual
                P.nopath = path;
            }
            else
            {
                string currentloc = Directory.GetCurrentDirectory(); // if the path isn't absolute we'll find out the absolute path, also save the filename.
                P.nopath = path;
                path = currentloc + "\\" + path;
            }
            if (path.Contains(".png"))
            {
                string name;
                string creator;
                if (!mapnamee)
                {
                    Console.WriteLine("\nEnter Level Name "); // ask for level name
                    name = Console.ReadLine(); // grab level name
                }
                else
                {
                    name = mapname;
                }
                if (!mapauthore)
                {
                    Console.WriteLine("\nEnter Level Creator "); // ask for level creator
                    creator = Console.ReadLine(); // grab level creator
                }
                else
                {
                    creator = mapauthor;
                }
                var img = new Bitmap(System.Drawing.Image.FromFile(path)); // grab selected image

                // create basic level structure
                string level = @"CLF 0.3.1

[META]
name: '" + name + @"'
creator: '" + creator + @"'
xscale: " + img.Width + @"
yscale: " + img.Height + @"

[LEVEL]";
                Color[] colors = {
                    Color.FromArgb(255, 0, 0, 0), // land
                    Color.FromArgb(255, 255, 0, 255),  // cubey
                    Color.FromArgb(255, 0, 100, 255), // key
                    Color.FromArgb(255, 255, 255, 0),  // door-1
                    Color.FromArgb(255, 255, 0, 0), // killcube-vertical  
                    Color.FromArgb(255, 200, 0, 0), // killcube-horizontal
                    Color.FromArgb(255, 0, 255, 255), // evilcube-reverser
                    Color.FromArgb(255, 0, 255, 0), // flower
                    Color.FromArgb(255, 1, 0, 0), // tutorial dialog 1
                    Color.FromArgb(255, 2, 0, 0), // tutorial dialog 2
                    Color.FromArgb(255, 3, 0, 0), // tutorial dialog 3
                    Color.FromArgb(255, 4, 0, 0), // tutorial dialog 4
                    Color.FromArgb(255, 5, 0, 0), // tutorial dialog 5
                    Color.FromArgb(255, 6, 0, 0), // tutorial dialog 6
                    Color.FromArgb(255, 7, 0, 0), // tutorial dialog 7
                    Color.FromArgb(255, 8, 0, 0), // tutorial dialog 
                    Color.FromArgb(255, 100, 200, 100), // jumppad
                    Color.FromArgb(255, 200, 0, 50), // evilkey
                    Color.FromArgb(255, 0, 200, 0), // flowershoot
                    Color.FromArgb(255, 100, 100, 100), // 4d-shooter
                    Color.FromArgb(255, 50, 50, 50), // land-nocol
                    Color.FromArgb(255, 255, 255, 255), // barrier
                    Color.FromArgb(255, 200, 255, 255), // flag 
                    Color.FromArgb(255, 72, 0, 0), // gate-red
                    Color.FromArgb(255, 172, 0, 0), // gate-red-key
                    Color.FromArgb(255, 0, 72, 0), // gate-green
                    Color.FromArgb(255, 0, 172, 0), // gate-green-key
                    Color.FromArgb(255, 0, 0, 72), // gate-blue
                    Color.FromArgb(255, 0, 0, 172), // gate-blue-key
                    Color.FromArgb(255, 255, 150, 255), // movingland
                    Color.FromArgb(255, 234, 123, 123), // meta display
                    Color.FromArgb(255, 255, 155, 0), // teleportal, deprecated
                    Color.FromArgb(255, 0, 10, 0), // mappack1-end
                    Color.FromArgb(255, 0, 25, 25), // land-reverser
                    Color.FromArgb(255, 0, 69, 255), // heart
                    Color.FromArgb(255, 255, 69, 0), // evilheart
                    Color.FromArgb(255, 90, 20, 90),
                    Color.FromArgb(255, 9, 0, 0) // tutorial dialog 9
                };

                for (int x = 0; x < img.Width; x++) // loop through all the X pixels
                {
                    for (int y = 0; y < img.Height; y++) // loop through all the Y pixels
                    {
                        Color pixelColor = img.GetPixel(x, y); // gets the colour of the pixel/tile
                       
                        if (pixelColor.A > 0) // dont want alphas
                        {
                            colorInArray = false;

                            int finalid = -1;
                            int id = 0;
                            foreach (Color colorMapping in colors)
                            {
                                if (pixelColor == colorMapping)
                                {
                                    colorInArray = true;
                                    finalid = id;
                                    break;
                                }
                                id += 1;
                            }

                            if (colorInArray)
                            {
                                level += "\n"; // newlines

                                level += x + "," + y + ",0," + finalid; // adds tile
                            }
                            else
                            {
                                string pc = "" + (pixelColor.R) + " " + (pixelColor.G) + " " + (pixelColor.B);
                                Console.WriteLine("The tile " + x + ":" + (img.Height - y - 1) + " has an invalid tile of color '" + pc);
                            }

                            //level += "\n "; // newlines
                            //
                            //level += x + ", " + y + ", 0, " + pixelColor.R + ", " + pixelColor.G + ", " + pixelColor.B; // adds tile
                        }
                    }
                }

                Console.WriteLine("\nConverting PNG..."); // alert user we're converting 

                string exportto = path.Replace(".png", "") + ".clf "; // get export location

                File.WriteAllText(exportto, level); // export
                Console.WriteLine("\nExported to " + exportto); // alert user
            }
            else
            {
                Console.WriteLine("That doesn't look like a PNG! Press ENTER to exit.");

                Console.ReadLine();
            }
        }
    }
}
