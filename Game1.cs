using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace StreamGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GameState gameState = GameState.StartMenu;
        public List<Tile> tiles = new List<Tile>();
        
        public enum GameState {
            StartMenu, 
            PauseMenu,
            InGame
        }


        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            base.Initialize();
            Player.changeSizes(30,60);
            tiles.Add(new Tile(-75 + (-1 * 75), -75 + (-1 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (0 * 75), -75 + (0 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (3 * 75), -75 + (1 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (5 * 75), -75 + (2 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (6 * 75), -75 + (3 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (7 * 75), -75 + (4 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (9 * 75), -75 + (5 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (10 * 75), -75 + (4 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));
            tiles.Add(new Tile(-75 + (12 * 75), -75 + (5 * 75), 75, 75, Content.Load<Texture2D>("Basic Tile")));

        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.sprite = Content.Load<Texture2D>("Character");
            
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouseInput = Mouse.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.D) && Player.dashingCooldown - Player.dTime <= 0)
            {
                Player.xVelocity = Player.walkingSpeed;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && Player.dashingCooldown - Player.dTime <= 0)
            {
                Player.xVelocity = -Player.walkingSpeed;
            }
           
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Player.attemptDash();
            }
            Player.commenceDash();
            Player.onGround = false;
            for (int i = 0; i < tiles.Count; i++)
            {
                Tile t = tiles[i];
                t.groundCheck();
                
            }


            if (!Player.onGround)
            {
                Player.timeOnGround = 0;
                if (Player.maxGravitySpeed > Player.timeInAir)
                {
                    Player.timeInAir += 0.5f;
                }
                
            }
            else
            {
                Player.yVelocity = 0;
                Player.timeInAir = 0;
                Player.timeOnGround += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Player.attemptJump();
                }
                
            }
            
            

            Player.dashingCooldown -= 1;
            Player.changePosition(Player.x + Player.xVelocity, Player.y - Player.timeInAir + Player.yVelocity);
            Player.xVelocity = Player.xVelocity / 1.5f;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null);
            _spriteBatch.Draw(Player.sprite, new Vector2(GraphicsDevice.DisplayMode.Width / 2, GraphicsDevice.DisplayMode.Height / 2), null, Color.White, 0, new Vector2(0, 0), new Vector2(Player.widthRatio, Player.heightRatio), SpriteEffects.None, 1);
            _spriteBatch.Draw(Player.sprite, new Vector2(-20, -20), null, Color.White, 0, new Vector2(0, 0), new Vector2(Player.widthRatio, Player.heightRatio), SpriteEffects.None, 1);

            for (int i = 0; i < tiles.Count; i++)
            {
                Tile t = tiles[i];
                _spriteBatch.Draw(t.sprite, new Vector2(t.x - Player.x + GraphicsDevice.DisplayMode.Width / 2, -t.y + Player.y + GraphicsDevice.DisplayMode.Height / 2), null, Color.White, 0, new Vector2(0, 0), new Vector2(t.widthRatio, t.heightRatio), SpriteEffects.None, 1);

            }
            base.Draw(gameTime);
            _spriteBatch.End();
        }

    }
}