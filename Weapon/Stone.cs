using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class Stone : IProjectile
    {
        public bool Delete { get; set; }
        public int ProjectileNum { get; set; }
        public Vector2 position;
        public float direction;
        public int speed;
        public int distance;
        public int damage;

        public Stone(Vector2 position, float direction, int damage)
        {
            ProjectileNum = 3;
            speed = 3;
            this.position = position;
            this.direction = direction;
            distance = 8 * 64;
            this.damage = damage;
        }

        public Rectangle GetRectangle(int tileSize)
        {
            return new Rectangle(position.ToPoint(), new Point(tileSize, tileSize));
        }

        // FROM GAME
        public void Update(Player player, Tower tower, Enemies enemies, int tileSize)
        {
            position.X += speed * (float)Math.Cos(direction);
            position.Y += speed * (float)Math.Sin(direction);
            distance -= speed;

            if (tower.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)))
            {
                tower.hp -= damage;
                Delete = true;
                return;
            }
            if (player.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)))
            {
                player.TakeDamage(damage);
                Delete = true;
                return;
            }

            if (distance <= 0)
                Delete = true;
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize, Texture2D texture)
        {
            var coord = camera.FindCoord(position);
            _spriteBatch.Draw(texture,
            new Rectangle((int)(coord.X), (int)(coord.Y), tileSize, tileSize),
            null,
            Color.White);


        }
    }
}
