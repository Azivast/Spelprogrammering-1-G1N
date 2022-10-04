using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void Win(Hero hero, Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine($"The {enemy.Name} takes a step back and collapses on the floor. " +
                              "Its body lies lifeless.\n\n" +
                              "You continue past it and go through the last door.\n" + 
                              $"Outside freedom greets you. Congratulations {hero.Name}, you win!");
            hero.Location ="gameover";
        }
    }
}