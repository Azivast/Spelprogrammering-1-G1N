using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SFML.System;

namespace Pacman
{
    public class SceneLoader
    {
        private readonly Dictionary<char, Func<Entity>> loaders;
        private string currentScene = "", nextScene = "";

        public SceneLoader() 
        {
            // Associate character with their counterpart class.
            loaders = new Dictionary<char, Func<Entity>> 
            { 
                {'#', () => new Wall()},
                {'p', () => new Pacman()},
                {'c', () => new Candy()},
                {'.', () => new Coin()},
                {'g', () => new Ghost()}
            };
        }
        
        private bool Create(char symbol, out Entity created)
        {
            if (loaders.TryGetValue(symbol, out Func<Entity> loader))
            {
                created = loader();
                return true;
            }

            created = null;
            return false;
        }
        public void HandleSceneLoad(Scene scene) {
            if (nextScene == "") return;
            scene.Clear();

            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loading scene '{file}'");

            // Loop through each line, character by character
            int row = 0;
            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                for (int column = 0; column < line.Length; column++)
                {
                    char currentChar = line[column];
                    
                    // Spawn entity based on character in scene file
                    if (Create(currentChar, out Entity entity))
                    {
                        entity.Position = new Vector2f(column * 18, row * 18);
                        scene.Spawn(entity);
                    }
                }
                // Move to next row
                row++;
            }

            // Spawn GUI if there isn't already one.
            if (!scene.FindByType<GUI>(out _))
            {
                scene.Spawn(new GUI());
            }
            
            currentScene = nextScene;
            nextScene = "";
        }
        
        public void Load(string scene) => nextScene = scene;
        public void Reload() => nextScene = currentScene;
        
    }
}