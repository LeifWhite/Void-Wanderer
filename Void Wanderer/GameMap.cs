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
        /// What coin looks like
        /// </summary>
        private Texture2D texture;
        /// <summary>
        /// What dirt look like
        /// </summary>
        private Texture2D dirt;
        private Texture2D ngDecor;
        private Texture2D oDecor;
        /// <summary>
        /// Map of where blocks are
        /// </summary>
        public string[] TileMap;
        /// <summary>
        /// List of blocks
        /// </summary>
        public List<Block> Blocks;
        private Dictionary<Vector2, Block> blockXYdictionary = new Dictionary<Vector2, Block>();
        /// <summary>
        /// List of coins
        /// </summary>
        public List<Coin> Coins;
        /// <summary>
        /// Where Rho starts
        /// </summary>
        public Vector2 RhoStartingPosition;
        private List<Vector2> possibleCoinLocations;
        private List<Vector2> possibleDecorLocations;
        /// <summary>
        /// How many coins are there?
        /// </summary>
        public int CoinCount = 10;
        /// <summary>
        /// What background are we on
        /// </summary>
        public int Bnum = 0;

        private int room = -1;
        private Color[] colorMap = new Color[]
        {
            Color.White,
            Color.SlateGray,
            Color.Firebrick,
            Color.Sienna,
            Color.ForestGreen
        };
        public Color[] ColorMap2 = new Color[]
        {
            Color.OrangeRed,
            Color.LightGreen,
            Color.BlueViolet
        };
        private Texture2D[] decorMap = new Texture2D[3];
        private List<Rectangle>[] rectangleMap= new List<Rectangle>[]
        {
            //ginesha textures
            new List<Rectangle> {new Rectangle(138, 9, 176-138, 47-9), new Rectangle(128, 49, 158 - 128, 88 - 49), new Rectangle(161, 49, 190 - 161, 88 - 49) },
            //new gloucester textures
            new List<Rectangle> {new Rectangle(6, 6, 22-6, 65-6), new Rectangle(25, 33, 50 - 25, 66 - 33), new Rectangle(56, 33, 56 - 25, 66 - 33), new Rectangle(6, 70, 20 - 6, 91 - 70) },
            //onyet textures
            new List<Rectangle> {new Rectangle(6, 7, 29, 50), new Rectangle(107, 9, 29, 50),  new Rectangle(42, 8, 29, 50), new Rectangle(75, 8, 29, 50), new Rectangle(42, 8, 29, 50), new Rectangle(107, 9, 29, 50) },


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
                "GGGGGGGGGGGGGGGGGGG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GAAAAAAAAAAAAAAAAAG",
                "GGGGGGGGGGGGGGGGGGG"
            };
            
            RandomizeTileMap();
            
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
                    bool grass = false;
                    if (i >= 1 && TileMap[i - 1][j] == 'A')
                    {
                        grass = true;
                    }
                    if (TileMap[i][j] == 'G')
                    {
                        Block addBlock = new Block(new Vector2(j * 48, i * 48) * (Screen.SIZE / 800f) * Screen.GS, ColorMap2[Bnum], grass);
                        blockXYdictionary[new Vector2(j, i)] = addBlock;
                        Blocks.Add(addBlock);

                    }

                }

            }
            var rand = new Random();
            double r;

            List<Vector2> coinLocations = new List<Vector2>();
            for (int i = 0; i < possibleCoinLocations.Count; i++)
            {


                r = rand.NextDouble();
                if (r < (float)(CoinCount - Coins.Count) / (possibleCoinLocations.Count - i))
                {
                    Coins.Add(new Coin(((possibleCoinLocations[i]) *48 + new Vector2(6 , 6)) * (Screen.SIZE / 800f) * Screen.GS, colorMap[Math.Min(room, 4)]));
                    coinLocations.Add(possibleCoinLocations[i]);
                }

            }
            for (int i = 0; i < possibleDecorLocations.Count; i++)
            {
                bool eq = false;

                for (int j = 0; j < coinLocations.Count; j++)
                {
                    if (possibleDecorLocations[i].Equals(coinLocations[j]))
                    {
                        eq = true;
                        break;
                    }

                }
                if (eq)
                {
                    continue;
                }
                r = rand.NextDouble();
                if (r > 0.85)
                {
                    Vector2 blockLocation = new Vector2(possibleDecorLocations[i].X, possibleDecorLocations[i].Y + 1);
                    blockXYdictionary[blockLocation].HasDecor = true;
                    blockXYdictionary[blockLocation].Decor = decorMap[Bnum];
                    if(Bnum !=2)
                        blockXYdictionary[blockLocation].DecorRect = new List<Rectangle> { rectangleMap[Bnum][rand.Next(rectangleMap[Bnum].Count)] };
                    else
                    {
                        blockXYdictionary[blockLocation].DecorRect = rectangleMap[Bnum];
                    }

                    blockXYdictionary[blockLocation].DecorRectFrame = rand.Next(blockXYdictionary[blockLocation].DecorRect.Count-1);
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
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
                "AAAAAAAAAAAAAAAAA",
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
            possibleDecorLocations = new List<Vector2>();
            for (int i = 2; i < TileMap.Length; i++)
            {
                for (int j = 1; j < TileMap[i].Length-1; j++)
                {
                    if (TileMap[i][j] == 'G' && TileMap[i-1][j] == 'A')
                    {
                        possibleCoinLocations.Add(new Vector2(j, i-1));
                    }
                    if(i>=4 && TileMap[i][j] == 'G' && TileMap[i - 1][j] == 'A' && TileMap[i - 2][j] == 'A' && TileMap[i - 3][j] == 'A')
                    {
                        possibleDecorLocations.Add(new Vector2(j, i - 1));
                    }
                 }
            }
            int ran = rand.Next(possibleCoinLocations.Count);
            RhoStartingPosition = (possibleCoinLocations[ran]) *48* (Screen.SIZE / 800f)*Screen.GS;
            possibleCoinLocations.RemoveAt(ran);
            
        }
        /// <summary>
        /// Loads content
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("colored_packed");
            dirt = content.Load<Texture2D>("VW Dirt");
            ngDecor = content.Load<Texture2D>("New Gloucester Decorations");
            oDecor = content.Load<Texture2D>("Onyet Decorations");
            decorMap[0] = dirt;
            decorMap[1] = ngDecor;
            decorMap[2] = oDecor;
            PopulateTileMap();
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
                Blocks[i].Draw(gameTime, spriteBatch, dirt);
            }
            for (int i = 0; i < Coins.Count; i++)
            {
                Coins[i].Draw(gameTime, spriteBatch, texture);
            }
        }
    }
}
