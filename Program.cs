using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {

        static void listPrint(IEnumerable<Artist> arr)
        {
            Console.Write("<");
            for(int i = 0; i < arr.Count(); i++)
            {
                Console.Write(arr.ElementAt(i).ArtistName);
                if(i < arr.Count() - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(">");
        }

        static void listPrint(IEnumerable<Group> arr)
        {
            Console.Write("<");
            for(int i = 0; i < arr.Count(); i++)
            {
                Console.Write(arr.ElementAt(i).GroupName);
                if(i < arr.Count() - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(">");
        }

        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            Artist Vernon = Artists.Where( artist => artist.Hometown == "Mount Vernon").First();
            Console.WriteLine(Vernon.ArtistName);
            Console.WriteLine();
            //Who is the youngest artist in our collection of artists?
            Artist Youngest = Artists.OrderBy( artist => artist.Age).First();
            Console.WriteLine(Youngest.ArtistName);
            Console.WriteLine();
            //Display all artists with 'William' somewhere in their real name
            var Wills = Artists.Where( artist => artist.RealName.Contains("William"));
            listPrint(Wills);
            Console.WriteLine();
            //Display the 3 oldest artist from Atlanta
            var OldAtlanta = Artists.Where( artist => artist.Hometown == "Atlanta").OrderByDescending( artist => artist.Age).Take(3);
            listPrint(OldAtlanta);
            Console.WriteLine();
            //(Optional) Display the Group Name of all groups that have members that are not from New York City
            var NotYork = Groups.Join(Artists.Where(artist => artist.Hometown != "New York City"), group => group.Id, artist => artist.GroupId, (group, artist) => {return group;}).Distinct();
            listPrint(NotYork);
            Console.WriteLine();
            //(Optional) Display the artist names of all members of the group 'Wu-Tang Clan'
            var WTC = Artists.Join(Groups.Where(group => group.GroupName == "Wu-Tang Clan"), artist => artist.GroupId, group => group.Id, (artist, group) => {return artist;});
            listPrint(WTC);
        }
    }
}
