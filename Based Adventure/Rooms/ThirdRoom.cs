using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void ThirdRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("On the floor before you lies a lifeless corpse.\n" +
                              "Its hand is clasped around something shiny.\n");

            if (Program.AskYesOrNo("Do you want to loot the corpse? Yes/No: "))
            {
                Console.WriteLine("You pick up an old silver necklace.");
                if (Program.RollD6() >= 3)
                {
                    Console.WriteLine("A warm feeling spread over your body.");
                    hero.Items.Add("Blessed Amulet");
                }
                else
                {
                    Console.WriteLine("A cold shiver runs your spine.");
                    hero.Items.Add("Cursed Amulet");
                }
            }
            
            Console.WriteLine("You leave the corpse and continue into the next room. \n" +
                              "Press any key to continue.");
            Console.ReadKey();
            
            hero.Location = "outsideroom";
        }
    }
}