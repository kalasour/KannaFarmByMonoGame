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
        TileMapDraw Layer1,Collition,PlantsLayer, RainLayer;
        Texture2D SourceTexture, PlantsTexture, RainTexture,CollitionTexture;
        ArrOfMap Arr = new ArrOfMap();
        Vector2 CharacterPos = new Vector2(1200, 100);
        String pathWalk = "boyMove";
        String pathActions = "boyAction";
        SpriteAnimations SpriteWalks;
        SpriteAnimations SpriteAction;
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
            CollitionTexture = Content.Load<Texture2D>("Maps/Collition");
            SpriteWalks.isEnable = true;
            MapHeight = 48;
            MapWidth = 85;
            RainTime = new Timer(50);
            RainTime.Elapsed += Rainy;
            RainTime.Enabled = false;
            SourceTexture = Content.Load<Texture2D>("Maps/BG");
            PlantsTexture = Content.Load<Texture2D>("Maps/Plants");
            RainTexture = Content.Load<Texture2D>("Maps/rain");
            PlantsLayer = new TileMapDraw(PlantsTexture, Arr.NullLayer(), MapWidth, MapHeight, ScreenSize);
            RainLayer = new TileMapDraw(RainTexture, Arr.NullLayer(), MapWidth, MapHeight, ScreenSize);
            Collition = new TileMapDraw(CollitionTexture, Arr.Collition(), MapWidth, MapHeight, ScreenSize);
            Layer1 = new TileMapDraw(SourceTexture, Arr.Layer1(), MapWidth, MapHeight, ScreenSize);
        }
        public Vector2 getIndexPos()
        {
            int size = Collition.PixelGet();
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
            Collition.Update(posMap);
            Layer1.Update(posMap);
            PlantsLayer.Update(posMap);
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 1, 6, 5, HaveRain));
                SpriteAction.isEnable = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 17, 7, 5, HaveRain));
                SpriteAction.isEnable = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 33, 6, 5, HaveRain));
                SpriteAction.isEnable = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 49, 7, 5, HaveRain));
                SpriteAction.isEnable = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 65, 7, 5, HaveRain));
                SpriteAction.isEnable = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 81, 7, 5, HaveRain));
                SpriteAction.isEnable = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D7))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 129, 7, 5, HaveRain));
                SpriteAction.isEnable = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D8))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 145, 7, 5, HaveRain));
                SpriteAction.isEnable = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D9))
            {
                if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0) StepUp.Add(new Plants(PlantsLayer, getIndexPos(), 57, 7, 5, HaveRain));
                SpriteAction.isEnable = true;
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
                if (Collition.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 0)
                    CharacterPos.X += speed;


                SpriteWalks.JustRow = 3;
                if (CharacterPos.X < 0) CharacterPos.X += speed;
                SpriteWalks.isEnable = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (CharacterPos.X < ScreenSize.X - ScreenSize.X / 3 || posMap.X < ScreenSize.X - Collition.GetSize().X)
                    CharacterPos.X += speed;
                else
                    posMap.X -= speed;
                if (Collition.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 0)
                    CharacterPos.X -= speed;


                SpriteWalks.JustRow = 1;
                if (CharacterPos.X + SpriteWalks.SpriteWidth > ScreenSize.X) CharacterPos.X -= speed;
                SpriteWalks.isEnable = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (CharacterPos.Y + SpriteWalks.SpriteHeight >= ScreenSize.Y - 16) CharacterPos.Y -= speed;
                if (CharacterPos.Y + SpriteWalks.SpriteHeight < ScreenSize.Y - ScreenSize.Y / 3 || posMap.Y < ScreenSize.Y - Collition.GetSize().Y)
                    CharacterPos.Y += speed;
                else
                    posMap.Y -= speed;
                if (Collition.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 0)
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
                if (Collition.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 0)
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
            Collition.Draw(spriteBatch);
            Layer1.Draw(spriteBatch);
            PlantsLayer.Draw(spriteBatch);
           if(HaveRain) RainLayer.Draw(spriteBatch);
            if (isAction)
            {
                SpriteAction.Draw(spriteBatch, CharacterPos);
            }
            else
                SpriteWalks.Draw(spriteBatch, CharacterPos);

            if (HaveRain) RainLayer.Draw(spriteBatch);
        }


    }
}
