using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Platformer
{
    class Program
    {
        public static Vector2f ScreenSize = new Vector2f(400, 300);
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(800, 600), "Platformer"))
            {
                window.Closed += (o, e) => window.Close();
                window.SetView(new View(new Vector2f(200, 150), ScreenSize));

                // Initialize
                Scene scene = new Scene();
                scene.Load("level0");
                Clock clock = new Clock();

                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    // Update
                    float deltaTime = clock.Restart().AsSeconds();
                    scene.UpdateAll(Math.Clamp(deltaTime, 0, 0.1f));
                    
                    // Draw
                    window.Clear();
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}