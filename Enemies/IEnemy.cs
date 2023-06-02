using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal interface IEnemy
    {
        public int EnemyNum { get; set; }
        public int Hp { get; set; }
        public bool Flying { get; set; }
        public Rectangle GetRectangle(int tileSize);
        public Vector2 GetCenterPosition(int tileSize);
        public void TakeDamage(int damage);
        public void Update(Player player, Tower tower, Projectiles projectiles, Drop drop, int tileSize);
        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize, Texture2D texture,Texture2D zeroBar, Texture2D hpBar, Texture2D stanTexture);

    }
}
