using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Void_Wanderer.Collisions;

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
        private Texture2D hitboxCircle;
        private Texture2D hitboxRectangle;
        private bool showHitbox = false;
        private double animationTimer;
        private short animationFrame = 0;
        public const float SIZESCALE = 1.7f;
        
        private Direction currentDirection;
        public Direction CurrentDirection => currentDirection;
        /// <summary>
        /// where go
        /// </summary>
        public Vector2 Position;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0,0), 29*SIZESCALE, 33*SIZESCALE);
        public BoundingRectangle Bounds => bounds;
        /// <summary>
        /// where find
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("VW rho");
            if (showHitbox)
            {
                hitboxCircle = content.Load<Texture2D>("VW Circle");
                hitboxRectangle = content.Load<Texture2D>("VW Square");
            }
            
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
            bounds.X = Position.X;
            bounds.Y = Position.Y;
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
            spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2((currentDirection == Direction.Still) ?-2.5f:0, 0), SIZESCALE, spriteEffects, 0);
            if (showHitbox)
            {
                var rectSource = new Rectangle(0, 0, (int)bounds.Width, (int)bounds.Height);
;                spriteBatch.Draw(hitboxRectangle, new Vector2(bounds.X, bounds.Y), rectSource, new Color(Color.White,0.5f));
            }
        }
        public void ForceMove(Vector2 loc)
        {
            Position = loc;
            bounds.X = loc.X;
            bounds.Y = loc.Y;
        }
        public void ForceMove(float x, float y) => ForceMove(new Vector2(x, y));
    }
}
