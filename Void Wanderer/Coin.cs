using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Void_Wanderer.Collisions;

namespace Void_Wanderer
{

    public class Coin
    {
        public Vector2 Position;
        private BoundingCircle bounds = new BoundingCircle(new Vector2(0, 0), 18);
        public BoundingCircle Bounds => bounds;
        public bool IsCollected = false;
        private Rectangle source;
        private double coinTime;
        public Coin(Vector2 position)
        {
            Random rand = new Random();
            coinTime = rand.NextDouble()*20;
            Position = position+new Vector2(18, 18);

            bounds.Center.X = Position.X;
            bounds.Center.Y = Position.Y;

            source = new Rectangle(41 * 16, 3 * 16, 16, 16);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D texture)
        {
            coinTime += gameTime.ElapsedGameTime.TotalSeconds;
            if(!IsCollected)
                 spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2(8, 8), new Vector2((float)Math.Abs(Math.Sin(coinTime))*2, 2), SpriteEffects.None, 0f);
        }

    }
}