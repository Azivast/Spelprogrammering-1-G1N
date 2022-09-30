using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace Platformer
{
    public class Scene
    {
        private readonly Dictionary<string, Texture> textures;
        private readonly List<Entity> entities;
        private string currentScene;
        private string nextScene;

        public Scene()
        {
            textures = new Dictionary<string, Texture>();
            entities = new List<Entity>();
        }
        
        public void Spawn(Entity entity)
        {
            entities.Add(entity);
            entity.Create(this);
        }

        public Texture LoadTexture(string name)
        {
            // Return texture if texture already loaded.
            if (textures.TryGetValue(name, out Texture found)) 
            {
                return found;
            }
            
            //Otherwise, load new texture.
            string fileName = $"assets/{name}.png";
            Texture texture = new Texture(fileName);
            textures.Add(name, texture);
            return texture;
        }

        public void UpdateAll(float deltaTime)
        {
            HandleSceneChange();
            
            // Update all entities.
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                Entity entity = entities[i];
                entity.Update(this, deltaTime);
            }
            
            // Goes through entities and removes once dead.
            for (int i = 0; i < entities.Count;)
            {
                Entity entity = entities[i];
                if (entity.Dead) entities.RemoveAt(i);
                else i++; // Prevents skipping entities[i+1] when removing entities[i]
            }
        }
        
        public void RenderAll(RenderTarget target)
        {
            foreach (Entity entity in entities)
            {
                entity.Render(target);
            }
        }
        
        /// Moves as far as possible without colliding
        public bool TryMove(Entity entity, Vector2f movement)
        {
            entity.Position += movement;
            bool collided = false;
            // Check against all entities
            for (int i = 0; i < entities.Count; i++) 
            {
                Entity other = entities[i];
                if (!other.Solid) continue; // skip if non-solid
                if (other == entity) continue; // or self
                FloatRect boundsA = entity.Bounds;
                FloatRect boundsB = other.Bounds;
                // Check collision
                if (Collision.RectangleRectangle(boundsA, boundsB, out Collision.Hit hit)) 
                {
                    // Move player
                    entity.Position += hit.Normal * hit.Overlap;
                    i = -1; // Check everything once again, 
                    collided = true;
                }
            }
            return collided;
        }

        public void Reload()
        {
            nextScene = currentScene;
        }
        public void Load(string scene)
        {
            nextScene = scene;
            HandleSceneChange();
        }

        ///Checks for parameter T, where T must be a type of entity
        public bool FindByType<T>(out T found) where T : Entity
        {
            foreach (Entity entity in entities)
            {
                //Checks if the specific entity is of type T (In this case typed), and if it is "Dead".
                if (!entity.Dead && entity is T typed) {
                    found = typed;
                    return true;
                }
            }
            found = default(T);
            return false; // No entity of type were found
        }
        
        
        private void HandleSceneChange()
        {
            if (nextScene == null) return; // No scene
            
            //Remove all entities
            entities.Clear();
            
            Spawn(new Background());

            string file = $"assets/{nextScene}.txt";
            Console.WriteLine($"Loading scene '{file}'");
            
            // Read file to place entities line by line
            foreach (var line in File.ReadLines(file, Encoding.UTF8)) 
            {
                string parsed = line.Trim(); // Remove leading and trailing white spaces
                
                if (parsed.Length <= 0) // do nothing if line empty
                    continue;
                int commentAt = parsed.IndexOf('#');
                if (commentAt >= 0) // Inline comments. Use everything before '#'
                {
                    parsed = parsed.Substring(0, commentAt);
                    parsed = parsed.Trim();
                }
                string[] words = parsed.Split(" "); // Split string into an array. Seperated with [space]

                switch (words[0]) // Spawn based with type based on character at index
                {
                    case "w":
                        Spawn(new Platform { 
                        Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2]))
                    });
                        break;
                    case "h":
                        Spawn(new Hero() { Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2]))});
                        break;
                    case "d":
                        Spawn(new Door
                        {
                            Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2])),
                            NextRoom = words[3]
                        });
                        
                        break;
                    case "k":
                        Spawn(new Key() { Position = new Vector2f(int.Parse(words[1]), int.Parse(words[2]))});
                        break;
                    
                }

            }
            
            // Move to next scene
            currentScene = nextScene;
            nextScene = null;
        }
    }
}