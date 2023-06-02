using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class CurrentState
    {
        public GameState State = GameState.Start;
        Timer hold = new(0.5);
        public bool happyEnd;
        public bool badEnd;

        public void Update(KeyboardState keyboardState, Player player, Tower tower)
        {
            if (player.hp <= 0 || tower.hp <= 0) badEnd = true;
            
            if (happyEnd)
            {
                State = GameState.HappyEnd;
                return;
            }
            if (badEnd)
            {
                State = GameState.BadEnd;
                return;
            } 

            if (State == GameState.Start && keyboardState.GetPressedKeyCount() > 0 && hold.State)
            {
                State = GameState.Playing;
                hold.Start();
            }

            if (keyboardState.IsKeyDown(Keys.Tab) && hold.State)
            {
                if (State == GameState.Playing)
                    State = GameState.Info;
                else if (State == GameState.Info)
                    State = GameState.Playing;
                hold.Start();
            }

            hold.Update();
        }
    }
}
