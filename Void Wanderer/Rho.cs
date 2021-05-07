using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Void_Wanderer.Collisions;
using Microsoft.Xna.Framework.Audio;
using Void_Wanderer.ParticleSystems;

namespace Void_Wanderer
{
    public enum Direction
    {
        Still,
        Left,
        Right
       
    }
    public class Rho : IParticleEmitter
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
        public static float SIZESCALE = 1.4f * (Screen.SIZE / 800f) * Screen.GS;

        /// <summary>
        /// Is Rho teleporting
        /// </summary>
        public bool Teleporting = false;
        private short teleportationState = 0;
        private double teleportationTimer;
        private Vector2 teleportationCoordinates;
        /// <summary>
        /// How long the cooldown is
        /// </summary>
        public double MAX_TELEPORTATION_COOLDOWN = 5;
        /// <summary>
        /// How long til can teleport again
        /// </summary>
        public double TeleportationCooldown = 5;

        private bool switchingLevels = false;
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

        private SoundEffect teleportSound;

        private Vector2 offset = Vector2.Zero;

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(0, 0), 29 * SIZESCALE, 33 * SIZESCALE);
        /// <summary>
        /// Bounds of Rho
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        Vector2 IParticleEmitter.Position => Position;
        private Vector2 velocity = Vector2.Zero;
        Vector2 IParticleEmitter.Velocity => velocity;
        //private Vector2 size = ;
        Vector2 IParticleEmitter.Size => new Vector2(bounds.Width, bounds.Height);
        private TeleportParticleSystem teleport;
        /// <summary>
        /// loads content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            teleport = new TeleportParticleSystem(this);
            teleport.LoadContent(content);
            texture = content.Load<Texture2D>("VW rho");
            teleportSound = content.Load<SoundEffect>("teleport");
           
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
            teleport.Update(gameTime);
            if (TeleportationCooldown > 0)
            {
                
                TeleportationCooldown -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            if(Position.X+direction.X<0 || Position.X + direction.X > Screen.SIZE*Screen.GS - bounds.Width/2)
            {
                direction.X = 0;
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
            velocity = direction;
            Position = Position + direction;
            bounds.X = Position.X;
            bounds.Y = Position.Y;
            //System.Diagnostics.Debug.WriteLine(direction);
        }

            /// <summary>
            /// how look, and runs teleport animation
            /// </summary>
            /// <param name="gameTime"></param>
            /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float offX = 0, float offY = 0)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            teleport.Draw(gameTime, spriteBatch, -offX, -offY);
            spriteBatch.End();
            offset.X = offX;
            offset.Y = offY;
            Matrix transform = Matrix.CreateTranslation(offX, offY, 0);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: transform);

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
                    TeleportationCooldown = MAX_TELEPORTATION_COOLDOWN;
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
            Vector2 tmIndexer = new Vector2((float)Math.Floor((destination.X-offset.X)/(48 * (Screen.SIZE / 800f) * Screen.GS)), (float)Math.Floor((destination.Y-offset.Y)/(48 * (Screen.SIZE / 800f) * Screen.GS)));
            if (tileMap[(int)tmIndexer.Y][(int)tmIndexer.X]=='A'&&tmIndexer.X<tileMap[0].Length-1)
            {
                Teleport(tmIndexer.X*48 * (Screen.SIZE / 800f) * Screen.GS + 1, tmIndexer.Y*48 * (Screen.SIZE / 800f) * Screen.GS + 48 * (Screen.SIZE / 800f) * Screen.GS - 33*Rho.SIZESCALE-1);
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
            teleportSound.Play(volume: 0.25f, pitch: -0.4f, pan: 0.0f);
            teleport.SpawnParticles();
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
            teleportSound.Play(volume: 0.25f, pitch: -0.4f, pan: 0.0f);

            switchingLevels = true;
        }
    }
}
