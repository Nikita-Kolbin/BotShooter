using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class Drop
    {
        public List<DropArrow> drop = new();
        public Texture2D arrowTexture;
        Timer respawn;

        public Drop()
        {
            respawn = new Timer(10);
            respawn.Start();
        }

        void Respawn(int tileSize)
        {
            if (respawn.State == true)
            {
                var pos = new Vector2(20 * tileSize, 16 * tileSize);
                var rnd = new Random();
                drop.Add(new DropArrow(pos + new Vector2(rnd.Next(-32, 32), rnd.Next(0, 32))));
                respawn.Start();
            }
        }
        
        // FROM GAME
        public void LoadTexture(ContentManager Content)
        {
            arrowTexture = Content.Load<Texture2D>("arrow");
        }

        public void UpdateAll(Player player, int tileSize)
        {
            foreach (var arrow in drop)
                arrow.Update(player, tileSize);
            drop = drop.Where(arrow => !arrow.delete).ToList();
            Respawn(tileSize);
            respawn.Update();
        }

        public void DrawAll(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            foreach (var arrow in drop)
                arrow.Draw(_spriteBatch, camera, tileSize, arrowTexture);
        }
    }
}
