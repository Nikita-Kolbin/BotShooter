using VillageDefence.Respawn;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class RespawnWave
    {
        public List<Wave> waves = new();
        public int waveNum;

        public RespawnWave()
        {
            // 1
            waves.Add(new Wave(new List<RespawnItem>() {
                new RespawnItem(1, 2),
                new RespawnItem(2, 3),
                new RespawnItem(1, 3),
                new RespawnItem(3, 5),
                new RespawnItem(1, 3),
                new RespawnItem(1, 5),
                new RespawnItem(2, 2),
                new RespawnItem(1, 2),
                new RespawnItem(3, 5),
                new RespawnItem(4, 1),
                new RespawnItem(1, 4),
                new RespawnItem(2, 23),

            }));
            // 2
            waves.Add(new Wave(new List<RespawnItem>() {
                new RespawnItem(5, 5),
                new RespawnItem(8, 1),
                new RespawnItem(7, 2),
                new RespawnItem(5, 8),
                new RespawnItem(6, 1),
                new RespawnItem(8, 15),
                new RespawnItem(5, 8),
                new RespawnItem(6, 1),
                new RespawnItem(7, 1),
                new RespawnItem(8, 8),
                new RespawnItem(6, 15),
            }));
            // 3
            waves.Add(new Wave(new List<RespawnItem>() {
                new RespawnItem(1, 1),
                new RespawnItem(1, 5),
                new RespawnItem(6, 1),
                new RespawnItem(6, 1),

                new RespawnItem(4, 0.5),
                new RespawnItem(4, 8),
                new RespawnItem(8, 6),
                
                new RespawnItem(7, 1),
                new RespawnItem(7, 9),
                new RespawnItem(2, 1),
                new RespawnItem(2, 10),

                new RespawnItem(5, 1),
                new RespawnItem(5, 1),
                new RespawnItem(5, 1),
                new RespawnItem(3, 12),

                new RespawnItem(4, 1),
                new RespawnItem(7, 1),
                new RespawnItem(3, 1),
                new RespawnItem(5, 1),
                new RespawnItem(1, 1),
                new RespawnItem(2, 1),
                new RespawnItem(8, 1),
                new RespawnItem(6, 1),
            }));
            
            waveNum = 0;
            waves[0].Start();
        }

        public void Update(Enemies enemies, int tileSize, CurrentState currentState)
        {
            waves[waveNum].Update(enemies, tileSize);
            
            if (waves[waveNum].end && waveNum + 1 < waves.Count)
            {
                waveNum++;
                waves[waveNum].Start();
            }

            if (waves[waves.Count - 1].end && enemies.enemies.Count == 0)
                currentState.happyEnd = true;
        }
    }
}
