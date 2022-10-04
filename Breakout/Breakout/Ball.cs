using System;
using System.ComponentModel.Design;
using System.Numerics;
using breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    public class Ball
    {
        private static readonly Vector2f StartingPosition = new Vector2f(250, 500);
        private static readonly int StartingHealth = 3;
        private static readonly Vector2f StartingDirection = new Vector2f(1, -1) / MathF.Sqrt(2.0f);
        
        public Sprite Sprite;
        private const float SPEED = 3.0f;
        public bool BallActive = false;
        public const float DIAMETER = 20f;
        public const float RADIUS = DIAMETER * 0.5f;
        public Vector2f Direction = StartingDirection;
        public int Health = StartingHealth;
        public int Score = 0;
        private Text gui;

        public void Reset(bool resetScore)
        {
            if (resetScore)
            {
                Score = 0;
                Health = StartingHealth;
            }
            Sprite.Position = StartingPosition;
            Direction = StartingDirection * SPEED;
            BallActive = false;
        }
        
        public Ball()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/ball.png");
            Sprite.Position = StartingPosition;
            Direction = StartingDirection * SPEED;

            Vector2f ballTextureSize = (Vector2f)Sprite.Texture.Size;
            Sprite.Origin = 0.5f * ballTextureSize;
            Sprite.Scale = new Vector2f(DIAMETER / ballTextureSize.X, DIAMETER / ballTextureSize.Y);

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
            if (BallActive)
            {
                var newPos = Sprite.Position;
                newPos += Direction * 100f * deltaTime;
                if (newPos.X > Program.ScreenW - RADIUS) // Right wall
                {
                    newPos.X = Program.ScreenW - RADIUS;
                    Reflect(new Vector2f(-1, 0));
                }
                else if (newPos.X < RADIUS) // Left wall
                {
                    newPos.X = RADIUS;
                    Reflect(new Vector2f(1, 0));
                }
                if (newPos.Y < RADIUS) // Top wall
                {
                    newPos.Y = RADIUS;
                    Reflect(new Vector2f(0, 1));
                }
                else if (newPos.Y > Program.ScreenH - RADIUS) // Bottom wall
                {
                    newPos.X = paddle.Sprite.Position.X;
                    newPos.Y = paddle.Sprite.Position.Y - RADIUS - 10;
                    BallActive = false;
                    if (new Random().Next() % 2 == 0)
                    {
                        Direction.X = 1;
                        Direction = Collision.Normalized(Direction) * SPEED;
                    }
                    else // rand == 1
                    {
                        Direction.X = -1;
                        Direction = Collision.Normalized(Direction) * SPEED;
                    }

                    Health--;
                }

                Sprite.Position = newPos;
            }
            else
            {
                Vector2f newPos;
                newPos.X = paddle.Sprite.Position.X;
                newPos.Y = paddle.Sprite.Position.Y - RADIUS - 20;
                Sprite.Position = newPos;

                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    BallActive = true;
                }
            }
            
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

            if (!BallActive)
            {
                gui.DisplayedString = "Press space to launch ball";
                gui.Position = new Vector2f(Program.ScreenW/2 - gui.GetGlobalBounds().Width/2, Program.ScreenH - 30);
                target.Draw(gui);
            }
        }
    }
}