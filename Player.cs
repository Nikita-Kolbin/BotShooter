using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotShooter
{
    internal class Player
    {
        public Vector2 position;
        public Weapon weapon;
        public int hp;
        public int speed;

        public Player(Vector2 position, Weapon weapon, int hp, int speed)
        {
            this.position = position;
            this.weapon = weapon;
            this.hp = hp;
            this.speed = speed;
        }   
    }

    enum Weapon
    {
        knife = 0,
    }
}
