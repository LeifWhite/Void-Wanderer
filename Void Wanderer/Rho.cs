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
        /// <summary>
        /// How big is Rho
        /// </summary>
        public const float SIZESCALE = 1.4f;
        /// <summary>
        /// Is Rho teleporting
        /// </summary>
        public bool Teleporting = false;
        private short teleportationState = 0;
        private double teleportationTimer;
        private Vector2 teleportationCoordinates;
        /// <summary>
        /// How long til can teleport again
        /// </summary>
        public double TeleportationCooldown = 4;
        private bool switchingLevels= false;
        /// <summary>
        /// Changes game room
        /// </summary>
        public bool UpdateNow = false;
        private Direction currentDirection;
        /// <summary>
        /// Which way facing
        /// </summary>
        public Direction CurrentDirection => currentDirection;
        /// <summary>
        /// where go
        /// </summary>
        public Vector2 Position;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0,0), 29*SIZESCALE, 33*SIZESCALE);
        /// <summary>
        /// Bounds of Rho
        /// </summary>
        public BoundingRectangle Bounds => bounds;
        /// <summary>
        /// loads content
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
        /// <summary>
        /// Updates rho
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="direction"></param>
        public void Update(GameTime gameTime, Vector2 direction)
        {
            if (TeleportationCooldown > 0)
            {
                TeleportationCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            }
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
            /// how look, and runs teleport animation
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
            if (Teleporting)
            {
                teleportationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (teleportationTimer > 0.13)
                {
                    teleportationTimer -= 0.13;
                    teleportationState++;
                    if (teleportationState == 3)
                    {
                        if(!switchingLevels)
                            ForceMove(teleportationCoordinates);
                        else
                        {
                            UpdateNow = true;
                            switchingLevels = false;
                        }

                    }

                    
                }
               
                
            }
                if (teleportationState == 0)
            {
                 var source = (currentDirection == Direction.Still) ? new Rectangle(89, 4, 24, 33) : new Rectangle(12 + animationFrame * 38, 4, 29, 33);

                SpriteEffects spriteEffects = (currentDirection == Direction.Left) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2((currentDirection == Direction.Still) ? -2.5f : 0, 0), SIZESCALE, spriteEffects, 0);
            }
            else
            {
                Rectangle source;
                if(teleportationState == 1 || teleportationState == 4)
                {
                    source =  new Rectangle(118, 3, 23, 33);
                }
                else if (teleportationState == 2 || teleportationState == 3)
                {
                    source = new Rectangle(148, 2, 24, 34);
                }
                else
                {
                    source = new Rectangle(118, 3, 23, 33);
                    teleportationState = 0;
                    Teleporting = false;
                    teleportationTimer = 0;
                    TeleportationCooldown = 3;
                }
                spriteBatch.Draw(texture, Position, source, Color.White, 0f, new Vector2(-2.5f,  0), SIZESCALE, SpriteEffects.None, 0);
            }
            if (showHitbox)
            {
                var rectSource = new Rectangle(0, 0, (int)bounds.Width, (int)bounds.Height);
;                spriteBatch.Draw(hitboxRectangle, new Vector2(bounds.X, bounds.Y), rectSource, new Color(Color.White,0.5f));
            }
        }
       /// <summary>
       /// moves rho and changes his boundaries
       /// </summary>
       /// <param name="loc"></param>
        public void ForceMove(Vector2 loc)
        {
            Position = loc;
            bounds.X = loc.X;
            bounds.Y = loc.Y;
        }
        /// <summary>
        /// alternate call style
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ForceMove(float x, float y) => ForceMove(new Vector2(x, y));
        /// <summary>
        /// tries to teleport to a destination
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="tileMap"></param>
        public void TryTeleport(Vector2 destination, string[] tileMap)
        {
            if (TeleportationCooldown > 0)
            {
                return;
            }

            //BoundingPoint p = new BoundingPoint(destination);
            Vector2 tmIndexer = new Vector2((float)Math.Floor(destination.X/48), (float)Math.Floor(destination.Y/48));
            if (tileMap[(int)tmIndexer.Y][(int)tmIndexer.X]=='A')
            {
                Teleport(tmIndexer.X*48+1, tmIndexer.Y*48+48-33*Rho.SIZESCALE-1);
            }
        }
        /// <summary>
        /// teleports to destination
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Teleport(float x, float y)
        {
            Teleporting = true;
            teleportationCoordinates = new Vector2(x, y);
        }
        /// <summary>
        /// telepots to new room
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SwitchTeleport(float x, float y)
        {
            Teleporting = true;
            teleportationState = 0;
            teleportationTimer = 0;
            teleportationCoordinates = new Vector2(x, y);
            switchingLevels = true;
        }
    }
}
