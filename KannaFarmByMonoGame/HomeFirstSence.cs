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
        ContentManager Content;
        Vector2 ScreenSize;
        int speed=5;
        bool isAction = false;
        Texture2D Character;
        Vector2 CharacterPos=new Vector2(50,50);
        String pathWalk = "boyMove";
        String pathActions = "boyAction";
        SpriteAnimations SpriteWalks;
        SpriteAnimations SpriteAction;
        public HomeFirstSence(ContentManager content,Vector2 screensize)
        {
            Content = content;
            ScreenSize = screensize;
            LoadContent();
        }
        public void LoadContent()
        {
            Character = Content.Load<Texture2D>(pathWalk);
            SpriteWalks = new SpriteAnimations(Content, pathWalk,4,4,1,4);
            SpriteAction = new SpriteAnimations(Content, pathActions, 1, 4, 0, 4);
            SpriteWalks.isEnable = true;
        }

        public void Update(GameTime gameTime)
        {
            SpriteWalks.Update(gameTime);
            SpriteAction.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                CharacterPos.X -= speed;
                SpriteWalks.JustRow = 3;
                if (CharacterPos.X < 0) CharacterPos.X += speed;
                SpriteWalks.isEnable = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                CharacterPos.X += speed;
                SpriteWalks.JustRow = 1;
                if (CharacterPos.X + SpriteWalks.SpriteWidth > ScreenSize.X) CharacterPos.X -= speed;
                SpriteWalks.isEnable = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                CharacterPos.Y += speed;
                SpriteWalks.JustRow = 0;
                if (CharacterPos.Y + SpriteWalks.SpriteHeight > ScreenSize.Y) CharacterPos.Y -= speed;
                SpriteWalks.isEnable = true;

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                CharacterPos.Y -= speed;
                SpriteWalks.JustRow = 2;
                if (CharacterPos.Y + (SpriteWalks.SpriteHeight / 2) < 0) CharacterPos.Y += speed;
                SpriteWalks.isEnable = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                isAction = true;
                SpriteAction.isEnable = true;
            }
            else
            {
                SpriteWalks.isEnable = false;
                isAction = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isAction)
            {
                SpriteAction.Draw(spriteBatch, CharacterPos);
            }else
            SpriteWalks.Draw(spriteBatch, CharacterPos);
        }
    }
}
