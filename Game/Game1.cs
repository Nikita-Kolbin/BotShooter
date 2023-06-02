using VillageDefence;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.ComponentModel;

namespace VillageDefence
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private CurrentState gameState;

        // MAP
        private Map map;
        private const int tileSize = 64;
        private Vector2 startPosition = new Vector2(20 * tileSize, 16 * tileSize);

        // TOWER
        private Tower tower;

        //INTERFACE BAR
        private Interface Interface;

        // PLAYER
        private Player player;

        // CAMERA
        private Camera camera;

        // ENEMIES
        private Enemies enemies;

        // PROJECTILES
        private Projectiles projectiles;

        // DROP
        private Drop drop;

        // MENU

        private Menu menu;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            gameState = new CurrentState();
            
            // MAP
            map = new Map();

            // TOWER
            tower = new Tower(new Vector2(18, 13));

            // INTERFACE
            Interface = new Interface(new Vector2(0, 11));

            // PLAYER
            player = new Player(new Vector2(startPosition.X, startPosition.Y));

            // CAMERA
            camera = new Camera(new Vector2(startPosition.X - 7 * tileSize, startPosition.Y - 5 * tileSize));

            // ENEMIES
            enemies = new Enemies();

            // PROJECTILES
            projectiles = new Projectiles();

            // DROP
            drop = new Drop();

            // MENU
            menu = new Menu();

            _graphics.PreferredBackBufferWidth = 15 * tileSize;
            _graphics.PreferredBackBufferHeight = 13 * tileSize;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // MAP
            map.LoadTexture(Content);

            // TOWER
            tower.LoadTexture(Content);

            // INTERFACE
            Interface.LoadTexture(Content);

            // PLAYER
            player.LoadTexture(Content);

            // ENEMY
            enemies.LoadTexture(Content);

            // PROJECTILES
            projectiles.LoadTexture(Content);

            // DROP
            drop.LoadTexture(Content);

            // MENU
            menu.LoadTexture(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            gameState.Update(keyboardState, player, tower);

            if (gameState.State == GameState.Playing)
            {
                // PLAYER MOVE
                player.Update(keyboardState, mouseState, map, tower, enemies, camera, projectiles, tileSize);

                // CAMERA MOVE
                camera.Update(keyboardState, map, player, tileSize);

                // ENEMIES
                enemies.UpdateAll(player, tower, projectiles, drop, gameState, tileSize);

                // PROJECTILES
                projectiles.UpdateAll(player, tower, enemies, tileSize);

                // DROP
                drop.UpdateAll(player, tileSize);
            }

            if ((gameState.State == GameState.HappyEnd || gameState.State == GameState.BadEnd) && keyboardState.IsKeyDown(Keys.Space))
                Initialize();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (gameState.State == GameState.Playing)
            {
                // MAP
                map.Draw(_spriteBatch, camera, tileSize);

                // TOWER
                tower.Draw(_spriteBatch, camera, tileSize);

                drop.DrawAll(_spriteBatch, camera, tileSize);

                // ENEMIES
                enemies.DrawAll(_spriteBatch, camera, tileSize);

                // PLAYER
                player.Draw(_spriteBatch, camera, tileSize);

                // PROJECTILES
                projectiles.DrawAll(_spriteBatch, camera, tileSize);

                // MAP OBJECTS
                map.DrawObjects(_spriteBatch, camera, tileSize);

                // INTERFACE
                Interface.Draw(_spriteBatch, player, tower, tileSize, enemies.respawn.waveNum);
            }
            else if (gameState.State == GameState.Start)
                _spriteBatch.Draw(menu.start, Vector2.Zero, Color.White);
            else if (gameState.State == GameState.Info)
                _spriteBatch.Draw(menu.info, Vector2.Zero, Color.White);
            else if (gameState.State == GameState.HappyEnd)
                _spriteBatch.Draw(menu.happyEnd, Vector2.Zero, Color.White);
            else if (gameState.State == GameState.BadEnd)
                _spriteBatch.Draw(menu.badEnd, Vector2.Zero, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}