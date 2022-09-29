using SFML.Graphics;
using SFML.System;
using SFML.Window; 
using System;
namespace Pacman {
    class Program {
        static void Main(string[] args) 
        {
            using (var window = new RenderWindow(
                       new VideoMode(828, 900), "Pacman"))
            {
                window.SetView(new View(new FloatRect(18, 0, 414, 450)));
                window.Closed += (o, e) => window.Close();
                // TODO: Initialize
                Scene scene = new Scene();
                scene.Loader.Load("maze");
                Clock clock = new Clock();
                
                while (window.IsOpen) 
                {
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.01f); // Collisions could break if deltaTime too high
                    // TODO: Updates
                    window.Clear(new Color(223, 246, 245));
                    scene.UpdateAll(deltaTime);
                    
                    // TODO: Drawing
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}