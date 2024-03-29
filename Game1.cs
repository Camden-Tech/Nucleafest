﻿using Microsoft.Xna.Framework;
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
        public static HashMap<String, Texture2d> textures = new HashMap<String, Texture2d>();
        
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
            tiles.Add(new Tile(-75 + (-1 * 75), -75 + (-1 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (0 * 75), -75 + (0 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (3 * 75), -75 + (1 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (5 * 75), -75 + (2 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (6 * 75), -75 + (3 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (7 * 75), -75 + (4 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (9 * 75), -75 + (5 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (10 * 75), -75 + (4 * 75), 75, 75, "Basic Tile"));
            tiles.Add(new Tile(-75 + (12 * 75), -75 + (5 * 75), 75, 75, "Basic Tile"));
            Player.sprite = "Character";

        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            textures.put("Character", Content.Load<Texture2D>("Character"));
            textures.put("Basic Tile", Content.Load<Texture2D>("Basic Tile"));
            
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Player.updatePlayer();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null);
            _spriteBatch.Draw(textures.get(Player.sprite), new Vector2(GraphicsDevice.DisplayMode.Width / 2, GraphicsDevice.DisplayMode.Height / 2), null, Color.White, 0, new Vector2(0, 0), new Vector2(Player.widthRatio, Player.heightRatio), SpriteEffects.None, 1);
            _spriteBatch.Draw(textures.get(Player.sprite), new Vector2(-20, -20), null, Color.White, 0, new Vector2(0, 0), new Vector2(Player.widthRatio, Player.heightRatio), SpriteEffects.None, 1);

            for (int i = 0; i < tiles.Count; i++)
            {
                Tile t = tiles[i];
                _spriteBatch.Draw(textures.get(t.sprite), new Vector2(t.x - Player.x + GraphicsDevice.DisplayMode.Width / 2, -t.y + Player.y + GraphicsDevice.DisplayMode.Height / 2), null, Color.White, 0, new Vector2(0, 0), new Vector2(t.widthRatio, t.heightRatio), SpriteEffects.None, 1);

            }
            base.Draw(gameTime);
            _spriteBatch.End();
        }

    }
}
