// namespace Pacman
// {
//     public delegate void ValueChangedEvent(Scene scene, int value);
//     public class EventManager
//     {
//
//         public event ValueChangedEvent EatCandy;
//         public event ValueChangedEvent GainScore;
//         public event ValueChangedEvent LoseHealth;
//         public int CandyEaten;
//         public int ScoreGained;
//         public int HealthLost;
//         
//         public void PublishCandyEaten(int amount) 
//             => candyEaten += amount;
//         public void PublishGainScore(int amount) 
//             => scoreGained += amount;
//         
//         public void PublishLoseHealth(int amount) 
//             => healthLost += amount;
//     }
// }