using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace Lab_08_Get_To_Know_Your_Classmates
{
    class Program
    {
        static void Main(string[] args)
        {   //Class information
            Dictionary<int, string> studentIDtoName = new Dictionary<int, string>()
            {   {0, "Student Name"},
                {1, "Jordan Documentary"},
                {2, "Ringo Pete"},
                {3, "Mary Zebulon"},
                {4, "Vivian Yibble" },
                {5, "Jerry Anklebrace" },
                {6, "Elizabeth Dribbleneck" },
                {7, "Shifty Runkle" },
                {8, "Lemmy Clementine" },
                {9, "Lester Sylvester" },
                {10, "Jackie Slack" },
                {11, "Gordie Gort" }          
            };
            Dictionary<int, string> studentIDtoHobby = new Dictionary<int, string>()
            {   {0, "Favorite Hobby"},
                {1, "Needlepoint"},
                {2, "Paper Mache"},
                {3, "Instagram Influencing"},
                {4, "Bazooka Joe Comic Collecting" },
                {5, "Competitive Slinky" },
                {6, "Influencing Instagram Influencing" },
                {7, "Competitive Meditation" },
                {8, "BMX" },
                {9, "MMA" },
                {10, "BMXMMA" },
                {11, "Podcasting" }          
            };
            Dictionary<int, string> studentIDtoFood = new Dictionary<int, string>()
            {   {0, "Favorite Food"},
                {1, "Tacos"},
                {2, "Spanikopita"},
                {3, "Gatorade Slushies"},
                {4, "Chickie-chickie-parm-parm" },
                {5, "Fluffernutters" },
                {6, "Burrito Sandwiches" },
                {7, "Chocolate covered vanilla beans" },
                {8, "Colorado Bottled Avocado" },
                {9, "Corn chips" },
                {10, "Booze" },
                {11, "Water" }          
            };
            Dictionary<int, string> studentIDtoHometown = new Dictionary<int, string>()
            {   {0, "Hometown"},
                {1, "Exeter, NJ"},
                {2, "Millington, TN"},
                {3, "Shoehorn, NH"},
                {4, "Big Bluff, NM" },
                {5, "Scranton, PA" },
                {6, "Washburn, WA" },
                {7, "Walla Walla, WA" },
                {8, "Denver, CO" },
                {9, "Texarkana, TX" },
                {10, "Lewiston, MN" },
                {11, "Cleveland, OH" }          
            };

            //Class information parameters
            int length = studentIDtoName.Count;
            string[] categories = { studentIDtoHobby[0], studentIDtoFood[0], studentIDtoHometown[0] };
            
            //Introduction to user
            Console.WriteLine("Welcome to our C# class. Which student would you like to learn more about?");
                        
            bool continueLookup = true;
            do
            {
                //Prompt user for student name or id
                
                int studentID = GetUserDesiredStudent("Enter a student ID from 1 to " + (length - 1) + " or a student name to search by name: ", studentIDtoName);
               
                while (continueLookup)
                {   //Prompt user for category to research
                    string info = GetDesiredTopic(categories, $"What would you like to know about {studentIDtoName[studentID]}?");

                    //Displays info according to category
                    Console.WriteLine()
                        ;
                    if (info == studentIDtoFood[0])
                    {
                        Console.WriteLine($"{studentIDtoName[studentID]}'s {studentIDtoFood[0]} is {studentIDtoFood[studentID]}");
                    }
                    else if (info == studentIDtoHobby[0])
                    {
                        Console.WriteLine($"{studentIDtoName[studentID]}'s {studentIDtoHobby[0]} is {studentIDtoHobby[studentID]}");
                    }
                    else if (info == studentIDtoHometown[0])
                    {
                        Console.WriteLine($"{studentIDtoName[studentID]}'s {studentIDtoHometown[0]} is {studentIDtoHometown[studentID]}");
                    }
                    Console.WriteLine();
                    continueLookup = GetYesOrNo("Would you like to look up anything else about this student? y/n: ");
                }
                Console.WriteLine();
                continueLookup = GetYesOrNo("Would you like to look up another student? y/n:  ");
            } while (continueLookup);
        }

        public static string GetDesiredTopic(string[] categories, string prompt)
        {   //displays available topcis to user
            //gets user input for desired topic
            //if input does not match availible topics, asks again

            bool match = false;
            Console.WriteLine();
            Console.WriteLine(prompt);
            Console.WriteLine("Available topics are:");
            foreach (string category in categories)
            {
                Console.WriteLine(category);
            }
            Console.WriteLine();
            string info = GetUserInput("Please enter a category to search: ");

            foreach (string category in categories)
            {
                if(info == category)
                {
                    match = true;
                }
            }
            
            if (match)
            {
                return info;
            } 
            else
            {
                Console.WriteLine("I'm sorry, that does not match any available topics. Please try again.");
                Console.WriteLine();
                info = GetDesiredTopic(categories, prompt);
            }
            return info;
        }
        public static int GetUserDesiredStudent(string prompt, Dictionary<int, string> dict)
        {   //asks user for student name OR ID number
            //if input is succesfully parsed, tries to display value using parsed key
            //if not parsed, runs a method to look up the key by the value
            //if either are not on record, throws exception

            string lookup = GetUserInput(prompt);
            int studentID = -1;

            try 
            { 
                if (!Int32.TryParse(lookup, out studentID))
                {
                    studentID = ReverseDictionaryLookup(lookup, dict);
                }
                Console.WriteLine($"Student {studentID} is {dict[studentID]}.");
            } 
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please try again.");
                Console.WriteLine();
                studentID = -1;
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("That entry does not match our records. Please try again.");
                Console.WriteLine();
                studentID = -1;
            }
            if (studentID == -1)
            {
                studentID = GetUserDesiredStudent(prompt, dict);
            }
            
            return studentID;
        }
        public static string GetUserInput(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            return input;
        }

        public static string ReturnDictionaryValue(int index, Dictionary<int, string> dict)
        {
            return "void";
        }

        public static int ReverseDictionaryLookup(string lookup, Dictionary<int, string> dict)
        {   //looks up dictionary key number by value - only works reliably if all values are unique

            foreach (int key in dict.Keys)
            {
                if (dict[key] == lookup)
                {
                    return key;
                }
            }
            return -1;
        }
        public static bool GetYesOrNo(string prompt)
        {
            //Prompts user for y/n; returns true for y and false for n
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine().ToLower();

                if (input == "y")
                    return true;
                else if (input == "n")
                    return false;
                else
                    Console.WriteLine("I'm sorry, I didn't get that.");
            }
        }
    }
}
