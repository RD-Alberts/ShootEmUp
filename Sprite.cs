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

namespace Fighter
{
    class Sprite
    {
        //Position
        public Vector2 position;
        public Vector2 origin;

        public Vector2 center
        {
            get { return new Vector2(position.X + srcRect.Width / 2, position.Y + srcRect.Height / 2); }
        }

        //sprite
        public Texture2D texture;
        public Rectangle srcRect;
        public SpriteEffects spriteEffect;
        public float rotation = 0f;
        public float scale = 1f;
        public float layerDepth = 0f;
        public Rectangle hitTest;

        //animaties 
        public int currentFrame, totalFrame;
        public int interspace;
        public float timer = 0;
        public float interval;

        public int animation
        {
            get { return _animation; }
            set
            {
                _animation = value;
                srcRect.Y = (value - 1) * (srcRect.Height + interspace);
            }
        }
        private int _animation = 1;

        //constructer
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.currentFrame = 1;

            this.srcRect = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        //update function
        public virtual void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;
                timer = 0f;
            }
            if (currentFrame > totalFrame)
                currentFrame = 1;

            srcRect.X = (currentFrame - 1) * (srcRect.Width + interspace);
        }

        //draw function
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, srcRect, Color.White, rotation, origin, scale, spriteEffect, layerDepth);
        }
    }
}
