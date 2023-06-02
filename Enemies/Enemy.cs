using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VillageDefence
{
    internal class Enemy
    {
        public int EnemyNum { get; set; }
        public int Hp { get; set; }
        public bool Flying { get; set; }
        public Vector2 position;
        public int maxHp;
        public int speed;
        public int damage;
        public Timer reload;
        public Timer stan = new(0.3);

        public Rectangle GetRectangle(int tileSize)
        {
            return new Rectangle(position.ToPoint(), new Point(tileSize, tileSize));
        }

        public Vector2 GetCenterPosition(int tileSize)
        {
            return position + new Vector2(tileSize / 2, tileSize / 2);
        }

        public void TakeDamage(int damage)
        {
            if (EnemyNum == 8 && new Random().Next(1, 100) > 70)
                return;

            Hp -= damage;
            stan.Start();
        }

        public void GetDrop(Drop drop)
        {
            var rnd = new Random();
            var num = rnd.Next(1, 100);
            if (Hp <= 0 && num > 50)
                drop.drop.Add(new DropArrow(position));
        }

        // FROM GAME
        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize, Texture2D texture, Texture2D zeroBar, Texture2D hpBar, Texture2D stanTexture)
        {
            var coord = camera.FindCoord(position);
            _spriteBatch.Draw(texture,
                new Rectangle((int)coord.X, (int)coord.Y, tileSize, tileSize),
                new Rectangle(0, 0, 16, 16),
                Color.White);

            // HP
            _spriteBatch.Draw(zeroBar,
                coord + new Vector2(0, -tileSize / 5),
                Color.White);
            _spriteBatch.Draw(hpBar,
                coord + new Vector2(0, -tileSize / 5),
                new Rectangle(0, 0, (int)(64 * ((float)Hp / maxHp)), 10),
                Color.White);

            // STAN
            if (!stan.State)
            {
                _spriteBatch.Draw(zeroBar,
                    coord + new Vector2(0, -tileSize * 0.4f),
                    Color.White);
                
                _spriteBatch.Draw(stanTexture,
                    coord + new Vector2(0, -tileSize * 0.4f),
                    new Rectangle(0, 0, (int)(64 * (1 - (stan.GetPercent()))), 10),
                    Color.White);
            }
        }
    }
}
