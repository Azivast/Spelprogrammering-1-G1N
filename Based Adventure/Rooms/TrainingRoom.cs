using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void TrainingRoom(Hero hero, Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine("You enter a well-lit room. The only thing within the room is a training dummy, "+
                              "begging to be hit. ");
            
            // Choose a weapon
            string weaponChoice;
            if (!hero.Items.Contains("Knife")) // player has no knife
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
                weaponChoice = Program.Ask("Which weapon do you wish to use? Knife/Wooden Sword: ").ToLower();
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
            
            Console.WriteLine("Press any key to start training.");
            Console.ReadKey();
            
            while (true)
            {
                Console.Clear();
                string playerChoice;
                do
                {
                    playerChoice = Program.Ask("What would you like to do? Attack/Dodge/Parry/Heal/Leave: ").ToLower();
                    // Calc attack damage for this round.
                    int monsterAttack = enemy.EnemyTurn(hero);
                    switch (playerChoice)
                    {
                        case "attack":
                            int heroAttack = hero.HeroAttack(enemy);
                            enemy.Damage(heroAttack);
                            break;
                        case "dodge":
                            Console.WriteLine($"The dummy does not attack. You successfully dodged nothing.");
                            break;
                        case "parry":
                            Console.WriteLine($"You return the dummy's menacing stare.");
                            break;
                        case "heal":
                            int healAmount = new Random().Next() % 6 + 20; // amount to heal: 20-25
                                hero.Heal(healAmount);
                                Console.WriteLine("You channel your inner aura in an attempt to heal.\n" +
                                                  $"You recover {healAmount} hp.");
                            break;
                        case "leave":
                            Console.WriteLine("You sheathe your weapon. Leaving the dummy alone, yet again. ");
                            Console.WriteLine("Press any key to leave the room.");
                            Console.ReadKey();
                            hero.Location = "breakroom";
                            return;
                        default:
                            playerChoice = "";
                            Console.WriteLine("That's not a valid option.");
                            break;
                    }
                } while (playerChoice == "");

                Console.WriteLine("Press any key to finish the round.");
                Console.ReadKey();
            }
        }
    }
}