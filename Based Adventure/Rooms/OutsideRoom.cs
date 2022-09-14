using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void OutsideRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("As you enter the room a large minotaur wakes up and takes out his weapon. " +
                              "Prepare for battle\n");
            
            if(hero.Items.Contains("Blessed Amulet"))
                Console.WriteLine("The blessed necklace shines as the monster approaches.\n");
            else if(hero.Items.Contains("Cursed Amulet"))
                Console.WriteLine("Negative energy flows from the cursed necklace as the monster approaches.\n");

            string weaponChoice;
            if (!hero.Items.Contains("Knife")) // player has no knife
            {
                if (hero.Items.Contains("Wooden Sword"))
                {
                    Console.WriteLine("You take out your wooden sword.");
                    hero.EquipedWeapon = "Wooden Sword";
                }
                else // only shiny sword
                {
                    Console.WriteLine("You take out your shiny sword. It glows a little.");
                    hero.EquipedWeapon = "Shiny Sword";
                }
            }
            else // player has a knife. If they have a knife, they cannot access the Shiny Sword
            {
                weaponChoice = Program.Ask("Which weapon do you wish to use? Knife/Wooden Sword: ").ToLower();
                switch (weaponChoice)
                {
                    case "wooden sword":
                    case "sword":
                        Console.WriteLine("You take out your wooden sword.");
                        hero.EquipedWeapon = "Wooden Sword";
                        break;
                    case "knife":
                        Console.WriteLine("You take out your Knife.");
                        hero.EquipedWeapon = "Knife";
                        break;
                }
            }
            
            Console.WriteLine("Press any key to start battle.");
            Console.ReadKey();

            hero.Location = "bossfight";
        }
    }
}