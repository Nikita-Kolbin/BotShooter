using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class OnlyTowerEnemy : Enemy, IEnemy
    {
        public OnlyTowerEnemy(int type, int hp, int damage, double reload, int speed, Vector2 pos)
        {
            EnemyNum = type;
            position = pos;
            this.speed = speed;
            this.damage = damage;
            this.reload = new Timer(reload);
            this.Hp = hp;
            maxHp = hp;
        }

        public void Move(Player player, Tower tower, int tileSize)
        {
            var enemyPos = GetCenterPosition(tileSize);
            var target = tower.GetCenterPosition(tileSize);

            var sin = (target.Y - enemyPos.Y) / ((target - enemyPos).Length());
            var cos = (target.X - enemyPos.X) / ((target - enemyPos).Length());

            if (!tower.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)))
            {
                position.X += speed * cos;
                position.Y += speed * sin;
            }
        }

        public void Attack(Player player, Tower tower, int tileSize)
        {
            if (tower.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)) && reload.State)
            {
                tower.hp -= damage;
                reload.Start();
                return;
            }
        }

        // FROME GAME
        public void Update(Player player, Tower tower, Projectiles projectiles, Drop drop, int tileSize)
        {
            if (stan.State)
            {
                Move(player, tower, tileSize);
                Attack(player, tower, tileSize);
                reload.Update();
            }
            GetDrop(drop);
            stan.Update();
        }
    }
}
