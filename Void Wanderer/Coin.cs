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
    /// Coin.  Must get coin
    /// </summary>
    public class Coin
    {
        /// <summary>
        /// Where coin at
        /// </summary>
        public Vector2 Position;
        
        private BoundingCircle bounds = new BoundingCircle(new Vector2(0, 0), 18 * (Screen.SIZE / 800f));
        /// <summary>
        /// Coin collision boundaries
        /// </summary>
        public BoundingCircle Bounds => bounds;
        /// <summary>
        /// Has coin been gotten
        /// </summary>
        public bool IsCollected = false;
        private Rectangle source;
        private double coinTime;
        private Color coinColor;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="cc"></param>
        public Coin(Vector2 position, Color cc)
        {
            coinColor = cc;
            Random rand = new Random();
            coinTime = rand.NextDouble()*20;
            Position = position+new Vector2(18 * (Screen.SIZE / 800f), 18 * (Screen.SIZE / 800f));

            bounds.Center.X = Position.X;
            bounds.Center.Y = Position.Y;
            //coin 41, 3
            //crystal 32, 10
            source = new Rectangle(32 * 16, 10 * 16, 16, 16);
        }
        /// <summary>
        /// Draws coin with fancy changing colors
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            coinTime += gameTime.ElapsedGameTime.TotalSeconds;
            if (!IsCollected)
            {
                spriteBatch.Draw(texture, Position, source, Color.Black, 0f, new Vector2(8, 8), new Vector2((float)Math.Abs(Math.Sin(coinTime)) * 2.2f, 2.2f) * (Screen.SIZE / 800f), SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, Position, source, coinColor, 0f, new Vector2(8, 8), new Vector2((float)Math.Abs(Math.Sin(coinTime)) * 2, 2) * (Screen.SIZE / 800f), SpriteEffects.None, 0f);
            }
        }

    }
}