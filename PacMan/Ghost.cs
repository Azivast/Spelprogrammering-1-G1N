using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public class Ghost : Actor
    {
        private float frozenTimer;
        private bool IsFrozen = false;

        public Ghost() : base("pacman") {}
        public override void Create(Scene scene)
        {
            direction = -1;
            speed = 100.0f;
            moving = true;
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            
            // Subscribe to the EatCandy Event
            scene.Events.EatCandy += (s, i) => frozenTimer = 5;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
        }

        protected override int PickDirection(Scene scene)
        {
            // Check for valid moves
            List<int> validMoves = new List<int>();
            for (int i = 0; i < 4; i++) { 
                if ((i + 2) % 4 == direction) continue; // Can't turn around 180 degrees
                if (IsFree(scene, i)) validMoves.Add(i);
            }
            // Randomize which of the valid moves to make
            int r = new Random().Next(0, validMoves.Count);
            return validMoves[r];
        }
        
        // Public events in case of collisions
        protected override void CollideWith(Scene scene, Entity e) 
        {
            // Pacman and ghosts are immune while resetting
            if (resetTimer <= 0 && e is Pacman) 
            {
                if (frozenTimer <= 0 && ((Pacman)e).ResetTimer <= 0) 
                {
                    scene.Events.PublishLoseHealth(1);
                }
                else if (((Pacman)e).ResetTimer <= 0)
                {
                    scene.Events.PublishGainScore(500);
                }
                Reset();
            }
 
        }

        public override void Render(RenderTarget target)
        {
            if (frozenTimer > 0.0f) IsFrozen = true;
            else IsFrozen = false;
            
            base.Render(target);
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

            // If frozen use those textures instead (located 18px down on sprite sheet)
            int y;
            if (IsFrozen) y = 18;
            else y = 0; 
            
            sprite.TextureRect = new IntRect(36+frame, y, 18, 18);
        }
    }
}