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
                    case "thirdroom":
                        ThirdRoom(hero);
                        break;
                    case "outsideroom":
                        OutsideRoom(hero);
                        break;
                    case "bossfight":
                        BossFight(hero);
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
            Console.WriteLine("You use the key on a locked door and walk in. \n" + "Inside the locked room you find a shiny sword");

            if (AskYesOrNo("Do you want it instead of your wooden sword? Yes/No: "))
            {
                hero.Items.Remove("Wooden Sword");
                hero.Items.Add("Shiny Sword");
                Console.WriteLine("You picked the shiny sword.");
            }
            
            Console.WriteLine("You proceed into the next room.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            hero.Location = "thirdroom";
        }

        static void OutsideRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("As you enter the room a large minotaur wakes up and takes out his weapon. " +
                              "Prepare for battle!");

            string weaponChoice;
            if (!hero.Items.Contains("Knife")) // player has no (kn/l)ife
            {
                if (hero.Items.Contains("Wooden Sword"))
                {
                    Console.WriteLine("You take out your wooden sword.");
                    hero.EquipedWeapon = "Wooden Sword";
                }
                else // only shiny sword
                {
                    Console.WriteLine("You take out your shiny sword. It glows a little.");
                    hero.EquipedWeapon = "Shiny Sword";
                }
            }
            else // player has a knife. If they have a knife, they cannot access the Shiny Sword
            {
                weaponChoice = Ask("Which weapon do you wish to use? Knife/Wooden Sword: ").ToLower();
                switch (weaponChoice)
                {
                    case "wooden sword":
                    case "sword":
                        Console.WriteLine("You take out your wooden sword.");
                        hero.EquipedWeapon = "Wooden Sword";
                        break;
                    case "knife":
                        Console.WriteLine("You take out your Knife.");
                        hero.EquipedWeapon = "Knife";
                        break;
                }
            }
            
            Console.WriteLine("Press any key to start battle.");
            Console.ReadKey();

            hero.Location = "bossfight";
        }

        static void BossFight(Hero hero, Enemy enemy)
        {
            Console.Clear();
            while (!enemy.IsDead)
            {
                int damage = hero.HeroAttack();
                enemy.Health -= damage;
                damage = enemy.RollEnemyDamage(hero);
                
                string playerChoice = Ask("The minotaur is gearing up for an attack: Dodge/Jump/Parry ").ToLower();
                switch (playerChoice)
                {
                    case "dodge":
                        int roll = RollD6();
                        if (roll <= 1)
                        {
                            damage = 0;
                            Console.WriteLine($"You dodge the {enemy.Name}'s attack");
                        }
                }
                
            }
        }

        static void ThirdRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("On the floor before you lies a lifeless corpse.\n" +
                              "Its hand is clasped around something shiny.\n");

            if (AskYesOrNo("Do you want to loot the corpse? Yes/No: "))
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
            
            Console.WriteLine("You leave the corpse and continue into the next room. \n" +
                              "Press any key to continue.");
            Console.ReadKey();
            
            hero.Location = "outsideroom";
        }
    }
    class Hero
    {
        public string Name = "";
        public int Health = 100;
        public List<string> Items = new List<string>(); // Note: use uppercase first letter for items.
        public string Location = "newgame";
        public string EquipedWeapon = "";

        public int HeroAttack()
        {
            int damage = 0;
            Console.WriteLine("Roll the dice to determine your attack damage\n" + "Press any key to continue");
            Console.ReadKey();
            if (EquipedWeapon == "Shiny Sword")
            {
                damage = new Random().Next() % 6 + 15; // 15-20 damage
                Console.WriteLine("You sliced though the tough hide of the minotaur. \n" + 
                                  $"Your attack dealt {damage} damage");
                return damage;

            }
            else if (EquipedWeapon == "Wooden Sword")
            {
                Console.WriteLine("You gave the minotaur a splinter with your wooden sword. \n" + 
                                  $"Your attack dealt {damage} damage");
                damage = new Random().Next() % 7 + 10; // 10-16 damage
                return damage;
            }
            else // Knife
            {
                damage = (new Random().Next() % 8 + 3); // 3-10 damage
                int damage2 = (new Random().Next() % 8 + 3); // 3-10 damage
                Console.WriteLine("You stabbed twice with your knife. \n" + 
                                  $"Your attacks dealt {damage} and {damage2} damage");
                damage += damage2;
                return damage;
            }
        }
    }
    
    class Enemy
    {
        public string Name = "";
        public int Health = 100;
        public bool IsDead = false;

        public Enemy(string name, int health)
        {
            Name = name;
            Health = health;
        }
        
        public int RollEnemyDamage(Hero hero)
        {
            if (hero.Items.Contains("Cursed Amulet"))
                return new Random().Next() % 6 + 10; // 10-15 damage

            else return new Random().Next() % 6 + 5; // 5-10 damage
        }
    }
}