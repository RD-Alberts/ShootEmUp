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
    class Player : Sprite
    {
        //rectangle int and float
        public int health,haiHealth;
        public Rectangle boundingBox, hitBox, haiHitBox;
        public Rectangle healthRectangle, haiRectangle;

        //texture
        public Texture2D haiTexture, healthTexture;

        //vector 2
        public Vector2 velocity, healthBarPosition, haiBarPosition;

        //boolean
        bool hasJumped;

        //eneum
        public enum State
        {
            Idle,
            Walk,
            Jump,
            Kick,
            Punch,
            haiduken
        }

        //declaring enum
        public State currentState;
        State prevState;

        //constructer
        public Player(Texture2D newTexture, Vector2 newPosition) : base(newTexture,newPosition)
        {
            //health player & health haiduken
            health = 100;
            haiHealth = 100;

            //boolean
            hasJumped = true;

            //animation
            totalFrame = 4;
            currentFrame = 1;
            interval = 200;
            srcRect = new Rectangle(0, 0, 80, 115);

            //position of the player healthbar & haiduken healthbar
            haiBarPosition = new Vector2(550, 50);
            healthBarPosition = new Vector2(50, 50);
        }

        public void loadContent(ContentManager Content)
        {
            //loading the textures of the healthbars
            healthTexture = Content.Load<Texture2D>("healthbar");
            haiTexture = Content.Load<Texture2D>("haibar");
        }

        //update method
        public override void Update(GameTime gameTime)
        {
            //gravity
            position += velocity;

            // update collision rectangle
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 40, srcRect.Height);

            //update hitbox collision
            hitBox = new Rectangle((int)position.X + srcRect.Width - 5, (int)position.Y + srcRect.Height / 2 - 25, 16, 16);

            //set rectangle for health bar
            healthRectangle = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, health, 25);
            haiRectangle = new Rectangle((int)haiBarPosition.X, (int)haiBarPosition.Y, haiHealth, 25);

            //movement
            KeyboardState keyState = Keyboard.GetState();

            //giving the state walking to the left and right keys
            if(keyState.IsKeyDown(Keys.Left))
            {
                velocity.X = -3f;
                currentState = State.Walk;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                velocity.X = 3f;
                currentState = State.Walk;
            }

            //when there is no movement, giving the state idle 
            else
            {
                velocity.X = 0;
                currentState = State.Idle;
            }

            //jump
            if (keyState.IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 15f;
                velocity.Y -= 5f;
                hasJumped = true;
            }

            //giving the state jump and making sure the gravity is not active for a few seconds
            if (hasJumped)
            {
                float i = 1;
                velocity.Y += 0.15f * i;

                currentState = State.Jump;
            }

            //punch and giving the state punch
            if(keyState.IsKeyDown(Keys.Z))
            {
                currentState = State.Punch;
            }

            //kick and giving the state kick
            if (keyState.IsKeyDown(Keys.X))
            {
                currentState = State.Kick;
            }

            //colision if hasJumped is false then the Y position cant ben higher then 500
            if (position.Y + boundingBox.Height >= 500)
                hasJumped = false;

            //if hasjumped is false velocity is zero
            if (hasJumped == false)
                velocity.Y = 0F;

            //if c is pressed and the haiHealth is higher then 0 the haiduken will show and the haiHealth gets mines 2
            if (keyState.IsKeyDown(Keys.C) && haiHealth > 0)
            {
                currentState = State.haiduken;
                haiHitBox = new Rectangle((int)position.X + srcRect.Width - 5, (int)position.Y + srcRect.Height / 2 - 25, 200, 16);
                haiHealth -= 2;
            }
            else         //if the c is not pressed and the haiHealth is below 100 the haiHealth gets plus 1 
            {
                haiHitBox = new Rectangle((int)position.X + srcRect.Width - 5, (int)position.Y + srcRect.Height / 2 - 25, 0, 0);
                if (haiHealth < 100)
                    haiHealth++;
            }

            //the animation playing should be currentState + 1
            animation = (int)currentState + 1;

            //calling functions
            changeAnimation();

            base.Update(gameTime);
        }

        //draw method
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //draw the kick/punch hitbox 
            spriteBatch.Draw(Asset.CreateRectangle(hitBox, Color.AliceBlue), hitBox, Color.White);

            //draw the haiduken hitbox
            spriteBatch.Draw(Asset.CreateRectangle(haiHitBox, Color.AliceBlue), haiHitBox, Color.Red);

            //draw the healthbar and the haibar
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
            spriteBatch.Draw(haiTexture, haiRectangle, Color.White);
        }

        //the changeAnimation function
        public void changeAnimation()
        {
            //if the currentState is not equal to the prev state then the switch will ocur
            if (currentState != prevState)
            {
                switch (currentState)
                {
                    //the currentframe is one and he has a total of 5 frames
                    case State.Walk:
                        currentFrame = 1;
                        totalFrame = 5;
                        break;

                    //the currentframe is one and he has a total of 4 frames
                    case State.Idle:
                        currentFrame = 1;
                        totalFrame = 4;
                        break;

                    //the currentframe is one and he has a total of 7 frames
                    case State.Jump:
                        currentFrame = 1;
                        totalFrame = 7;
                        break;

                    //the currentframe is one and he has a total of 4 frames
                    case State.Punch:
                        currentFrame = 1;
                        totalFrame = 4;
                        break;

                    //the currentframe is one and he has a total of 3 frames
                    case State.Kick:
                        currentFrame = 1;
                        totalFrame = 3;
                        break;

                    case State.haiduken:
                        currentFrame = 1;
                        totalFrame = 1;
                        break;
                }
            }
            //the prevState is equal to the currentState
            prevState = currentState;
        }
    }
}
