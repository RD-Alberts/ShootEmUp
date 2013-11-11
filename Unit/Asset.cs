using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Fighter.Unit;

namespace Fighter.Unit
{
    class Asset
    {

        public static ContentManager Content;
        public static GraphicsDevice graphicsDevice;

        public static Texture2D enemyTexture;
        public static Texture2D enemy2Texture;
       
        public Asset()
        {
            
        }

        //this is so i can draw a rectangle on the screen
        public static Texture2D CreateRectangle(Rectangle rect, Color color)
        {
            if (rect.Width <= 0)
                rect.Width = 1;
            if (rect.Height <= 0)
                rect.Height = 1;

            Texture2D tex = new Texture2D(graphicsDevice, rect.Width, rect.Height);
            Color[] data = new Color[rect.Width * rect.Height];

            for (int i = 0; i < data.Length; ++i)
                data[i] = color;

            tex.SetData(data);

            return tex;
        }
    }
}
