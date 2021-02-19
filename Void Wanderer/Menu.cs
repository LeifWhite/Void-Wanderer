using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Void_Wanderer
{
    /// <summary>
    /// Menu screen
    /// </summary>
    public class Menu
    {


        
        private Texture2D texture;
        private Texture2D titleTexture;
        private Texture2D playTexture;
        private double blinkTimer = 0;
        /// <summary>
        /// best time achieved
        /// </summary>
        public int BestTime = -1;
        /// <summary>
        /// most recent time achieved
        /// </summary>
        public int CurrentTime = -1;
        private SpriteFont arial;
        private SpriteFont arial_small;
        /// <summary>
        /// Aim Low by Thomas White
        /// </summary>
        public Song BackgroundMusic;
        /// <summary>
        /// unused, initializes
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here

            

        }
        /// <summary>
        /// loads content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {

            texture = content.Load<Texture2D>("VW Eye Open Close");
            titleTexture = content.Load<Texture2D>("VW title_screen");
            playTexture = content.Load<Texture2D>("VW play_button");
            arial = content.Load<SpriteFont>("arial");
            arial_small = content.Load<SpriteFont>("arial_small");
            BackgroundMusic = content.Load<Song>("Menusong");


        }
        /// <summary>
        /// unused, updates
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            

        }
        /// <summary>
        /// draws menu
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            blinkTimer += gameTime.ElapsedGameTime.TotalSeconds;
            
            Rectangle source = Math.Floor(blinkTimer)%4==0 ? new Rectangle(0, 0, 200, 200) : new Rectangle(200, 0, 200, 200);
            spriteBatch.Draw(texture, new Vector2(0, -150), source, 
                new Color(1-(float)Math.Abs(Math.Sin(blinkTimer)/3), 1 - (float)Math.Abs(Math.Sin(blinkTimer)/3), 1-(float)Math.Abs(Math.Sin(blinkTimer)/3)), 
                0f, new Vector2(0, 0), 4f, SpriteEffects.None, 0);
            Rectangle titleSource = Math.Floor(blinkTimer*2.5) % 3 == 0 ? new Rectangle(0, 6, 189, 44): Math.Floor(blinkTimer) % 3 == 1 ? new Rectangle(0, 59, 189, 44): new Rectangle(0, 112, 189, 44);
            spriteBatch.Draw(titleTexture, new Vector2(164, 10), titleSource,
              Color.White,
               0f, new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);

            spriteBatch.Draw(playTexture, new Vector2(300, 312), null,
              Color.White,
               0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0);
            if (CurrentTime != -1)
            {
                spriteBatch.DrawString(arial, "Time", new Vector2(40, 390), Color.Silver);
                spriteBatch.DrawString(arial, "Time", new Vector2(41, 392), Color.White);
                string csecs = ((CurrentTime % 60)<=9) ? "0"+ (CurrentTime % 60).ToString() : (CurrentTime % 60).ToString();
                spriteBatch.DrawString(arial, Math.Floor(CurrentTime/60.0).ToString()+":"+csecs, new Vector2(40, 430), Color.Silver);
                spriteBatch.DrawString(arial, Math.Floor(CurrentTime / 60.0).ToString() + ":" + csecs, new Vector2(41, 432), Color.White);

                spriteBatch.DrawString(arial, "Best", new Vector2(685, 390), Color.Silver);
                spriteBatch.DrawString(arial, "Best", new Vector2(686, 392), Color.White);
                string bsecs = ((BestTime % 60) <= 9) ? "0" + (BestTime % 60).ToString() : (BestTime % 60).ToString();
                spriteBatch.DrawString(arial, Math.Floor(BestTime / 60.0).ToString() + ":" +bsecs, new Vector2(685, 430), Color.Silver);
                spriteBatch.DrawString(arial, Math.Floor(BestTime / 60.0).ToString() + ":" + bsecs, new Vector2(686, 432), Color.White);

            }
            else
            {
                spriteBatch.DrawString(arial_small, "WASD to move", new Vector2(30+(float)Math.Sin(blinkTimer)*15, 420), Color.Silver);
                spriteBatch.DrawString(arial_small, "WASD to move", new Vector2(31 + (float)Math.Sin(blinkTimer) * 15, 421), Color.White);
                spriteBatch.DrawString(arial_small, "R to restart", new Vector2(45 + (float)Math.Sin(blinkTimer) * 15, 444), Color.Silver);
                spriteBatch.DrawString(arial_small, "R to restart", new Vector2(46 + (float)Math.Sin(blinkTimer) * 15, 446), Color.White);
                spriteBatch.DrawString(arial_small, "Click to teleport", new Vector2(615 + (float)Math.Sin(blinkTimer) * 15, 420), Color.Silver);
                spriteBatch.DrawString(arial_small, "Click to teleport", new Vector2(616 + (float)Math.Sin(blinkTimer) * 15, 421), Color.White);

            }

        }
    }
}
