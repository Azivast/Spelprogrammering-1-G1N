﻿using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public class Scene
    {
        private readonly List<Entity> entities;
        public readonly SceneLoader Loader;
        public readonly AssetManager Assets;

        public Scene()
        {
            entities = new List<Entity>();
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
        
        public bool FindByType<T>(out T found) where T : Entity
        {
            foreach (Entity entity in entities)
            {
                if (!entity.Dead && entity is T typed) {
                    found = typed;
                    return true;
                }
            }
            found = default(T);
            return false;
        }
        
        public IEnumerable<Entity> FindIntersects(FloatRect bounds) 
        {
            int lastEntity = entities.Count - 1;
            for (int i = lastEntity; i >= 0; i--) // Iterate backwards so new elements can be added without disturbing
            {
                Entity entity = entities[i];
                if (entity.Dead) continue;
                if (entity.Bounds.Intersects(bounds)) 
                {
                    yield return entity;
                }
            }
        }

        // Loop backwards through entities and remove all
        public void Clear()
        {
            for (int i = entities.Count - 1; i >= 0; i--) {
                Entity entity = entities[i];
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }
}