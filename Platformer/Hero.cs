using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Platformer
{
    public class Hero : Entity
    {
        public const float WALKSPEED = 100.0f;
        public const float JUMPFORCE = 250.0f;
        public const float GRAVITYFORCE = 400.0f;
        
        private const float ANIMATIONSTOPTIME = 0.25f;
        private readonly IntRect frame1 = new IntRect(0, 0, 24, 24);
        private readonly IntRect frame2 = new IntRect(24, 0, 24, 24);
        
        private bool faceRight = false;
        private float verticalSpeed;
        private bool isGrounded = false;
        private bool isUpPressed = false;
        private float walkingTimer = 0;
        
        

        public Hero() : base("characters")
        {
            sprite.TextureRect = frame1;
            sprite.Origin = new Vector2f(12, 12);
        }


        public override void Update(Scene scene, float deltaTime)
        {
            // Input, also checks if movement possible
            bool IsMoving = false;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                scene.TryMove(this, new Vector2f(-WALKSPEED * deltaTime, 0));
                faceRight = false;
                IsMoving = true;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                scene.TryMove(this, new Vector2f(WALKSPEED * deltaTime, 0));
                faceRight = true;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) // Won't move (play animation) when pressing both directions at once.
                    IsMoving = false;
                else IsMoving = true;
            }
            
            // Jumping
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                if (isGrounded && !isUpPressed)
                {
                    verticalSpeed = -JUMPFORCE;
                    isUpPressed = true;
                }
            }
            else
            {
                isUpPressed = false;
            }
            verticalSpeed += GRAVITYFORCE * deltaTime;
            if (verticalSpeed > 500.0f) verticalSpeed = 500.0f; // velocity cap

            isGrounded = false;
            Vector2f velocity = new Vector2f(0, verticalSpeed * deltaTime);
            if (scene.TryMove(this, velocity))
            {
                if (verticalSpeed > 0.0f)
                {
                    isGrounded = true;
                    verticalSpeed = 0.0f;
                }
                else
                {
                    verticalSpeed = -0.5f * verticalSpeed;
                }
            }
            
            // Checks if player is outside of screen, with accounting for player size
            if (Position.X < 0 - base.Bounds.Width/2 
                || Position.X > Program.ScreenSize.X + base.Bounds.Width/2
                || Position.Y < 0 - base.Bounds.Height/2
                || Position.Y > Program.ScreenSize.Y + base.Bounds.Height/2)
            {
                scene.Reload();
            }
            
            // Imperfections in collision make the verticalSpeed (and isGrounded) flicker a slight amount. We therefore
            // need to filter it when using it for animations.
            bool isGroundedFiltered = verticalSpeed is <= 0.1f and >= -0.1f; 
            if (isGroundedFiltered)
            {
                if (IsMoving && walkingTimer >= ANIMATIONSTOPTIME)
                {
                    if (sprite.TextureRect == frame1)
                        sprite.TextureRect = frame2;
                    else sprite.TextureRect = frame1;

                    walkingTimer = 0;
                }
                else if (!IsMoving)
                {
                    walkingTimer = 0;
                    sprite.TextureRect = frame1;
                }
                walkingTimer += deltaTime;
                Console.WriteLine(deltaTime);
            }
            else
            {
                walkingTimer = 0;
                sprite.TextureRect = frame2;
            }

        }

        public override void Render(RenderTarget target)
        {
            sprite.Scale = new Vector2f(faceRight ? -1 : 1, 1); // ternary expression flip sprite towards travel direction.

            base.Render(target); // Use "Entity" implementation to make sure scaling is correct.
        }

        public override FloatRect Bounds
        {
            get
            {
                var bounds = base.Bounds;
                bounds.Left += 3;
                bounds.Width -= 6;
                bounds.Top += 3;
                bounds.Height -= 3;
                return bounds;
            }
        }
    }
}