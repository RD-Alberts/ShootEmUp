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
    class Enemy
    {
        //texture
        public Texture2D texture;

        //rectangle & int & float
        public Rectangle boundingBox;
        public float timer;
        public int speed,enemyHealth;

        //vector
        public Vector2 position;

        //boolean
        public bool isVisible;

        //constructor
        public Enemy(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            speed = 3;
            enemyHealth = 20;
            isVisible = true;
        }

        //the update function
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
                //how long the knockback will last, afterwards the speed will be reset and the timer
                if (timer > 500)
                {
                    speed = speed * -1;
                    timer = 0;
                }
            }
            //if enemyHealts is zero or less the enemy will become invisible
            if (enemyHealth <= 0)
                isVisible = false;

            //move enemy back to the right of the screen if the X is bigger then 800
            if (position.X >= 800)
                position.X = -75;
        }

        //the draw function
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        //the knockback function
        public void knockBack()
        {
            speed = speed * -1;
        }

        public void changeAnimation()
        {

        }
    }
}
