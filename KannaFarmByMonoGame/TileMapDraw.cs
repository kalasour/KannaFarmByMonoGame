using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

namespace KannaFarmByMonoGame
{
    class TileMapDraw
    {
        Texture2D Map;
        Vector2 PositionMap;
        string File;
        int mapWidth,TileWidth;
        int PixelMap = 32;
        int PixelTexture = 16;
        int mapHeight,TileHeight;
        int tileCount;
        string IDArray;
        string[] SplitArray;
        public int[,] intID;
        public bool[,] CanGet;
        Vector2 mapSize;
        Vector2[] SourcePos;
        Vector2 ScreenSize;
        Vector2 TileSize;
        public TileMapDraw(Texture2D texture, string file,int mapw,int maph, Vector2 screenSize)
        {
            Map = texture;
            mapWidth = mapw;
            mapHeight = maph;
            mapSize = new Vector2(mapWidth * PixelMap,mapHeight * PixelMap);
            File = file;
            ScreenSize=screenSize;
            tileCount = mapHeight * mapWidth;
            TileSize = new Vector2(Map.Width / PixelTexture, Map.Height / PixelTexture);
            LoadContent();
            PositionMap = new Vector2(0,0);
        }
        public Vector2 GetMapSize()
        {
            return new Vector2(mapWidth,mapHeight);
        }
        public int PixelGet()
        {
            return PixelMap;
        }
        public Vector2 GetSize()
        {
            return mapSize;
        }
        public void LoadContent()
        {
            SplitArray = File.Split(',');
            intID = new int[mapWidth, mapHeight];
            CanGet=new bool[mapWidth, mapHeight];
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    intID[i, j] = int.Parse(SplitArray[i+j*mapWidth]);
                    CanGet[i, j] = false;
                }
            }
        }
        public void Update(Vector2 inputPos)
        {
            PositionMap = inputPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    spriteBatch.Draw(Map,new Rectangle(i*PixelMap+(int)PositionMap.X,j*PixelMap+(int)PositionMap.Y, PixelMap,PixelMap),new Rectangle(((intID[i,j]-1)%(int)TileSize.X)*16, ((intID[i, j]-1)/ (int)TileSize.X) *PixelTexture, PixelTexture, PixelTexture), Color.White);
                }
            }
            
        }
    }
}
