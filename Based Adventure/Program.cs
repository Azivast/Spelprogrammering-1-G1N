using System;
using System.Collections.Generic;
using System.Security;

namespace Based_Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Text Adventure!");

            Hero hero = new Hero();
            while (hero.Location != "quit")
            {
                switch (hero.Location)
                {
                    case "newgame":
                        NewGame(hero);
                        break;
                    case "tableroom":
                        TableRoom(hero);
                        break;
                    case "corridor":
                        Corridor(hero);
                        break;
                    case "lockedroom":
                        LockedRoom(hero);
                        break;
                    default:
                        Console.Error.WriteLine($"You forgot to implement '{hero.Location}'!");
                        break;
                }
                
            }
            string name = "";

            do
            {
                name = Ask("What is your name, Adventurer? ");
            } while (!AskYesOrNo($"So, {name} it is?  Yes/No: "));
        }

        static string Ask(string question)
        {
            string response;
            do
            {
                Console.Write(question);
                response = Console.ReadLine().Trim();
            } while (response == "");

            return response;
        }

        static bool AskYesOrNo(string question)
        {
            while (true)
            {
                string response = Ask(question).ToLower();
                switch (response)
                {
                    case "yes":
                    case "ok":
                        return true;
                    case "no":
                        return false;
                }
            }
        }

        static int RollD6()
        {
            return new Random().Next() % 6 + 1;
            //         random number % 0-5 + 1
        }
        
        // -------------------------------- Rooms -----------------------------------------------

        static void NewGame(Hero hero)
        {
            Console.Clear();
            string name = "";
            
            do
            {
                name = Ask("What is your name, Adventurer? ");
            } while (!AskYesOrNo($"So, {name} it is?  Yes/No: "));

            hero.Name = name;
            hero.Location = "tableroom";
        }
        
        static void TableRoom(Hero hero)
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
                answer = Ask("Which do you pick up? Key/Knife/None: ");
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
        
        static void Corridor(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("You exit the room and find yourself standing in a dark " +
                              "hallway. You can either enter another room on your right " +
                              "side, or continue down the hallway on your left.");
            
            string answer;
            do
            {
                answer = Ask("Right or left?: ");
                switch (answer.ToLower())
                {
                    case "right":
                        if (hero.Items.Contains("Key"))
                        {
                            hero.Location = "lockedroom";
                            hero.Items.Remove("Key");
                        }
                        else
                        {
                            Console.WriteLine("You inspect the door. It needs a key. " +
                                              "You do not have one and walk back.");
                            answer = "";
                            break;
                        }
                        Console.WriteLine("You picked up the key.");
                        break;
                    case "left":
                        hero.Location = "thirdroom";
                        break;
                }
            } while (answer == "");
        }
            
        static void LockedRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("Inside the locked room you find a shiny sword");

            if (AskYesOrNo("Do you want it instead of your wooden sword? Yes/No: "))
            {
                hero.Items.Remove("Wooden Sword");
                hero.Items.Add("Shiny Sword");
                Console.WriteLine("You picked the shiny sword.");
            }
            
            Console.WriteLine("Press any key to continue.");
            Console.Read();

            hero.Location = "thirdroom";
        }

        static void ThirdRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("On the floor before you lies a lifeless corpse.\n" +
                              "Its hand is clasped around something shiny.\n");

            if (AskYesOrNo("Do you want to loot the corpse or leave it?"))
            {
                Console.WriteLine("You pick up an old silver necklace.");
                if (RollD6() >= 3)
                {
                    Console.WriteLine("A warm feeling spread over your body.");
                    hero.Items.Add("Blessed Amulet");
                }
                else
                {
                    Console.WriteLine("A cold shiver runs your spine.");
                    hero.Items.Add("Cursed Amulet");
                }
            }
            
            Console.WriteLine("You leave corpse and continue into the next room. \n" +
                              "Press any key to continue.");
            Console.Read();
        }
    }
    class Hero
    {
        public string Name = "";
        public int Health = 100;
        public List<string> Items = new List<string>(); // Note: use uppercase first letter for items.
        public string Location = "newgame";
    }
}