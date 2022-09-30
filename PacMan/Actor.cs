using System;
using System.Buffers.Text;
using System.Linq;
using SFML.Graphics;
using SFML.System;

namespace Pacman
{
    public class Actor : Entity
    {
        private bool wasAligned;
        protected float speed;
        protected int direction;
        protected bool moving;
        protected Vector2f originalPosition;
        protected float originalSpeed;
        protected Actor(string textureName) : base(textureName) {}

        protected virtual void Reset()
        {
            wasAligned = false;
            Position = originalPosition;
            speed = originalSpeed;
        }
        
        public override void Create(Scene scene)
        {
            base.Create(scene);
            originalPosition = Position;
            originalSpeed = speed;
            Reset();
        }
        
        // Will only work when FPS >= 100, otherwise actors might move 2 far. And never be aligned, resulting in no way to turn
        protected bool IsAligned =>
            (int) MathF.Floor(Position.X) % 18 == 0 &&
            (int) MathF.Floor(Position.Y) % 18 == 0;
        
        protected bool IsFree(Scene scene, int dir) 
        {
            Vector2f at = Position + new Vector2f(9, 9);
            at += 18 * ToVector(dir);
            FloatRect rect = new FloatRect(at.X, at.Y, 1, 1);
            return !scene.FindIntersects(rect).Any(e => e.Solid);
        }
        
        protected static Vector2f ToVector(int dir) 
        {
            // TODO: if 0: return right, 1: return up and so on.
            switch (dir)
            {
                case 0:
                    return new Vector2f(1, 0);
                case 1:
                    return new Vector2f(0, -1);
                case 2:
                    return new Vector2f(-1, 0);
                case 3:
                    return new Vector2f(0, 1);
                default:
                    throw new Exception($"{dir} is not a valid direction.");
            }
        }
        protected virtual int PickDirection(Scene scene) { return 0; }

        public override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);
            if (IsAligned)
            {
                if (!wasAligned)
                {
                    direction = PickDirection(scene);
                }

                if (moving)
                {
                    wasAligned = true;
                }
            }
            else
            {
                wasAligned = false;
            }

            if (!moving) return;
            Position += ToVector(direction) * (speed * deltaTime);
            Position = MathF.Floor(Position.X) switch
            {
                < 0 => new Vector2f(432, Position.Y),
                > 432 => new Vector2f(0, Position.Y),
                _ => Position
            };
        }
    }
}