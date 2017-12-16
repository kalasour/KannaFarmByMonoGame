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
    class OverSence
    {
        private ContentManager Content;
        private Vector2 ScreenSize, CloudPos;
        private Texture2D BackGround,Cloud;
        private Texture2D ButtonImage,ButtonExit;
        private SpriteButton BtnStart,BtnExit;
        private Boolean CanEsc=false;
        int[] CloudPosX = new int[10];
        public OverSence(ContentManager content,Vector2 screenSize)
        {
            Content = content;
            ScreenSize = screenSize;
            LoadContent();
        }

        private void LoadContent()
        {
            BackGround = Content.Load<Texture2D>("Bg");
            ButtonImage = Content.Load<Texture2D>("BtnRetry");
            ButtonExit = Content.Load<Texture2D>("BtnExit");
            Cloud = Content.Load<Texture2D>("cloud_1");
            BtnStart = new SpriteButton(ButtonImage, new Vector2(200, 100), new Vector2(ScreenSize.X / 2 - 25-200, ScreenSize.Y / 2 - 50), ScreenSize, 2,Content);
            BtnExit = new SpriteButton(ButtonExit, new Vector2(200, 100), new Vector2(ScreenSize.X / 2 - 25+200, ScreenSize.Y / 2 - 50), ScreenSize, 0,Content);
            CloudPosX[0] = (int)CloudPos.X;
            CloudPosX[1] = 300 + (int)CloudPos.X;
            CloudPosX[2] = 100 + (int)CloudPos.X;
            CloudPosX[3] = -400 + (int)CloudPos.X;
            CloudPosX[4] = -200 + (int)CloudPos.X;
            CloudPosX[5] = -150 + (int)CloudPos.X;
            CloudPosX[6] = 400 + (int)CloudPos.X;
            CloudPosX[7] = 500 + (int)CloudPos.X;
        }
        public void Update(GameTime gameTime)
        {
            int BtnStartValue = BtnStart.GetValue(), BtnExitValue = BtnExit.GetValue();
            BtnStart.Update(gameTime);
            if (BtnStartValue != -1)
            {
                CanEsc = false;
                GamePlaySence.Reset = true;
                Game1.GameSence = BtnStartValue;
                GamePlaySence.pause = false;
            }
            BtnExit.Update(gameTime);
            if (BtnExitValue != -1) Game1.GameSence = BtnExitValue;

            for (int i = 0; i < 8; i++)
            {
                if (CloudPosX[i] > ScreenSize.X)
                {
                    if (i == 0 || i == 7) CloudPosX[i] = -Cloud.Width;
                    else CloudPosX[i] = -Cloud.Width / 2;
                }
                else { CloudPosX[i] += (gameTime.ElapsedGameTime.Milliseconds / 10); }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGround,new Rectangle(0,0,(int)ScreenSize.X,(int)ScreenSize.Y),Color.White);
            spriteBatch.Draw(Cloud, new Vector2(CloudPosX[0], CloudPos.Y), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[1], 100 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[7], 400 + (int)CloudPos.Y, Cloud.Width, Cloud.Height), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[2], 500 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[3], 500 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[4], 100 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[5], 300 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[6], 200 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            BtnStart.Draw(spriteBatch);
            BtnExit.Draw(spriteBatch);
        }
    }
}
