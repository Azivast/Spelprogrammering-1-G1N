using System;
using System.Numerics;
using breakout;
using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    public class Ball
    {
        private static readonly Vector2f StartingPosition = new Vector2f(400, 100);
        private static readonly int StartingHealth = 3;
        private static readonly Vector2f StartingDirection = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
        
        public Sprite Sprite;
        public const float Diameter = 20f;
        public const float Radius = Diameter * 0.5f;
        public Vector2f Direction = StartingDirection;
        public int Health = StartingHealth;
        public int Score = 0;
        private Text gui;

        public void Reset()
        {
            Score = 0;
            Health = StartingHealth;
            Sprite.Position = StartingPosition;
            Direction = StartingDirection;
        }
        
        public Ball()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/ball.png");
            Sprite.Position = StartingPosition;

            Vector2f ballTextureSize = (Vector2f)Sprite.Texture.Size;
            Sprite.Origin = 0.5f * ballTextureSize;
            Sprite.Scale = new Vector2f(Diameter / ballTextureSize.X, Diameter / ballTextureSize.Y);

            gui = new Text();
            gui.CharacterSize = 24;
            gui.Font = new Font("assets/future.ttf");
        }

        public void Reflect(Vector2f normal)
        {
            // Reflects direction against the normal for bouncing.
            Direction -= normal * (2 * (Direction.X * normal.X + Direction.Y * normal.Y));
        }

        public void Update(float deltaTime, Paddle paddle)
        {
            var newPos = Sprite.Position;
            newPos += Direction * 100f * deltaTime;
            if (newPos.X > Program.ScreenW - Radius) // Right wall
            {
                newPos.X = Program.ScreenW - Radius;
                Reflect(new Vector2f(-1, 0));
            }
            else if (newPos.X < Radius) // Left wall
            {
                newPos.X = Radius;
                Reflect(new Vector2f(1, 0));
            }
            if (newPos.Y < Radius) // Top wall
            {
                newPos.Y = Radius;
                Reflect(new Vector2f(0, 1));
            }
            else if (newPos.Y > Program.ScreenH - Radius) // Bottom wall
            {
                newPos.X = paddle.Sprite.Position.X - Radius;
                newPos.Y = paddle.Sprite.Position.Y - Radius - 10;
                if (new Random().Next() % 2 == 0)
                {
                    Direction.X = 1;
                    Direction = Collision.Normalized(Direction);
                }
                else // rand == 1
                {
                    Direction.X = -1;
                    Direction = Collision.Normalized(Direction);
                }

                Health--;
            }

            Sprite.Position = newPos;
            
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
            
            // Draw GUI
            gui.DisplayedString = $"Health: {Health}";
            gui.Position = new Vector2f(12, 8);
            target.Draw(gui);
            
            gui.DisplayedString = $"Score: {Score}";
            gui.Position = new Vector2f(Program.ScreenW - gui.GetGlobalBounds().Width - 12, 8);
            target.Draw(gui);
        }
    }
}