using System;
using System.Collections.ObjectModel;
using System.Numerics;
using breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    public class Paddle
    {
        public Sprite Sprite;
        private const float SPEED = 300f;
        private const float TEXTURESCALE = 0.2f;
        private float widthModifier = 1f;
        private Vector2f size;
        
        private readonly static Vector2f StartingPosition = new Vector2f(Program.ScreenW / 2, Program.ScreenH - 100);
        
        public void Reset()
        {
            Sprite.Position = StartingPosition;
            widthModifier = 1f;
        }

        public Paddle()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/paddle.png");
            Sprite.Position = StartingPosition;

            Vector2f paddleTextureSize = (Vector2f)Sprite.Texture.Size;
            Sprite.Origin = 0.5f * paddleTextureSize;
            Sprite.Scale = new Vector2f(TEXTURESCALE * widthModifier, TEXTURESCALE);

            size = new Vector2f(
                Sprite.GetGlobalBounds().Width,
                Sprite.GetGlobalBounds().Height);
        }

        public void Update( Ball ball, float deltaTime)
        {
            // New movement from input
            var newPos = Sprite.Position;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                newPos.X += deltaTime * SPEED;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                newPos.X -= deltaTime * SPEED;
            
            // Restrict paddle movement to within the window. Accounts for origin being in the middle of sprite.
            newPos.X = Math.Clamp(newPos.X, size.X/2, Program.ScreenW - size.X/2);
            
            // Check collision
            if (Collision.CircleRectangle(ball.Sprite.Position, Ball.RADIUS,
                    this.Sprite.Position, size, out Vector2f hit))
            {
                ball.Sprite.Position += hit;
                ball.Reflect(hit.Normalized());
            }

            // Move
            Sprite.Position = newPos;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }
    }
}