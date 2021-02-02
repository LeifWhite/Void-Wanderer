using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Void_Wanderer
{
    public class GameMap
    {
        private Texture2D texture;
        private string[] tileMap;
        public List<Block> Blocks;
        public GameMap()
        {
            Blocks = new List<Block>();
            tileMap = new string[]
            {
                "GGGGGGGGGGGGGGGGG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAG",
                "GGGGGGGGGGGGGGGGG"
            };
            for (int i = 0; i < tileMap.Length; i++)
            {
                for (int j = 0; j < tileMap[i].Length; j++)
                {
                    if (tileMap[i][j] == 'G')
                        Blocks.Add(new Block(new Vector2(j * 48, i * 48)));
                       
                }

            }
        }
        /// <summary>
        /// where find
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("colored_packed");
        }
        public void Update(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// how look
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
           
            for(int i = 0; i<Blocks.Count; i++)
            {
                Blocks[i].Draw(gameTime, spriteBatch, texture);
            }
            
        }
    }
}
