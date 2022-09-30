using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class GUI : Entity
    {

        private readonly string font = "pixel-font";
        private Text scoreText = new Text();
        private int maxHealth = 4;
        private int currentHealth;
        private int currentScore;
        public GUI() : base("pacman") {}
        
        public override void Create(Scene scene)
        {
            scoreText.Font = scene.Assets.LoadFont(font);
            scoreText.CharacterSize = 72;
            scoreText.Scale = new Vector2f(0.5f, 0.5f);
            
            scoreText.DisplayedString = "Score";
            currentHealth = maxHealth;

            scene.Events.LoseHealth += OnLoseHealth;
            scene.Events.GainScore += OnScoreGain;
            
            base.Create(scene);
        }

        public override void Render(RenderTarget target)
        {
            sprite.Position = new Vector2f(36, 396);
            for (int i = 0; i < maxHealth; i++) 
            {
                sprite.TextureRect = i < currentHealth
                    ? new IntRect(72, 36, 18, 18) // Full heart
                    : new IntRect(72, 0, 18, 18); // Empty heart
                base.Render(target); 
                sprite.Position += new Vector2f(18, 0);
            }
            scoreText.DisplayedString = $"Score: {currentScore}";
            scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
            target.Draw(scoreText);
        }
        
        private void OnLoseHealth(Scene scene, int amount) 
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                DontDestroyOnLoad = false;
                scene.Events.LoseHealth -= OnLoseHealth;
                scene.Loader.Reload();
            }
        }
        
        private void OnScoreGain(Scene scene, int amount)
        {
            currentScore += amount;
            
            if (!scene.FindByType<Coin>(out _)) {
                DontDestroyOnLoad = true;
                scene.Loader.Reload();
            }
        }
    }
}