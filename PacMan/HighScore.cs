using System;
using System.IO;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Pacman
{
    public class HighScore : Entity
    {

        private readonly string font = "pixel-font";
        private Text scoreText = new Text();
        private Text restartText = new Text();
        private int currentScore;
        private int storedHighScore;
        private string file = $"assets/highscore.txt";

        public HighScore(int currentScore) : base("pacman")
        {
            this.currentScore = currentScore;
        }
        
        public override void Create(Scene scene)
        {
            // Initialize variables
            scoreText.Font = scene.Assets.LoadFont(font);
            scoreText.CharacterSize = 36;
            scoreText.Scale = new Vector2f(0.5f, 0.5f);
            scoreText.DisplayedString = "High Score";
            
            restartText.Font = scene.Assets.LoadFont(font);
            restartText.CharacterSize = 36;
            restartText.Scale = new Vector2f(0.5f, 0.5f);
            restartText.DisplayedString = "Press SPACE to play again";

            // Read stored high score and write over it if current score is higher.
            storedHighScore = ReadScoreFromFile();
            if (currentScore > storedHighScore)
            {
                storedHighScore = currentScore;
                WriteScoreToFile(currentScore);
            }

            base.Create(scene);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            
            // Check if play again
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                scene.Loader.Reload();
        }
        
        public override void Render(RenderTarget target)
        {
            // Print out high score and restart message.
            scoreText.DisplayedString = $"High Score: {storedHighScore}";
            scoreText.Position = new Vector2f(target.GetView().Center.X - scoreText.GetGlobalBounds().Width/2, 100);
            target.Draw(scoreText);
            
            restartText.Position = new Vector2f(target.GetView().Center.X - restartText.GetGlobalBounds().Width/2, 200);
            target.Draw(restartText);
        }

        private int ReadScoreFromFile()
        {
            return int.Parse(File.ReadAllText(file, Encoding.UTF8).Trim());
        }
        
        private void WriteScoreToFile(int score)
        { 
            File.WriteAllText(file, $"{score}");
        }
    }
}