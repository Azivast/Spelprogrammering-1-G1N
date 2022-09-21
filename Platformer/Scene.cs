using System.Collections.Generic;
using SFML.Graphics;

namespace Platformer
{
    public class Scene
    {
        private readonly Dictionary<string, Texture> textures;
        private readonly List<Entity> entities;

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
            if (textures.TryGetValue(name, out Texture found))
            {
                return found;
            }

            string fileName = $"assets/{name}.png";
            Texture texture = new Texture(fileName);
            textures.Add(name, texture);
            return texture;
        }

        public void UpdateAll(float deltaTime)
        {
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
                else i++; // Prevents skipping entities[i+1] when removing entities[i].
            }
        }
        
        public void RenderAll(RenderTarget target)
        {
            foreach (Entity entity in entities)
            {
                entity.Render(target);
            }
        }
    }
}