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
    public class GamePlaySence
    {
        Plants[,] Arrplants = new Plants[85,48];
        ContentManager Content;
        Vector2 ScreenSize;
        Timer RainTime;
        private double PercentHealth;
        private double Coin;
        int speed = 3;
        bool PlantsUpdated=true;
        bool FirstRain = true;
        bool HaveRain = false;
        bool CanChangeRain = true;
        bool isAction = false;
        private Boolean canPause=true;
        static public Boolean pause = false;
        int MapWidth, MapHeight;
        Texture2D Character;
        TileMapDraw Layer1,Collition,PlantsLayer, RainLayer, LandLayer, LandLayer2;
        Texture2D SourceTexture, PlantsTexture, RainTexture,CollitionTexture, testTexture2D, testTexture2D2,DailBox;
        ArrOfMap Arr = new ArrOfMap();
        Vector2 CharacterPos = new Vector2(1200, 100);
        String pathWalk = "boyMove";
        String pathActions = "boyAction";
        SpriteAnimations SpriteWalks;
        SpriteAnimations SpriteAction;
        Vector2 posMap;
        private SpriteFont Fonts;
        int[] AmountPlants=new int[9];

        public GamePlaySence(ContentManager content, Vector2 screensize)
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
            PercentHealth = 100;
            Coin = 100;
            SourceTexture = Content.Load<Texture2D>("Maps/BG");
            PlantsTexture = Content.Load<Texture2D>("Maps/Plants");
            RainTexture = Content.Load<Texture2D>("Maps/rain");
            testTexture2D = Content.Load<Texture2D>("Maps/Land");
            testTexture2D2= Content.Load<Texture2D>("Maps/BadLand");
            DailBox = Content.Load<Texture2D>("DialogBox");
            Fonts = Content.Load<SpriteFont>("Fonts");
            PlantsLayer = new TileMapDraw(PlantsTexture, Arr.NullLayer(), MapWidth, MapHeight, ScreenSize);
            RainLayer = new TileMapDraw(RainTexture, Arr.NullLayer(), MapWidth, MapHeight, ScreenSize);
            Collition = new TileMapDraw(CollitionTexture, Arr.Collition(), MapWidth, MapHeight, ScreenSize);
            LandLayer = new TileMapDraw(testTexture2D, Arr.Land(), MapWidth, MapHeight, ScreenSize);
            LandLayer2 = new TileMapDraw(testTexture2D2, Arr.Land2(), MapWidth, MapHeight, ScreenSize);
            Layer1 = new TileMapDraw(SourceTexture, Arr.Layer1(), MapWidth, MapHeight, ScreenSize);
            
        }

        Plants ListPlants(int x)
        {
            if(x==1)return new Plants(PlantsLayer, getIndexPos(), 1, 6, 5, HaveRain,10);
            if (x == 2) return new Plants(PlantsLayer, getIndexPos(), 17, 7, 5, HaveRain,10);
            if (x == 3) return new Plants(PlantsLayer, getIndexPos(), 33, 6, 5, HaveRain,10);
            if (x == 4) return new Plants(PlantsLayer, getIndexPos(), 49, 7, 5, HaveRain,10);
            if (x == 5) return new Plants(PlantsLayer, getIndexPos(), 65, 7, 5, HaveRain,10);
            if (x == 6) return new Plants(PlantsLayer, getIndexPos(), 81, 7, 5, HaveRain,10);
            if (x == 7) return new Plants(PlantsLayer, getIndexPos(), 129, 7, 5, HaveRain,10);
            if (x == 8) return new Plants(PlantsLayer, getIndexPos(), 145, 7, 5, HaveRain,10);
            if (x == 9) return new Plants(PlantsLayer, getIndexPos(), 57, 7, 5, HaveRain,10);
            return null;
        }
        public Vector2 getIndexPos()
        {
            int size = Collition.PixelGet();
            return new Vector2((int)((CharacterPos.X - posMap.X + SpriteWalks.SpriteWidth / 2) / size), (int)((CharacterPos.Y - posMap.Y + SpriteWalks.SpriteHeight) / size));
        }

        void GrowPlants(int a)
        {
            
            if (PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0 &&
                LandLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] != 0 && AmountPlants[a-1] != 0 && LandLayer2.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0)
            {
                Arrplants[(int)getIndexPos().X, (int)getIndexPos().Y] = ListPlants(a);
                AmountPlants[a-1]--;
                LandLayer2.intID[(int)getIndexPos().X, (int)getIndexPos().Y] = 1;
                PercentHealth -= 0.5;
            }
           
        }

        public void Update(GameTime gameTime)
        {
            if (pause) Game1.GameSence = 3;
            else Game1.GameSence = 2;
            if (!pause)
            {
                for (int i = 0; i < 48; i++)
                {
                    for (int j = 0; j < 85; j++)
                    {
                        if (Arrplants[j, i] != null)
                        {
                            if(Arrplants[j, i].timer!=null)
                            Arrplants[j, i].timer.Enabled = true;
                        }
                    }

                }
            }
            if (canPause && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                canPause = false;
                pause = !pause;
                for (int i = 0; i < 48; i++)
                {
                    for (int j = 0; j < 85; j++)
                    {
                        if (Arrplants[j, i] != null)
                        {
                            if (Arrplants[j, i].timer != null)
                                Arrplants[j, i].timer.Enabled=false;
                        }
                    }

                }
            }
            if (!canPause && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                canPause = true;
            }
            if (pause)return;
            if(Keyboard.GetState().IsKeyDown(Keys.Z)&&CanChangeRain)
            {
                AmountPlants[0] = 10;
                HaveRain = !HaveRain;
                CanChangeRain = false;
            }
            if(Keyboard.GetState().IsKeyUp(Keys.Z))
            {
                CanChangeRain = true;
            }
            if (HaveRain && PlantsUpdated)
            {
                for (int i = 0; i < 48; i++)
                {
                    for (int j = 0; j < 85; j++)
                    {
                          if (Arrplants[j,i]!=null)
                        {
                            if (Arrplants[j, i].timer != null)
                                Arrplants[j,i].Update(HaveRain);
                        }
                    }
                    
                }
                PlantsUpdated = false;
            }
            else if(!HaveRain&&!PlantsUpdated)
            {
                PlantsUpdated = true;
                for (int i = 0; i < 48; i++)
                {
                    for (int j = 0; j < 85; j++)
                    {
                        if (Arrplants[j, i] != null)
                        {
                            if (Arrplants[j, i].timer != null)
                                Arrplants[j, i].Update(HaveRain);
                        }
                    }

                }
            }

            SpriteWalks.Update(gameTime);
            SpriteAction.Update(gameTime);
            RainLayer.Update(gameTime,posMap);
            Collition.Update(gameTime,posMap);
            Layer1.Update(gameTime,posMap);
            LandLayer.Update(gameTime,posMap);
            LandLayer2.Update(gameTime, posMap);
            PlantsLayer.Update(gameTime,posMap);
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                GrowPlants(1);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                GrowPlants(2);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                GrowPlants(3);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                GrowPlants(4);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                GrowPlants(5);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                GrowPlants(6);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D7))
            {
                GrowPlants(7);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D8))
            {
                GrowPlants(8);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D9))
            {
                GrowPlants(9);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E) && PlantsLayer.CanGet[(int)getIndexPos().X, (int)getIndexPos().Y])
            {
                Arrplants[(int) getIndexPos().X, (int) getIndexPos().Y] = ListPlants(0);

                PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] = 0;
                PlantsLayer.CanGet[(int)getIndexPos().X, (int)getIndexPos().Y]=false;
                PercentHealth -= 0.5;

            }

            if (Keyboard.GetState().IsKeyDown(Keys.R) && LandLayer2.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 1 && PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] == 0)
            {
                LandLayer2.intID[(int)getIndexPos().X, (int)getIndexPos().Y] = 0;
                SpriteAction.isEnable = true;
                PercentHealth -= 1;
                isAction = true;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.R))
            {
                isAction = false;
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
                if(!pause)
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
            LandLayer.Draw(spriteBatch);
            LandLayer2.Draw(spriteBatch);
            PlantsLayer.Draw(spriteBatch);
           if(HaveRain) RainLayer.Draw(spriteBatch);
            if (isAction)
            {
                SpriteAction.Draw(spriteBatch, CharacterPos);
            }
            else
                SpriteWalks.Draw(spriteBatch, CharacterPos);
            if (HaveRain) RainLayer.Draw(spriteBatch);

            for (int i = 0; i < 9; i++)
            {
                spriteBatch.Draw(DailBox,new Rectangle(300+83*i,600,100,100),Color.White);
                spriteBatch.DrawString(Fonts, (i + 1).ToString(), new Vector2(325 + 83 * i, 620), Color.Black);
                spriteBatch.DrawString(Fonts, AmountPlants[i].ToString(), new Vector2(370 + 83 * i, 660), Color.Black);
            }
            spriteBatch.DrawString(Fonts, PercentHealth.ToString() + " %", new Vector2(1300, 50), Color.Black);
            spriteBatch.DrawString(Fonts, Coin.ToString() + " $", new Vector2(1300, 70), Color.Black);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320, 615, 60, 60), new Rectangle(((6 - 1) % (int)16) * 16, ((6 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320+83, 605, 60, 60), new Rectangle(((23 - 1) % (int)16) * 16, ((23 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83*2, 615, 60, 60), new Rectangle(((38 - 1) % (int)16) * 16, ((38 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 3, 615, 60, 60), new Rectangle(((55 - 1) % (int)16) * 16, ((55 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 4, 615, 60, 60), new Rectangle(((71 - 1) % (int)16) * 16, ((71 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 5, 615, 60, 60), new Rectangle(((87 - 1) % (int)16) * 16, ((87 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 6, 615, 60, 60), new Rectangle(((135 - 1) % (int)16) * 16, ((135 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 7, 610, 60, 60), new Rectangle(((151 - 1) % (int)16) * 16, ((151 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 8, 615, 60, 60), new Rectangle(((63 - 1) % (int)16) * 16, ((63 - 1) / 16) * 16, 16, 16), Color.White);

        }


    }
}
