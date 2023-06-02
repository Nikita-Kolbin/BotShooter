using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class FlyingEnemy : Enemy, IEnemy 
    {
        public FlyingEnemy(int type, int hp, int damage, int reload, int speed, Vector2 pos)
        {
            EnemyNum = type;
            Flying = true;
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
            var target = player.GetCenterPosition(tileSize);
            
            var sin = (target.Y - enemyPos.Y) / ((target - enemyPos).Length());
            var cos = (target.X - enemyPos.X) / ((target - enemyPos).Length());

            if (!player.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)))
            {
                position.X += speed * cos;
                position.Y += speed * sin;
            }
        }

        public void Attack(Player player, Tower tower, int tileSize)
        {
            if (player.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)) && reload.State)
            {
                player.TakeDamage(damage);
                reload.Start();
                return;
            }
        }

        // FROM GAME
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
