using System;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Pacman
{
    public class Pacman : Actor
    {
        public Pacman() : base("pacman") {}
        public override void Create(Scene scene)
        {
            speed = 100.0f;
            base.Create(scene);
            sprite.TextureRect = new IntRect(0, 0, 18, 18);
            scene.Events.LoseHealth += OnLoseHealth; // Subscribe to event
        }
        
        private void OnLoseHealth(Scene scene, int amount) 
        {
            Reset(); // inherited from actor.cs
        }
        
        public override void Destroy(Scene scene) {
            base.Destroy(scene);
            scene.Events.LoseHealth -= OnLoseHealth;
        }

        protected override int PickDirection(Scene scene)
        {
            int dir = direction;
            if (Keyboard.IsKeyPressed(Right))
            {
                dir = 0;
                moving = true;
            }
            else if (Keyboard.IsKeyPressed(Up))
            {
                dir = 1;
                moving = true;
            }
            else if (Keyboard.IsKeyPressed(Left))
            {
                dir = 2;
                moving = true;
            }
            else if (Keyboard.IsKeyPressed(Down))
            {
                dir = 3;
                moving = true;
            }
            if (IsFree(scene, dir)) return dir; // Return direction if pacman won't collide
            if (!IsFree(scene, direction)) moving = false; // Otherwise stop moving
            return direction;
        }

        protected override void Animate(float deltaTime)
        {
            base.Animate(deltaTime);
            
            if (animationTimer <= 0)
            {
                frame += 18; // next frame is 18px away
                if (frame % 36 == 0) // reset to first frame
                {
                    frame = 0;
                }
                animationTimer = ANIMATIONTIME;
            }


            // Change sprite based on direction.
            switch (direction)
            {
                case 0: // right
                    sprite.TextureRect = new IntRect(frame, 0, 18, 18);
                    break;
                case 1: // up
                    sprite.TextureRect = new IntRect(frame, 18, 18, 18);
                    break;
                case 2: // left
                    sprite.TextureRect = new IntRect(frame, 36, 18, 18);
                    break;
                case 3: // down
                    sprite.TextureRect = new IntRect(frame, 54, 18, 18);
                    break;
            }
        }
    }
}