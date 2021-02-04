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
        public string[] TileMap;
        public List<Block> Blocks;
        public GameMap()
        {
            Blocks = new List<Block>();
            TileMap = new string[]
            {
                "GGGGGGGGGGGGGGGGG",
                "GAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAGAAAG",
                "GAAAAGGAAAAAGAAAG",
                "GAAAAAAAAAAGGGAAG",
                "GAAAAAAGAAAAAAAAG",
                "GAAAAAGGGAAAAAAAG",
                "GAAAAAAAAAAGAAAAG",
                "GAAAAAAAAAGGGAAAG",
                "GGGGGGGGGGGGGGGGG"
            };
            for (int i = 0; i < TileMap.Length; i++)
            {
                for (int j = 0; j < TileMap[i].Length; j++)
                {
                    if (TileMap[i][j] == 'G')
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
