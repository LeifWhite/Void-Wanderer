using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Void_Wanderer
{
    public class Rho
    {

        private Texture2D texture;
        
        private double animationTimer;
        private short animationFrame = 1;
        private bool goingRight;

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
            texture = content.Load<Texture2D>("platformerGraphicsDeluxe_Updated");
        }
        public void Update(GameTime gameTime, Vector2 direction)
        {
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
            if (animationTimer > 0.6)
            {
                animationTimer -= 0.6;
                animationFrame++;
                if (animationFrame > 2)
                {
                    animationFrame = 1;
                }
            }
            var source = new Rectangle(21+36*animationFrame, 510, 36, 46);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
    }
}
