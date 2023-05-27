using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace BotShooter
{   
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        // MAP
        private Map map;
        private const int tileSize = 64;
        private Vector2 startPosition = new Vector2(15 * tileSize, 11 * tileSize);

        // TOWER
        private Tower tower;
        
        //INTERFACE BAR
        private Texture2D black;

        // PLAYER
        private Player player;

        // CAMERA
        private Camera camera;

        // ENEMIES
        private Enemies enemies;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // MAP
            map = new Map();

            // TOWER
            tower = new Tower(new Vector2(13, 8)); 

            // PLAYER
            player = new Player(new Vector2(startPosition.X, startPosition.Y), 0, 100, 8);

            // CAMERA
            camera = new Camera(new Vector2(startPosition.X - 7 * tileSize, startPosition.Y - 5 * tileSize));

            // ENEMIES
            enemies = new Enemies();
            for(int i = 1; i <= 8; i++)
                enemies.enemies.Add(new Enemy(i, new Vector2(10 * tileSize, 5 * tileSize) + new Vector2(i * tileSize, 0)));

            _graphics.PreferredBackBufferWidth = 15 * tileSize;
            _graphics.PreferredBackBufferHeight = 13 * tileSize;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //MAP
            map.LoadTexture(Content);
            black = Content.Load<Texture2D>("black");

            // TOWER
            tower.LoadTexture(Content);

            //PLAYER
            player.LoadTexture(Content);

            // ENEMY
            enemies.LoadTexture(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // PLAYER MOVE
            player.Move(keyboardState, map, tower, tileSize);

            // CAMERA MOVE
            camera.Move(keyboardState, map, player, tileSize);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            // MAP
            map.Draw(_spriteBatch, camera, tileSize);

            // TOWER
            tower.Draw(_spriteBatch, camera, tileSize);

            // ENEMIES
            enemies.DrawAll(_spriteBatch, camera, tileSize);

            // PLAYER
            player.Draw(_spriteBatch, camera, tileSize);

            // DOWN BAR
            _spriteBatch.Draw(black, new Rectangle(0, 11 * tileSize, 15 * tileSize, 2 * tileSize), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}