using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;

namespace BotShooter
{
    public class Map
    {
        static int[,] mapElements = new int[20, 30];
        public MapTile[,] mapTiles = CreateMap(mapElements);
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
