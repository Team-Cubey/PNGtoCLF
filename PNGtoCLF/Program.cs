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
            Program P = new Program();
            Console.WriteLine("Enter PNG location");
            string path = Console.ReadLine();
            if (path.Contains(@":\"))
            {
                path = path;
                P.nopath = path;
            }
            else
            {
                string currentloc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                P.nopath = path;
                path = currentloc + "\\" + path;
            }
            if (P.nopath.Contains(".png"))
            {
                Console.WriteLine("Enter Level Name");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Level Creator");
                string creator = Console.ReadLine();
                var img = new Bitmap(System.Drawing.Image.FromFile(path));

                string level = @"CLF 0.1

[META]
name: '" + name + @"'
creator: '" + creator + @"'
xscale: " + img.Width + @"
yscale: " + img.Height + @"

[LEVEL]
";

                Console.WriteLine(level);
            }
            else
            {
                Console.WriteLine("That doesn't look like a PNG! Press ENTER to exit.");
                Console.ReadLine();
            }
        }

    }
}
