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
        private const float speed = 300f;
        private const float textureScale = 0.2f;
        private float widthModifier = 1f;
        private Vector2f size;

        public Paddle()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/paddle.png");
            Sprite.Position = new Vector2f(Program.ScreenW / 2, Program.ScreenH - 100);

            Vector2f paddleTextureSize = (Vector2f)Sprite.Texture.Size;
            Sprite.Origin = 0.5f * paddleTextureSize;
            Sprite.Scale = new Vector2f(textureScale * widthModifier, textureScale);

            size = new Vector2f(
                Sprite.GetGlobalBounds().Width,
                Sprite.GetGlobalBounds().Height);
        }

        public void Update( Ball ball, float deltaTime)
        {
            var newPos = Sprite.Position;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                newPos.X += deltaTime * speed;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                newPos.X -= deltaTime * speed;
            // Restrict paddle movement to within the window. Accounts for origin being in the middle of sprite.
            newPos.X = Math.Clamp(newPos.X, size.X/2, Program.ScreenW - size.X/2);
            
            // Check collision
            if (Collision.CircleRectangle(ball.Sprite.Position, Ball.Radius,
                    this.Sprite.Position, size, out Vector2f hit))
            {
                ball.Sprite.Position += hit;
                ball.Reflect(hit.Normalized());
            }

            Sprite.Position = newPos;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(Sprite);
        }
    }
}