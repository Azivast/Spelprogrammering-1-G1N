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

            using (RenderWindow window = new RenderWindow(new VideoMode(500, 700), "Breakfast"))
            {
                window.Closed += (s, e) => window.Close();
                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    paddle.Update(ball, deltaTime);
                    ball.Update(deltaTime, paddle);

                    if (ball.Health <= 0) // player lost
                    {
                        ball.Reset();
                        //paddle.Reset();
                    }
                    
                    window.Clear(new Color(131, 197, 235));
                    paddle.Draw(window);
                    ball.Draw(window);
                    window.Display();
                }
            }
        }
    }
}