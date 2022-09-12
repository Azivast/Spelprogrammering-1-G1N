using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void LockedRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("You use the key on a locked door and walk in. \n" + "Inside the locked room you find a shiny sword");

            if (Program.AskYesOrNo("Do you want it instead of your wooden sword? Yes/No: "))
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
    }
}