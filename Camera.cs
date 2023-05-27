using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotShooter
{
    internal class Camera
    {
        public Vector2 coord = new Vector2();

        public Camera(Vector2 vector)
        {
            coord = vector;
        }

        public void Move(KeyboardState keyboardState, Map map, Player player, int tileSize)
        {
            int x = 0;
            if (player.position.X - 7 * tileSize > 0)
                x = Math.Min((int)player.position.X - 7 * tileSize, (int)(map.width - 15) * tileSize);

            int y = 0;
            if (player.position.Y - 5 * tileSize > 0)
                y = Math.Min((int)player.position.Y - 5 * tileSize, (int)(map.height - 11) * tileSize);

            coord = new Vector2(x, y);
                
        }

        public Vector2 FindCoord(Vector2 vector)
        {
            return new Vector2(vector.X - coord.X, vector.Y - coord.Y);
        }
    }
}
