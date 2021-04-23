using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Void_Wanderer.Collisions;
using Microsoft.Xna.Framework.Media;
using System;
using Void_Wanderer.ParticleSystems;



namespace Void_Wanderer
{
    public enum GameState
    {
        Menu,
        Game,
        GameOver


    }
    public static class Screen
    {
        /// <summary>
        /// How big is the screen?
        /// </summary>
        public static int SIZE = (int)(Math.Min(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height)*0.9);
    }
    public class VoidWandererGame : Game
    {
        //private bool fading = false;
        //private GameState nextGameState;
        private InputManager inputManager;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //private ResizeStatus resizing; // Records status and (width, height) while resizing

        // private InputManager inputManager;
        private GameScreen gameplay;
        private MenuScreen menu;
        private GameState gameState = GameState.Menu;
        //private double currentRunSecs = 0;
       
        private double bestRunSecs = -1;
        /// <summary>
        /// Constructor for game
        /// </summary>
        public VoidWandererGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Screen.SIZE;
            graphics.PreferredBackBufferWidth = Screen.SIZE;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //I will implement this later
            //Window.AllowUserResizing = true;
            //Window.ClientSizeChanged += OnResize;
        }
        public void OnResize(Object sender, EventArgs e)
        {
            // Additional code to execute when the user drags the window
            // or in the case you programmatically change the screen or windows client screen size.
            // code that might directly change the backbuffer width height calling apply changes.
            // or passing changes that must occur in other classes or even calling there OnResize methods
            // though those methods can simply be added to the Windows event caller
            Screen.SIZE = Math.Min(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
        }
        /// <summary>
        /// Initializes game
        /// </summary>
        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            gameplay = new GameScreen();
            menu = new MenuScreen();
            inputManager = new InputManager();
            gameplay.Initialize();
            menu.Initialize();
            //gameplay.Rain.DestroyAllParticles();
           
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
            inputManager.Update(gameTime);
            
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || (inputManager.currentKeyboardState.IsKeyDown(Keys.Escape) && !inputManager.priorKeyboardState.IsKeyDown(Keys.Escape)) && gameState == GameState.Menu))
                Exit();


            // TODO: Add your update logic here

            switch (gameState)
            {
                case GameState.Menu:
                   
                    menu.Update(gameTime);
                    
                    if (menu.PlayButton.Clicked()) {
                            gameplay.ChangeBackground();
                            gameState = GameState.Game;
                            //fading = true;
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Play(gameplay.BackgroundMusic);
                            if (menu.CurrentTime != -1 || gameplay.CurrentTime!=-1)
                            {
                                gameplay = new GameScreen();
                                gameplay.Initialize();
                                gameplay.LoadContent(Content);

                            }
                            
                        
                    }
                    break;
                case GameState.Game:
                   
                    
                    if (gameplay.RoomsCleared >= 5 && gameplay.Player.UpdateNow)
                    {
                        if (bestRunSecs < -0.5 || gameplay.CurrentTime < bestRunSecs)
                        {
                            bestRunSecs = gameplay.CurrentTime;
                        }
                        menu.CurrentTime = (int) gameplay.CurrentTime;
                        menu.BestTime = (int)bestRunSecs;
                        //currentRunSecs = 0;
                        //gameplay.Rain.DestroyAllParticles();
                        //gameplay.Rain.IsRaining = false;
                        
                        gameState = GameState.Menu;
                        MediaPlayer.Play(menu.BackgroundMusic);
                    }
                    else
                    {
                        gameplay.Update(gameTime);
                        //System.Diagnostics.Debug.WriteLine("moo");
                        if (inputManager.currentKeyboardState.IsKeyDown(Keys.R) || inputManager.currentKeyboardState.IsKeyDown(Keys.Escape))
                        {

                            
                            //currentRunSecs = 0;
                            //gameplay.Rain.DestroyAllParticles();
                            //gameplay.Rain.IsRaining = false;
                            
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
