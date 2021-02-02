using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Void_Wanderer
{
    public struct BoundingRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
    }
    public class Block
    {
        public Vector2 Position;
        public BoundingRectangle BRec;
        private Rectangle source;
        public Block(Vector2 position)
        {
            Position = position;
            BRec = new BoundingRectangle();
            BRec.X = Position.X;
            BRec.Y = Position.Y;
            BRec.Width = 48;
            BRec.Height = 48;
            source = new Rectangle(10 * 16, 17 * 16, 16, 16);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {

               
                spriteBatch.Draw(texture, Position, source, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);
        }
        
    }
}
