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
                    hero.Location = "win";
                else if (hero.IsDead)
                    hero.Location = "lose";
                
                // Calc attack damage for this round.
                int damage = hero.HeroAttack();
                enemy.Damage(damage);
                damage = enemy.RollEnemyDamage(hero);
                
                // Check how player will counter enemy
                string playerChoice = Program.Ask($"The {enemy.Name} is gearing up for an attack: " +
                                          "Dodge/Parry/Heal ").ToLower();
                int roll = roll = Program.RollD6();;
                switch (playerChoice)
                {
                    case "dodge":
                        if (roll <= 1)
                        {
                            damage = 0;
                            Console.WriteLine($"You dodge the {enemy.Name}'s attack.");
                        }
                        else
                        {
                            Console.WriteLine("You try to dodge but are too slow. " +
                                              $"You take {damage} damage.");

                            hero.Damage(damage);
                        }

                        break;

                    case "parry":
                        if (roll < 2)
                        {
                            damage = damage / 4;
                            enemy.Damage(damage);
                            hero.Damage(damage);
                            Console.WriteLine($"You attempt to parry the {enemy.Name}'s attack.\n" +
                                              $"You both stumbles and take {damage} damage each.\n");
                        }
                        else
                        {
                            Console.WriteLine("You try to parry but fail.\n" +
                                              $"You take {damage} damage.");

                            hero.Damage(damage);
                        }

                        break;

                    case "heal":
                        if (roll < 2)
                        {
                            int healAmount = new Random().Next() % 6 + 20; // amount to heal: 20-25
                            hero.Heal(healAmount);
                            Console.WriteLine("You channel your inner aura in an attempt to heal.\n" +
                                              $"You recover {healAmount} hp.");
                        }
                        else
                        {
                            Console.WriteLine("You channel your inner aura in an attempt to " +
                                              $"heal but {enemy.Name} interrupts you.\n" +
                                              $"You take {damage} damage.");

                            hero.Damage(damage);
                        }
                        break;
                }
                Console.WriteLine("Press any key to finish the round.");
                Console.ReadKey();
            }
        }
    }
}