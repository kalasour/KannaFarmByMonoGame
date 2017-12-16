using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace KannaFarmByMonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Vector2 ScreenSize = new Vector2(1360, 768);
        Vector2 toMouse = new Vector2(0, 0);
        Texture2D cursor;
        bool  CanPress = true;
        int speed = 5;
        public static int GameSence=1;
        public static  bool showMouse = false;
        GamePlaySence gamePlaySence;
        BackgroundScreen BackgroundScreen;
        private MenuSence menuSence;
        private OverSence overSence;
        private Song SoundBG;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        //ToAdd coodown in any button.
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            ScreenSize = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            gamePlaySence = new GamePlaySence(Content, ScreenSize);
            BackgroundScreen = new BackgroundScreen(Content,ScreenSize);
            menuSence=new MenuSence(Content,ScreenSize);
            overSence=new OverSence(Content,ScreenSize);
            this.IsMouseVisible = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            cursor = Content.Load<Texture2D>("Cursor");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            SoundBG = Content.Load<Song>("BGSound");
            MediaPlayer.Volume = 1f;
            MediaPlayer.Play(SoundBG);
            MediaPlayer.IsRepeating = true;
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.F4))
                Exit();
            if (Keyboard.GetState().IsKeyUp(Keys.LeftAlt) && Keyboard.GetState().IsKeyUp(Keys.Enter) && Keyboard.GetState().IsKeyUp(Keys.LeftControl))
            {
                CanPress = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (CanPress)
                {
                    if (graphics.IsFullScreen)
                    {
                        graphics.IsFullScreen = false;
                        graphics.ApplyChanges();
                    }
                    else
                    {
                        graphics.IsFullScreen = true;
                        graphics.ApplyChanges();
                    }

                    CanPress = false;
                }
            }

            if(Keyboard.GetState().IsKeyDown(Keys.LeftControl)&&CanPress)
            {
                showMouse = !showMouse;
                CanPress = false;
            }
            if(BackgroundScreen.CanEnter&&Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                BackgroundScreen.CanEnter = false;
                //showMouse = true;
                GameSence = 2;
            }
            
            


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (GameSence)
            {
                case 1:
                    BackgroundScreen.Draw(spriteBatch);
                    BackgroundScreen.Update(gameTime);
                    MediaPlayer.Volume = 1f;
                    break;
                case 2:
                    MediaPlayer.Volume = 0.4f;
                    gamePlaySence.Draw(spriteBatch);
                    gamePlaySence.Update(gameTime);
                    break;
                case 3:
                    showMouse = true;
                    MediaPlayer.Volume = 1f;
                    menuSence.Draw(spriteBatch);
                    menuSence.Update(gameTime);
                    break;
                case 4:
                    showMouse = true;
                    MediaPlayer.Volume = 1f;
                    overSence.Draw(spriteBatch);
                    overSence.Update(gameTime);
                    break;




                case 0:
                    Exit();
                    break;
            }
            if (showMouse)
            {
                spriteBatch.Draw(cursor, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, cursor.Width / 2, cursor.Height / 2), Color.White);
            } 
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

    }
}
