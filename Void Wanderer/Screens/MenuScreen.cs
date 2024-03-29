﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Void_Wanderer
{
    public enum ScrollDirection
    {
        Up,
        Down,
        Still
    }
    public enum ScrollTarget
    {
        Up,
        Down,
        Middle
    }
    /// <summary>
    /// Menu screen
    /// </summary>
    public class MenuScreen
    {


        
        private Texture2D texture;
        private Texture2D titleTexture;
        private Texture2D playTexture;
        private Texture2D settingsTexture;
        private Color lilac = new Color(228, 199, 255);
        private ScrollDirection sc = ScrollDirection.Still;
        private ScrollTarget st = ScrollTarget.Middle;
        /// <summary>
        /// How loud song
        /// </summary>
        public int SongVolume = 5;
        /// <summary>
        /// How loud sound
        /// </summary>
        public int SoundVolume = 5;
        /// <summary>
        /// Begins game
        /// </summary>
        public Button PlayButton;
        private Button SettingsButton;
        private Button StoryButton;
        private Button[] volumeButtons;
        
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
        private SoundEffect coinPickup;
        private Vector2 offset = Vector2.Zero;
        /// <summary>
        /// Aim Low by Thomas White
        /// </summary>
        /// 
        public Song BackgroundMusic;
        /// <summary>
        /// unused, initializes
        /// </summary>
        /// 
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
            settingsTexture = content.Load<Texture2D>("colored_packed");
            coinPickup = content.Load<SoundEffect>("Pickup_Coin15");

            SettingsButton = new Button(new Vector2(734, 25) * (Screen.SIZE / 800f), settingsTexture, new Rectangle(45 * 16, 16 * 16, 16, 16), 3.5f * (Screen.SIZE / 800f));
            PlayButton = new Button(new Vector2(303, 670) * (Screen.SIZE / 800f), playTexture, 2f * (Screen.SIZE / 800f));
            StoryButton = new Button(new Vector2(10, 25) * (Screen.SIZE / 800f), settingsTexture, new Rectangle(33 * 16, 15 * 16, 16, 16), 3.5f * (Screen.SIZE / 800f));

            volumeButtons = new Button[]
            {
                new Button( new Vector2(150, 835)*(Screen.SIZE / 800f), settingsTexture, new Rectangle(16 * 37, 16 * 20, 16, 16), 4f*(Screen.SIZE / 800f)),
                new Button( new Vector2(586, 835)*(Screen.SIZE / 800f), settingsTexture, new Rectangle(16 * 36, 16 * 20, 16, 16), 4f*(Screen.SIZE / 800f)),
                new Button( new Vector2(150, 1485)*(Screen.SIZE / 800f), settingsTexture, new Rectangle(16 * 37, 16 * 20, 16, 16), 4f*(Screen.SIZE / 800f)),
                new Button( new Vector2(586, 1485)*(Screen.SIZE / 800f), settingsTexture, new Rectangle(16 * 36, 16 * 20, 16, 16), 4f*(Screen.SIZE / 800f))

            };
        }
        /// <summary>
        /// updates
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            PlayButton.Update(gameTime);
            SettingsButton.Update(gameTime);
            StoryButton.Update(gameTime);
            if (SettingsButton.Clicked())
            {
                if (offset.Y >= -0.01)
                {
                    sc = ScrollDirection.Down;
                    st = ScrollTarget.Down;
                }
                else
                {
                    sc = ScrollDirection.Up;
                    st = ScrollTarget.Middle;
                }
            }
            if (StoryButton.Clicked())
            {
                if (offset.Y <= 0.01)
                {
                    sc = ScrollDirection.Up;
                    st = ScrollTarget.Up;
                }
                else
                {
                    sc = ScrollDirection.Down;
                    st = ScrollTarget.Middle;
                }
            }
            if (sc == ScrollDirection.Down)
            {
                offset.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * 400 * (Screen.SIZE / 800f);

            }
            else if(sc == ScrollDirection.Up)
            {
                offset.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * 400 * (Screen.SIZE / 800f);

            }
            if (offset.Y > 800 * (Screen.SIZE / 800f) || offset.Y < -800 * (Screen.SIZE / 800f) || 
                (st == ScrollTarget.Middle&&
                offset.Y <=  (float)gameTime.ElapsedGameTime.TotalSeconds * 400 * (Screen.SIZE / 800f) &&
                offset.Y >= -(float)gameTime.ElapsedGameTime.TotalSeconds * 400 * (Screen.SIZE / 800f)))
            {
                sc = ScrollDirection.Still;
                
                MathHelper.Clamp(offset.Y, -800 * (Screen.SIZE / 800f), 800*(Screen.SIZE / 800f));
                if(st == ScrollTarget.Middle)
                {
                    offset.Y = 0;
                }
            }
            PlayButton.MoveButtonBoundary(new Vector2(PlayButton.Position.X, PlayButton.Position.Y) + offset);
            for (int i = 0; i < volumeButtons.Length; i++)
            {
                volumeButtons[i].Update(gameTime);
                volumeButtons[i].MoveButtonBoundary(new Vector2(volumeButtons[i].Position.X, volumeButtons[i].Position.Y) + offset);
            }
            if (volumeButtons[2].Clicked()&&SoundVolume>=1)
            {
               
                SoundVolume--;

                SoundEffect.MasterVolume = SoundVolume/10.0f;
                coinPickup.Play(volume: 0.25f, pitch: -0.4f, pan: 0.0f);
            }
            else if (volumeButtons[3].Clicked() && SoundVolume<=9)
            {
                SoundVolume++;
                SoundEffect.MasterVolume = SoundVolume / 10.0f;
                coinPickup.Play(volume: 0.25f, pitch: -0.4f, pan: 0.0f);
            }
            else if (volumeButtons[0].Clicked() && SongVolume>=1)
            {
                SongVolume--;
                MediaPlayer.Volume = SongVolume / 10f;
            }
            else if (volumeButtons[1].Clicked() && SongVolume<=9)
            {
                SongVolume++;
                MediaPlayer.Volume = SongVolume / 10f;
            }

        }
        /// <summary>
        /// draws menu
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)//!
        {
            blinkTimer += gameTime.ElapsedGameTime.TotalSeconds;

            Rectangle source = Math.Floor(blinkTimer) % 4 == 0 ? new Rectangle(0, 0, 200, 200) : new Rectangle(200, 0, 200, 200);
            spriteBatch.Draw(texture, new Vector2(0, 0), source,
                new Color(1 - (float)Math.Abs(Math.Sin(blinkTimer) / 3), 1 - (float)Math.Abs(Math.Sin(blinkTimer) / 3), 1 - (float)Math.Abs(Math.Sin(blinkTimer) / 3)),
                0f, new Vector2(0, 0), 4f * (Screen.SIZE / 800f), SpriteEffects.None, 0);
            Matrix transform = Matrix.CreateTranslation(offset.X, offset.Y, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: transform);
            
            Rectangle titleSource = Math.Floor(blinkTimer*2.5) % 3 == 0 ? new Rectangle(0, 6, 189, 44): Math.Floor(blinkTimer) % 3 == 1 ? new Rectangle(0, 59, 189, 44): new Rectangle(0, 112, 189, 44);
            spriteBatch.Draw(titleTexture, new Vector2(400, 300) * (Screen.SIZE / 800f), titleSource,
              Color.White,
               0f, new Vector2(titleTexture.Width/2, titleTexture.Height/2), 2.75f * (Screen.SIZE / 800f), SpriteEffects.None, 0);

            source = new Rectangle(16 * 38, 16 * 16, 16, 16);
            //System.Diagnostics.Debug.WriteLine(5f - SoundVolume * (4f / 10));
            spriteBatch.Draw(settingsTexture, new Vector2(400, 1510) * (Screen.SIZE / 800f), source, lilac, 0f, new Vector2(8, 8), (5f-(10-SoundVolume)*(4f/10)) * (Screen.SIZE / 800f), SpriteEffects.None, 0);
            source = new Rectangle(16 * 39, 16 * 16, 16, 16);
            spriteBatch.Draw(settingsTexture, new Vector2(400, 860) * (Screen.SIZE / 800f), source, lilac, 0f, new Vector2(8, 8), (5f-(10-SongVolume)*(4f/10)) * (Screen.SIZE / 800f), SpriteEffects.None, 0);
            

            PlayButton.Draw(gameTime, spriteBatch);
            for(int i = 0; i<volumeButtons.Length; i++)
            {
                volumeButtons[i].Draw(gameTime, spriteBatch, 0, lilac);
            }
            if (CurrentTime != -1)
            {
                spriteBatch.DrawString(arial, "Time", new Vector2(40, 690) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial, "Time", new Vector2(41, 692) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f),SpriteEffects.None, 0);
                string csecs = ((CurrentTime % 60)<=9) ? "0"+ (CurrentTime % 60).ToString() : (CurrentTime % 60).ToString();
                spriteBatch.DrawString(arial, Math.Floor(CurrentTime/60.0).ToString()+":"+csecs, new Vector2(40, 730) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial, Math.Floor(CurrentTime / 60.0).ToString() + ":" + csecs, new Vector2(41, 732) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);

                spriteBatch.DrawString(arial, "Best", new Vector2(685, 690) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial, "Best", new Vector2(686, 692) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                string bsecs = ((BestTime % 60) <= 9) ? "0" + (BestTime % 60).ToString() : (BestTime % 60).ToString();
                spriteBatch.DrawString(arial, Math.Floor(BestTime / 60.0).ToString() + ":" +bsecs, new Vector2(685, 730) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial, Math.Floor(BestTime / 60.0).ToString() + ":" + bsecs, new Vector2(686, 732) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);

            }
            else
            {
                spriteBatch.DrawString(arial_small, "WASD to move", new Vector2(30+(float)Math.Sin(blinkTimer)*15, 720) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE/1600f),SpriteEffects.None, 0);
                spriteBatch.DrawString(arial_small, "WASD to move", new Vector2(31 + (float)Math.Sin(blinkTimer) * 15, 721) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial_small, "R to restart", new Vector2(45 + (float)Math.Sin(blinkTimer) * 15, 744) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial_small, "R to restart", new Vector2(46 + (float)Math.Sin(blinkTimer) * 15, 746) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial_small, "Click to teleport", new Vector2(615 + (float)Math.Sin(blinkTimer) * 15, 720) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
                spriteBatch.DrawString(arial_small, "Click to teleport", new Vector2(616 + (float)Math.Sin(blinkTimer) * 15, 721) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);

            }
            spriteBatch.DrawString(arial, "Wanderer,", new Vector2(280, -770) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, 1.5f*(Screen.SIZE / 1600f), SpriteEffects.None, 0);
            spriteBatch.DrawString(arial, "Wanderer,", new Vector2(281, -769) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, 1.5f*(Screen.SIZE / 1600f), SpriteEffects.None, 0);

            spriteBatch.DrawString(arial, "Gather all the crystals on each world.", new Vector2(110, -640) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
            spriteBatch.DrawString(arial, "Gather all the crystals on each world.", new Vector2(111, -639) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);

            spriteBatch.DrawString(arial, "Go quickly.  The Void waits for their crystals.", new Vector2(50, -240) * (Screen.SIZE / 800f), Color.Silver, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);
            spriteBatch.DrawString(arial, "Go quickly.  The Void waits for their crystals.", new Vector2(51, -239) * (Screen.SIZE / 800f), lilac, 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            SettingsButton.Draw(gameTime, spriteBatch, MathHelper.PiOver4, lilac);
            StoryButton.Draw(gameTime, spriteBatch, 0, lilac);
        }
    }
}
