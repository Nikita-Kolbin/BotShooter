using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class Enemies
    {
        public List<IEnemy> enemies = new();
        Dictionary<int, Texture2D> enemiesTextures = new();
        public RespawnWave respawn = new RespawnWave();
        SpriteFont font;
        Texture2D hpBar;
        Texture2D zeroBar;
        Texture2D stanTextute;

        // FROM GAME
        public void LoadTexture(ContentManager Content)
        {
            enemiesTextures[1] = Content.Load<Texture2D>("enemy1");
            enemiesTextures[2] = Content.Load<Texture2D>("enemy2");
            enemiesTextures[3] = Content.Load<Texture2D>("enemy3");
            enemiesTextures[4] = Content.Load<Texture2D>("enemy4");
            enemiesTextures[5] = Content.Load<Texture2D>("enemy5");
            enemiesTextures[6] = Content.Load<Texture2D>("enemy6");
            enemiesTextures[7] = Content.Load<Texture2D>("enemy7");
            enemiesTextures[8] = Content.Load<Texture2D>("enemy8");
            zeroBar = Content.Load<Texture2D>("enemy_zero_hp");
            hpBar = Content.Load<Texture2D>("enemy_hp");
            stanTextute = Content.Load<Texture2D>("stan");
        }

        public void UpdateAll(Player player, Tower tower, Projectiles projectiles, Drop drop, CurrentState currentState, int tileSize)
        {
            foreach (var enemy in enemies)
                enemy.Update(player, tower, projectiles, drop, tileSize);
            enemies = enemies.Where(enemy => enemy.Hp > 0).ToList();
            respawn.Update(this, tileSize, currentState);
        }

        public void DrawAll(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            foreach (var enemy in enemies)
                enemy.Draw(_spriteBatch, camera, tileSize, enemiesTextures[enemy.EnemyNum], zeroBar, hpBar, stanTextute);
        }
    }
}
