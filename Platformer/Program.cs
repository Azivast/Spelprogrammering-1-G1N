using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(800, 600), "Platformer"))
            {
                window.Closed += (o, e) => window.Close();
                window.SetView(new View(new Vector2f(200, 150), new Vector2f(400, 300)));

                // TODO: Initialize
                
                Scene scene = new Scene();
                Clock clock = new Clock();
                scene.Spawn(new Background());
                //scene.Spawn(new Door());
                //scene.Spawn(new Key());
                scene.Spawn(new Hero());
                
                for (int i = 0; i < 21; i++)
                {
                    scene.Spawn(new Platform{Position = new Vector2f(18+i*18, 288)});
                }
                
                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    // Update
                    float deltaTime = clock.Restart().AsSeconds();
                    scene.UpdateAll(deltaTime);
                    
                    // Draw
                    window.Clear();
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}