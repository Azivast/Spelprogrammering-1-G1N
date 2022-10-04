using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class Entity
    {
        private string textureName = "";
        protected Sprite sprite;
        public bool Dead;
        public bool DontDestroyOnLoad = false;

        public Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        
        public virtual FloatRect Bounds => 
            sprite.GetGlobalBounds(); // Hitbox
        
        public virtual bool Solid => false;

        protected Entity(string textureName)
        {
            this.textureName = textureName;
            sprite = new Sprite();
        }
        
        public virtual void Create(Scene scene)
        {
            sprite.Texture = scene.Assets.LoadTexture(textureName);
        }
        
        public virtual void Destroy(Scene scene) {} // to be overriden

        protected virtual void CollideWith(Scene s, Entity other) {}
            // Empty -> Overridden by implementing classes
        
        public virtual void Update(Scene scene, float deltaTime)
        {
            foreach (Entity found in scene.FindIntersects(Bounds)) 
            {
                CollideWith(scene, found);
            }
        }
        
        public virtual void Render(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}