using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public class Ghost : Actor
    {
        private float frozenTimer;
        private bool IsFrozen = false;
        private float resetTimer;
        private const float RESETTIME = 1f;

        public Ghost() : base("pacman") {}
        public override void Create(Scene scene)
        {
            direction = -1;
            speed = 100.0f;
            moving = true;
            base.Create(scene);
            sprite.TextureRect = new IntRect(36, 0, 18, 18);
            
            scene.Events.EatCandy += (s, i) => frozenTimer = 5;
        }

        public override void Update(Scene scene, float deltaTime)
        {
            if (resetTimer > 0) moving = false;
            else moving = true;
            
            base.Update(scene, deltaTime);
            frozenTimer = MathF.Max(frozenTimer - deltaTime, 0.0f);
            resetTimer = MathF.Max(resetTimer - deltaTime, 0.0f);
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
        
        protected override void CollideWith(Scene scene, Entity e) 
        {
            if (resetTimer <= 0 && e is Pacman) 
            {
                if (frozenTimer <= 0)
                {
                    scene.Events.PublishLoseHealth(1);
                }
                else
                {
                    scene.Events.PublishGainScore(500);
                }
                Reset();
            }
 
        }

        protected override void Reset()
        {
            base.Reset();
            resetTimer = RESETTIME;
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
                frame += 18;
                if (frame % 36 == 0)
                {
                    frame = 0;
                }
                animationTimer = ANIMATIONTIME;
            }

            int y;
            if (IsFrozen) y = 18;
            else y = 0; 
            
            sprite.TextureRect = new IntRect(36+frame, y, 18, 18);
        }
    }
}