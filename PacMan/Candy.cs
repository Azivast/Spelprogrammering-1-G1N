using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class Candy : Entity
    {
        
        public Candy() : base("pacman") {}
        
        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(56, 39, 14, 13);
            sprite.Origin = new Vector2f(-2, -3);
            base.Create(scene);
        }
        
        protected override void CollideWith(Scene scene, Entity e) {
            if (e is Pacman) {
                scene.Events.PublishCandyEaten(1);
                Dead = true;
            }
        }
    }
}