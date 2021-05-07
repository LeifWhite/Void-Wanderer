using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Void_Wanderer.ParticleSystems;



namespace Void_Wanderer
{
   /// <summary>
   /// What happens during gameplay
   /// </summary>
    public class GameScreen 
    {

       

        private InputManager inputManager;
        private GameMap gameMap;
        /// <summary>
        /// player, his name is Rho
        /// </summary>
        public Rho Player;
        private BoundingRectangle projectedLocation;
        private BoundingRectangle projectedLocationX;
        private BoundingRectangle projectedLocationY;
        private Vector2 move;
        /// <summary>
        /// How many coins have been collected
        /// </summary>
        public int CoinsCollected = 0;
        /// <summary>
        /// How many rooms have been cleared
        /// </summary>
        public int RoomsCleared = 0;
        /// <summary>
        /// How high you jump
        /// </summary>
        private float JUMPHEIGHT = (9.3f * (Screen.SIZE / 800f) * Screen.GS);
        /// <summary>
        /// How high you jump
        /// </summary>
        private int MOVESPEED = (int)(1 * (Screen.SIZE / 800f) * Screen.GS);

        /// <summary>
        /// How high you jump
        /// </summary>
        private float GRAVITY = 0.3f * (Screen.SIZE / 800f) * Screen.GS;
        /// <summary>
        /// How high you jump
        /// </summary>
        private float TERMINALVELOCITY = 6.5f * (Screen.SIZE / 800f) * Screen.GS;
        /// <summary>
        /// Sound effect when you get a coin
        /// </summary>
        private SoundEffect coinPickup;
        
        /// <summary>
        /// Avenida de los Sueños by Thomas White
        /// </summary>
        public Song BackgroundMusic;
        private Texture2D[] backgrounds = new Texture2D[3];
        /// <summary>
        /// Elapsed time
        /// </summary>
        public float CurrentTime;
        private SpriteFont arial;
        private Texture2D greySquare;
        public int CurrentBackground;

        /// <summary>
        /// Rain particle effect
        /// </summary>
        public RainParticleSystem Rain;
        /// <summary>
        /// Landing particle effect
        /// </summary>
        public LandingParticleSystem Land;
        private float saveFallSpeed = 0;
        private float apex = 0;
        private Vector2 sight = new Vector2(0, 0);
        /// <summary>
        /// Initializes
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here
            Rain = new RainParticleSystem(new Rectangle(0, -20, Screen.SIZE, 10));
            
           
            //Rain.IsRaining = false;
            projectedLocation = new BoundingRectangle();
            projectedLocation.Width = 29 * Rho.SIZESCALE;
            projectedLocation.Height = 33 * Rho.SIZESCALE;
            projectedLocationX = new BoundingRectangle();
            projectedLocationX.Width = 29 * Rho.SIZESCALE;
            projectedLocationX.Height = 33 * Rho.SIZESCALE;
            projectedLocationY = new BoundingRectangle();
            projectedLocationY.Width = 29 * Rho.SIZESCALE;
            projectedLocationY.Height = 33 * Rho.SIZESCALE;
            move = Vector2.Zero;
            gameMap = new GameMap();
            Player = new Rho() { Position = gameMap.RhoStartingPosition };
            
            Land = new LandingParticleSystem(Player, Color.White);
            inputManager = new InputManager();
            CurrentTime = 0;

            ChangeBackground();
        }
        public void ChangeBackground()
        {
            CurrentBackground = RandomHelper.Next(0, 3);
            gameMap.Bnum = CurrentBackground;
            Land.DirtColor = gameMap.ColorMap2[gameMap.Bnum];
            if (CurrentBackground == 1)
            {
                //Rain.IsRaining = true;
            }
            else
            {
               
                //Rain.DestroyAllParticles();
                //Rain.IsRaining = false;
            }
        }

        /// <summary>
        /// Loads content
        /// </summary>
        /// <param name="content"></param>
       public void LoadContent(ContentManager content)
        {
            
            Player.LoadContent(content);
            gameMap.LoadContent(content);
            Rain.LoadContent(content);
            Land.LoadContent(content);
           
            coinPickup = content.Load<SoundEffect>("Pickup_Coin15");
            if(RandomHelper.NextFloat(0,1)>0.5)
                BackgroundMusic = content.Load<Song>("Gamesong");
            else
                BackgroundMusic = content.Load<Song>("Stepping");
            arial = content.Load<SpriteFont>("arial");
            greySquare = content.Load<Texture2D>("VW Grey Square");
            backgrounds[0] = content.Load<Texture2D>("VW Ginesha");
            backgrounds[1] = content.Load<Texture2D>("VW New Gloucester");
            backgrounds[2] = content.Load<Texture2D>("VW Onyet");
           
        }
        /// <summary>
        /// Updates game
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (CurrentBackground == 1)
                Rain.Update(gameTime);
            Land.Update(gameTime);
            if (Player.UpdateNow)
            {

                Player.UpdateNow = false;
                
                gameMap.Blocks = new List<Block>();
                ChangeBackground();
                gameMap.PopulateTileMap();
                Player.ForceMove(gameMap.RhoStartingPosition);
            }
            // TODO: Add your update logic here
            move.X = 0;
            inputManager.Update(gameTime);
            gameMap.Update(gameTime);
            if (Player.Teleporting)
            {
                move.Y = 0.00001f;
            }
            else
            {
                move.X = inputManager.Direction.X*MOVESPEED;

                if (inputManager.TryJump && move.Y == 0)
                {
                    move.Y = -JUMPHEIGHT;
                    
                }
                else
                {
                    move.Y = MathHelper.Min(move.Y + GRAVITY, TERMINALVELOCITY);
                }

                projectedLocation.X = MathHelper.Clamp((Player.Position.X + move.X), 0, Screen.SIZE*Screen.GS);
                projectedLocation.Y = Player.Position.Y + move.Y;
                if (move.X == 0)
                {
                    projectedLocation.X += 2.5f;
                    projectedLocation.Width = 24 * Rho.SIZESCALE;
                    projectedLocationX.Width = 24 * Rho.SIZESCALE;
                    projectedLocationX.Width = 24 * Rho.SIZESCALE;
                }
                else
                {
                    projectedLocation.Width = 29 * Rho.SIZESCALE;
                    projectedLocationX.Width = 29 * Rho.SIZESCALE;
                    projectedLocationY.Width = 29 * Rho.SIZESCALE;


                }
                projectedLocationX.X = projectedLocation.X;
                projectedLocationX.Y = Player.Position.Y;
                projectedLocationY.X = Player.Position.X;
                projectedLocationY.Y = projectedLocation.Y;
                for (int i = 0; i < gameMap.Blocks.Count; i++)
                {
                    if (projectedLocation.CollidesWith(gameMap.Blocks[i].Bounds))
                    {
                        if (projectedLocationY.CollidesWith(gameMap.Blocks[i].Bounds))
                        {

                            if (Player.Bounds.Top > gameMap.Blocks[i].Bounds.Bottom)
                            {
                                move.Y = 0.1f;
                                Player.ForceMove(Player.Position.X, gameMap.Blocks[i].Bounds.Bottom + 0.1f);
                            }
                            else if (Player.Bounds.Bottom < gameMap.Blocks[i].Bounds.Top)
                            {

                                move.Y = 0;
                                if (saveFallSpeed != 0)
                                {
                                    Land.SpawnParticles(100*(Player.Position.Y-apex)/Screen.SIZE);
                                }
                                Player.ForceMove(Player.Position.X, gameMap.Blocks[i].Bounds.Top - Player.Bounds.Height - 0.1f);
                            }
                        }
                        if (projectedLocationX.CollidesWith(gameMap.Blocks[i].Bounds))
                        {
                            move.X = 0;
                            if (Player.Bounds.Left > gameMap.Blocks[i].Bounds.Right)
                            {
                                Player.ForceMove(gameMap.Blocks[i].Bounds.Right + 0.1f, Player.Position.Y);
                            }
                            else if (Player.Bounds.Right < gameMap.Blocks[i].Bounds.Left)
                            {
                                Player.ForceMove(gameMap.Blocks[i].Bounds.Left - Player.Bounds.Width - 0.1f, Player.Position.Y);
                            }
                        }


                    }
                }
            }
            if(saveFallSpeed<=0 && move.Y > 0 || move.Y==0)
            {
                apex = Player.Position.Y;
            }
            saveFallSpeed = move.Y;
            Player.Update(gameTime, move);
            for (int i = 0; i < gameMap.Coins.Count; i++)
            {
                if (!gameMap.Coins[i].IsCollected && Player.Bounds.CollidesWith(gameMap.Coins[i].Bounds) )
                {
                    gameMap.Coins[i].IsCollected = true;
                    CoinsCollected++;
                    coinPickup.Play(volume: 0.25f, pitch: -0.4f, pan: 0.0f);
                }
            }

            if (inputManager.MouseClicked())
            {
                Player.TryTeleport(inputManager.MouseCoordinates, gameMap.TileMap);
            }
            if (CoinsCollected >= gameMap.CoinCount && gameMap.CoinCount>=1)
            {
                CoinsCollected = 0;
                RoomsCleared++;
                NewRoom();
            }
        }
       /// <summary>
       /// Creates new room
       /// </summary>
        public void NewRoom()
        {
            gameMap.Coins = new List<Coin>();
            gameMap.CoinCount -=2;
            if(gameMap.CoinCount!=0)
                gameMap.RandomizeTileMap();
            
            Player.SwitchTeleport(gameMap.RhoStartingPosition.X, gameMap.RhoStartingPosition.Y);
           
           
            
        }
       /// <summary>
       /// Dras game
       /// </summary>
       /// <param name="gameTime"></param>
       /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgrounds[CurrentBackground], new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 4 * (Screen.SIZE / 800f), SpriteEffects.None, 0f);
            spriteBatch.End();
            
            float playerX = MathHelper.Clamp(Player.Position.X, Screen.SIZE / 2, gameMap.TileMap[0].Length * 48 *(Screen.SIZE/800f)*Screen.GS- Screen.SIZE / 2);
            float playerY = MathHelper.Clamp(Player.Position.Y, Screen.SIZE / 2, gameMap.TileMap[0].Length * 48 * (Screen.SIZE / 800f) * Screen.GS- Screen.SIZE / 2);
            sight.X =  playerX - Screen.SIZE / 2;
            sight.Y =  playerY - Screen.SIZE / 2;
            Matrix transform = Matrix.CreateTranslation(-sight.X, -sight.Y, 0);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: transform);

            gameMap.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch, -sight.X, -sight.Y);
            if (CurrentBackground ==1 )
                Rain.Draw(gameTime, spriteBatch);
            Land.Draw(gameTime, spriteBatch, sight.X, sight.Y);
            //spriteBatch.DrawString(arial, "Time", new Vector2(40, 390), Color.Silver);
            //spriteBatch.DrawString(arial, "Time", new Vector2(41, 392), Color.White);
            spriteBatch.End();
           
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
            spriteBatch.Draw(greySquare, new Vector2(360, 756) * (Screen.SIZE / 800f), new Rectangle(0, 0, 10, 5), Color.White*0.95f, 0f, Vector2.Zero, 8 * (Screen.SIZE / 800f), SpriteEffects.None, 0f);
            if(Player.TeleportationCooldown>0)
            spriteBatch.Draw(greySquare, new Vector2(0, 790) * (Screen.SIZE / 800f), new Rectangle(0, 0, 10, 10), Color.Purple * 0.95f, 0f, Vector2.Zero, 80*(float)(Player.TeleportationCooldown/Player.MAX_TELEPORTATION_COOLDOWN) * (Screen.SIZE / 800f), SpriteEffects.None, 0f);
            string csecs = (((int)CurrentTime % 60) <= 9) ? "0" + ((int)CurrentTime % 60).ToString() : ((int)CurrentTime % 60).ToString();
            //spriteBatch.DrawString(arial, Math.Floor((int)CurrentTime / 60.0).ToString() + ":" + csecs, new Vector2(40, 435), Color.Black);
            spriteBatch.DrawString(arial, Math.Floor((int)CurrentTime / 60.0).ToString() + ":" + csecs, new Vector2(368, 758) * (Screen.SIZE / 800f), new Color(20, 20, 20), 0, Vector2.Zero, (Screen.SIZE / 1600f), SpriteEffects.None, 0);

        }


    }
}

