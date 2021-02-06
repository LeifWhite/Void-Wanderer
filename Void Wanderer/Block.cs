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
        
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0, 0), 48, 48);
        /// <summary>
        /// Collision boundary
        /// </summary>
        public BoundingRectangle Bounds => bounds;
        private Rectangle source;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        public Block(Vector2 position)
        {
            Position = position;
            
            bounds.X = Position.X;
            bounds.Y = Position.Y;
           
            source = new Rectangle(10 * 16, 17 * 16, 16, 16);
        }
        /// <summary>
        /// Draws block
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {

                spriteBatch.Draw(texture, Position, source, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }
        
    }
}
