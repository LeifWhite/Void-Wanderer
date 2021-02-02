using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Void_Wanderer
{
    public enum Direction
    {
        Still,
        Left,
        Right
       
    }
    public class Rho
    {
        
        private Texture2D texture;
        
        private double animationTimer;
        private short animationFrame = 0;
        private Direction currentDirection;
        /// <summary>
        /// where go
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// where find
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("VW rho");
        }
        public void Update(GameTime gameTime, Vector2 direction)
        {
            if (direction.X > 0)
            {
                currentDirection = Direction.Right;
            }
            else if (direction.X < 0)
            {
                currentDirection = Direction.Left;
            }
            else
            {
                currentDirection = Direction.Still;
            }

            Position += direction;
            //System.Diagnostics.Debug.WriteLine(direction);
        }

            /// <summary>
            /// how look
            /// </summary>
            /// <param name="gameTime"></param>
            /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.3)
            {
                animationTimer -= 0.3;
                animationFrame++;
                if (animationFrame > 1)
                {
                    animationFrame = 0;
                }
            }
            var source = (currentDirection == Direction.Still) ? new Rectangle(89, 4, 24, 33) : new Rectangle(12+animationFrame*38, 4, 29, 33);
            SpriteEffects spriteEffects = (currentDirection==Direction.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2(0, 0), 2f, spriteEffects, 0);
        }
    }
}
