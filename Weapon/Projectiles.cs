using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class Projectiles
    {
        public List<IProjectile> projectiles = new();
        private Dictionary<int, Texture2D> projectilesTextures = new();

        // FROM GAME
        public void LoadTexture(ContentManager Content)
        {
            projectilesTextures[1] = Content.Load<Texture2D>("arrow");
            projectilesTextures[2] = Content.Load<Texture2D>("spike");
            projectilesTextures[3] = Content.Load<Texture2D>("stone");
        }

        public void UpdateAll(Player player, Tower tower, Enemies enemies, int tileSize)
        {
            foreach (var projectile in projectiles)
                projectile.Update(player, tower, enemies, tileSize);
            projectiles = projectiles.Where(proj => !proj.Delete).ToList();
        }

        public void DrawAll(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            foreach (var projectile in projectiles)
                projectile.Draw(_spriteBatch, camera, tileSize, projectilesTextures[projectile.ProjectileNum]);
        }
    }
}
