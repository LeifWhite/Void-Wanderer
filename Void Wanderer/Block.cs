using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Void_Wanderer.Collisions;

namespace Void_Wanderer
{
    /// <summary>
    /// Block class
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Wher it is at
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// Are there decorations on this block?
        /// </summary>
        public bool HasDecor = false;
        /// <summary>
        /// What texture the decor comes from
        /// </summary>
        public Texture2D Decor;
        /// <summary>
        /// What rectangle from the decor is it?
        /// </summary>
        public List<Rectangle> DecorRect;
        public int DecorRectFrame;
        private float animationTime;
        private float animationSwitch = 0.8f;
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0, 0), 48 * (Screen.SIZE / 800f) * Screen.GS, 48 * (Screen.SIZE / 800f) * Screen.GS);
        /// <summary>
        /// Collision boundary
        /// </summary>
        public BoundingRectangle Bounds => bounds;
        private Rectangle source;
        private Color color;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        public Block(Vector2 position, Color color, bool grass=false)
        {
            Random r = new Random();
            animationTime = (float)r.NextDouble()*animationSwitch;
            Position = position;
            this.color = color;
            bounds.X = Position.X;
            bounds.Y = Position.Y;
            if (!grass)
            {
                source = new Rectangle(10, 10, 48, 48);
            }
            else
            {
                source = new Rectangle(67, 10, 48, 48);
            }
            
        }
        /// <summary>
        /// Draws block
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            
            if (HasDecor)
            {
                if (DecorRect.Count > 1)
                {
                   
                    animationTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (animationTime > animationSwitch * 2.5f || (DecorRectFrame != 0 && animationTime > animationSwitch && DecorRectFrame != 4 && DecorRectFrame != 3))
                    {
                        animationTime = 0;
                        DecorRectFrame++;
                        if (DecorRectFrame >= DecorRect.Count)
                        {
                            DecorRectFrame = 0;
                        }
                    }
                }
                spriteBatch.Draw(Decor, new Vector2(Position.X+(24 - DecorRect[DecorRectFrame].Width/2) * (Screen.SIZE / 800f) * Screen.GS, Position.Y-(DecorRect[DecorRectFrame].Height-2) * (Screen.SIZE / 800f) * Screen.GS), DecorRect[DecorRectFrame], Color.White, 0f, Vector2.Zero, 1f * (Screen.SIZE / 800f) * Screen.GS, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(texture, Position, source, color, 0f, Vector2.Zero, 1.01f * (Screen.SIZE / 800f) * Screen.GS, SpriteEffects.None, 0f);
            //spriteBatch.Draw(texture, new Vector2(bounds.X, bounds.Y), new Rectangle(12, 12, 1, 1), new Color(255, 0, 0, 60), 0f, Vector2.Zero, new Vector2(bounds.Width, bounds.Height), SpriteEffects.None, 0f);

        }
        
    }
}
