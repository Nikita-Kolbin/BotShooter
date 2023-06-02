using VillageDefence.Weapon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class ShootingEnemy: Enemy, IEnemy
    {
        public int distance;

        public ShootingEnemy(int type, int hp, int damage, int reload, int distance, int speed, Vector2 pos)
        {
            EnemyNum = type;
            position = pos;
            this.speed = speed;
            this.damage = damage;
            this.reload = new Timer(reload);
            this.Hp = hp;
            maxHp = hp;
            this.distance = distance;
        }

        public void Move(Player player, Tower tower, int tileSize)
        {
            var enemyPos = GetCenterPosition(tileSize);
            var playerPos = player.GetCenterPosition(tileSize);
            var towerPos = tower.GetCenterPosition(tileSize);
            Vector2 target;

            if ((playerPos - enemyPos).Length() <= (towerPos - enemyPos).Length())
                target = playerPos;
            else
                target = towerPos;

            var sin = (target.Y - enemyPos.Y) / ((target - enemyPos).Length());
            var cos = (target.X - enemyPos.X) / ((target - enemyPos).Length());

            if ((target == towerPos && (target - position).Length() >= distance) ||           
                (target == playerPos && (target - position).Length() >= distance))
            {
                position.X += speed * cos;
                position.Y += speed * sin;
            }
        }

        public void Attack(Player player, Tower tower, Projectiles projectiles, int tileSize)
        {
            if (!reload.State) return;
            Vector2 target;
            if ((tower.GetCenterPosition(tileSize) - position).Length() <= distance)
                target = tower.GetCenterPosition(tileSize);
            else if ((player.GetCenterPosition(tileSize) - position).Length() <= distance)
                target = player.GetCenterPosition(tileSize);
            else
                return;
          
            var enemyPos = GetCenterPosition(tileSize);
            var sin = (target.Y - enemyPos.Y) / ((target - enemyPos).Length());
            double angle;

            if (target.X - enemyPos.X > 0)
                angle = Math.Asin(sin);
            else
                angle = Math.Asin(-sin) + Math.PI;

            if(EnemyNum == 3)
                projectiles.projectiles.Add(new Spike(position, (float)angle, damage));
            else
                projectiles.projectiles.Add(new Stone(position, (float)angle, damage));

            reload.Start();
            return;
        }

        // FROM GAME
        public void Update(Player player, Tower tower, Projectiles projectiles, Drop drop, int tileSize)
        {
            if (stan.State)
            {
                Move(player, tower, tileSize);
                Attack(player, tower, projectiles, tileSize);
                reload.Update();
            }
            GetDrop(drop);
            stan.Update();
        }
    }
}
