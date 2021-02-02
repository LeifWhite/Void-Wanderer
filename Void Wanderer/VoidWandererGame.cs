using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public VoidWandererGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rho = new Rho() { Position = new Vector2(50, 367) };
            projectedLocation = new BoundingRectangle();
            projectedLocation.Width = 29;
            projectedLocation.Height = 33;
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
            inputManager.Update(gameTime);
            gameMap.Update(gameTime);
            Vector2 xMove = inputManager.Direction;
            projectedLocation.X = rho.Position.X + inputManager.Direction.X;
            projectedLocation.Y = rho.Position.X + inputManager.Direction.X;
            for (int i = 0; i < gameMap.Blocks.Count; i++)
            {
                /*if(Collides(gameMap.Blocks[i].BRec, projectedLocation))
                {
                    if()
                    xMove = Vector2.Zero;
                }*/
            }
            rho.Update(gameTime, xMove);
           
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
       /// <summary>
       /// 
       /// </summary>
       /// <param name="r1"></param>
       /// <param name="r2"></param>
       /// <returns>0 = nothing, 1 = top, 2 =  right, 3 = bottom, 4 = left</returns>
        public static int Collides(BoundingRectangle r1, BoundingRectangle r2)
        {
            // r1 is to the left of r2
            if (!(r1.X + r1.Width < r2.X))
            {
                return 4;
            }
            // r1 is to the right of r2
            if (!(r1.X > r2.X + r2.Width))
            {
                return 2;
            }
            // r1 is above r2 
            if (!(r1.Y + r1.Height < r2.Y))
            {
                return 1;
            }
            // r1 is below r2
            if (!(r1.Y > r2.Y + r2.Height))
            {
                return 3;
            }

            return 0;
        }
    }
}
