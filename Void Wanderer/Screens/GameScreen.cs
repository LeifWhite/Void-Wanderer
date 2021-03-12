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
        private const int JUMPHEIGHT = 8;
        /// <summary>
        /// Sound effect when you get a coin
        /// </summary>
        private SoundEffect coinPickup;
        /// <summary>
        /// Avenida de los Sueños by Thomas White
        /// </summary>
        public Song BackgroundMusic;

        /// <summary>
        /// Elapsed time
        /// </summary>
        public float CurrentTime;
        private SpriteFont arial;
        private Texture2D greySquare;
        /// <summary>
        /// Initializes
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here
           
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
            inputManager = new InputManager();
            CurrentTime = 0;
        }
        /// <summary>
        /// Loads content
        /// </summary>
        /// <param name="content"></param>
       public void LoadContent(ContentManager content)
        {
            
            Player.LoadContent(content);
            gameMap.LoadContent(content);
            coinPickup = content.Load<SoundEffect>("Pickup_Coin15");
            BackgroundMusic = content.Load<Song>("Gamesong");
            arial = content.Load<SpriteFont>("arial");
            greySquare = content.Load<Texture2D>("VW Grey Square");
        }
        /// <summary>
        /// Updates game
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Player.UpdateNow)
            {

                Player.UpdateNow = false;
                
                gameMap.Blocks = new List<Block>();
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
                move.X = inputManager.Direction.X;

                if (inputManager.TryJump && move.Y == 0)
                {
                    move.Y = -JUMPHEIGHT;
                }
                else
                {
                    move.Y = MathHelper.Min(move.Y + 0.3f, 6.5f);
                }

                projectedLocation.X = Player.Position.X + move.X;
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

            gameMap.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch);
            //spriteBatch.DrawString(arial, "Time", new Vector2(40, 390), Color.Silver);
            //spriteBatch.DrawString(arial, "Time", new Vector2(41, 392), Color.White);
            spriteBatch.Draw(greySquare, new Vector2(360, 436), new Rectangle(0, 0, 10, 5), Color.White*0.95f, 0f, Vector2.Zero, 8, SpriteEffects.None, 0f);
            if(Player.TeleportationCooldown>0)
            spriteBatch.Draw(greySquare, new Vector2(0, 470), new Rectangle(0, 0, 10, 10), Color.Purple * 0.95f, 0f, Vector2.Zero, 80*(float)(Player.TeleportationCooldown/Player.MAX_TELEPORTATION_COOLDOWN), SpriteEffects.None, 0f);
            string csecs = (((int)CurrentTime % 60) <= 9) ? "0" + ((int)CurrentTime % 60).ToString() : ((int)CurrentTime % 60).ToString();
            //spriteBatch.DrawString(arial, Math.Floor((int)CurrentTime / 60.0).ToString() + ":" + csecs, new Vector2(40, 435), Color.Black);
            spriteBatch.DrawString(arial, Math.Floor((int)CurrentTime / 60.0).ToString() + ":" + csecs, new Vector2(368, 438), new Color(20, 20, 20));
            
        }


    }
}

