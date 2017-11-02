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
    class BackgroundScreen
    {
        private Texture2D Background, Logo, Cloud, PressEnter;
        ContentManager Content;
        int[] CloudPosX = new int[10];
        Vector2 ScreenSize, LogoPos, CloudPos;
        public bool CanEnter = false;
        Timer Swap;
        bool SwapVa=true;
        

        public BackgroundScreen(ContentManager content, Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }
        public void LoadContent()
        {

            Background = Content.Load<Texture2D>("BG");
            Logo = Content.Load<Texture2D>("Logo");
            Cloud = Content.Load<Texture2D>("cloud_1");
            PressEnter = Content.Load<Texture2D>("PressEnterto");
            CloudPos = new Vector2(0, -Logo.Height);
            LogoPos = new Vector2((1366 / 2) - (Logo.Width / 2), -(Logo.Height));
            CloudPosX[0] = (int)CloudPos.X;
            CloudPosX[1] = 300 + (int)CloudPos.X;
            CloudPosX[2] = 100 + (int)CloudPos.X;
            CloudPosX[3] = -400 + (int)CloudPos.X;
            CloudPosX[4] = -200 + (int)CloudPos.X;
            CloudPosX[5] = -150 + (int)CloudPos.X;
            CloudPosX[6] = 400 + (int)CloudPos.X;
            CloudPosX[7] = 500 + (int)CloudPos.X;
            Swap = new Timer(1000);
            Swap.Elapsed += SwapMethod;
            Swap.Enabled = false;
        }

        public void Update(GameTime gameTime)
        {

            for (int i = 0; i < 8; i++)
            {
                if (CloudPosX[i] > ScreenSize.X)
                {
                    if (i == 0 || i == 7) CloudPosX[i] = -Cloud.Width;
                    else CloudPosX[i] = -Cloud.Width / 2;
                }
                else { CloudPosX[i] += (gameTime.ElapsedGameTime.Milliseconds / 10); }
            }
            if (!(LogoPos.Y > (762 / 2) - (Logo.Height) + (Logo.Height / 4) + 20))
            {
                LogoPos.Y += gameTime.ElapsedGameTime.Milliseconds / 8;
                CloudPos.Y += gameTime.ElapsedGameTime.Milliseconds / 8;
            }
            else
            {
                CanEnter = true;
                Swap.Enabled = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Space)||Mouse.GetState().LeftButton==ButtonState.Pressed)
            {
                LogoPos.Y = (762 / 2) - (Logo.Height) + (Logo.Height / 4) + 20;
                CloudPos.Y = (762 / 2) - (Logo.Height) + (Logo.Height / 4) + 20;
                CanEnter = true;
                Swap.Enabled = true;
            }
        }

        public void UnloadContent()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Rectangle(0, (int)LogoPos.Y, (int)ScreenSize.X * 2, (int)ScreenSize.Y * 2), Color.White);
            spriteBatch.Draw(Cloud, new Vector2(CloudPosX[0], CloudPos.Y), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[1], 100 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[7], 400 + (int)CloudPos.Y, Cloud.Width, Cloud.Height), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[2], 500 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[3], 500 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[4], 100 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Logo, new Rectangle((int)LogoPos.X, (int)LogoPos.Y, Logo.Width, (Logo.Height)), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[5], 300 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            spriteBatch.Draw(Cloud, new Rectangle(CloudPosX[6], 200 + (int)CloudPos.Y, Cloud.Width / 2, Cloud.Height / 2), Color.White);
            if (CanEnter&&SwapVa)
            {
                spriteBatch.Draw(PressEnter, new Rectangle((int)(ScreenSize.X / 2 - PressEnter.Width / 8), 500, PressEnter.Width / 4, PressEnter.Height / 4), Color.White);
            }
        }
        private void SwapMethod(Object source, ElapsedEventArgs e)
        {
            if (SwapVa == true) SwapVa = false;
            else SwapVa = true;
        }
    }
}
