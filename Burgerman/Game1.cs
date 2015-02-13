#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

#endregion

namespace Burgerman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private static Game1 instance;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Balloon player;
        private Child child;
        private Soldier soldier;
        private Helicopter helicopter;
        public static List<Sprite> sprites { get; set; } 
        
        private CollissionHandler collissionHandler;

        private float terrainInclination;

        public static Game1 getInstance()
        {
            if (instance == null)
            {
                instance = new Game1();
            }
            return instance;
        }

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1150;
            graphics.PreferredBackBufferHeight = 600;
            var screen = Screen.AllScreens.First(e => e.Primary);
            Window.IsBorderless = true;
            Window.Position = new Point(screen.Bounds.X, screen.Bounds.Y);
            graphics.PreferredBackBufferWidth = screen.Bounds.Width;
            graphics.PreferredBackBufferHeight = screen.Bounds.Height;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sprites = new List<Sprite>();
            //   dikkiDinosaurTexture2D = Content.Load<Texture2D>("dikkiDinosaur.png");
            Texture2D childTexture = Content.Load<Texture2D>("child.png");
            Texture2D ballonTexture = Content.Load<Texture2D>("./balloon_animated/animatedballoon.png");
            Texture2D soldierTexture = Content.Load<Texture2D>("animated_soldier.png");
            Texture2D helicopterTexture = Content.Load<Texture2D>("Helicopter.png");
            
            player = new Balloon(ballonTexture, new Vector2(550, 130));
            child = new Child(childTexture, new Vector2(410, 550), player);
            soldier = new Soldier(soldierTexture, new Vector2(700,550));
            helicopter = new Helicopter(helicopterTexture, new Vector2(1200, 130));
            sprites.Add(player);
            sprites.Add(child);
            sprites.Add(soldier);
            sprites.Add(helicopter);
            collissionHandler = new CollissionHandler(sprites);
            collissionHandler.CollisionListenersList.Add(child);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            collissionHandler.Update(gameTime);
            foreach (Sprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.Draw(dikkiDinosaurTexture2D, dikkiDinosaurPosition, Color.White);
            foreach (Sprite sprite in sprites)
            {
                sprite.Draw(gameTime,spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
