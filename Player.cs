using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotShooter
{
    internal class Player
    {
        private Texture2D playerTexture;
        public Vector2 position;
        public Weapon weapon;
        public int hp;
        public int speed;

        public Player(Vector2 position, Weapon weapon, int hp, int speed)
        {
            this.position = position;
            this.weapon = weapon;
            this.hp = hp;
            this.speed = speed;
        }

        public void Move(KeyboardState keyboardState, Map map, Tower tower, int tileSize)
        {
            if (keyboardState.IsKeyDown(Keys.A) && 
                position.X > 0 && 
                map.PositionToTileNum((int)position.X - speed, (int)position.Y, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X - speed, (int)position.Y), new Point(tileSize, tileSize)))
                )
                position.X -= speed;
            
            if (keyboardState.IsKeyDown(Keys.D) && 
                position.X + tileSize < map.width * tileSize && 
                map.PositionToTileNum((int)position.X + speed, (int)position.Y, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X + speed, (int)position.Y), new Point(tileSize, tileSize)))
                )
                position.X += speed;
            
            if (keyboardState.IsKeyDown(Keys.W) && 
                position.Y > 0 && 
                map.PositionToTileNum((int)position.X, (int)position.Y - speed, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X, (int)position.Y - speed), new Point(tileSize, tileSize)))
                )
                position.Y -= speed;
           
            if (keyboardState.IsKeyDown(Keys.S) && 
                position.Y + tileSize < map.height * tileSize && 
                map.PositionToTileNum((int)position.X, (int)position.Y + speed, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X, (int)position.Y + speed), new Point(tileSize, tileSize)))
                )
                position.Y += speed;
        }

        public Rectangle GetRectangle(int tileSize)
        {
            return new Rectangle(position.ToPoint(), new Point(tileSize, tileSize));
        }

        public void LoadTexture(ContentManager Content)
        {
            playerTexture = Content.Load<Texture2D>("player1");
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            var playerCoord = camera.FindCoord(position);
            _spriteBatch.Draw(playerTexture,
                new Rectangle((int)playerCoord.X, (int)playerCoord.Y, tileSize, tileSize),
                new Rectangle(0, 0, 16, 16),
                Color.White);
        }
    }

    enum Weapon
    {
        knife = 0,
    }
}
