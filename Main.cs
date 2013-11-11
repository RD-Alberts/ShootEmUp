using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Fighter.Unit;

namespace Fighter
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //textures
        public Texture2D background;

        //int
        public int enemyCounter, enemyKills;
        public float timer;

        //class
        Player player;

        //lists
        List<Enemy> enemyList = new List<Enemy>();
        List<Enemy2> enemyList2 = new List<Enemy2>();

        //constructer
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.Window.Title = "Fighter 2D";
            Content.RootDirectory = "Content";

            enemyCounter = 3;
            timer = 0;
            enemyKills = 0;

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        //the initialize function
        protected override void Initialize()
        {
            Asset.Content = this.Content;
            Asset.graphicsDevice = GraphicsDevice;

            base.Initialize();
        }

        //the loadContent function
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //adding the asset sprites
            Asset.enemyTexture = Content.Load<Texture2D>("sprite2");
            Asset.enemy2Texture = Content.Load<Texture2D>("sprite3");

            //because of the ... is give the player his texture and position over here
            player = new Player(Content.Load<Texture2D>("FighterAnimation"), new Vector2(50, 400));
            player.loadContent(Content);

            //the lists
            enemyList.Add(new Enemy(Asset.enemyTexture, new Vector2(600,400)));
            
            //load the background texture
            background = Content.Load<Texture2D>("background");
        }

        //the update function
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //update the player
            player.Update(gameTime);

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            //how much times is between the spawning of the enemies
            if (timer >= 4000)
            {
                Console.WriteLine(enemyKills < 5);
                //aslong as there are less then 3 enemies on the screen and the enemyKills is less then 5 an enemy1 will be added to the screen
                if (enemyList.Count <= 3 && enemyKills < 5)
                    enemyList.Add(new Enemy(Asset.enemyTexture, new Vector2(600, 400)));

                //aslong as there are less then 3 enemies on the screen and the enemyKills is more then 5 an enemy2 will be added to the screen
                if (enemyList2.Count <= 3 && enemyKills >= 5)
                {
                    enemyList2.Add(new Enemy2(Asset.enemy2Texture, new Vector2(600, 400)));
                }

                //afterwards the timer will be reset to zero
                timer = 0;
            }

            //update the enemy
            foreach (Enemy e in enemyList)
            {
                //if the enemy hits the player & the current state of the player is punch or kick & the currentframe is 2 & the enemy is visible 
                if (e.boundingBox.Intersects(player.hitBox) && (player.currentState == Player.State.Punch || player.currentState == Player.State.Kick) && player.currentFrame == 2 && e.isVisible)
                {
                    //if the speed of the enemy is equal or bigger then 0 else call the knockback function & there has either been a punch or a kick
                    if (e.speed >= 0)
                    {
                        e.knockBack();

                        //with a punch the enemy loses 3 health
                        if(player.currentState == Player.State.Punch)
                            e.enemyHealth -= 3;

                        //with a kick the enemy loses 5 health
                        if (player.currentState == Player.State.Kick)
                            e.enemyHealth -= 5;
                    }
                }

                //if the enemy is hit with an haiduken
                if (e.boundingBox.Intersects(player.haiHitBox))
                {
                    if (e.speed >= 0)
                    {
                        //with a haiduken the enemy loses 10 health & gets knocked back
                        if (player.currentState == Player.State.haiduken)
                        {
                            e.enemyHealth -= 10;
                            e.knockBack();
                        }
                    }
                }

                //if the enemy hits the player the enemy becomes invisible and the player loses 5 health
                if (e.boundingBox.Intersects(player.boundingBox))
                {
                    e.isVisible = false;
                    player.health -= 5;
                }

                //update the enemy class
                if (e.isVisible == true)
                {
                    e.Update(gameTime);
                }
            }

            //if any of the asteroids in the list are destroyed (or are invisible) then remove them from the list this is for enemy 1
            for (int i = 0; i < enemyList.Count(); i++)
            {
                if (!enemyList[i].isVisible)
                {
                    enemyList.RemoveAt(i);
                    i--;
                    enemyKills++;
                }
            }

            //update the enemy2
            foreach (Enemy2 e2 in enemyList2)
            {
                //if the enemy hits the player & the current state of the player is punch or kick & the currentframe is 2 & the enemy is visible 
                if (e2.boundingBox.Intersects(player.hitBox) && (player.currentState == Player.State.Punch || player.currentState == Player.State.Kick) && player.currentFrame == 2 && e2.isVisible)
                {
                    //if the speed of the enemy is bigger then 0 else call the knockback function & there has either been en punch or a kick
                    if (e2.speed >= 0)
                    {
                        e2.knockBack();

                        //with a punch the enemy loses 3 health
                        if (player.currentState == Player.State.Punch)
                            e2.enemyHealth -= 3;
                        
                        //with a kick the enemy loses 5 health
                        if (player.currentState == Player.State.Kick)
                            e2.enemyHealth -= 5;
                    }
                }

                //if the enemy hits the player, the player loses 5 health
                if (e2.boundingBox.Intersects(player.boundingBox))
                {
                    e2.isVisible = false;
                    player.health -= 5;
                    enemyCounter++;
                }

                //if the enemy is hit with an haiduken
                if (e2.boundingBox.Intersects(player.haiHitBox))
                {
                    if (e2.speed >= 0)
                    {
                        //with a haiduken the enemy loses 10 health & gets knocked back
                        if (player.currentState == Player.State.haiduken)
                        {
                            e2.enemyHealth -= 10;
                            e2.knockBack();
                        }
                    }
                }

                //update the enemy class
                if (e2.isVisible == true)
                {
                    e2.Update(gameTime);
                }
            }

            //if any of the asteroids in the list are destroyed (or are invisible) then remove them from the list this is for enemy 2
            for (int i = 0; i < enemyList2.Count(); i++)
            {
                if (!enemyList2[i].isVisible)
                {
                    enemyList2.RemoveAt(i);
                    i--;
                    enemyKills++;
                }
            }

            base.Update(gameTime);
        }

        //the draw function
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //draw the background
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            //draw the player
            player.Draw(spriteBatch);

            //draw the enemy
            foreach (Enemy e in enemyList)
            {
                if (e.isVisible == true)
                    e.Draw(spriteBatch);
            }

            //draw the enemy2
            foreach (Enemy2 e2 in enemyList2)
            {
                if(e2.isVisible == true)
                    e2.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
