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
        public Rectangle DecorRect;
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0, 0), 48, 48);
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
                spriteBatch.Draw(Decor, new Vector2(Position.X+24-DecorRect.Width/2, Position.Y-DecorRect.Height), DecorRect, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(texture, Position, source, color, 0f, Vector2.Zero, 1, SpriteEffects.None, 0f);
            
        }
        
    }
}
