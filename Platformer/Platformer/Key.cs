using System.Xml.Linq;
using SFML.Graphics;
using SFML.System;

namespace Platformer
{
     public class Key : Entity
     {
          public Key() : base("tileset")
          {
               sprite.TextureRect = new IntRect(126, 18, 18, 18);
               sprite.Origin = new Vector2f(9, 9);
          }


          public override void Update(Scene scene, float deltaTime)
          {
               // Unlock door when player picks up (collides with) key.
               if (scene.FindByType<Hero>(out Hero hero))
               {
                    if (Collision.RectangleRectangle(Bounds, hero.Bounds, out _)) 
                    {
                         scene.FindByType<Door>(out Door door);
                         door.UnlockDoor();
                         Dead = true; // Remove key
                    }
               }
          }
     }
}