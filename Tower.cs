using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BotShooter
{
    internal class Tower
    {
        private Dictionary<int, Texture2D> towerTextures = new();
        static int[,] towerElements = Tower1.tower;
        public Vector2 vector;
        public MapTile[,] towerTiles;
        public readonly int height;
        public readonly int width;

        public Tower(Vector2 position)
        {
            this.vector = position;
            towerTiles = CreateTower(towerElements, position);
            height = towerElements.GetLength(0);
            width = towerElements.GetLength(1);
        }

        private static MapTile[,] CreateTower(int[,] towerElements, Vector2 position)
        {
            var result = new MapTile[towerElements.GetLength(0), towerElements.GetLength(1)];
            for (int i = 0; i < towerElements.GetLength(0); i++)
                for (int j = 0; j < towerElements.GetLength(1); j++)
                {
                    var pos = new Vector2(j, i) + position;
                    var texNum = (int)towerElements[i, j];
                    result[i, j] = new MapTile(texNum, pos);
                }
            return result;
        }

        public Rectangle GetRectangle(int tileSize)
        {
            return new Rectangle((vector * tileSize).ToPoint(), new Point(tileSize * width, tileSize * height));
        }

        public void LoadTexture(ContentManager Content)
        {
            towerTextures[1] = Content.Load<Texture2D>("tower6");
            towerTextures[2] = Content.Load<Texture2D>("tower7");
            towerTextures[3] = Content.Load<Texture2D>("tower8");
            towerTextures[4] = Content.Load<Texture2D>("tower9");
            towerTextures[5] = Content.Load<Texture2D>("tower10");
            towerTextures[6] = Content.Load<Texture2D>("tower11");
            towerTextures[7] = Content.Load<Texture2D>("tower12");
            towerTextures[8] = Content.Load<Texture2D>("tower13");
            towerTextures[9] = Content.Load<Texture2D>("tower14");
            towerTextures[10] = Content.Load<Texture2D>("tower15");
            towerTextures[11] = Content.Load<Texture2D>("tower16");
            towerTextures[12] = Content.Load<Texture2D>("tower17");
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    var coord = camera.FindCoord(towerTiles[i, j].vector * tileSize);
                    _spriteBatch.Draw(towerTextures[towerTiles[i, j].textureNum],
                        new Rectangle((int)coord.X, (int)coord.Y, tileSize, tileSize),
                        new Rectangle(0, 0, 16, 16),
                        Color.White);
                }
        }
    }

    class Tower1
    {
        public static int[,] tower = new int[3, 4] {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 }
        };
    }
}
