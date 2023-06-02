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
    internal class Knife
    {
        public static Texture2D texture;
        public int damage = 5;
        public int distance = 60;
        public Timer reload = new(0.7);
        public Timer visible = new(0.3);
        public int direction;

        public Rectangle GetRectangle(Player player, MouseState mousePosition, Camera camera, int tileSize)
        {
            var center = player.GetCenterPosition(tileSize);
            if (center.X < mousePosition.X + camera.coord.X)
                return new Rectangle((int)center.X, (int)(center.Y - tileSize / 2 - distance / 2), 
                    (int)(tileSize / 2 + distance), (int)(tileSize + distance));
            else
                return new Rectangle((int)(center.X - tileSize / 2 - distance), (int)(center.Y - tileSize / 2 - distance / 2),
                    (int)(tileSize / 2 + distance), (int)(tileSize + distance));
        }

        public void UpdateDirection(Player player, MouseState mousePosition, Camera camera, int tileSize)
        {
            var center = player.GetCenterPosition(tileSize);
            if (center.X < mousePosition.X + camera.coord.X)
                direction = 0;
            else
                direction = 1;
        }

        public void Attack(MouseState mouseState, Player player, Enemies enemies, Camera camera, int tileSize)
        {
            if (reload.State)
            {
                UpdateDirection(player, mouseState, camera, tileSize);
                foreach (var enemy in enemies.enemies)
                    if (enemy.GetRectangle(tileSize).Intersects(GetRectangle(player, mouseState, camera, tileSize)) && !enemy.Flying)
                    {
                        var rnd = new Random();
                        if (rnd.Next(1, 100) > 70)
                            enemy.TakeDamage(damage * 2);
                        else
                            enemy.TakeDamage(damage);
                    }
                reload.Start();
                visible.Start();
            }
        }

        // FROM GAME
        public void Update()
        {
            reload.Update();
            visible.Update();
        }

        public void Draw(SpriteBatch _spriteBatch, Vector2 playerCoord, int tileSize)
        {
            if (!visible.State)
                if (direction == 0)
                    _spriteBatch.Draw(texture,
                    new Rectangle((int)playerCoord.X + tileSize / 3, (int)playerCoord.Y - tileSize / 8, tileSize * 3 / 2, tileSize * 3 / 2),
                    null,
                    Color.White);
                else
                    _spriteBatch.Draw(texture,
                    new Rectangle((int)(playerCoord.X - 0.9 * tileSize), (int)playerCoord.Y - tileSize / 8, tileSize * 3 / 2, tileSize * 3 / 2),
                    null,
                    Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
        }
    }
}
