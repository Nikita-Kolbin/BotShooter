using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using System.Collections.Generic;

namespace VillageDefence
{
    internal class Map
    {
        private Dictionary<int, Texture2D> mapTextures = new();
        static int[,] mapElements = Map1.map;
        static int[,] objects = Map1.objects;
        public MapTile[,] mapTiles = CreateMap(mapElements);
        public MapTile[,] mapObjects = CreateMap(objects);
        public readonly int height = mapElements.GetLength(0);
        public readonly int width = mapElements.GetLength(1);

        private static MapTile[,] CreateMap(int[,] mapElements)
        {
            var result = new MapTile[mapElements.GetLength(0), mapElements.GetLength(1)];
            for(int i = 0; i < mapElements.GetLength(0); i++)
                for (int j = 0; j < mapElements.GetLength(1); j++)
                {
                    var pos = new Vector2(j, i);
                    var texNum = (int)mapElements[i, j];
                    result[i, j] = new MapTile(texNum, pos);
                }
            return result;
        }

        public int PositionToTileNum(int x, int y, int tileSize)
        {
            return mapTiles[y / tileSize, x / tileSize].textureNum;
        }

        // FROM GAME
        public void LoadTexture(ContentManager Content)
        {
            mapTextures[1] = Content.Load<Texture2D>("map1");
            mapTextures[2] = Content.Load<Texture2D>("map2");
            mapTextures[3] = Content.Load<Texture2D>("map3");
            mapTextures[4] = Content.Load<Texture2D>("map4");
            mapTextures[5] = Content.Load<Texture2D>("map5");       
            mapTextures[8] = Content.Load<Texture2D>("map_obj1");
            mapTextures[9] = Content.Load<Texture2D>("map_obj2");
            mapTextures[10] = Content.Load<Texture2D>("map_obj3");
            mapTextures[11] = Content.Load<Texture2D>("map_obj4");
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    if (mapTiles[i, j].textureNum == 0) continue;
                    var coord = camera.FindCoord(mapTiles[i, j].vector * tileSize);
                    _spriteBatch.Draw(mapTextures[mapTiles[i, j].textureNum],
                        new Rectangle((int)coord.X, (int)coord.Y, tileSize, tileSize),
                        new Rectangle(0, 0, 16, 16),
                        Color.White);
                }
        }

        public void DrawObjects(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    if (mapObjects[i, j].textureNum == 0) continue;
                    var coord = camera.FindCoord(mapObjects[i, j].vector * tileSize);
                    _spriteBatch.Draw(mapTextures[mapObjects[i, j].textureNum],
                        new Rectangle((int)coord.X, (int)coord.Y, tileSize, tileSize),
                        null,
                        Color.White);
                }
        }
    }

    public class MapTile
    {
        public readonly int textureNum;
        public readonly Vector2 vector;

        public MapTile(int texture, Vector2 vector)
        {
            this.textureNum = texture;
            this.vector = vector;
        }

    }
}
