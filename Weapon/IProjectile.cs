using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal interface IProjectile
    {
        public int ProjectileNum { get; set; }
        public bool Delete { get; set; }
        
        public Rectangle GetRectangle(int tileSize);
        public void Update(Player player, Tower tower, Enemies enemies, int tileSize);
        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize, Texture2D texture);
    }
}
