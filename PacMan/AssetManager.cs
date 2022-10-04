using System.Collections.Generic;
using SFML.Graphics;

namespace Pacman
{
    public class AssetManager
    {
        public static readonly string AssetPath = "assets";
        private readonly Dictionary<string, Texture> textures;
        private readonly Dictionary<string, Font> fonts;

        public AssetManager()
        {
            textures = new Dictionary<string, Texture>();
            fonts = new Dictionary<string, Font>();
        }
        
        public Texture LoadTexture(string name)
        {
            // Return texture if already loaded
            if (textures.TryGetValue(name, out Texture found))
            {
                return found;
            }
            
            // Otherwise load new texture and return that
            string fileName = $"assets/{name}.png";
            Texture texture = new Texture(fileName);
            textures.Add(name, texture);
            return texture;
        }

        public Font LoadFont(string pixelFont)
        {
            // Return font if already loaded
            if (fonts.TryGetValue(pixelFont, out Font found))
            {
                return found;
            }
            
            // Otherwise load new font and return that
            string fileName = $"assets/{pixelFont}.ttf";
            Font font = new Font(fileName);
            fonts.Add(pixelFont, font);
            return font;
        }
    }
}