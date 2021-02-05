using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;
using Microsoft.Xna.Framework.Content;



namespace Void_Wanderer
{
   
    public class Gameplay
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputManager inputManager;
        private GameMap gameMap;
        public Rho Player;
        private BoundingRectangle projectedLocation;
        private BoundingRectangle projectedLocationX;
        private BoundingRectangle projectedLocationY;
        private Vector2 move;

        private const int JUMPHEIGHT = 8;
       

        public void Initialize()
        {
            // TODO: Add your initialization logic here
            Player = new Rho() { Position = new Vector2(60, 257) };
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
            inputManager = new InputManager();
           
        }

       public void LoadContent(ContentManager content)
        {
            
            Player.LoadContent(content);
            gameMap.LoadContent(content);
            
        }

        public void Update(GameTime gameTime)
        {
            
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
                if (Player.Bounds.CollidesWith(gameMap.Coins[i].Bounds))
                {
                    gameMap.Coins[i].IsCollected = true;
                }
            }

            if (inputManager.MouseClicked())
            {
                Player.TryTeleport(inputManager.MouseCoordinates, gameMap.TileMap);
            }
          
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            gameMap.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch);

        }


    }
}

