using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VillageDefence
{
    internal class Interface
    {
        public Vector2 vector;
        public Texture2D backgroundTexture;
        public Texture2D hpBar;
        public Texture2D towerBar;
        public Texture2D zeroBar;
        public Texture2D weapon1;
        public Texture2D weapon2;
        public SpriteFont font;
        public SpriteFont waveFont;

        public Interface(Vector2 vector)
        {
            this.vector = vector;
        }

        // FROM GAME
        public void LoadTexture(ContentManager Content)
        {
            backgroundTexture = Content.Load<Texture2D>("v3");
            weapon1 = Content.Load<Texture2D>("weapon1");
            weapon2 = Content.Load<Texture2D>("weapon2");
            font = Content.Load<SpriteFont>("font1");
            waveFont = Content.Load<SpriteFont>("Wave_font");
            zeroBar = Content.Load<Texture2D>("zero_bar");
            hpBar = Content.Load<Texture2D>("hp_bar");
            towerBar = Content.Load<Texture2D>("tower_bar");
        }

        public void Draw(SpriteBatch _spriteBatch, Player player, Tower tower, int tileSize, int waveNum)
        {
            _spriteBatch.Draw(backgroundTexture, vector * tileSize, Color.White);
            
            if (player.weapon == 1)
                _spriteBatch.Draw(weapon1, vector * tileSize, Color.White);
            if (player.weapon == 2)
                _spriteBatch.Draw(weapon2, vector * tileSize, Color.White);

            var hpPlayerPos = vector * tileSize + new Vector2(10 * tileSize, 0.2f * tileSize);
            var hpTowerPos = vector * tileSize + new Vector2(10 * tileSize, 1.1f * tileSize);
            _spriteBatch.Draw(zeroBar, hpPlayerPos, Color.White);
            _spriteBatch.Draw(zeroBar, hpTowerPos, Color.White);
            _spriteBatch.Draw(hpBar, hpPlayerPos, new Rectangle(0, 0, (int)(hpBar.Width * ((double)player.hp / player.maxHp)), hpBar.Height), Color.White);
            _spriteBatch.Draw(towerBar, hpTowerPos, new Rectangle(0, 0, (int)(towerBar.Width * ((double)tower.hp / tower.maxHp)), towerBar.Height), Color.White);

            _spriteBatch.DrawString(font, player.hp.ToString(), new Vector2(11.9f * tileSize, 11.25f * tileSize), Color.Black);
            _spriteBatch.DrawString(font, tower.hp.ToString(), new Vector2(11.8f * tileSize, 12.15f * tileSize), Color.Black);
            
            _spriteBatch.DrawString(font, player.bow.arrowCount.ToString(), new Vector2(4.75f * tileSize, 12.25f * tileSize), Color.Black);

            _spriteBatch.Draw(backgroundTexture, 
                new Rectangle((int)(0.7 * tileSize), (int)(11.25 * tileSize), (int)((tileSize * 3 / 2) * (1 - player.knife.reload.GetPercent())), tileSize * 3 / 2), 
                Color.Black * 0.5f);

            _spriteBatch.Draw(backgroundTexture,
                new Rectangle((int)(2.53 * tileSize), (int)(11.25 * tileSize), (int)((tileSize * 3 / 2) * (1 - player.bow.reload.GetPercent())), tileSize * 3 / 2),
                Color.Black * 0.5f);

            _spriteBatch.DrawString(waveFont, "Wave " + (waveNum + 1).ToString(), new Vector2(12.8f * tileSize, 5), Color.Red);
        }
    }
}
