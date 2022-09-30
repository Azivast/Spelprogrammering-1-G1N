using SFML.Graphics;
using SFML.System;
using SFML.Window; 
using System;
namespace Pacman {
    class Program
    {
        static void Main(string[] args) 
        {
            using (var window = new RenderWindow(
                       new VideoMode(828, 900), "Pacman"))
            {
                window.SetView(new View(new FloatRect(18, 0, 414, 450)));
                window.Closed += (o, e) => window.Close();
                // TODO: Initialize
                Clock clock = new Clock();
                Scene scene = new Scene();
                scene.Loader.Load("maze");

                while (window.IsOpen) 
                {
                    window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();
                    deltaTime = MathF.Min(deltaTime, 0.01f); // Collisions could break if deltaTime too high
                    
                    window.Clear(new Color(80, 146, 165));
                    scene.UpdateAll(deltaTime);
                    
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}