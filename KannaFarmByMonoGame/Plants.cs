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
        public Plants(TileMapDraw map,Vector2 index,int start,int step,int seccond,bool HaveRain)
        {
            map.intID[(int)index.X, (int)index.Y] = start;
            IndexOfPlant = index;
            Map = map;
            Start = start;
            Step = step;
            End = Start + Step-1;
            Seccond = seccond*1000;
            timer = new Timer();
            if(HaveRain)timer.Interval = Seccond/2;
            else timer.Interval = Seccond;
            timer.Elapsed += ChangePlants;
            timer.Enabled = true;
            
        }
        public void Update(bool HaveRain)
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
        private void ChangePlants(Object source, ElapsedEventArgs e)
        {
            if (Start >= End)
            {
                timer.Enabled = false;
                Map.CanGet[(int)IndexOfPlant.X, (int)IndexOfPlant.Y] = true;
            }
            else Start++;
            Map.intID[(int)IndexOfPlant.X, (int)IndexOfPlant.Y] = Start;
        }
    }
}
