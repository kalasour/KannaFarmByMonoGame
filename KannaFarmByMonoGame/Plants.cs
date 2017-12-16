using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KannaFarmByMonoGame
{
    class Plants
    {
        private int Seccond, Step, Start,End;
        private Vector2 IndexOfPlant;
        public Timer timer;
        TileMapDraw Map;
        private bool haveRain;
        private Boolean Todie;
        public Plants(TileMapDraw map,Vector2 index,int start,int step,int seccond,bool HaveRain,int live)
        {
            map.intID[(int)index.X, (int)index.Y] = start;
            IndexOfPlant = index;
            Map = map;
            Start = start;
            Todie = false;
            Step = step;
            End = Start + Step-1;
            haveRain = HaveRain;
            Seccond = seccond*1000;
            timer = new Timer();
            if(HaveRain)timer.Interval = Seccond/2;
            else timer.Interval = Seccond;
            timer.Elapsed += ChangePlants;
            timer.Enabled = true;
            
        }
        public void  Update(bool HaveRain)
        {
            haveRain = HaveRain;
            if (Todie)
            {
                if (haveRain) timer.Interval = Seccond;
                else timer.Interval = Seccond * 2;
            }
            else
            {
                if(HaveRain)
            {
                timer.Interval=Seccond/2;
            }
            else if(!HaveRain)
            {
                timer.Interval = Seccond;
            }
            }
            
            
        }
        private void ChangePlants(Object source, ElapsedEventArgs e)
        {
                Start++;
            if (Start == End)
            {
                Todie = true;
                if (haveRain) timer.Interval = Seccond ;
                else timer.Interval = Seccond*2;
                Map.CanGet[(int)IndexOfPlant.X, (int)IndexOfPlant.Y] = true;
            }
            if (Start > End)
            {
                Map.intID[(int) IndexOfPlant.X, (int) IndexOfPlant.Y] = 0;
                
                Start = 0;
                if (timer != null)
                {
                    GamePlaySence.PercentHealth -= 5;
                    timer.Enabled = false;
                timer = null;
                }
                
            }
            Map.intID[(int)IndexOfPlant.X, (int)IndexOfPlant.Y] = Start;
        }
    }
}
