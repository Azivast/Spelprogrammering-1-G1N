using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void TableRoom(Hero hero)
        {
            Console.Clear();
            hero.Items.Add("Wooden Sword");
            Console.WriteLine("You are equipped with one wooden sword, and your task " +
                              "is to slay the monster at the end of the adventure.\n" +
                              "In front you is a stone table with two items on it. " +
                              "A knife and a key.\n" +
                              "You can only pick up one of these items.");

            string answer;
            do
            {
                answer = Program.Ask("Which do you pick up? Key/Knife/None: ");
                switch (answer.ToLower())
                {
                    case "key":
                    case "the key":
                        hero.Items.Add("Key");
                        Console.WriteLine("You picked up the key.");
                        break;
                    case "knife":
                    case "the knife":
                        hero.Items.Add("Knife");
                        Console.WriteLine("You picked up the knife.");
                        break;
                    case "none":
                    case "nothing":
                        Console.WriteLine("You picked up nothing.");
                        break;
                    default:
                        answer = "";
                        Console.WriteLine("That's not a valid option.");
                        break;
                }
            } while (answer == "");

            hero.Location = "corridor";
        }
    }
}