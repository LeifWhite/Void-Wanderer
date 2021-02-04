using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Void_Wanderer.Collisions;

namespace Void_Wanderer
{
    
    public class Block
    {
        public Vector2 Position;
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0, 0), 48, 48);
        public BoundingRectangle Bounds => bounds;
        private Rectangle source;
        public Block(Vector2 position)
        {
            Position = position;
            
            bounds.X = Position.X;
            bounds.Y = Position.Y;
           
            source = new Rectangle(10 * 16, 17 * 16, 16, 16);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {

                spriteBatch.Draw(texture, Position, source, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }
        
    }
}
