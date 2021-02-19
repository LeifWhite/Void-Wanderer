using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Void_Wanderer
{
    /// <summary>
    /// This class was partially based off of the CIS 580 Input Management tutorial
    /// </summary>
    public class InputManager
    {
        public KeyboardState currentKeyboardState;
        KeyboardState priorKeyboardState;
        MouseState currentMouseState;
        MouseState priorMouseState;
        /// <summary>
        /// Where mouse at
        /// </summary>
        public Vector2 MouseCoordinates => new Vector2(currentMouseState.X, currentMouseState.Y);
        /// <summary>
        /// Moves direction
        /// </summary>
        public Vector2 Direction { get; private set; }
        /// <summary>
        /// Attempts to jump
        /// </summary>
        public bool TryJump;
        private float speed = 140;
        /// <summary>
        /// Updates input management
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            TryJump = false;
            priorKeyboardState = currentKeyboardState;
            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
            Direction = new Vector2(0, 0);
            if (currentKeyboardState.IsKeyDown(Keys.Left) || currentKeyboardState.IsKeyDown(Keys.A))
            {
                Direction += new Vector2(-speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) || currentKeyboardState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W) || currentKeyboardState.IsKeyDown(Keys.Space))
            {
                TryJump = true;
            }
        }
       /// <summary>
       /// has the mouse been clicked
       /// </summary>
       /// <returns></returns>
        public bool MouseClicked()
        {
            if (currentMouseState.X > 0 && currentMouseState.Y > 0 && currentMouseState.X < 800 && currentMouseState.Y < 480)
            {
                if (priorMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
