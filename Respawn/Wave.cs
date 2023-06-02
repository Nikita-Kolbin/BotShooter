using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence.Respawn
{
    internal class Wave
    {
        public List<RespawnItem> items;
        public bool start;
        public bool end;
        public Timer interval = new(1);
        int counter;

        public Wave(List<RespawnItem> items)
        {
            this.items = items;
        }

        public void Start()
        {
            start = true;
        }

        // FRPM GAME
        public void Update(Enemies enemies, int tileSize)
        {
            if (start && interval.State && counter < items.Count)
            {
                items[counter].Respawn(enemies, tileSize);
                interval = items[counter].interval;
                interval.Start();
                counter++;
            }
            interval.Update();
            if (counter == items.Count && interval.State)
            {
                end = true;
                start = false;
            }
        }

        public void Draw(SpriteBatch _spriteBatch, int tileSize, SpriteFont font)
        {
            _spriteBatch.DrawString(font, "Волна 1", new Vector2(35 * tileSize, 0), Color.Red);
        }

    }
}
