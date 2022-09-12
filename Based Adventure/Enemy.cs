using System;

namespace Based_Adventure
{
    public class Enemy
    {
        public string Name = "";
        private int maxHealth = 100;
        public int Health = 100;
        public bool IsDead = false;

        public Enemy(string name, int health)
        {
            Name = name;
            maxHealth = health;
            Health = maxHealth;
        }
        
        public int RollEnemyDamage(Hero hero)
        {
            if (hero.Items.Contains("Cursed Amulet"))
                return new Random().Next() % 6 + 10; // 10-15 damage

            else return new Random().Next() % 6 + 5; // 5-10 damage
        }
        
        public void Damage(int amount)
        {
            Health -= amount;
            if (Health > 0) return;
            Health = 0;
            IsDead = true;
        }
    }
}