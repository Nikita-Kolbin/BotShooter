using VillageDefence.Weapon;
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

namespace VillageDefence
{
    internal class Player
    {
        private Texture2D playerTexture;
        public Vector2 position;
        public int weapon;
        public Knife knife;
        public Bow bow;
        public readonly int maxHp;
        public int hp;
        public int speed;
        public Timer healReload;
        public Timer heal;

        public Player(Vector2 position)
        {
            this.position = position;
            this.weapon = 1;
            hp = 100;
            maxHp = 100;
            this.speed = 8;
            knife = new Knife();
            bow = new Bow();
            healReload = new Timer(5);
            heal = new Timer(1);
        }

        public void Move(KeyboardState keyboardState, Map map, Tower tower, int tileSize)
        {
            var moveVector = new Vector2(0, 0);

            if (keyboardState.IsKeyDown(Keys.A) && 
                position.X > 0 && 
                map.PositionToTileNum((int)position.X - speed, (int)position.Y, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X - speed, (int)position.Y), new Point(tileSize, tileSize)))
                )
                moveVector.X -= speed;
            
            if (keyboardState.IsKeyDown(Keys.D) && 
                position.X + tileSize < map.width * tileSize && 
                map.PositionToTileNum((int)position.X + speed, (int)position.Y, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X + speed, (int)position.Y), new Point(tileSize, tileSize)))
                )
                moveVector.X += speed;
            
            if (keyboardState.IsKeyDown(Keys.W) && 
                position.Y > 0 && 
                map.PositionToTileNum((int)position.X, (int)position.Y - speed, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X, (int)position.Y - speed), new Point(tileSize, tileSize)))
                )
                moveVector.Y -= speed;
           
            if (keyboardState.IsKeyDown(Keys.S) && 
                position.Y + tileSize < map.height * tileSize && 
                map.PositionToTileNum((int)position.X, (int)position.Y + speed, tileSize) != 4 &&
                !tower.GetRectangle(tileSize).Intersects(new Rectangle(new Point((int)position.X, (int)position.Y + speed), new Point(tileSize, tileSize)))
                )
                moveVector.Y += speed;

            if (moveVector.X != 0 && moveVector.Y != 0)
                moveVector /= (float)Math.Sqrt(2.0);

            position += moveVector;
        }

        public void CheckWeapon(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.D1))
                weapon = 1;
            if (keyboardState.IsKeyDown(Keys.D2))
                weapon = 2;
        }

        public void Attack(MouseState mouseState, Enemies enemies, Camera camera, Projectiles projectiles, int tileSize)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
                if (weapon == 1)
                    knife.Attack(mouseState, this, enemies, camera, tileSize);
                else
                    bow.Attack(mouseState, this, projectiles, camera, tileSize);
        }

        public void HealUpdate()
        {
            if(healReload.State && heal.State && hp < 100)
            {
                hp += 1;
                heal.Start();
            }
            healReload.Update();
            heal.Update();
        }

        public void TakeDamage(int damage)
        {
            hp -= damage;
            healReload.Start();
        }

        public Rectangle GetRectangle(int tileSize)
        {
            return new Rectangle(position.ToPoint(), new Point(tileSize, tileSize));
        }

        public Vector2 GetCenterPosition(int tileSize)
        {
            return position + new Vector2(tileSize / 2, tileSize / 2);
        }

        // FROM GAME      
        public void LoadTexture(ContentManager Content)
        {
            playerTexture = Content.Load<Texture2D>("player1");
            Knife.texture = Content.Load<Texture2D>("knife_hit");
            Bow.texture = Content.Load<Texture2D>("bow");
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState, Map map, Tower tower, Enemies enemies,Camera camera, Projectiles projectiles, int tileSize)
        {
            Move(keyboardState, map, tower, tileSize);
            CheckWeapon(keyboardState);
            Attack(mouseState, enemies, camera, projectiles, tileSize);
            HealUpdate();
            knife.Update();
            bow.Update();
        }

        public void Draw(SpriteBatch _spriteBatch, Camera camera, int tileSize)
        {
            var playerCoord = camera.FindCoord(position);

            
            _spriteBatch.Draw(playerTexture,
                new Rectangle((int)playerCoord.X, (int)playerCoord.Y, tileSize, tileSize),
                new Rectangle(0, 0, 16, 16),
                Color.White);

            knife.Draw(_spriteBatch, playerCoord, tileSize);
            
            if (weapon == 2)
                bow.Draw(_spriteBatch, playerCoord, tileSize);
            
        }
    }
}
