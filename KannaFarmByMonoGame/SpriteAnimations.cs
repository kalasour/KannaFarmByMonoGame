using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace KannaFarmByMonoGame
{
    class SpriteAnimations
    {
        public Texture2D Texture;
        public int Row;
        public int Column;
        public int JustRow;
        public int JustCol;
        private int CurrentFrame;
        private int TotalFrame;
        int SizeOfSprite = 2;
        private int TimeSinceLastFrame = 0;
        private int MillionsecondPerFrame = 50;
        public int SpriteHeight, SpriteWidth;
        public bool isEnable=false;

        public SpriteAnimations(ContentManager content,String file, int row, int column,int justRow, int justCol)
        {
            Texture = content.Load<Texture2D>(file);
            Row = row;
            Column = column;
            CurrentFrame = 0;
            TotalFrame = Row * Column;
            JustRow = justRow;
            JustCol = justCol;
            SpriteHeight = Texture.Height / row*SizeOfSprite;
            SpriteWidth = Texture.Width / column*SizeOfSprite;
        }

        public void Update( GameTime gameTime)
        {
            if(isEnable)
            TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (TimeSinceLastFrame > MillionsecondPerFrame)
            {
                TimeSinceLastFrame -= MillionsecondPerFrame;
                CurrentFrame++;
                TimeSinceLastFrame = 0;
                if (CurrentFrame == TotalFrame)
                {
                    CurrentFrame = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Column;
            int height = Texture.Height / Row;
            if (JustCol > Column) JustCol = Column;
            if (JustRow > Row) JustRow = Row;
            int row = JustRow;
            int column = CurrentFrame % JustCol;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width*SizeOfSprite, height*SizeOfSprite);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);  

        }


    }
}
