using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace BotShooter
{
    internal class Enemies
    {
        public List<Enemy> enemies = new();
        private Dictionary<int, Texture2D> enemiesTextures = new();

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
        }

        public void DrawAll(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            foreach (var enemy in enemies)
                enemy.Draw(_spriteBatch, camera, tileSize, enemiesTextures[enemy.enemyNum]);
        }
    }
}
