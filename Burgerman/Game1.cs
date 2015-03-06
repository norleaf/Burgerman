#region Using Statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        public static float GroundLevel;
        private static Game1 _instance;
        private readonly GraphicsDeviceManager _graphics;
        private Screen screen = Screen.AllScreens.First(e => e.Primary);
        public SoundEffect ShotSound { get; set; }
        private SpriteBatch _spriteBatch;
        public static Vector2 Gravity = new Vector2(0,0.1f);
        public Balloon Player { get; private set; }
        public bool PlayerDead { get; set; }
        public bool ChildDead { get; set; }
        public Child Child { get; private set; }
        public Soldier Soldier { get; private set; }
        public Hut Hut { get; private set; }
        public Jet Jet { get; private set; }
        public Helicopter Helicopter { get; private set; }
        public Cow Cow { get; private set; }
        private Texture2D _backgroundTexture;
        private Texture2D _palmtreeTexture;
        public Texture2D BulletTex { get; private set; }
        public Texture2D ChildDeathTexture { get; private set; }
        public Texture2D BalloonDeathTexture { get; private set; }
        public Texture2D BurgerTexture { get; private set; }
        public Texture2D HeadTexture { get; private set; }
        public Texture2D HeadOKTexture { get; private set; }
        public Texture2D HeadDEADTexture { get; private set; }
        private Sprite _background; 
        private IntroBalloon _introBalloon;
        private SpriteFont _font;
        

        

        public enum GameState { Intro, Pause, Level1, Level2, Level3 }
        public GameState State { get; private set; }
        private GameState _currentLevel;
        private bool _restarting;
        private bool _justpressed;
        
        public int ChildrenFed { get; set; }
        public int ChildrenFedGoal { get; set; }
        public int ChildrenDied { get; set; }
        public int ChildrenTotal { get; set; }

       
        private List<Sprite> DeadSprites { get; set; }
        private List<Sprite> Sprites { get; set; }
        private List<Sprite> BackgroundSprites { get; set; }
        public List<Sprite> NewSprites { get; private set; }
        private CollissionHandler CollissionHandler { get; set; }
        public Vector2 ScreenSize { get; private set; }
        private Random _ran;
        public string ScreenText { get; set; }
        public double TextDuration { get; set; }
        private bool _newText;
        private double _restartTime;
        private double _timeSinceLastTree;
        private int _treeDelay = 7000;

        public static Game1 Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Game1();
                }
                return _instance;
            }
        }
        
        private Game1()
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
            ScreenSize = new Vector2(x: screen.Bounds.Width, y: screen.Bounds.Height);
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
            GroundLevel = ScreenSize.Y*0.8f;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprites = new List<Sprite>();
            DeadSprites = new List<Sprite>();
            BackgroundSprites = new List<Sprite>();
            NewSprites = new List<Sprite>();

            ShotSound = Content.Load<SoundEffect>("./sounds/shot");
           

            //_font = Content.Load<SpriteFont>()
            Texture2D titleTexture = Content.Load<Texture2D>("title");
            Sprite intro = new Sprite(titleTexture,new Vector2(ScreenSize.X * 0.4f,ScreenSize.X * 0.1f));
            Texture2D introBalloonTexture2D = Content.Load<Texture2D>("introBalloon");
            _introBalloon = new IntroBalloon(introBalloonTexture2D, new Vector2(50,ScreenSize.Y));

            Texture2D childTexture = Content.Load<Texture2D>("child");
            Texture2D ballonTexture = Content.Load<Texture2D>("./balloon/balloon");
            Texture2D soldierTexture = Content.Load<Texture2D>("animated_soldier");
            Texture2D helicopterTexture = Content.Load<Texture2D>("Helicopter.png");
            Texture2D jetTexture = Content.Load<Texture2D>("./attackplane/attackplane");
            Texture2D mountainTexture = Content.Load<Texture2D>("mountain");
            Texture2D hutTexture = Content.Load<Texture2D>("hut");
            Texture2D groundTexture = Content.Load<Texture2D>("ground");
            Texture2D cowTexture = Content.Load<Texture2D>("cow");

            ChildDeathTexture = Content.Load<Texture2D>("diechild");
            BalloonDeathTexture = Content.Load<Texture2D>("./balloon/balloonburning");

            BulletTex = Content.Load<Texture2D>("bullet");
            BurgerTexture = Content.Load<Texture2D>("burger");
            HeadTexture = Content.Load<Texture2D>("childhead");
            HeadOKTexture = Content.Load<Texture2D>("childheadOK");
            HeadDEADTexture = Content.Load<Texture2D>("childheadDEAD");
            _palmtreeTexture = Content.Load<Texture2D>("palm");
            _backgroundTexture = Content.Load<Texture2D>("background");
            
            
            _background = new Sprite(_backgroundTexture,new Vector2(0,0));

            BackgroundSprite mount1 = new BackgroundSprite(mountainTexture, new Vector2(x: ScreenSize.X / 5f, y: ScreenSize.Y * 0.8f - mountainTexture.Height));
            BackgroundSprite mount2 = new BackgroundSprite(mountainTexture, new Vector2(x: ScreenSize.X / 2f, y: ScreenSize.Y * 0.8f - mountainTexture.Height));
            BackgroundSprite mount3 = new BackgroundSprite(mountainTexture, new Vector2(x: ScreenSize.X / 1f, y: ScreenSize.Y * 0.8f - mountainTexture.Height));
            Hut = new Hut(hutTexture, new Vector2(ScreenSize.X / 2f, ScreenSize.Y * 0.8f - hutTexture.Height));
            Player = new Balloon(ballonTexture, new Vector2(0, 0));
            Child = new Child(childTexture, new Vector2(0, 0));
            Soldier = new Soldier(spriteTexture: soldierTexture, position: new Vector2(ScreenSize.X, ScreenSize.Y * 0.8f - soldierTexture.Height));
            Jet = new Jet(jetTexture, new Vector2(0,0));
            Helicopter = new Helicopter(helicopterTexture, new Vector2(ScreenSize.X + 100, ScreenSize.Y * 0.2f));
            Cow = new Cow(cowTexture,new Vector2(0,0));

            _ran = new Random();

            BackgroundSprites.Add(mount1);
            BackgroundSprites.Add(mount2);
            BackgroundSprites.Add(mount3);

            for (int i = 0; i < 15; i++)
            {
                float scale = ((float)_ran.Next(7, 11) / 10);
                int offset = _ran.Next((int)ScreenSize.X + 100);
                BackgroundSprites.Add(PalmTree.MakeNewTree(_palmtreeTexture, scale, offset));
            }
            for (int i = 0; i < ScreenSize.X/30+3; i++)
            {
                BackgroundSprites.Add(new Ground(groundTexture, new Vector2(30 * i, ScreenSize.Y * 0.8f)));
            }

            State = GameState.Intro;
            Sprites.Add(_introBalloon);
            Sprites.Add(intro);

            
            //Levels: kald en levelgenerator med static metoder som returnerer en sprites liste
           // Sprites = LevelConstructor.Level1(this);
            CollissionHandler = new CollissionHandler(Sprites);

            _font = Content.Load<SpriteFont>("superfont");

            //Restart();
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

            if (State == GameState.Intro) 
            {
                _introBalloon.Update(gameTime);
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    State = GameState.Level1;
                    Restart();
                    _justpressed = false;
                }
            }
            
            if (State != GameState.Intro && _justpressed &&
                (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                 Keyboard.GetState().IsKeyDown(Keys.Space)))
            {

                if (State != GameState.Pause)
                {
                    CreateTextMessage("Paused", 1000);
                    _currentLevel = State;
                    State = GameState.Pause;
                }
                else
                {
                    State = _currentLevel;
                }
                _justpressed = false;
            }
            
            if (Keyboard.GetState().IsKeyDown(Keys.End) && _justpressed)
            {
                if (State == GameState.Level2){ 
                    State = GameState.Level3;
                    Restart();
                }
                if (State == GameState.Level1)
                {
                    State = GameState.Level2;
                    Restart();
                }
                _justpressed = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.End) && Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                _justpressed = true;
            }

            // TODO: Add your update logic here
            
            if (State != GameState.Pause) { 
            CollissionHandler.Update(gameTime);
            if (gameTime.TotalGameTime.TotalMilliseconds > _timeSinceLastTree + _treeDelay)
            {
                float scale = ((float)_ran.Next(7, 11) / 10);
                int offset = (int)ScreenSize.X + _ran.Next(200);
                BackgroundSprites.Add(PalmTree.MakeNewTree(_palmtreeTexture, scale, offset));
                _timeSinceLastTree = gameTime.TotalGameTime.TotalMilliseconds;
                _treeDelay = 3500 + _ran.Next(3500);
            }
            AddNewSprites();
            RemoveDeadSprites();
            CheckLevelDone(gameTime);

            foreach (Sprite sprite in Sprites)
            {
              
                sprite.Update(gameTime);
            }
            
            foreach (Sprite sprite in BackgroundSprites)
            {
                sprite.Update(gameTime);
            }
            
            }
            if (_newText)
            {
                TextDuration += gameTime.TotalGameTime.TotalMilliseconds;
                _newText = false;
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
            _spriteBatch.Begin();
           // _spriteBatch.Draw(_backgroundTexture, position: new Vector2(0,0), drawRectangle: null, sourceRectangle: null, origin: new Vector2(0,0), rotation: 0f, scale: new Vector2(1920,1));

            if (State != GameState.Intro)
            {
                for (int i = 0; i < ScreenSize.X; i++)
                {
                    _background.Position = new Vector2(1*i, 0);
                    _background.Draw(_spriteBatch);
                }
                foreach (Sprite sprite in BackgroundSprites)
                {
                    sprite.Draw(_spriteBatch);
                }
                DrawHUD();
            }
            DrawSprites();
            //Only draw text until the time set
            if (gameTime.TotalGameTime.TotalMilliseconds < TextDuration)
            {
                DrawText(_spriteBatch);
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawHUD()
        {
            for (int i = 0; i < Player.Ammo; i++)
            {
                _spriteBatch.Draw(texture: BurgerTexture, position: new Vector2(10 + i * BurgerTexture.Width * 1.1f, ScreenSize.Y * 0.9f), drawRectangle: null, sourceRectangle: null, origin: new Vector2(0, 0), rotation: 0f, scale: new Vector2(1, 1));
            }
            int heads = 0;
            for (int i = 0; i < ChildrenFed; i++)
            {
                _spriteBatch.Draw(texture: HeadOKTexture, position: new Vector2(10 + heads * BurgerTexture.Width * 1.1f, ScreenSize.Y * 0.95f), drawRectangle: null, sourceRectangle: null, origin: new Vector2(0, 0), rotation: 0f, scale: new Vector2(1, 1));
                heads++;
            }
            for (int i = 0; i < ChildrenDied; i++)
            {
                _spriteBatch.Draw(texture: HeadDEADTexture, position: new Vector2(10 + heads * BurgerTexture.Width * 1.1f, ScreenSize.Y * 0.95f), drawRectangle: null, sourceRectangle: null, origin: new Vector2(0, 0), rotation: 0f, scale: new Vector2(1, 1));
                heads++;
            }
            for (int i = 0; i < ChildrenTotal-ChildrenDied-ChildrenFed; i++)
            {
                _spriteBatch.Draw(texture: HeadTexture, position: new Vector2(10 + heads * BurgerTexture.Width * 1.1f, ScreenSize.Y * 0.95f), drawRectangle: null, sourceRectangle: null, origin: new Vector2(0, 0), rotation: 0f, scale: new Vector2(1, 1));
                heads++;
            }
        }

        private void DrawSprites()
        {
            
            foreach (Sprite sprite in Sprites)
            {
                sprite.Draw( _spriteBatch);
            }
        }

        private void DrawText(SpriteBatch spriteBatch)
        {
            if(ScreenText != null)
            spriteBatch.DrawString(_font, ScreenText, new Vector2(ScreenSize.X*0.25f, ScreenSize.Y*0.2f), Color.White);
        }


        //This method is called during the update loop an places new sprites in a temp list to be added before the next update loop
        public void SpawnSpriteAtRuntime(Sprite sprite)
        {
            if (sprite != null)
            {
                NewSprites.Add(sprite);
         //       Console.WriteLine(NewSprites.Count);
            }
        }

        //This method takes the sprites from the temporary list and adds them to the main sprites list prior to running the update loop
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


        //This method is called in the update sprites loop and prepares a given sprite for removal before the next update loop. 
        internal void MarkForRemoval(Sprite sprite)
        {
            
            DeadSprites.Add(sprite);
        }

        private void RemoveDeadSprites()
        {
                foreach (Sprite deadSprite in DeadSprites)
                {
                    Sprites.Remove(deadSprite);
                    CollissionHandler.AllElements.Remove(deadSprite);
                    BackgroundSprites.Remove(deadSprite);
                    if (deadSprite is ICollidable)
                    {
                        CollissionHandler.CollisionListenersList.Remove((ICollidable)deadSprite);
                    }                 
                }
                DeadSprites.Clear();
        }

        // This method is called whenever a condition requires the current level to be reset. For instance player death or too many children died.
        private void Restart()
        {
            Player.Position = new Vector2(Player.BoundingBox.Width,Player.BoundingBox.Height);
            Player.Ammo = 5;
            ChildrenFed = 0;
            ChildrenDied = 0;
            NewSprites.Clear();
            DeadSprites.Clear();
            Sprites.Clear();
            switch (State)
            {
                    case GameState.Level1:
                    CreateTextMessage("LEVEL 1:\nFeed 2 hungry children... \nDon't get them killed!", 3000);
                    Sprites = LevelConstructor.Level1(this);
                    ChildrenTotal = 3;
                    ChildrenFedGoal = 2;
                    break;
                    case GameState.Level2:
                    CreateTextMessage("LEVEL 2:\nCows can be burgers...", 3000);
                    Sprites = LevelConstructor.Level2(this);
                    ChildrenTotal = 6;
                    ChildrenFedGoal = 5;
                    break;
                    case GameState.Level3:
                    CreateTextMessage("LEVEL 3:\nJust chill...", 3000);
                    Sprites = LevelConstructor.Level3(this);
                    break;
            }
            CollissionHandler = new CollissionHandler(Sprites);
            
        }

        // This method is called whenever a message should appear to the player.
        public void CreateTextMessage(string text, double duration)
        {
            ScreenText = text;
            TextDuration = duration;
            _newText = true;
        }

        public void CheckLevelDone(GameTime gameTime)
        {
            if (ChildrenTotal - ChildrenDied < ChildrenFedGoal - ChildrenFed)
            {
                CreateTextMessage("Not enough children left to complete level goal.",2000);
                _restartTime = gameTime.TotalGameTime.TotalMilliseconds;
                _restarting = true;
            }
            if (PlayerDead )
            {
                //Restarting level in 3 secs...
                _restartTime = gameTime.TotalGameTime.TotalMilliseconds;
                PlayerDead = false;
                _restarting = true;
            }

            if (gameTime.TotalGameTime.TotalMilliseconds > _restartTime + 3000 && _restarting)
            {
                _restarting = false;
                Restart();
            }

            switch (State)
            {
                    case GameState.Level1:
                    if (ChildrenFed >= 2)
                    {
                        State = GameState.Level2;
                        Restart();
                    }
                    break;
                    case GameState.Level2:
                    if (ChildrenFed >= 5)
                    {
                        State = GameState.Level3;
                        Restart();
                    }
                    break;
                    case GameState.Level3:
                    if (ChildrenFed >= 7)
                    {
                        //goto level 4 or end game :)
                    }
                    break;
            }
        }

        
    }
}
