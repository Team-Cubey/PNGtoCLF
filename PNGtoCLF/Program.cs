using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace PNGtoCLF
{
    class Program
    {
        public string nopath;

        static void Main(string[] args)
        {
            Console.Title = "PNGtoCLF"; // set title

            Program P = new Program(); // grab program
            Console.WriteLine("PNGtoCLF 1.0 - CLF FORMAT 0.1\n(C) Team Cubey 2020\n\n"); // copyright
            Console.WriteLine("Enter PNG location"); // ask for png location
            string path = Console.ReadLine(); // grab the path they wanted
            if (path.Contains(@":\"))
            {
                path = path; // if the path is absolute we'll just set up our variables like usual
                P.nopath = path;
            }
            else
            {
                string currentloc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); // if the path isn't absolute we'll find out the absolute path, also save the filename.
                P.nopath = path;
                path = currentloc + "\\" + path;
            }
            if (P.nopath.Contains(".png"))
            {
                Console.WriteLine("\nEnter Level Name"); // ask for level name
                string name = Console.ReadLine(); // grab level name
                Console.WriteLine("\nEnter Level Creator"); // ask for level creator
                string creator = Console.ReadLine(); // grab level creator
                var img = new Bitmap(System.Drawing.Image.FromFile(path)); // grab selected image

                // create basic level structure
                string level = @"CLF 0.1

[META]
name: '" + name + @"'
creator: '" + creator + @"'
xscale: " + img.Width + @"
yscale: " + img.Height + @"

[LEVEL]";
                for (int x = 0; x < img.Width; x++) // loop through all the X pixels
                {
                    for (int y = 0; y < img.Height; y++) // loop through all the Y pixels
                    {
                        Color pixelColor = img.GetPixel(x, y); // gets the colour of the pixel/tile
                        if (pixelColor.A > 0) // dont want alphas
                        {
                            level += "\n"; // newlines

                            level += x + "," + y + ",0," + pixelColor.R + "," + pixelColor.G + "," + pixelColor.B; // adds tile
                        }
                    }
                }

                Console.WriteLine("\nConverting PNG..."); // alert user we're converting 

                string exportto = path.Replace(".png", "") + ".clf"; // get export location

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
