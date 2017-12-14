using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace KannaFarmByMonoGame
{
    class MenuSence
    {
        private ContentManager Content;
        private Vector2 ScreenSize;
        private Texture2D BackGround;
        private Texture2D ButtonImage,ButtonExit;
        private SpriteButton BtnStart,BtnExit;
        private Boolean CanEsc=false;
        public MenuSence(ContentManager content,Vector2 screenSize)
        {
            Content = content;
            ScreenSize = screenSize;
            LoadContent();
        }

        private void LoadContent()
        {
            BackGround = Content.Load<Texture2D>("Bg");
            ButtonImage = Content.Load<Texture2D>("BtnStart");
            ButtonExit = Content.Load<Texture2D>("BtnExit");
            BtnStart = new SpriteButton(ButtonImage, new Vector2(200, 100), new Vector2(ScreenSize.X / 2 - 25-200, ScreenSize.Y / 2 - 50), ScreenSize, 2);
            BtnExit = new SpriteButton(ButtonExit, new Vector2(200, 100), new Vector2(ScreenSize.X / 2 - 25+200, ScreenSize.Y / 2 - 50), ScreenSize, 0);
        }
        public void Update(GameTime gameTime)
        {
            int BtnStartValue = BtnStart.GetValue(), BtnExitValue = BtnExit.GetValue();
            BtnStart.Update(gameTime);
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                CanEsc = true;
            }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)&&CanEsc)
                {
                    CanEsc = false;
                Game1.GameSence = 2;
                GamePlaySence.pause = false;
            }
            if (BtnStartValue != -1)
            {
                Game1.GameSence = BtnStartValue;
                GamePlaySence.pause = false;
            }
            BtnExit.Update(gameTime);
            if (BtnExitValue != -1) Game1.GameSence = BtnExitValue;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGround,new Rectangle(0,0,(int)ScreenSize.X,(int)ScreenSize.Y),Color.White);
            BtnStart.Draw(spriteBatch);
            BtnExit.Draw(spriteBatch);
            
        }
    }
}
