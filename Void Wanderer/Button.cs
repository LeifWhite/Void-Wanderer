using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Void_Wanderer.Collisions;


namespace Void_Wanderer
{
    public class Button
    {
        /// <summary>
        /// Where it is at
        /// </summary>
        public Vector2 Position;
        private InputManager inputManager;
        private Texture2D texture;
        private BoundingRectangle bounds;
        private BoundingPoint mouseLocation = new BoundingPoint();
        /// <summary>
        /// Collision boundary
        /// </summary>
        public BoundingRectangle Bounds => bounds;
        private float scale = 1f;
        private Rectangle source;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
        
        public Button(Vector2 position, Texture2D texture, float scale=1f)
        {
            Position = position;
            this.texture = texture;
            this.scale = scale;
            bounds = new BoundingRectangle(new Vector2(0, 0), texture.Bounds.Width * this.scale, texture.Bounds.Height * this.scale);
            bounds.X = Position.X;
            bounds.Y = Position.Y;
            source = new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height);
            
            inputManager = new InputManager();


        }
        public Button(Vector2 position, Texture2D texture, Rectangle source, float scale=1f)
        {
            Position = position;
            this.texture = texture;
            this.scale = scale;
            this.source = source;
            bounds = new BoundingRectangle(new Vector2(0, 0), source.Width * this.scale, source.Height * this.scale);
            bounds.X = Position.X;
            bounds.Y = Position.Y;


            inputManager = new InputManager();


        }
       
        /// <summary>
        /// Draws block
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="texture"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float rotationSpeed=0)
        {

            spriteBatch.Draw(texture, Position, source, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }
        /// <summary>
        /// updates
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            inputManager.Update(gameTime);

        }
        public bool Clicked()
        {
            mouseLocation.X = inputManager.MouseCoordinates.X;
            mouseLocation.Y = inputManager.MouseCoordinates.Y;
            if (inputManager.MouseClicked())
            {
                if (mouseLocation.CollidesWith(bounds))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
