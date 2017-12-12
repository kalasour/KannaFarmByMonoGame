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
    class SpriteButton
    {
        private Vector2 ButtonSize;
        private Vector2 Screensize;
        private Texture2D Image;
        private Vector2 ButtonLocation;
        private Boolean isTouch = false;
        private Boolean press = false;
        private int Value = -1;
        private Color color;
        private int Target;
        public SpriteButton(Texture2D image,Vector2 buttonSize,Vector2 buttonLocation, Vector2 screenSize,int target)
        {
            ButtonSize = buttonSize;
            Image = image;
            Screensize = screenSize;
            ButtonLocation = buttonLocation;
            Target = target;
            color = Color.White;

        }

        public void Update(GameTime gameTime)
        {
            if (new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 0, 0).Intersects(new Rectangle((int)ButtonLocation.X - (int)ButtonSize.X / 2, (int)ButtonLocation.Y - (int)ButtonSize.Y / 2, (int)ButtonSize.X, (int)ButtonSize.Y)))
            {
                isTouch = true;
            }
            if(!new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 0, 0).Intersects(new Rectangle((int)ButtonLocation.X - (int)ButtonSize.X, (int)ButtonLocation.Y - (int)ButtonSize.Y, (int)ButtonSize.X * 2, (int)ButtonSize.Y * 2)))
            {
                isTouch = false;
            }
            if (Mouse.GetState().LeftButton==ButtonState.Pressed&& new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 0, 0).Intersects(new Rectangle((int)ButtonLocation.X - (int)ButtonSize.X, (int)ButtonLocation.Y - (int)ButtonSize.Y, (int)ButtonSize.X * 2, (int)ButtonSize.Y * 2)))
            {
                isTouch = false;
                press = true;
                color = Color.Gray;
            }
            if (press&& Mouse.GetState().LeftButton != ButtonState.Pressed && new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 0, 0).Intersects(new Rectangle((int)ButtonLocation.X - (int)ButtonSize.X, (int)ButtonLocation.Y - (int)ButtonSize.Y, (int)ButtonSize.X * 2, (int)ButtonSize.Y * 2)))
            {
                Value = Target;
            }
            else if(Mouse.GetState().LeftButton != ButtonState.Pressed)
            {
                press = false;
                color = Color.White;
            }
        }

        public int GetValue()
        {
            return Value;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isTouch)
            {
                spriteBatch.Draw(Image, new Rectangle((int)ButtonLocation.X- (int)ButtonSize.X, (int)ButtonLocation.Y-(int)ButtonSize.Y, (int)ButtonSize.X*2, (int)ButtonSize.Y*2), color);
            }
            else
            {
                spriteBatch.Draw(Image, new Rectangle((int)ButtonLocation.X - (int)ButtonSize.X / 2, (int)ButtonLocation.Y - (int)ButtonSize.Y / 2, (int)ButtonSize.X, (int)ButtonSize.Y), color);
            }
            
        }
    }
}
