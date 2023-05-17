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
        
        private Map map;
        private Dictionary<int, Texture2D> mapTextures = new Dictionary<int, Texture2D>();
        private const int tileSize = 64;
        private Vector2 startPosition = new Vector2(14 * tileSize, 9 * tileSize);
        private Texture2D black;

        private Player player;
        private Texture2D playerTexture;

        private Vector2 cameraMove;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            map = new Map();
            player = new Player(new Vector2(startPosition.X, startPosition.Y), 0, 100, 8);
            cameraMove = new Vector2(startPosition.X - 7 * tileSize, startPosition.Y - 5 * tileSize);

            _graphics.PreferredBackBufferWidth = 15 * tileSize;
            _graphics.PreferredBackBufferHeight = 13 * tileSize;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            mapTextures[0] = Content.Load<Texture2D>("map0");
            black = Content.Load<Texture2D>("black");

            playerTexture = Content.Load<Texture2D>("men");
            }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // PLAYER MOVE
            if (keyboardState.IsKeyDown(Keys.A) && player.position.X > 0)
                player.position.X -= player.speed;
            if (keyboardState.IsKeyDown(Keys.D) && player.position.X + tileSize < map.width * tileSize)
                player.position.X += player.speed;
            if (keyboardState.IsKeyDown(Keys.W) && player.position.Y > 0)
                player.position.Y -= player.speed;
            if (keyboardState.IsKeyDown(Keys.S) && player.position.Y + tileSize < map.height * tileSize)
                player.position.Y += player.speed;

            // CAMERA MOVE
            if (keyboardState.IsKeyDown(Keys.A) && player.position.X <= map.width * tileSize - 8 * tileSize && cameraMove.X > 0)
                cameraMove.X -= player.speed;
            if (keyboardState.IsKeyDown(Keys.D) && player.position.X >= 7 * tileSize && cameraMove.X < map.width * tileSize - 15 * tileSize)
                cameraMove.X += player.speed;
            if (keyboardState.IsKeyDown(Keys.W) && player.position.Y <= map.height * tileSize - 6 * tileSize && cameraMove.Y > 0)
                cameraMove.Y -= player.speed;
            if (keyboardState.IsKeyDown(Keys.S) && player.position.Y >= 5 * tileSize && cameraMove.Y < map.height * tileSize - 11 * tileSize)
                cameraMove.Y += player.speed;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // MAP
            for (int i = 0; i < map.mapTiles.GetLength(0); i++)
                for (int j = 0; j < map.mapTiles.GetLength(1); j++)
                    _spriteBatch.Draw(mapTextures[map.mapTiles[i, j].textureNum], 
                        new Rectangle((int)(map.mapTiles[i, j].vector.X) * tileSize - (int)cameraMove.X, (int)(map.mapTiles[i, j].vector.Y) * tileSize - (int)cameraMove.Y, tileSize, tileSize),
                        new Rectangle(0, 0, 16, 16),
                        Color.White);

            // PLAYER
            _spriteBatch.Draw(playerTexture,
                new Rectangle((int)(player.position.X) - (int)cameraMove.X, (int)(player.position.Y) - (int)cameraMove.Y, tileSize, tileSize),
                new Rectangle(0, 0, 16, 16),
                Color.White);

            // DOWN BAR
            _spriteBatch.Draw(black, new Rectangle(0, 11 * tileSize, 15 * tileSize, 2 * tileSize), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}