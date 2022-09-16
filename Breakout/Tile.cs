using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    public class Tile
    {
        public List<Sprite> Sprites = new List<Sprite>();
        private readonly float textureScale = 0.5f;
        private Vector2f textureSize;

        public void Reset()
        {
            Sprites.Clear();
            GenerateTiles();
        }
        public Tile()
        {
            GenerateTiles();
        }
        
        public void GenerateTiles()
        {
            Texture pink = new Texture("assets/tilePink.png");
            Texture green = new Texture("assets/tileGreen.png");
            Texture blue = new Texture("assets/tileBlue.png");

            // Places tiles in a grid
            for (int i = -2; i <= 2; i++) {
                for (int j = -2; j <= 2; j++)
                {
                    Sprite sprite = new Sprite();
                    switch (j)
                    {
                        case -2:
                        case -1:
                            sprite.Texture = pink;
                            break;
                        case 0:
                        case 1:
                            sprite.Texture = green;
                            break;
                        case 2:
                            sprite.Texture = blue;
                            break;
                    }
                    textureSize = (Vector2f)sprite.Texture.Size; 
                    sprite.Origin = 0.5f * textureSize;
                    sprite.Scale = new Vector2f(textureScale , textureScale);
                    
                    var pos = new Vector2f(
                        Program.ScreenW * 0.5f + i * 96.0f, 
                        Program.ScreenH * 0.3f + j * 48.0f);

                    sprite.Position = pos;

                    Sprites.Add(sprite);
                }
            }
        }

        public void Update(float deltaTime, Ball ball)
        {
            for (int i = 0; i < Sprites.Count; i++) {
                var pos = Sprites[i].Position;
                if (Collision.CircleRectangle(
                        ball.Sprite.Position, Ball.Radius,
                        pos, Sprites[i].Origin, out Vector2f hit)) {
                    ball.Sprite.Position += hit;
                    ball.Reflect(hit.Normalized());
                    Sprites.RemoveAt(i);
                    ball.Score += 100;
                    i = 0; // Check all again since ball was moved
                }
            }
        }

        public void Draw(RenderTarget target) {
            for (int i = 0; i < Sprites.Count; i++) {
                target.Draw(Sprites[i]);
            }
        }
    }
}