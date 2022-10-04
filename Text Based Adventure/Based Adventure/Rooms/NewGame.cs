using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void NewGame(Hero hero)
        {
            Console.Clear();
            string name = "";
            
            do
            {
                name = Program.Ask("What is your name, Adventurer? ");
            } while (!Program.AskYesOrNo($"So, {name} it is?  Yes/No: "));

            hero.Name = name;
            hero.Location = "tableroom";
        }
    }
}