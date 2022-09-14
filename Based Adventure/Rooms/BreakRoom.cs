using System;

namespace Based_Adventure
{
    // All rooms are accessible under the 'Rooms' class.
    // It is only split into multiple files for readability.
    public static partial class Rooms 
    {
        public static void BreakRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("You walk into a large chamber, the high ceiling is rather unusual for this dungeon. \n" +
                              "Two doorways lead further into the dungeon. \n" +
                              "The left passage leads down a dark hallway, and the right leads to a lit room");
            
            string answer;
            do
            {
                answer = Program.Ask("Right or Left?: ");
                switch (answer.ToLower())
                {
                    case "right":
                        hero.Location = "trainingroom";
                        break;
                    case "left":
                        hero.Location = "outsideroom";
                        break;
                    default:
                        Console.WriteLine("That is not a valid option. ");
                        answer = "";
                        break;
                }
            } while (answer == "");
        }
    }
}