using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void Lose(Hero hero, Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine($"The {enemy.Name} lands a deep cut and you stumble backwards. " +
                              "You feel light headed and your consciousness slips away." + 
                              "Your body lies lifeless on the floor.\n" +
                              $"You lose {hero.Name}.");
            hero.Location ="gameover";
        }
    }
}