using SFML.Graphics;

namespace Pacman
{
    public class Candy : Entity
    {
        
        public Candy() : base("pacman") {}
        
        public override void Create(Scene scene)
        {
            sprite.TextureRect = new IntRect(54, 36, 18, 18);
            base.Create(scene);
        }
        
        protected override void CollideWith(Scene scene, Entity e) {
            if (e is Pacman) {
                scene.PublishCandyEaten(1);
                Dead = true;
            }
        }
    }
}