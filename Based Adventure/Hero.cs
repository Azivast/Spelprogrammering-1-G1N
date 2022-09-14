using System;
using System.Collections.Generic;

namespace Based_Adventure
{
    public class Hero
    {
        public string Name = "";
        private readonly int maxHealth = 50;
        public int Health;
        public bool IsDead = false;
        public List<string> Items = new List<string>(); // Note: use uppercase first letter for items.
        public string Location = "newgame";
        public string EquipedWeapon = "";

        public Hero()
        {
            Health = maxHealth;
        }
        
        // Checks what weapon the player is using and returning the damage dealt.
        public int HeroAttack(Enemy enemy)
        {
            int damage = 0;
            Console.WriteLine("Roll to determine your attack damage\n" + "Press any key to roll");
            Console.ReadKey();
            Console.Clear();
            if (EquipedWeapon == "Shiny Sword")
            {
                damage = new Random().Next() % 6 + 15; // 15-20 damage
                Console.WriteLine($"You slice though the tough hide of the {enemy.Name}. \n" + 
                                  $"Your attack deals {damage} damage");
                return damage;

            }
            else if (EquipedWeapon == "Wooden Sword")
            {
                damage = new Random().Next() % 7 + 2; // 2-8 damage
                Console.WriteLine($"You give the {enemy.Name} a splinter with your wooden sword. \n" + 
                                  $"Your attack deals {damage} damage");
                return damage;
            }
            else // Knife
            {
                damage = (new Random().Next() % 8 + 3); // 3-10 damage
                int damage2 = (new Random().Next() % 8 + 3); // 3-10 damage
                Console.WriteLine("You stab twice with your knife. \n" + 
                                  $"Your attacks deal {damage} and {damage2} damage");
                damage += damage2;
                return damage;
            }
        }
        
        public void Heal(int amount)
        {
            Health = Math.Clamp(Health + amount, 0, maxHealth); // clamp => cant heal past MaxHealth
        }
        
        // Damage to player
        public void Damage(int amount)
        {
            Health -= amount;
            if (Health > 0) return;
            Health = 0;
            IsDead = true;
        }
    }
}