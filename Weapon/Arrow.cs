using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class Arrow : IProjectile
    {
        public bool Delete { get; set; }
        public int ProjectileNum { get; set; }
        public Vector2 position;
        public float direction;
        public int speed;
        public int distance;
        public int damage;

        public Arrow(Vector2 position, float direction, int damage)
        {
            ProjectileNum = 1;
            this.position = position;
            this.direction = direction;
            speed = 12;
            distance = 64 * 20;
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
            
            foreach (var enemy in enemies.enemies)
                if (enemy.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)))
                {
                    var rnd = new Random();
                    if (rnd.Next(1, 100) > 85)
                        enemy.TakeDamage(damage * 2);
                    else
                        enemy.TakeDamage(damage);
                    Delete = true;
                    return;
                }

            if (distance <= 0 || tower.GetRectangle(tileSize).Intersects(GetRectangle(tileSize)))
                Delete = true;
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize, Texture2D texture)
        {
            var coord = camera.FindCoord(position);
            _spriteBatch.Draw(texture,
            new Rectangle((int)(coord.X) + tileSize / 2, (int)(coord.Y) + tileSize / 2, tileSize, tileSize),
            null,
            Color.White, 
            direction + (float)(Math.PI / 4), new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0);
        }
    }
}
