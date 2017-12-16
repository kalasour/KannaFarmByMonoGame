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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace KannaFarmByMonoGame
{
    public class GamePlaySence
    {
        
        private Boolean page;
        private SoundEffect RainSound,DoorSound,GetCoin,Heart;
        public static Boolean Reset;
        private Boolean RedA;
        private int LiveDown;
        private int time,time2;
        private Plants[,] Arrplants;
        ContentManager Content;
        Vector2 ScreenSize;
        Timer RainTime;
        public static double PercentHealth;
        private double Coin;
        int speed = 3;
        private bool PlantsUpdated;
        private bool FirstRain;
        private bool HaveRain;
        private bool CanChangeRain;
        private bool isAction;
        private Boolean isStarter;
        private Boolean CanTab;
        private int Secconds, Minutes, Hours;
        private Boolean canPause;
        static public Boolean pause;
        int MapWidth, MapHeight;
        private float colorLerp,colorLerp2;
        Texture2D Character;
        TileMapDraw Layer1,Collition,PlantsLayer, RainLayer, LandLayer, LandLayer2;
        Texture2D SourceTexture, PlantsTexture, RainTexture,CollitionTexture, testTexture2D, testTexture2D2,DailBox,Status,Store,Starter,Help;
        private ArrOfMap Arr;
        private Vector2 CharacterPos;
        String pathWalk = "boyMove";
        String pathActions = "boyAction";
        private SoundEffectInstance FuckingSound,RainSoundIn,No;
        SpriteAnimations SpriteWalks;
        SpriteAnimations SpriteAction;
        Vector2 posMap;
        private int clock;
        private Boolean StoreCheck, StoreStand,Played;
        private SpriteFont Fonts,FontsStatus;
        private int[] AmountPlants;
        private Boolean ShowMsgTodie;
        private Boolean CanOffStore;
        private int[] priceSeeds,PricePlants;
        private Texture2D BtnBlank,FoodPage;
        private int PerCentRain;
        private SpriteButton[] BtnBuy,BtnFood;
        private Texture2D CoinTexture, HearthTexture;
        private Boolean CoinShow, HearthShow;
        private int CoinDelay, HearthDelay;
        private SpriteFont ClockFonts;
        private int RainTimeCount,CountDownRain;
        private int LenghtRain;
        private Boolean RainIsComings;
        private Boolean PressH, isHelp;
        public GamePlaySence(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }
        public void LoadContent()
        {
            isStarter = true;
            isHelp = false;
            PressH = false;
            BtnFood=new SpriteButton[3];
            CanTab = true;
            Played = true;
            RainSound = Content.Load<SoundEffect>("Rain");
            DoorSound = Content.Load<SoundEffect>("Door");
            GetCoin = Content.Load<SoundEffect>("GetCoin");
            Heart = Content.Load<SoundEffect>("Heart");
            Starter = Content.Load<Texture2D>("Starter");
            Help = Content.Load<Texture2D>("HowToPlay");
            FuckingSound = Heart.CreateInstance();
            FuckingSound.IsLooped = true;
            No = Content.Load<SoundEffect>("No").CreateInstance();
            RainSoundIn = RainSound.CreateInstance();
            RainSoundIn.IsLooped = true;
            RainIsComings = false;
            CountDownRain = 0;
            page = true;
            RainTimeCount = 0;
            PerCentRain = 100;
            clock = 0;
            Secconds = 0;
            Minutes = 0;
            Hours = 0;
            CanOffStore = false;
            LenghtRain = 0;
            CoinShow = false;
            CoinDelay = 0;
            HearthShow = false;
            HearthDelay = 0;
            StoreCheck = false;
            StoreStand = true;
            priceSeeds = new int[9];
            priceSeeds[0] = 40;
            priceSeeds[1] = 100;
            priceSeeds[2] = 50;
            priceSeeds[3] = 40;
            priceSeeds[4] = 150;
            priceSeeds[5] = 200;
            priceSeeds[6] = 120;
            priceSeeds[7] = 100;
            priceSeeds[8] = 50;
            PricePlants=new int[9];
            PricePlants[0] = 70;
            PricePlants[1] = 125;
            PricePlants[2] = 80;
            PricePlants[3] =60;
            PricePlants[4] =170 ;
            PricePlants[5] = 250;
            PricePlants[6] = 150;
            PricePlants[7] = 130;
            PricePlants[8] = 100;
            CoinTexture = Content.Load<Texture2D>("Coin");
            HearthTexture = Content.Load<Texture2D>("Hearth");
            ClockFonts = Content.Load<SpriteFont>("ClockFonts");
            FoodPage = Content.Load<Texture2D>("Food");
            Arr = new ArrOfMap();
            AmountPlants = new int[9];
            time = 0;
            Store = Content.Load<Texture2D>("Store");
            BtnBlank = Content.Load<Texture2D>("BtnShop");
            BtnBuy=new SpriteButton[9];
            BtnBuy[0] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(500, 275), ScreenSize, 0,Content);
            BtnBuy[1] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(500, 360), ScreenSize, 0, Content);
            BtnBuy[2] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(500, 450), ScreenSize, 0, Content);
            BtnBuy[3] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(740, 275), ScreenSize, 0, Content);
            BtnBuy[4] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(740, 360), ScreenSize, 0, Content);
            BtnBuy[5] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(740, 450), ScreenSize, 0, Content);
            BtnBuy[6] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(980, 275), ScreenSize, 0, Content);
            BtnBuy[7] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(980, 350), ScreenSize, 0, Content);
            BtnBuy[8] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(980, 450), ScreenSize, 0, Content);
            Reset = false;
            BtnFood[0] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(490, 450), ScreenSize, 1, Content);
            BtnFood[1] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(730, 450), ScreenSize, 2, Content);
            BtnFood[2] = new SpriteButton(BtnBlank, new Vector2(60, 30), new Vector2(970, 450), ScreenSize, 3, Content);
            time2 = 0;
            RedA = false;
            LiveDown = 15000;
            colorLerp = 0;
            colorLerp2 = 0;
            canPause = true;
            PlantsUpdated = true;
            Arrplants = new Plants[85, 48];
            CharacterPos = new Vector2(1000, 100);
            posMap = new Vector2(-1300, 0);
            FirstRain = true;
            Character = Content.Load<Texture2D>(pathWalk);
            SpriteWalks = new SpriteAnimations(Content, pathWalk, 4, 4, 1, 4);
            SpriteAction = new SpriteAnimations(Content, pathActions, 1, 5, 0, 5);
            CollitionTexture = Content.Load<Texture2D>("Maps/Collition");
            SpriteWalks.isEnable = true;
            HaveRain = false;
            pause = false;
            isAction = false;
            CanChangeRain = true;
            MapHeight = 48;
            MapWidth = 85;
            RainTime = new Timer(50);
            RainTime.Elapsed += Rainy;
            RainTime.Enabled = false;
            ShowMsgTodie = false;
            PercentHealth = 100;
            Coin = 100;
            Status = Content.Load<Texture2D>("status");
            SourceTexture = Content.Load<Texture2D>("Maps/BG");
            PlantsTexture = Content.Load<Texture2D>("Maps/Plants");
            RainTexture = Content.Load<Texture2D>("Maps/rain");
            testTexture2D = Content.Load<Texture2D>("Maps/Land");
            testTexture2D2= Content.Load<Texture2D>("Maps/BadLand");
            DailBox = Content.Load<Texture2D>("DialogBox");
            Fonts = Content.Load<SpriteFont>("Fonts");
            FontsStatus = Content.Load<SpriteFont>("FontsStatus");
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

        public void RedAlert(GameTime gameTime)
        {
            if (TileMapDraw.MyColor == Color.Red)
            {
                RedA = false;
                colorLerp2 = 0;
            }
            if (TileMapDraw.MyColor == Color.White)
            {
                RedA = true;
                colorLerp2 = 0;
            }
            time2 += gameTime.ElapsedGameTime.Milliseconds;
            if (time2 >= 50)
            {
                time2 = 0;
                colorLerp2=colorLerp2+(float)0.1;
            }
            if(RedA)TileMapDraw.MyColor = Color.Lerp(Color.White, Color.Red, colorLerp2);
            else TileMapDraw.MyColor = Color.Lerp(Color.Red, Color.White, colorLerp2);
        }

        public void Die(GameTime gameTime)
        {
            FuckingSound.Stop();
            if (!Played)
            {
                No.Play();
                Played = true;
            }
            time += gameTime.ElapsedGameTime.Milliseconds;
            if (time >= 200)
            {
                time = 0;
                colorLerp = colorLerp + (float)0.1;
            }
            TileMapDraw.MyColor = Color.Lerp(Color.White, Color.Black, colorLerp);
            if (TileMapDraw.MyColor == Color.Black)
            {
                ShowMsgTodie = true;
            }
        }

        public void RainIsComing(GameTime gameTime)
        {
            RainTimeCount += gameTime.ElapsedGameTime.Milliseconds;
            if (!HaveRain)
            {
                if (RainTimeCount >= 1000)
            {
                CountDownRain++;
                RainTimeCount = 0;
            }
            }
            
            if (CountDownRain >= 10)
            {
                RainSoundIn.Play();

                CountDownRain = 0;
                HaveRain=true;
            }
            
        }
        public void Update(GameTime gameTime)
        {
            if (isStarter)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    isStarter = false;
                    AmountPlants[0] = 9;
                    isHelp = true;
                }
            }
            if (PercentHealth >= 100) PercentHealth = 100;
            if (Reset)
            {
                LoadContent();

            }
            if (ShowMsgTodie && Keyboard.GetState().IsKeyDown(Keys.Space) ||
                Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Game1.GameSence = 4;
            }
            if (PercentHealth <= 0)
            {
                Die(gameTime);
                return;
            }
            if (PercentHealth <= 5)
            {
                if (Played)
                {
                    FuckingSound.Play();
                    Played = false;
                }
                RedAlert(gameTime);
            }
            if (PercentHealth > 5)
            {
                Played = true;
                FuckingSound.Stop();
                Heart.CreateInstance().Pause();
            }
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
                Random fuck = new Random();

                PerCentRain = fuck.Next(1, 100);
                if (PerCentRain <= 40 && !HaveRain)
                {
                    RainIsComings = true;
                    LenghtRain = PerCentRain;
                    PerCentRain = 100;
                }
                CanChangeRain = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.H) && PressH)
            {
                PressH = false;
                isHelp = !isHelp;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.H))
            {
                PressH = true;
            }
            if (HaveRain)
            {
                RainTimeCount += gameTime.ElapsedGameTime.Milliseconds;
                RainIsComings = false;
                if (RainTimeCount >= 1000)
                {
                    RainTimeCount = 0;
                    LenghtRain--;
                    RainTimeCount = 0;

                }
            }
            if (LenghtRain == 0)
            {
                HaveRain = false;
                RainSoundIn.Stop();
                
            }
            time += gameTime.ElapsedGameTime.Milliseconds;
            if (time >= LiveDown)
            {
                time = 0;
                PercentHealth--;
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
            clock += gameTime.ElapsedGameTime.Milliseconds;
            if (clock >= 1000)
            {
                clock = 0;
                Secconds++;
            }
            if (Secconds >= 60)
            {
                Secconds = 0;
                Minutes++;
                
                Random fuck=new Random();
                
                PerCentRain =fuck.Next(1,100);
                if (PerCentRain <= 40&&!HaveRain)
                {
                    RainIsComings = true;
                    LenghtRain = PerCentRain;
                    PerCentRain = 100;
                }
            }
            if (Minutes >= 60)
            {
                Minutes = 0;
                Hours++;

            }
            if (RainIsComings)
            {
                RainIsComing(gameTime);
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
                Arrplants[(int) getIndexPos().X, (int) getIndexPos().Y].timer.Enabled = false;
                Arrplants[(int)getIndexPos().X, (int)getIndexPos().Y].timer = null;
                Arrplants[(int) getIndexPos().X, (int) getIndexPos().Y] = ListPlants(0);
                
                PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] = 0;
                PlantsLayer.CanGet[(int)getIndexPos().X, (int)getIndexPos().Y]=false;
                
                PercentHealth += 5;
                HearthShow = true;
                HearthDelay = 0;

            }
            if (HearthShow)
            {
                HearthDelay += gameTime.ElapsedGameTime.Milliseconds;
                if (HearthDelay >= 1000)
                {
                    HearthDelay = 0;
                    HearthShow = false;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && PlantsLayer.CanGet[(int)getIndexPos().X, (int)getIndexPos().Y])
            {
                int Index;
                Arrplants[(int)getIndexPos().X, (int)getIndexPos().Y].timer.Enabled = false;
                Arrplants[(int)getIndexPos().X, (int)getIndexPos().Y].timer = null;
                Arrplants[(int)getIndexPos().X, (int)getIndexPos().Y] = ListPlants(0);
                
                Index = PlantsLayer.intID[(int) getIndexPos().X, (int) getIndexPos().Y];
                PlantsLayer.intID[(int)getIndexPos().X, (int)getIndexPos().Y] = 0;
                PlantsLayer.CanGet[(int)getIndexPos().X, (int)getIndexPos().Y] = false;
                if (Index == 6) Index = 0;
                if (Index == 23) Index = 1;
                if (Index == 38) Index = 2;
                if (Index == 55) Index = 3;
                if (Index == 71) Index = 4;
                if (Index == 87) Index = 5;
                if (Index == 135) Index = 6;
                if (Index == 151) Index = 7;
                if (Index == 63) Index = 8;
                Coin += PricePlants[Index];
                GetCoin.Play();
                PercentHealth -= 0.5;
                CoinShow = true;
                CoinDelay = 0;
            }
            if (CoinShow)
            {
                CoinDelay += gameTime.ElapsedGameTime.Milliseconds;
                if (CoinDelay >= 1000)
                {
                    CoinDelay = 0;
                    CoinShow = false;
                }
            }
            if (getIndexPos().X == 76 && getIndexPos().Y == 38&&StoreStand)
            {
                DoorSound.Play();
                StoreStand = false;
                StoreCheck = true;
                TileMapDraw.MyColor = Color.Gray;
                CanOffStore = true;
            }
            if (StoreCheck)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Tab) && CanTab)
                {
                    CanTab = false;
                    page = !page;
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Tab) )
                {
                    CanTab = true;
                }
                if (page)
                {
                    for (int h = 0; h < 9; h++)
                    {
                        int val = BtnBuy[h].GetValue();
                        if (val != -1)
                        {
                            if (Coin >= priceSeeds[h])
                            {
                                AmountPlants[h]++;
                                Coin -= priceSeeds[h];
                            }
                        }
                    }
                }
                else
                {
                    for (int h = 0; h < 3; h++)
                    {
                        int val = BtnFood[h].GetValue();
                        if (val != -1)
                        {
                            if (val == 1)
                            {
                                if (Coin >= 100)
                                {
                                    Coin -= 100;
                                    PercentHealth += 10;
                                }
                            }else if (val == 2)
                            {
                                if (Coin >= 200)
                                {
                                    Coin -= 200;
                                    PercentHealth += 30;
                                }
                            }
                            else if (val == 3)
                            {
                                if (Coin >= 350)
                                {
                                    Coin -= 350;
                                    PercentHealth += 60;
                                }
                            }
                        }
                    }
                }
                
                
            }
            
            if ((getIndexPos().X != 76 || getIndexPos().Y != 38)&&CanOffStore)
            {
                CanOffStore = false;
                StoreStand = true;
                StoreCheck = false;
                TileMapDraw.MyColor = Color.White;
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
            for(int abc=0;abc<9;abc++)
            BtnBuy[abc].Update(gameTime);

            for (int a = 0; a < 3; a++)
            {
                BtnFood[a].Update(gameTime);
            }
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
            Boolean Draw = true;
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
                spriteBatch.Draw(DailBox,new Rectangle(300+83*i,670,100,100),Color.White);
                spriteBatch.DrawString(Fonts, (i + 1).ToString(), new Vector2(325 + 83 * i, 690), Color.Black);
                spriteBatch.DrawString(FontsStatus, AmountPlants[i].ToString(), new Vector2(360 + 83 * i, 730), Color.Black);
                
            }
            spriteBatch.Draw(Status,new Rectangle(1200,50,130,100),Color.White);
            spriteBatch.DrawString(FontsStatus, PercentHealth.ToString() + " %", new Vector2(1260, 58), Color.SaddleBrown);
            spriteBatch.DrawString(FontsStatus, Coin.ToString() + " $", new Vector2(1236, 116), Color.SaddleBrown);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320, 685, 60, 60), new Rectangle(((6 - 1) % (int)16) * 16, ((6 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320+83, 675, 60, 60), new Rectangle(((23 - 1) % (int)16) * 16, ((23 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83*2, 685, 60, 60), new Rectangle(((38 - 1) % (int)16) * 16, ((38 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 3, 685, 60, 60), new Rectangle(((55 - 1) % (int)16) * 16, ((55 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 4, 685, 60, 60), new Rectangle(((71 - 1) % (int)16) * 16, ((71 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 5, 685, 60, 60), new Rectangle(((87 - 1) % (int)16) * 16, ((87 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 6, 685, 60, 60), new Rectangle(((135 - 1) % (int)16) * 16, ((135 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 7, 680, 60, 60), new Rectangle(((151 - 1) % (int)16) * 16, ((151 - 1) / 16) * 16, 16, 16), Color.White);
            spriteBatch.Draw(PlantsTexture, new Rectangle(320 + 83 * 8, 685, 60, 60), new Rectangle(((63 - 1) % (int)16) * 16, ((63 - 1) / 16) * 16, 16, 16), Color.White);
            if(ShowMsgTodie) spriteBatch.DrawString(FontsStatus, "You loss bro!!!\nYou must try again later...", new Vector2(CharacterPos.X, CharacterPos.Y - 50), Color.White);
            if (StoreCheck)
            {
                Game1.showMouse = true;
                if(page)
                {
                    spriteBatch.Draw(Store, new Vector2(300, 0), Color.White);
                    for (int abc = 0; abc < 9; abc++)
                        BtnBuy[abc].Draw(spriteBatch);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[0].ToString() + " $", new Vector2(527, 260), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[1].ToString() + " $", new Vector2(527, 345), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[2].ToString() + " $", new Vector2(527, 435), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[3].ToString() + " $", new Vector2(527 + 245, 260), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[4].ToString() + " $", new Vector2(527 + 245, 345), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[5].ToString() + " $", new Vector2(527 + 245, 435), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[6].ToString() + " $", new Vector2(527 + 480, 260), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[7].ToString() + " $", new Vector2(527 + 480, 335), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus, priceSeeds[8].ToString() + " $", new Vector2(527 + 480, 435), Color.SaddleBrown);
                }
                else
                {
                    spriteBatch.Draw(FoodPage, new Vector2(245, 55), Color.White);
                    spriteBatch.DrawString(FontsStatus,  "100 $\n 10 %", new Vector2(470, 380), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus,"200 $\n 30%", new Vector2(710, 380), Color.SaddleBrown);
                    spriteBatch.DrawString(FontsStatus,"350 $\n 60%", new Vector2(950, 380), Color.SaddleBrown);
                    for (int a = 0; a < 3; a++)
                    {
                        BtnFood[a].Draw(spriteBatch);
                    }
                }
                
            }
            for (int i = 0; i < 9; i++)
            {
                if (Game1.showMouse &&
                    new Rectangle(300 + 83 * i, 670, 100, 100).Intersects(new Rectangle(Mouse.GetState().X,
                        Mouse.GetState().Y, 0, 0)) && Draw)
                {
                    spriteBatch.DrawString(ClockFonts, PricePlants[i].ToString() + " $", new Vector2(Mouse.GetState().X - 50, Mouse.GetState().Y - 60), Color.Black);
                    Draw = false;
                }
            }
            if (HearthShow)
            {
                spriteBatch.Draw(HearthTexture,new Rectangle((int)CharacterPos.X,(int)CharacterPos.Y+15,25,25),Color.White);
            }
            if (CoinShow)
            {
                spriteBatch.Draw(CoinTexture, new Rectangle((int)CharacterPos.X, (int)CharacterPos.Y + 15, 25, 25), Color.White);
            }
            spriteBatch.DrawString(ClockFonts,Hours.ToString()+" : "+Minutes.ToString()+" : "+Secconds.ToString(),new Vector2(1200,5), Color.White);
            if(RainIsComings) spriteBatch.DrawString(ClockFonts, "Rain is Coming in 10 secs For " + LenghtRain.ToString() + " Secs.", new Vector2(300, 5), Color.Black);
            if(isHelp)spriteBatch.Draw(Help,new Vector2(300,200),Color.White);
            if(isStarter)spriteBatch.Draw(Starter,new Vector2(300,200),Color.White);
        }


    }
}
