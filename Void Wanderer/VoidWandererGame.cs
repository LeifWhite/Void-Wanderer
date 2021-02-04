using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;

namespace Void_Wanderer
{
    public class VoidWandererGame : Game
    {
       
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputManager inputManager;
        private GameMap gameMap;
        private Rho rho;
        private BoundingRectangle projectedLocation;
        private BoundingRectangle projectedLocationX;
        private BoundingRectangle projectedLocationY;
        private Vector2 move;
        private const int JUMPHEIGHT = 8;
        public VoidWandererGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rho = new Rho() { Position = new Vector2(60, 257) };
            projectedLocation = new BoundingRectangle();
            projectedLocation.Width = 29* Rho.SIZESCALE;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            rho.LoadContent(Content);
            gameMap.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            // TODO: Add your update logic here
            move.X = 0;
            inputManager.Update(gameTime);
            gameMap.Update(gameTime);
            move.X = inputManager.Direction.X;
            if(inputManager.TryJump && move.Y == 0)
            {
                move.Y = -JUMPHEIGHT;
            }
            else
            {
                move.Y = MathHelper.Min(move.Y+0.3f, 6.5f);
            }
            projectedLocation.X = rho.Position.X + move.X;
            projectedLocation.Y = rho.Position.Y + move.Y;
            if(rho.CurrentDirection == Direction.Still)
            {
                projectedLocation.X += 2.5f;
                projectedLocation.Width = 24 * Rho.SIZESCALE;
                projectedLocationX.Width = 24 * Rho.SIZESCALE;
                projectedLocationX.Width = 24 * Rho.SIZESCALE;
            }
            else
            {
                projectedLocation.Width = 29 *Rho.SIZESCALE;
                projectedLocationX.Width = 29 * Rho.SIZESCALE;
                projectedLocationY.Width = 29 * Rho.SIZESCALE;


            }
            projectedLocationX.X = projectedLocation.X;
            projectedLocationX.Y = rho.Position.Y;
            projectedLocationY.X = rho.Position.X;
            projectedLocationY.Y = projectedLocation.Y;
            for (int i = 0; i < gameMap.Blocks.Count; i++)
            {
                if(projectedLocation.CollidesWith(gameMap.Blocks[i].Bounds))
                {
                    if (projectedLocationX.CollidesWith(gameMap.Blocks[i].Bounds))
                    {
                        move.X = 0;
                        if (rho.Bounds.Left > gameMap.Blocks[i].Bounds.Right)
                        {
                            rho.ForceMove(gameMap.Blocks[i].Bounds.Right + 0.1f, rho.Position.Y);
                        }
                        else if (rho.Bounds.Right < gameMap.Blocks[i].Bounds.Left)
                        {
                            rho.ForceMove(gameMap.Blocks[i].Bounds.Left-rho.Bounds.Width-0.1f, rho.Position.Y);
                        }
                    }
                    if (projectedLocationY.CollidesWith(gameMap.Blocks[i].Bounds))
                    {
                        
                        if (rho.Bounds.Top > gameMap.Blocks[i].Bounds.Bottom)
                        {
                            move.Y = 0.1f;
                            rho.ForceMove(rho.Position.X, gameMap.Blocks[i].Bounds.Bottom + 0.1f);
                        }
                        else if(rho.Bounds.Bottom < gameMap.Blocks[i].Bounds.Top)
                        {
                            move.Y = 0;
                            rho.ForceMove(rho.Position.X, gameMap.Blocks[i].Bounds.Top - rho.Bounds.Height-0.1f);
                        }
                    }
                        
                }
            }
            rho.Update(gameTime, move);
           
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            gameMap.Draw(gameTime, spriteBatch);
            rho.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void ModifyProjectedLocation()
        {

        }
       
    }
}
