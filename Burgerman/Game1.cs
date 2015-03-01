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
        public static float groundLevel;
        private static Game1 instance;
        private readonly GraphicsDeviceManager _graphics;
        private Screen screen = Screen.AllScreens.First(e => e.Primary);
        private static int screenWidth ;
        private static int screenHeight;
        private SpriteBatch spriteBatch;
        public static Vector2 Gravity = new Vector2(0,0.1f);
        public Balloon Player { get; private set; }
        public Child Child { get; private set; }
        public Soldier Soldier { get; private set; }
        public Hut Hut { get; private set; }
        public Jet Jet { get; private set; }
        public Helicopter Helicopter { get; private set; }
        private Texture2D backgroundTexture;
        private Texture2D palmtreeTexture;
       
        public Texture2D BulletTex { get; private set; }
        public Texture2D BurgerTexture { get; private set; }
        
        
        private Random ran;

        private enum GameState { Level1, Level2, Level3 }
        private GameState gameState;

        private double _timeSinceLastTree = 0;
        private int _treeDelay = 7000;
        private int childrenFed = 0;

        public Vector2 ScreenSize
        {
            get { return new Vector2(screenWidth,screenHeight);}
        }

        private List<Sprite> DeadSprites { get; set; }
        private List<Sprite> Sprites { get; set; }
        private List<Sprite> BackgroundSprites { get; set; }
        public List<Sprite> NewSprites { get; private set; }
 
        
        private CollissionHandler CollissionHandler { get; set; }

        public static Game1 Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game1();
                }
                return instance;
            }
        }
        
        private Game1()
            : base()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            _graphics.PreferredBackBufferWidth = screen.Bounds.Width;
            _graphics.PreferredBackBufferHeight = screen.Bounds.Height;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            groundLevel = screenHeight*8/10;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprites = new List<Sprite>();
            
            BackgroundSprites = new List<Sprite>();
            NewSprites = new List<Sprite>();

           //   dikkiDinosaurTexture2D = Content.Load<Texture2D>("dikkiDinosaur.png");
            Texture2D childTexture = Content.Load<Texture2D>("child.png");
            Texture2D ballonTexture = Content.Load<Texture2D>("./balloon/balloon.png");
            Texture2D soldierTexture = Content.Load<Texture2D>("animated_soldier.png");
            Texture2D helicopterTexture = Content.Load<Texture2D>("Helicopter.png");
            Texture2D jetTexture = Content.Load<Texture2D>("./attackplane/attackplane.png");
            Texture2D mountainTexture = Content.Load<Texture2D>("mountain.png");
            Texture2D hutTexture = Content.Load<Texture2D>("hut.png");
            Texture2D groundTexture = Content.Load<Texture2D>("ground.png");

            BulletTex = Content.Load<Texture2D>("bullet.png");
            BurgerTexture = Content.Load<Texture2D>("burger.png");
            palmtreeTexture = Content.Load<Texture2D>("palm.png");
            backgroundTexture = Content.Load<Texture2D>("background.jpg");
            
            BackgroundSprite mount1 = new BackgroundSprite(mountainTexture, new Vector2(screenWidth / 5, screenHeight * 0.8f - mountainTexture.Height));
            BackgroundSprite mount2 = new BackgroundSprite(mountainTexture, new Vector2(screenWidth / 2, screenHeight * 0.8f - mountainTexture.Height));
            BackgroundSprite mount3 = new BackgroundSprite(mountainTexture, new Vector2(screenWidth / 1, screenHeight * 0.8f - mountainTexture.Height));
            Hut = new Hut(hutTexture, new Vector2(screenWidth / 2, screenHeight * 0.8f - hutTexture.Height));
            Player = new Balloon(ballonTexture, new Vector2(ballonTexture.Width, 0));
            Child = new Child(childTexture, new Vector2(0, 0));
            Child = new Child(childTexture, new Vector2(0, 0));
            Soldier = new Soldier(soldierTexture, new Vector2(screenWidth, screenHeight * 0.8f - soldierTexture.Height));
            Jet = new Jet(jetTexture, new Vector2(0,0));
            Helicopter = new Helicopter(helicopterTexture, new Vector2(screenWidth + 100, screenHeight * 2 / 10), BulletTex);
            //Sprite Burger = new Sprite(burgerTexture, new Vector2(screenWidth/2,screenHeight/2));

            //ProtoTypes.Add(Player);
            //ProtoTypes.Add(child);
            //ProtoTypes.Add(helicopter);
            //ProtoTypes.Add(Soldier);
            ran = new Random();

            BackgroundSprites.Add(mount1);
            BackgroundSprites.Add(mount2);
            BackgroundSprites.Add(mount3);

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
            //Levels: kald en levelgenerator med en liste af prototyper. Lad static metoder returnere en sprites liste
            
            
            gameState = GameState.Level1;
            Sprites = LevelConstructor.Level1(this);
          
            CollissionHandler = new CollissionHandler(Sprites);
            

            
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
        //    Console.WriteLine("Main update loop");
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
            AddNewSprites();
            RemoveDeadSprites();

            switch (gameState)
            {
                    case GameState.Level1:
                    checkLevelOneDone(gameTime);
                    break;
                    case GameState.Level2:
                    checkLevelTwoDone(gameTime);
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
            
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SpawnSpriteAtRuntime(Sprite sprite)
        {
            if (sprite != null)
            {
                NewSprites.Add(sprite);
         //       Console.WriteLine(NewSprites.Count);
            }
        }

        private void AddNewSprites()
        {
         
            foreach (Sprite sprite in NewSprites)
            {
                Sprites.Add(sprite);
                if (sprite is ICollidable)
                {
                    CollissionHandler.CollisionListenersList.Add((ICollidable)sprite);
                }
            }
            NewSprites.Clear();
        }

        internal void MarkForRemoval(Sprite sprite)
        {
            if (DeadSprites == null)
            {
                DeadSprites = new List<Sprite>();
            }
            DeadSprites.Add(sprite);
        }

        private void RemoveDeadSprites()
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
            if (childrenFed == 3)
            {
                
            }
        }

        private void checkLevelTwoDone(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

    }
}
