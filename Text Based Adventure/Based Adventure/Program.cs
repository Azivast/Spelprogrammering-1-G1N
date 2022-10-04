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

            Hero hero = new Hero();
            Enemy boss = new Enemy("Minotaur", 100);
            Enemy dummy = new Enemy("Dummy", 0);
            while (hero.Location != "quit")
            {
                switch (hero.Location)
                {
                    // Controls which room the player is in.
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
                    case "breakroom":
                        Rooms.BreakRoom(hero);
                        break;
                    case "trainingroom":
                        Rooms.TrainingRoom(hero, dummy);
                        break;
                    case "outsideroom":
                        Rooms.OutsideRoom(hero);
                        break;
                    case "bossfight":
                        boss = new Enemy("Minotaur", 100);
                        Rooms.BossFight(hero, boss);
                        break;
                    case "win":
                        Rooms.Win(hero, boss);
                        break;
                    case "lose":
                        Rooms.Lose(hero, boss);
                        break;
                    case "gameover":
                        Rooms.GameOver(hero, boss);
                        break;
                    default:
                        Console.Error.WriteLine($"You forgot to implement '{hero.Location}'!");
                        break;
                }
            }
            // End of game loop, game exits.
            Console.Clear();
            Console.WriteLine("Thank you for playing the game. Be well traveler.");
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
                    case "y" :
                    case "ok":
                        return true;
                    case "no":
                    case "n":
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