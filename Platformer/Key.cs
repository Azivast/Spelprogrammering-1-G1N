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
     }
}