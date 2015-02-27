#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
        private static int screenWidth ;
        private static int screenHeight;
        SpriteBatch spriteBatch;
        private static Balloon player;
        private Child child;
        private Soldier soldier;
        private Helicopter helicopter;
        private Texture2D backgroundTexture;
        public Texture2D palmtreeTexture;
        public Texture2D childTexture;
        public Texture2D ballonTexture;
        public Texture2D soldierTexture;
        public Texture2D helicopterTexture;
        public Texture2D mountainTexture;
        public Texture2D hutTexture;
        public Texture2D groundTexture;
        public Texture2D bullet;
        public Texture2D burgerTexture;
        private Random ran;
        public Dictionary<String, Texture2D> TextureDictionary { set; get; }

        private enum GameState { Level1, Level2, Level3 }
        
        
        private GameState gameState;

        private static List<Sprite> _sprites;
        
        private static List<Sprite> _newSprites;
        private double _timeSinceLastTree = 0;
        private int _treeDelay = 7000;

        public Vector2 ScreenSize
        {
            get { return new Vector2(screenWidth,screenHeight);}
        }

        protected static List<Sprite> DeadSprites { get; set; }

        protected List<Sprite> Sprites1 { get; set; }
        protected List<Sprite> Sprites2 { get; set; }
        protected List<Sprite> Sprites3 { get; set; }
        protected List<Sprite> Sprites
        {
            get { return _sprites;}
            set { _sprites = value; }
        }

        protected List<Sprite> BackgroundSprites { get; set; }

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
            Sprites1 = new List<Sprite>();
            Sprites2 = new List<Sprite>();
            BackgroundSprites = new List<Sprite>();
            NewSprites = new List<Sprite>();
            //   dikkiDinosaurTexture2D = Content.Load<Texture2D>("dikkiDinosaur.png");
            childTexture = Content.Load<Texture2D>("child.png");
            ballonTexture = Content.Load<Texture2D>("./balloon/balloon.png");
            soldierTexture = Content.Load<Texture2D>("animated_soldier.png");
            helicopterTexture = Content.Load<Texture2D>("Helicopter.png");
            mountainTexture = Content.Load<Texture2D>("mountain.png");
            hutTexture = Content.Load<Texture2D>("hut.png");
            palmtreeTexture = Content.Load<Texture2D>("palm.png");
            groundTexture = Content.Load<Texture2D>("ground.png");
            bullet = Content.Load<Texture2D>("bullet.png");
            burgerTexture = Content.Load<Texture2D>("burger.png");
            backgroundTexture = Content.Load<Texture2D>("background.jpg");
            
            BackgroundSprite mount1 = new BackgroundSprite(mountainTexture, new Vector2(screenWidth / 5, screenHeight * 0.8f - mountainTexture.Height));
            BackgroundSprite mount2 = new BackgroundSprite(mountainTexture, new Vector2(screenWidth / 2, screenHeight * 0.8f - mountainTexture.Height));
            BackgroundSprite mount3 = new BackgroundSprite(mountainTexture, new Vector2(screenWidth / 1, screenHeight * 0.8f - mountainTexture.Height));
            PalmTree hut = new PalmTree(hutTexture, new Vector2(screenWidth / 2, screenHeight * 0.8f - hutTexture.Height));
            player = new Balloon(ballonTexture, new Vector2(ballonTexture.Width, 0));
            child = new Child(childTexture, new Vector2(screenWidth/2.0f, screenHeight*0.8f - childTexture.Height));
            soldier = new Soldier(soldierTexture, new Vector2(screenWidth, screenHeight * 0.8f - soldierTexture.Height));
            helicopter = new Helicopter(helicopterTexture, new Vector2(screenWidth+100, screenHeight * 2/10),bullet);
            Sprite burger = new Sprite(burgerTexture, new Vector2(screenWidth/2,screenHeight/2));
            ran = new Random();

            BackgroundSprites.Add(mount1);
            BackgroundSprites.Add(mount2);
            BackgroundSprites.Add(mount3);
            BackgroundSprites.Add(hut);

            for (int i = 0; i < 15; i++)
            {
                float scale = ((float)ran.Next(7, 11) / 10);
                int offset = ran.Next(screenWidth + 100);
                BackgroundSprites.Add(PalmTree.MakeNewTree(palmtreeTexture, scale, offset));
            }
            for (int i = 0; i < screenWidth/30+3; i++)
            {
                BackgroundSprites.Add(new Ground(groundTexture, new Vector2(30 * i, screenHeight * 0.8f)));
            }

            
            Sprites1.Add(player);
            Sprites1.Add(soldier);
            Sprites1.Add(helicopter);
            Sprites1.Add(child);


            Sprites2.Add(helicopter.CloneAt(screenWidth,screenHeight*0.1f));
            Sprites2.Add(helicopter.CloneAt(screenWidth*1.1f, screenHeight * 0.3f));
            

            gameState = GameState.Level1;
            Sprites = Sprites1;
          
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
        /// 
        /// 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            CollissionHandler.Update(gameTime);
            if (gameTime.TotalGameTime.TotalMilliseconds > _timeSinceLastTree + _treeDelay)
            {
                float scale = ((float)ran.Next(7, 11) / 10);
                int offset = screenWidth + ran.Next(200);
                BackgroundSprites.Add(PalmTree.MakeNewTree(palmtreeTexture, scale, offset));
                _timeSinceLastTree = gameTime.TotalGameTime.TotalMilliseconds;
                _treeDelay = 3500 + ran.Next(3500);
            }
            addNewSprites();
            removeDeadSprites();
            Console.WriteLine("size of sprites list: " + Sprites.Count);

            switch (gameState)
            {
                    case GameState.Level1:
                    checkLevelOneDone(gameTime);
                    break;
                    case GameState.Level2:
                    
                    break;
            }
            foreach (Sprite sprite in Sprites)
            {
                sprite.Update(gameTime);
            }
            
            foreach (Sprite sprite in BackgroundSprites)
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
            spriteBatch.Draw(backgroundTexture,new Vector2(0,0),null,null,new Vector2(0,0),0f,new Vector2(1920,1));


            foreach (Sprite sprite in BackgroundSprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }
            foreach (Sprite sprite in Sprites)
            {
                sprite.Draw(gameTime, spriteBatch);
            }
            switch (gameState)
            {
                case GameState.Level1:
                {
                     break;
                }
                case GameState.Level2:
                {
                    
                    break;
                }
                case GameState.Level3:
                {
                    
                    break;
                }
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
        
            }
        }

        private void addNewSprites()
        {
        //    Console.WriteLine("size of newSprites list: " + NewSprites.Count);
            foreach (Sprite sprite in NewSprites)
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

        internal void markForRemoval(Sprite sprite)
        {
            if (DeadSprites == null)
            {
                DeadSprites = new List<Sprite>();
            }
            DeadSprites.Add(sprite);
        }

        private void removeDeadSprites()
        {
            if (DeadSprites != null)
            {
                foreach (Sprite deadSprite in DeadSprites)
                {
                    Sprites.Remove(deadSprite);
                    BackgroundSprites.Remove(deadSprite);
                }
                DeadSprites.Clear();
            }
        }

        private void checkLevelOneDone(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > 10000)
            {
                gameState = GameState.Level2;
                Sprites = Sprites2;
                Sprites.Add(player);
                int i = 5;
            }
        }
    }
}
