using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;


namespace Void_Wanderer
{
    public enum GameState
    {
        Menu,
        Game,
        GameOver

    }
    public class VoidWandererGame : Game
    {
       
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
       // private InputManager inputManager;
        private Gameplay gameplay;
        public VoidWandererGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameplay = new Gameplay();
            gameplay.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameplay.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here
            gameplay.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (gameplay.Player.TeleportationCooldown == 0)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else
            {
                GraphicsDevice.Clear(new Color((float)gameplay.Player.TeleportationCooldown/8, (float)gameplay.Player.TeleportationCooldown/16, (float)gameplay.Player.TeleportationCooldown/8));
            }
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            gameplay.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
       
       
    }
}
