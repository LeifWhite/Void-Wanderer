using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Void_Wanderer
{
    /// <summary>
    /// Tile grid
    /// </summary>
    public class GameMap
    {
        /// <summary>
        /// What block looks like
        /// </summary>
        private Texture2D texture;
        /// <summary>
        /// Map of where blocks are
        /// </summary>
        public string[] TileMap;
        /// <summary>
        /// List of blocks
        /// </summary>
        public List<Block> Blocks;
        /// <summary>
        /// List of coins
        /// </summary>
        public List<Coin> Coins;
        /// <summary>
        /// Where Rho starts
        /// </summary>
        public Vector2 RhoStartingPosition;
        private List<Vector2> possibleCoinLocations;
        /// <summary>
        /// How many coins are there?
        /// </summary>
        public int CoinCount = 10;
        private int room = -1;
        private Color[] colorMap = new Color[]
        {
            Color.White,
            Color.SlateGray,
            Color.Firebrick,
            Color.Sienna,
            Color.ForestGreen
        };
        /// <summary>
        /// Constructor
        /// </summary>
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
            PopulateTileMap();
        }
        /// <summary>
        /// Generates coin and block lists
        /// </summary>
        public void PopulateTileMap()
        {
            for (int i = 0; i < TileMap.Length; i++)
            {
                for (int j = 0; j < TileMap[i].Length; j++)
                {
                    if (TileMap[i][j] == 'G')
                        Blocks.Add(new Block(new Vector2(j * 48, i * 48)));

                }

            }
            var rand = new Random();
            double r;
           
            for (int i = 0; i < possibleCoinLocations.Count; i++)
            {


                r = rand.NextDouble();
                if (r < (float)(CoinCount - Coins.Count) / (possibleCoinLocations.Count - i))
                {
                    Coins.Add(new Coin(possibleCoinLocations[i] * 48 + new Vector2(6, 6), colorMap[Math.Min(room, 4)]));
                }
            }
        }
       /// <summary>
       /// Generates coin and block locations
       /// </summary>
        public void RandomizeTileMap()
        {
            room++;
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
            possibleCoinLocations = new List<Vector2>();
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
            int ran = rand.Next(possibleCoinLocations.Count);
            RhoStartingPosition = possibleCoinLocations[ran] * 48;
            possibleCoinLocations.RemoveAt(ran);
        }
        /// <summary>
        /// Loads content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("colored_packed");
        }
        /// <summary>
        /// Updaates, not needed
        /// </summary>
        /// <param name="gameTime"></param>
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
