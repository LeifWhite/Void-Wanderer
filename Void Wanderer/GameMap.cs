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
        public List<Coin> Coins;
        private const int COINCOUNT = 10;
        public GameMap()
        {
            Blocks = new List<Block>();
            Coins = new List<Coin>();
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
            RandomizeTileMap();
            for (int i = 0; i < TileMap.Length; i++)
            {
                for (int j = 0; j < TileMap[i].Length; j++)
                {
                    if (TileMap[i][j] == 'G')
                        Blocks.Add(new Block(new Vector2(j * 48, i * 48)));
                       
                }

            }
        }
        public void RandomizeTileMap()
        {
            var rand = new Random();
            TileMap = new string[]
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
            double r;
            for (int i = 0; i < TileMap.Length; i++)
            {
                for (int j = 0; j < TileMap[i].Length; j++)
                {
                    if (TileMap[i][j] == 'A')
                    {
                        r = rand.NextDouble();
                        
                        if (r > 0.85)
                        {
                            StringBuilder sb = new StringBuilder(TileMap[i]);
                            sb[j] = 'G';
                            TileMap[i] = sb.ToString();

                        }
                    }
                }
            }
            for (int k = 0; k < 2; k++)
            {
                for (int i = 2; i < TileMap.Length - 2; i++)
                {
                    for (int j = 2; j < TileMap[i].Length - 2; j++)
                    {
                        if (TileMap[i][j] == 'A')
                        {
                            int countNear = 0;
                            if (TileMap[i - 1][j] == 'G')
                            {
                                countNear++;
                            }
                            if (TileMap[i + 1][j] == 'G')
                            {
                                countNear++;
                            }
                            if (TileMap[i][j - 1] == 'G')
                            {
                                countNear++;
                            }
                            if (TileMap[i][j + 1] == 'G')
                            {
                                countNear++;
                            }
                            r = rand.NextDouble();

                            if (r > 0.95 - countNear / 4)
                            {
                                StringBuilder sb = new StringBuilder(TileMap[i]);
                                sb[j] = 'G';
                                TileMap[i] = sb.ToString();

                            }
                        }
                    }
                }
            }
            List<Vector2> possibleCoinLocations = new List<Vector2>();
            for (int i = 2; i < TileMap.Length; i++)
            {
                for (int j = 1; j < TileMap[i].Length-1; j++)
                {
                    if (TileMap[i][j] == 'G' && TileMap[i-1][j] == 'A')
                    {
                        possibleCoinLocations.Add(new Vector2(j, i-1));
                    }

                 }
            }

            for(int i = 0; i < possibleCoinLocations.Count; i++)
            {

                r = rand.NextDouble();
                if (r < (float)(COINCOUNT - Coins.Count) / (possibleCoinLocations.Count - i))
                {
                    Coins.Add(new Coin(possibleCoinLocations[i]*48+new Vector2(6,6)));
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
            for (int i = 0; i < Coins.Count; i++)
            {
                Coins[i].Draw(gameTime, spriteBatch, texture);
            }
        }
    }
}
