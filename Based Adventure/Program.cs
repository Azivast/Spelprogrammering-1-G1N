using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security;

namespace Based_Adventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Text Adventure!");

            Hero hero = new Hero();;
            while (hero.Location != "quit")
            {
                switch (hero.Location)
                {
                    case "newgame":
                        hero = new Hero();
                        Rooms.NewGame(hero);
                        break;
                    case "tableroom":
                        Rooms.TableRoom(hero);
                        break;
                    case "corridor":
                        Rooms.Corridor(hero);
                        break;
                    case "lockedroom":
                        Rooms.LockedRoom(hero);
                        break;
                    case "thirdroom":
                        Rooms.ThirdRoom(hero);
                        break;
                    case "outsideroom":
                        Rooms.OutsideRoom(hero);
                        break;
                    case "bossfight":
                        Enemy boss = new Enemy("Minotaur", 50);
                        Rooms.BossFight(hero, boss);
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

        public static string Ask(string question)
        {
            string response;
            do
            {
                Console.Write(question);
                response = Console.ReadLine().Trim();
            } while (response == "");

            return response;
        }

        public static bool AskYesOrNo(string question)
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

        public static int RollD6()
        {
            return new Random().Next() % 6 + 1;
            //         random number % 0-5 + 1
        }
    }
}