namespace Pacman
{

    public class EventManager
    {
        public delegate void ValueChangedEvent(Scene scene, int value);
        
        public event ValueChangedEvent EatCandy;
        public event ValueChangedEvent GainScore;
        public event ValueChangedEvent LoseHealth;
        
        public int CandyEaten;
        public int ScoreGained;
        public int HealthLost;
        
        public void PublishCandyEaten(int amount) 
            => CandyEaten += amount;
        public void PublishGainScore(int amount) 
            => ScoreGained += amount;
        public void PublishLoseHealth(int amount) 
            => HealthLost += amount;


    
        public void Update(Scene scene)
        {
            if (CandyEaten != 0) 
            {
                EatCandy?.Invoke(scene, CandyEaten);
                CandyEaten = 0;
            }
            if (ScoreGained != 0) 
            {
                GainScore?.Invoke(scene, ScoreGained);
                ScoreGained = 0;
            }
            if (HealthLost != 0) 
            {
                LoseHealth?.Invoke(scene, HealthLost);
                HealthLost = 0;
            }
        }
    }
}