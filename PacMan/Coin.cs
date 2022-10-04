using System.Buffers.Text;
using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class Coin : Entity
    {

        public Coin() : base("pacman") {}
        
        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(39, 39, 12, 12);
            sprite.Origin = new Vector2f(-3, -3);
            base.Create(scene);
        }
        
        protected override void CollideWith(Scene scene, Entity e) {
            if (e is Pacman) {
                scene.Events.PublishGainScore(100);
                Dead = true;
            }
        }
    }
}