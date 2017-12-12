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
        private Texture2D ButtonImage;
        private SpriteButton BtnStart;
        public MenuSence(ContentManager content,Vector2 screenSize)
        {
            Content = content;
            ScreenSize = screenSize;
            LoadContent();
        }

        private void LoadContent()
        {
            BackGround = Content.Load<Texture2D>("Bg");
            ButtonImage = Content.Load<Texture2D>("PressEnterto");
            BtnStart=new SpriteButton(ButtonImage,new Vector2(100,50),new Vector2(200,200),ScreenSize,3);
        }
        public void Update(GameTime gameTime)
        {
            BtnStart.Update(gameTime);
            if(BtnStart.GetValue()!=-1)Game1.GameSence = BtnStart.GetValue();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackGround,new Rectangle(0,0,(int)ScreenSize.X,(int)ScreenSize.Y),Color.White);
            BtnStart.Draw(spriteBatch);
            
        }
    }
}
