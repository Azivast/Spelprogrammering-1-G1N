using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pacman
{
    public class SceneLoader
    {
        private readonly Dictionary<char, Func<Entity>> loaders;
        private string currentScene = "", nextScene = "";

        public SceneLoader() 
        {
            loaders = new Dictionary<char, Func<Entity>> 
            {
                {'#', () => new Wall()}
            };
        }

        //
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
            // TODO: Load scene file
            
            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loading scene '{file}'");

            int row = 0;
            foreach (var line in File.ReadLines(file, Encoding.UTF8))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    char currentChar = line[i];
                    
                    Create(currentChar, );
                }
                row++;
            }
                currentScene = nextScene;
            nextScene = "";
        }
        
        
    }
}