using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class DropArrow
    {
        public Vector2 position;
        public bool delete;

        public DropArrow(Vector2 position)
        {
            this.position = position;
        }

        public Rectangle GetRectangle(int tileSize)
        {
            return new Rectangle((position + (new Vector2(tileSize, tileSize) / 4)).ToPoint(), new Point(tileSize / 2, tileSize / 2));
        }

        // FROM GAME
        public void Update(Player player, int tileSize)
        {
            if (GetRectangle(tileSize).Intersects(player.GetRectangle(tileSize)))
            {
                player.bow.arrowCount++;
                delete = true;
            }
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize, Texture2D texture)
        {
            var coord = camera.FindCoord(position);
            _spriteBatch.Draw(texture, new Rectangle(coord.ToPoint(), new Point(tileSize, tileSize)), Color.White);
        }
    }
}
