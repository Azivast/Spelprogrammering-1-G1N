using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void GameOver(Hero hero, Enemy enemy)
        {
            if (Program.AskYesOrNo("\n\nWould you like to play again? Yes/No: "))
                // No need to reset inventory / stats since hero and enemy are get overriden with new instances.
                hero.Location = "newgame";
            else hero.Location = "quit";
        }
    }
}