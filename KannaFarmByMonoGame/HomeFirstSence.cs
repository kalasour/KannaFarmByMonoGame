using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework.Input;

namespace KannaFarmByMonoGame
{
    class HomeFirstSence
    {
        List<Plants> StepUp = new List<Plants>();
        ContentManager Content;
        Vector2 ScreenSize;
        Timer RainTime;
        int speed = 3;
        bool PlantsUpdated=true;
        bool FirstRain = true;
        bool HaveRain = false;
        bool CanChangeRain = true;
        bool isAction = false;
        int MapWidth, MapHeight;
        Texture2D Character;
        TileMapDraw Home, Layer, PlantsLayer, RainLayer;
        Texture2D SourceTexture, PlantsTexture, RainTexture;
        ArrOfMap Arr = new ArrOfMap();
        Vector2 CharacterPos = new Vector2(100, 100);
        String pathWalk = "boyMove";
        String pathActions = "boyAction";
        SpriteAnimations SpriteWalks;
        SpriteAnimations SpriteAction;
        string HomeArr;
        Vector2 posMap;

        public HomeFirstSence(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }
        public void LoadContent()
        {
            posMap = new Vector2(0, 0);
            Character = Content.Load<Texture2D>(pathWalk);
            SpriteWalks = new SpriteAnimations(Content, pathWalk, 4, 4, 1, 4);
            SpriteAction = new SpriteAnimations(Content, pathActions, 1, 5, 0, 5);
            SpriteWalks.isEnable = true;
            HomeArr = Arr.firstSence();
            MapHeight = 48;
            MapWidth = 85;
            RainTime = new Timer(100);
            RainTime.Elapsed += Rainy;
            RainTime.Enabled = false;
            SourceTexture = Content.Load<Texture2D>("Maps/Test");
            PlantsTexture = Content.Load<Texture2D>("Maps/Plants");
            RainTexture = Content.Load<Texture2D>("Maps/rain");
            PlantsLayer = new TileMapDraw(PlantsTexture, Arr.NullLayer(), MapWidth, MapHeight, ScreenSize);
            RainLayer = new TileMapDraw(RainTexture, Arr.NullLayer(), MapWidth, MapHeight, ScreenSize);
            Home = new TileMapDraw(SourceTexture, HomeArr, MapWidth, MapHeight, ScreenSize);
            Layer = new TileMapDraw(SourceTexture, Arr.LayerBackground(), MapWidth, MapHeight, ScreenSize);
        }
        public Vector2 getIndexPos()
        {
            int size = Home.PixelGet();
            return new Vector2((int)((CharacterPos.X - posMap.X + SpriteWalks.SpriteWidth / 2) / size), (int)((CharacterPos.Y - posMap.Y + SpriteWalks.SpriteHeight) / size));
        }

        public void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.R)&&CanChangeRain)
            {
                if (HaveRain) HaveRain = false;
                else HaveRain = true;
                CanChangeRain = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.R))
            {
                CanChangeRain = true;
            }
            if (HaveRain && PlantsUpdated)
            {
                for (int z = 0; z < StepUp.Count; z++)
                {
                    if (!StepUp[z].timer.Enabled)
                    {
                        StepUp.Remove(StepUp[z]);
                        z--;
                    }
                    else
                    {
                        StepUp[z].Update(HaveRain);
                    }
                }
                PlantsUpdated = false;
            }
            else if(!HaveRain&&!PlantsUpdated)
            {
                PlantsUpdated = true;
                for (int z = 0; z < StepUp.Count; z++)
                {
                    if (!StepUp[z].timer.Enabled)
                    {
                        StepUp.Remove(StepUp[z]);
                        z--;
                    }
                    else
                    {
                        StepUp[z].Update(HaveRain);
                    }
                }
            }

            SpriteWalks.Update(gameTime);
            SpriteAction.Update(gameTime);
            RainLayer.Update(posMap);
            Home.Update(posMap);
            Layer.Update(posMap);
            PlantsLayer.Update(posMap);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 1, 6, 5,HaveRain));
                isAction = true;
                SpriteAction.isEnable = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.A))
            {
                isAction = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E) && PlantsLayer.CanGet[(int)getIndexPos().X, (int)getIndexPos().Y])
            {
                PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (CharacterPos.X > ScreenSize.X / 3 || posMap.X >= 0)
                    CharacterPos.X -= speed;
                else
                    posMap.X += speed;
                if (Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 176 && Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 1953)
                    CharacterPos.X += speed;


                SpriteWalks.JustRow = 3;
                if (CharacterPos.X < 0) CharacterPos.X += speed;
                SpriteWalks.isEnable = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (CharacterPos.X < ScreenSize.X - ScreenSize.X / 3 || posMap.X < ScreenSize.X - Home.GetSize().X)
                    CharacterPos.X += speed;
                else
                    posMap.X -= speed;
                if (Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 176 && Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 1953)
                    CharacterPos.X -= speed;


                SpriteWalks.JustRow = 1;
                if (CharacterPos.X + SpriteWalks.SpriteWidth > ScreenSize.X) CharacterPos.X -= speed;
                SpriteWalks.isEnable = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (CharacterPos.Y + SpriteWalks.SpriteHeight >= ScreenSize.Y - 16) CharacterPos.Y -= speed;
                if (CharacterPos.Y + SpriteWalks.SpriteHeight < ScreenSize.Y - ScreenSize.Y / 3 || posMap.Y < ScreenSize.Y - Home.GetSize().Y)
                    CharacterPos.Y += speed;
                else
                    posMap.Y -= speed;
                if (Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 176 && Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 1953)
                    CharacterPos.Y -= speed;

                SpriteWalks.JustRow = 0;

                SpriteWalks.isEnable = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (CharacterPos.Y > ScreenSize.Y / 3 || posMap.Y >= 0)
                    CharacterPos.Y -= speed;
                else
                    posMap.Y += speed;
                if (Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 176 && Home.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 1953)
                    CharacterPos.Y += speed;


                SpriteWalks.JustRow = 2;
                if (CharacterPos.Y + (SpriteWalks.SpriteHeight / 2) < 0) CharacterPos.Y += speed;
                SpriteWalks.isEnable = true;
            }
            else
            {
                SpriteWalks.isEnable = false;
            }
            RainTime.Enabled = HaveRain;
        }
        public void Rainy(Object source, ElapsedEventArgs e)
        {
            if (FirstRain)
            {
                Random Rn = new Random();
                for (int i = 0; i < RainLayer.GetMapSize().X; i++)
                {
                    for (int j = 0; j < RainLayer.GetMapSize().Y; j++)
                    {
                        RainLayer.intID[i, j] = Rn.Next(5);
                    }
                }
                FirstRain = false;
            }
            else
            {
                for (int i = 0; i < RainLayer.GetMapSize().X; i++)
                {
                    for (int j = 0; j < RainLayer.GetMapSize().Y; j++)
                    {
                        RainLayer.intID[i, j]++;
                        RainLayer.intID[i, j] %= 5;
                    }
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Layer.Draw(spriteBatch);
            Home.Draw(spriteBatch);
            PlantsLayer.Draw(spriteBatch);
           if(HaveRain) RainLayer.Draw(spriteBatch);
            if (isAction)
            {
                SpriteAction.Draw(spriteBatch, CharacterPos);
            }
            else
                SpriteWalks.Draw(spriteBatch, CharacterPos);
        }


    }
}
