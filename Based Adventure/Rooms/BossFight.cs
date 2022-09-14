using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void BossFight(Hero hero, Enemy enemy)
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("----------------------\n" +
                                 $"{enemy.Name} HP: {enemy.Health}\n" +
                                 $"Your HP: {hero.Health}\n" +
                                  "----------------------");

                // Check if player has won or lost
                if (enemy.IsDead)
                {
                    hero.Location = "win";
                    break;
                }
                else if (hero.IsDead)
                {
                    hero.Location = "lose";
                    break;
                }
                
                Console.Write($"The {enemy.Name} is gearing up for an attack.\n");
                string playerChoice;
                do
                {
                    playerChoice = Program.Ask("What would you like to do? Attack/Dodge/Parry/Heal: ").ToLower();
                    int roll = Program.RollD6();
                    if (hero.Items.Contains("Blessed Amulet")); // higher chance of success with blessed amulet
                        roll++;
                    // Calc attack damage for this round.
                    int monsterAttack = enemy.EnemyTurn(hero);
                    switch (playerChoice)
                    {
                        case "attack":
                            int heroAttack = hero.HeroAttack();
                            enemy.Damage(heroAttack);
                            if (monsterAttack == 0)
                                Console.WriteLine($"The {enemy.Name} catches their breath.");
                            else
                            {
                                Console.WriteLine($"The {enemy.Name} attacks back, dealing {monsterAttack} damage to you.");
                                hero.Damage(monsterAttack);
                            }
                            break;
                        case "dodge":
                            if (roll >= 5)
                            {
                                monsterAttack = 0;
                                Console.WriteLine($"You dodge the {enemy.Name}'s attack.");
                            }
                            else if (monsterAttack == 0)
                                Console.WriteLine($"The {enemy.Name} catches their breath.");
                            else
                            {
                                Console.WriteLine("You try to dodge but are too slow. " +
                                                  $"You take {monsterAttack} damage.");

                                hero.Damage(monsterAttack);
                            }

                            break;

                        case "parry":
                            if (monsterAttack == 0)
                                Console.WriteLine($"The {enemy.Name} catches their breath.");
                            else if (roll >= 5)
                            {
                                monsterAttack = monsterAttack / 2;
                                enemy.Damage(monsterAttack);
                                hero.Damage(monsterAttack);
                                Console.WriteLine($"You attempt to parry the {enemy.Name}'s attack.\n" +
                                                  $"You both stumbles and take {monsterAttack} damage each.\n");
                            }
                            else
                            {
                                Console.WriteLine("You try to parry but fail.\n" +
                                                  $"You take {monsterAttack} damage.");

                                hero.Damage(monsterAttack);
                            }

                            break;

                        case "heal":
                            if (monsterAttack <= 0)
                            {
                                int healAmount = new Random().Next() % 6 + 20; // amount to heal: 20-25
                                hero.Heal(healAmount);
                                Console.WriteLine("You channel your inner aura in an attempt to heal.\n" +
                                                  $"You recover {healAmount} hp.");
                            }
                            else
                            {
                                Console.WriteLine("You channel your inner aura in an attempt to " +
                                                  $"heal but the {enemy.Name} interrupts you.\n" +
                                                  $"You take {monsterAttack} damage.");

                                hero.Damage(monsterAttack);
                            }

                            break;
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