using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotShooter
{
    internal class Enemy
    {
        public int enemyNum;
        public int hp;
        public int speed;
        public Vector2 position;
        public int damage;

        public Enemy(int type, Vector2 pos)
        {
            enemyNum = type;
            position = pos;
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize, Texture2D texture)
        {
            var coord = camera.FindCoord(position);
            _spriteBatch.Draw(texture,
                new Rectangle((int)coord.X, (int)coord.Y, tileSize, tileSize),
                new Rectangle(0, 0, 16, 16),
                Color.White);
        }
    }
}
