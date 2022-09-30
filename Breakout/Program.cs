using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace Breakout
{
    class Program
    {
        public const int ScreenW = 500;
        public const int ScreenH = 700;
        static void Main(string[] args)
        {
            
            Clock frameClock = new Clock();
            float deltaTime = frameClock.Restart().AsSeconds();
            Clock clock = new Clock();
            Ball ball = new Ball();
            Paddle paddle = new Paddle();
            Tile tile = new Tile();

            using (RenderWindow window = new RenderWindow(new VideoMode(500, 700), "Breakfast"))
            {
                window.Closed += (s, e) => window.Close();
                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    paddle.Update(ball, deltaTime);
                    ball.Update(deltaTime, paddle);
                    tile.Update(deltaTime, ball);

                    if (ball.Health <= 0) // if player lost
                    {
                        ball.Reset(true);
                        paddle.Reset();
                        tile.Reset();
                    }

                    if (tile.Sprites.Count == 0) // all tiles destroyed, will spawn new ones
                    {
                        ball.Reset(false);
                        tile.Reset();
                    }
                    
                    window.Clear(new Color(20, 20, 70));
                    paddle.Draw(window);
                    ball.Draw(window);
                    tile.Draw(window);
                    window.Display();
                }
            }
        }
    }
}