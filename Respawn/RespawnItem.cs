using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VillageDefence
{
    internal class RespawnItem
    {
        public int enemyNum;
        public Timer interval;

        public RespawnItem(int enemyNum, double interval)
        {
            this.enemyNum = enemyNum;
            this.interval = new Timer(interval);
        }

        public void Respawn(Enemies enemies, int tileSize)
        {
            var rnd = new Random();
            // Forest
            if (enemyNum == 1 || enemyNum == 6) 
            {
                var pos = new Vector2(-2 * tileSize, 32 * tileSize - rnd.Next(0, 6) * tileSize);
                if (enemyNum == 1)
                    // Mouse
                    enemies.enemies.Add(new UsualEnemy(type: 1, 
                        hp: 20, 
                        damage:5, 
                        reload: 1, 
                        speed: 3, 
                        pos: pos));
                else
                    // Spider
                    enemies.enemies.Add(new UsualEnemy( type: 6,
                        hp: 10,
                        damage: 8,
                        reload: 1,
                        speed: 6,
                        pos: pos));
            }
            
            // Orange forest
            else if (enemyNum == 2 || enemyNum == 7)
            {
                var pos = new Vector2(42 * tileSize, -2 * tileSize + rnd.Next(0, 6) * tileSize);
                if (enemyNum == 2)
                    // Bat
                    enemies.enemies.Add(new FlyingEnemy(type: 2,
                        hp: 20,
                        damage: 5,
                        reload: 1,
                        speed: 3,
                        pos: pos));
                else
                    // Cyclops
                    enemies.enemies.Add(new ShootingEnemy(type: 7,
                        hp: 50,
                        damage: 24,
                        reload: 2,
                        distance: 5 * tileSize,
                        speed: 1,
                        pos: pos));
            }

            // Water
            else if (enemyNum == 4 || enemyNum == 8)
            {
                var pos = new Vector2(-2 * tileSize, -2 * tileSize + rnd.Next(0, 6) * tileSize);
                // Crab
                if (enemyNum == 4)
                    enemies.enemies.Add(new OnlyTowerEnemy(type: 4,
                        hp: 100,
                        damage: 4,
                        reload: 0.7,
                        speed: 1,
                        pos: pos));
                else
                   // Ghost
                   enemies.enemies.Add(new FlyingEnemy(type: 8,
                        hp: 20,
                        damage: 10,
                        reload: 1,
                        speed: 5,
                        pos: pos));
            }
           
            // Desert
            else if (enemyNum == 3 || enemyNum == 5)
            {
                var pos = new Vector2(42 * tileSize, 32 * tileSize - rnd.Next(0, 6) * tileSize);
                if (enemyNum == 3)
                    // Spike
                    enemies.enemies.Add(new ShootingEnemy(type: 3, 
                        hp: 20, 
                        damage: 5, 
                        reload: 1, 
                        distance: 6 * tileSize, 
                        speed: 2, 
                        pos: pos ));
                else
                    // Desert mouse
                    enemies.enemies.Add(new OnlyTowerEnemy(type: 5,
                        hp: 30,
                        damage: 5,
                        reload: 0.5,
                        speed: 3,
                        pos: pos));
            }
        }
    }
}
