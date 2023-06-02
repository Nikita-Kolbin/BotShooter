using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence.Weapon
{
    internal class Bow
    {
        public static Texture2D texture;
        public int damage = 5;
        public Timer reload = new(1);
        public int direction;
        public int arrowCount = 15;

        public void Attack(MouseState mouseState, Player player, Projectiles projectiles, Camera camera, int tileSize)
        {
            if (reload.State && arrowCount > 0)
            {
                var mousePos = new Vector2(mouseState.X, mouseState.Y);
                var playerPos = camera.FindCoord(player.GetCenterPosition(tileSize));
                var sin = (mousePos.Y - playerPos.Y) / ((mousePos - playerPos).Length());
                double angle;

                if (mousePos.X - playerPos.X > 0)
                {
                    angle = Math.Asin(sin);
                    direction = 0;
                }
                else
                { 
                    angle = Math.Asin(-sin) + Math.PI; 
                    direction = 1;
                }

                projectiles.projectiles.Add(new Arrow(player.position, (float)angle, damage));

                arrowCount--;
                reload.Start();
            }
        }

        // FROM GAME
        public void Update()
        {
            reload.Update();
        }

        public void Draw(SpriteBatch _spriteBatch, Vector2 playerCoord, int tileSize)
        {
            if (direction == 0)
                _spriteBatch.Draw(texture,
                new Rectangle((int)playerCoord.X + 32, (int)playerCoord.Y + 16, tileSize * 3 / 4, tileSize * 3 / 4),
                null,
                Color.White);
            else
                _spriteBatch.Draw(texture,
                new Rectangle((int)playerCoord.X - 16, (int)playerCoord.Y + 16, tileSize * 3 / 4, tileSize * 3 / 4),
                null,
                Color.White,
                0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
        }
    }
}
