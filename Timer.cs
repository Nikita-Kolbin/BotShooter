using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class Timer
    {
        readonly int TimeInTick;
        int Curent;
        public bool State;

        public Timer(double timeInSecond)
        {
            TimeInTick = (int)(timeInSecond * 60);
            Curent = (int)(timeInSecond * 60);
            State = true;
        }

        public void Start()
        {
            Curent = 0;
            State = false;
        }

        public void Update()
        {
            if(Curent == TimeInTick)
            {
                State = true;
                return;
            }
            if (Curent < TimeInTick) Curent++;
        }

        public float GetPercent()
        {
            return (float)Curent / TimeInTick;
        }
    }
}
