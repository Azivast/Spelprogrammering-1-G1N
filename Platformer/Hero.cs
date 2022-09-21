using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Platformer
{
    public class Hero : Entity
    {
        public const float WalkSpeed = 100.0f;
        public const float JumpForce = 250.0f;
        public const float GravityForce = 400.0f;
        
        private bool faceRight = false;
        private float verticalSpeed;
        private bool isGrounded = false;
        private bool isUpPressed = false;

        public Hero() : base("characters")
        {
            sprite.TextureRect = new IntRect(0, 0, 24, 24);
            sprite.Origin = new Vector2f(12, 12);
        }


        public override void Update(Scene scene, float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                scene.TryMove(this, new Vector2f(-WalkSpeed * deltaTime, 0));
                faceRight = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                scene.TryMove(this, new Vector2f(WalkSpeed * deltaTime, 0));
                faceRight = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                verticalSpeed = -JumpForce;
            }
            verticalSpeed += GravityForce * deltaTime;
            if (verticalSpeed > 500.0f) verticalSpeed = 500.0f;
            
        }

        public override void Render(RenderTarget target)
        {
            sprite.Scale = new Vector2f(faceRight ? -1 : 1, 1);
            base.Render(target); // Use "Entity" implementation to make sure scaling is correct.
        }
    }
}