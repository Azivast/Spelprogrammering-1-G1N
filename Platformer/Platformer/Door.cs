using SFML.Graphics;
using SFML.System;

namespace Platformer
{
    public class Door : Entity
    {
        public string NextRoom;
        private bool unlocked;
        
        public Door() : base("tileset")
        {
            sprite.TextureRect = new IntRect(180, 103, 18, 23);
            sprite.Origin = new Vector2f(9, 11);
        }

        public override void Update(Scene scene, float deltaTime)
        {
            // Move to next room if door is unlocked and player collides.
            if (scene.FindByType<Hero>(out Hero hero))
            {
                if (unlocked && Collision.RectangleRectangle(Bounds, hero.Bounds, out _))
                {
                    scene.Load(NextRoom);
                }
            }
        }

        public void UnlockDoor()
        {
            unlocked = true;
            sprite.Color = Color.Black;
        }
    }
}