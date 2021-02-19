using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;
using Microsoft.Xna.Framework.Media;



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
        private bool fading = false;
        private GameState nextGameState;
        private InputManager inputManager;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
       // private InputManager inputManager;
        private Gameplay gameplay;
        private Menu menu;
        private GameState gameState = GameState.Menu;
        private double currentRunSecs = 0;
        private double bestRunSecs = -1;
        /// <summary>
        /// Constructor for game
        /// </summary>
        public VoidWandererGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// Initializes game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameplay = new Gameplay();
            menu = new Menu();
            inputManager = new InputManager();
            gameplay.Initialize();
            menu.Initialize();
            base.Initialize();
        }
        /// <summary>
        /// loads content
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameplay.LoadContent(Content);
            menu.LoadContent(Content);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(menu.BackgroundMusic);
        }
        /// <summary>
        /// updates game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    inputManager.Update(gameTime);
                    if (inputManager.MouseClicked())
                    {
                        if (inputManager.MouseCoordinates.X > 306 &&
                            inputManager.MouseCoordinates.Y > 376 &&
                            inputManager.MouseCoordinates.X < 494 &&
                            inputManager.MouseCoordinates.Y < 442)
                        {
                            gameState = GameState.Game;
                            fading = true;
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Play(gameplay.BackgroundMusic);
                            if (menu.CurrentTime != -1)
                            {
                                gameplay = new Gameplay();
                                gameplay.Initialize();
                                gameplay.LoadContent(Content);

                            }
                            
                        }
                    }
                    break;
                case GameState.Game:
                    //currentRunSecs += gameTime.ElapsedGameTime.TotalSeconds;
                    
                    if (gameplay.RoomsCleared >= 5 && gameplay.Player.UpdateNow)
                    {
                        if (bestRunSecs < -0.5 || gameplay.CurrentTime < bestRunSecs)
                        {
                            bestRunSecs = gameplay.CurrentTime;
                        }
                        menu.CurrentTime = (int) gameplay.CurrentTime;
                        menu.BestTime = (int)bestRunSecs;
                        //currentRunSecs = 0;
                        gameState = GameState.Menu;
                        MediaPlayer.Play(menu.BackgroundMusic);
                    }
                    else
                    {
                        gameplay.Update(gameTime);
                        //System.Diagnostics.Debug.WriteLine("moo");
                        if (Keyboard.GetState().IsKeyDown(Keys.R))
                        {
                            //currentRunSecs = 0;
                            gameState = GameState.Menu;
                            MediaPlayer.Play(menu.BackgroundMusic);
                        }
                    }
                    break;
            }
            
            base.Update(gameTime);
        }
        /// <summary>
        /// draws game
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            if (gameplay.Player.TeleportationCooldown == 0 || gameState != GameState.Game)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            else
            {
                GraphicsDevice.Clear(new Color((float)gameplay.Player.TeleportationCooldown/8, (float)gameplay.Player.TeleportationCooldown/16, (float)gameplay.Player.TeleportationCooldown/8));
            }
            // TODO: Add your drawing code here
            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(gameTime, spriteBatch);
                    break;
                case GameState.Game:
                    gameplay.Draw(gameTime, spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
       
       
    }
}
