using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void Corridor(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("You exit the room and find yourself standing in a dark " +
                              "hallway. You can either enter another room on your right " +
                              "side, or continue down the hallway on your left.");
            
            string answer;
            do
            {
                answer = Program.Ask("Right or Left?: ");
                switch (answer.ToLower())
                {
                    case "right":
                        if (hero.Items.Contains("Key"))
                        {
                            hero.Location = "lockedroom";
                            hero.Items.Remove("Key");
                        }
                        else
                        {
                            Console.WriteLine("You inspect the door. It needs a key. " +
                                              "You do not have one and walk back.");
                            answer = "";
                            break;
                        }
                        Console.WriteLine("You picked up the key.");
                        break;
                    case "left":
                        hero.Location = "thirdroom";
                        break;
                }
            } while (answer == "");
        }
    }
}