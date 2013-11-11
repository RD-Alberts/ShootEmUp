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

namespace Fighter.Unit
{
    class Enemy2
    {
        //texture
        public Texture2D texture;

        //rectangle & int & float
        public Rectangle boundingBox;
        public float timer;
        public int speed, enemyHealth;

        //vector
        public Vector2 position;

        //boolean
        public bool isVisible;

        //constructer
        public Enemy2(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            speed = 4;
            enemyHealth = 30;
            isVisible = true;
        }

        //update function
        public void Update(GameTime gameTime)
        {
            // update collision rectangle
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //update enemy movement
            position.X -= speed;

            //helps the knockBack functions
            if (speed < 0)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > 500)
                {
                    speed = speed * -1;
                    timer = 0;
                }
            }

            if (enemyHealth <= 0)
                isVisible = false;
            //move enemy back to the right of the screen if the X is bigger then 800
            if (position.X >= 800)
                position.X = -75;
        }

        //draw function
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        //knockBack function
        public void knockBack()
        {
            speed = speed * -1;
        }

        public void changeAnimation()
        {

        }
    }
}
