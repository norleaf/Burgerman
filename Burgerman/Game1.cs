#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private Screen screen = Screen.AllScreens.First(e => e.Primary);
        private int screenWidth ;
        private int screenHeight;
        SpriteBatch spriteBatch;
        private static Balloon player;
        private Child child;
        private Soldier soldier;
        private Helicopter helicopter;
        private Texture2D backgroundTexture;

        private static List<Sprite> _sprites; 
        private static List<Sprite> _newSprites;

        protected List<Sprite> Sprites
        {
            get { return _sprites;}
            set { _sprites = value; }
        }

        protected List<Sprite> NewSprites
        {
            get { return _newSprites; }
            set { _newSprites = value; }
        }
 
        
        private CollissionHandler CollissionHandler { get; set; }

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
          //  graphics.PreferredBackBufferWidth = 1150;
         //   graphics.PreferredBackBufferHeight = 600;
            screenWidth = screen.Bounds.Width;
            screenHeight = screen.Bounds.Height;
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
            Sprites = new List<Sprite>();
            NewSprites = new List<Sprite>();
            //   dikkiDinosaurTexture2D = Content.Load<Texture2D>("dikkiDinosaur.png");
            Texture2D childTexture = Content.Load<Texture2D>("child.png");
            Texture2D ballonTexture = Content.Load<Texture2D>("./balloon_animated/animatedballoon.png");
            Texture2D soldierTexture = Content.Load<Texture2D>("animated_soldier.png");
            Texture2D helicopterTexture = Content.Load<Texture2D>("Helicopter.png");
            Texture2D palmtreeTexture = Content.Load<Texture2D>("palm.png");
            Texture2D groundTexture = Content.Load<Texture2D>("ground.png");
            Texture2D bullet = Content.Load<Texture2D>("bullet.png");
            backgroundTexture = Content.Load<Texture2D>("background.jpg");
            
            
            player = new Balloon(ballonTexture, new Vector2(ballonTexture.Width, 0));
         //   Bullet testbullet = new Bullet(bullet, new Vector2(screenWidth, screenHeight / 2), player.Position);
            child = new Child(childTexture, new Vector2(410, screenHeight*0.8f - childTexture.Height), player);
            soldier = new Soldier(soldierTexture, new Vector2(screenWidth, screenHeight * 0.8f - soldierTexture.Height));
            helicopter = new Helicopter(helicopterTexture, new Vector2(1200, 130),bullet);
            Random ran = new Random();
            for (int i = 0; i < 10; i++)
            {
                float scale = ((float)ran.Next(7, 11) / 10);
                PalmTree palm = new PalmTree(palmtreeTexture, new Vector2(ran.Next(1920), screenHeight * 0.8f - palmtreeTexture.Height*scale));
                palm.Scale = scale;
                Sprites.Add(palm);
            }
            for (int i = 0; i < screenWidth/30+1; i++)
            {
                Sprites.Add(new Sprite(groundTexture,new Vector2(30*i, screenHeight * 0.8f)));
            }

     //       Sprites.Add(testbullet);
            Sprites.Add(player);
            Sprites.Add(child);
            Sprites.Add(soldier);
            Sprites.Add(helicopter);

          
            CollissionHandler = new CollissionHandler(Sprites);
            CollissionHandler.CollisionListenersList.Add(child);
            CollissionHandler.CollisionListenersList.Add(player);
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
            CollissionHandler.Update(gameTime);
            addNewSprites();
        //    Console.WriteLine("size of sprites list: " + Sprites.Count);
            foreach (Sprite sprite in Sprites)
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
            spriteBatch.Draw(backgroundTexture,new Vector2(0,0),null,null,new Vector2(0,0),0f,new Vector2(1920,1));
          
            foreach (Sprite sprite in Sprites)
            {
                sprite.Draw(gameTime,spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SpawnSpriteAtRuntime(Sprite sprite)
        {
            if (sprite != null)
            {
                if (NewSprites == null)
                {
                    NewSprites = new List<Sprite>();
                }
                NewSprites.Add(sprite);
                Console.WriteLine("sprite added");
                Console.WriteLine("size of newSprites list: " + NewSprites.Count);
            }
        }

        private void addNewSprites()
        {
            Console.WriteLine("size of newSprites list: " + NewSprites.Count);
            foreach (Sprite sprite in _newSprites)
            {
                Sprites.Add(sprite);
                CollissionHandler.AllElements.Add(sprite);
            }
            NewSprites.Clear();
           // Console.WriteLine("new sprites cleared");
        }

        public Balloon getPlayer()
        {
            return player;
        }
    }
}
