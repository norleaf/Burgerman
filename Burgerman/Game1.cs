#region Using Statements

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Burgerman.Sprites;
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
        public SoundEffect ChildFedSound { get; set; }
        public SoundEffect ChildDeathSound { get; set; }
        public SoundEffect MooSound { get; set; }
        public SoundEffect SoldierDeathSound { get; set; }
        public SoundEffect HelicopterExplosionSound { get; set; }

        public ScreenShake ScreenShake { get; set; }
        private SpriteBatch _spriteBatch;
        public static Vector2 Gravity = new Vector2(0,0.1f);
        public Balloon Player { get; private set; }
        public bool PlayerDead { get; set; }
        public bool ChildDead { get; set; }

        private Texture2D _backgroundTexture;
        private Texture2D _blackbottom;
        public Texture2D ChildDeathTexture { get; private set; }
        public Texture2D SoldierDeathTexture { get; private set; }
        public Texture2D SoldierEatingTexture { get; private set; }
        public Texture2D ExplosionTexture { get; private set; }
        public Texture2D BalloonDeathTexture { get; private set; }
        public Texture2D BurgerTexture { get; private set; }
        public Texture2D HeadTexture { get; private set; }
        public Texture2D HeadOKTexture { get; private set; }
        public Texture2D HeadDEADTexture { get; private set; }
        public Texture2D BulletTracer { get; private set; }
        private Sprite _background; 
        private IntroBalloon _introBalloon;
        public SpriteFont Font { get; set; }
        public Text Text { get; set; }

        //THE CURRENT LEVEL WHICH WILL BE HOLDING ALL SPRITES BOTH BACKGROUND AND INTERACTIVE SPRITES
        public Level Level { get; set; }
        public LevelConstructor LevelConstructor { get; set; }

        public enum GameState { Intro, Pause, Level0A, Level0B, Level1, Level2, Level3,Level4 }
        public GameState State { get; private set; }
        private GameState _currentLevel;
        private bool _restarting;
        private bool _justpressed;
        public double Time { get; private set; }
        
        public int ChildrenFed { get; set; }
        public int ChildrenFedGoal { get; set; }
        public int ChildrenDied { get; set; }
        public int ChildrenTotal { get; set; }

       
        public CollissionHandler CollisionHandler { get; set; }
        public Vector2 ScreenSize { get; private set; }
        public string ScreenText { get; set; }
        public double TextDuration { get; set; }
        private bool _newText;
        private double _restartTime;
        

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
            Level = new Level();
           
            //SOUNDS
            ShotSound = Content.Load<SoundEffect>("./sounds/shot");
            ChildFedSound = Content.Load<SoundEffect>("./sounds/BurgerPickUp");
            MooSound = Content.Load<SoundEffect>("./sounds/moo");
            ChildDeathSound = Content.Load<SoundEffect>("./sounds/ChildDead");
            SoldierDeathSound = Content.Load<SoundEffect>("./sounds/SoldierDeath");
            HelicopterExplosionSound = Content.Load<SoundEffect>("./sounds/Explosion");

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("star"));
            textures.Add(Content.Load<Texture2D>("star"));
            textures.Add(Content.Load<Texture2D>("star"));
            FireworksEmitter fireworks = new FireworksEmitter(textures, new Vector2(400, 240));

            List<Texture2D> textures2 = new List<Texture2D>();
            textures2.Add(Content.Load<Texture2D>("helidebris"));

            HelicopterDebris helicopterDebrisEmitter = new HelicopterDebris(textures2,new Vector2());

            Texture2D titleTexture = Content.Load<Texture2D>("title");
            Sprite intro = new Sprite(titleTexture,new Vector2(ScreenSize.X * 0.4f,ScreenSize.X * 0.1f));
            
            //This is the balloon used on the intro screen
            Texture2D introBalloonTexture2D = Content.Load<Texture2D>("introBalloon");
            _introBalloon = new IntroBalloon(introBalloonTexture2D, new Vector2(50,ScreenSize.Y));
            
            // This is the player balloon object. It will be the only one created so lets keep track of it so we don't loose it.
            Texture2D ballonTexture = Content.Load<Texture2D>("./balloon/balloon");
            Player = new Balloon(ballonTexture, new Vector2(0, 0));

            // THESE ARE THE NEW PROTOTYPES THAT WILL BE USED IN LEVELCONSTRUCTOR!//////////////////////////////////////
            Vector2 vectorZero = new Vector2(0,0);
            Child ChildProto = new Child(Content.Load<Texture2D>("child"), vectorZero);
            Soldier SoldierProto = new Soldier(Content.Load<Texture2D>("animated_soldier"), vectorZero);
            Hut HutProto = new Hut(Content.Load<Texture2D>("hut"), vectorZero);
            Jet JetProto = new Jet(Content.Load<Texture2D>("./attackplane/attackplane"), vectorZero);
            Helicopter HelicopterProto = new Helicopter(Content.Load<Texture2D>("Helicopter"), vectorZero);
            Cow CowProto = new Cow(Content.Load<Texture2D>("cow"), vectorZero);
            Cloud CloudProto = new Cloud(Content.Load<Texture2D>("./cloud/cloud"), vectorZero);
            Mountain MountainProto = new Mountain(Content.Load<Texture2D>("mountain"), vectorZero);
            PalmTree PalmTreeProto = new PalmTree(Content.Load<Texture2D>("palm"), vectorZero);
            Ground GroundProto = new Ground(Content.Load<Texture2D>("ground"),vectorZero);
            
            ChildDeathTexture = Content.Load<Texture2D>("diechild");
            SoldierDeathTexture = Content.Load<Texture2D>("diesoldier");
            SoldierEatingTexture = Content.Load<Texture2D>("eatingsoldier");

            ExplosionTexture = Content.Load<Texture2D>("explosionflash");
            
            BalloonDeathTexture = Content.Load<Texture2D>("./balloon/balloonburning");

            //These classes are spawned during the game and are needed by the classes that spawn 
            Bullet BulletProto = new Bullet(Content.Load<Texture2D>("bullet"), vectorZero,null);
            BurgerTexture = Content.Load<Texture2D>("burger");
            HeadTexture = Content.Load<Texture2D>("childhead");
            HeadOKTexture = Content.Load<Texture2D>("childheadOK");
            HeadDEADTexture = Content.Load<Texture2D>("childheadDEAD");
            BulletTracer = Content.Load<Texture2D>("bulletTracer");

            //Here we initialise our level contructor. It will hold all of our object prototypes.
            LevelConstructor = new LevelConstructor(ChildProto, SoldierProto, HutProto, JetProto, HelicopterProto, CowProto, CloudProto, MountainProto, PalmTreeProto, GroundProto, BulletProto);
            LevelConstructor.HelicopterDebris = helicopterDebrisEmitter;
            LevelConstructor.Fireworks = fireworks;
            
            //This is a 1 pixel wide color gradient image that we draw a lot to fill the screen. Wonder if a big picture would be better?
            _backgroundTexture = Content.Load<Texture2D>("background");
            _background = new Sprite(_backgroundTexture,vectorZero);

            _blackbottom = Content.Load<Texture2D>("blackbottom");

            //The game starts with us showing the Intro screen. Therefore we set the gamestate to Intro. Who would have thought...
            State = GameState.Intro;
            
            // We use the levelconstructor to set the level to the intro screen.
            Level = LevelConstructor.IntroScreen();
            Level.LevelSprites.Add(_introBalloon);
            Level.LevelSprites.Add(intro);

            // The CollisionHandler is created with the level list of sprites that we want to check collisions for
            CollisionHandler = new CollissionHandler(Level.LevelSprites);

            Font = Content.Load<SpriteFont>("superfont");
            Text = new Text("",0);

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
            Time = gameTime.TotalGameTime.TotalMilliseconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (State == GameState.Intro) 
            {
                _introBalloon.Update(gameTime);
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                    Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    State = GameState.Level0A;
                    Restart();
                    _justpressed = false;
                }
            }
            
            //If the player pressed space and it is not the intro screen...
            if (State != GameState.Intro && _justpressed &&
                (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                 Keyboard.GetState().IsKeyDown(Keys.Space)))
            {
                // ... if we are not already paused, then pause
                if (State != GameState.Pause)
                {
                    Text = new Text("Paused", 1000 + Time);
                    _currentLevel = State;
                    State = GameState.Pause;
                }
                    //... else resume the game.
                else
                {
                    State = _currentLevel;
                }
                _justpressed = false;
            }
            
            // A method to skip levels for testing purposes.
            if (Keyboard.GetState().IsKeyDown(Keys.End) && _justpressed)
            {
                switch (State)
                {
                    case GameState.Level0A:
                        State = GameState.Level1;
                        break;
                    case GameState.Level0B:
                        State = GameState.Level1;
                        break;
                    case GameState.Level1:
                        State = GameState.Level2;
                        break;
                    case GameState.Level2:
                        State = GameState.Level3;
                        break;
                }
                Restart();
                _justpressed = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.End) && Keyboard.GetState().IsKeyUp(Keys.Space) && GamePad.GetState(PlayerIndex.One).IsButtonUp(Buttons.Start))
            {
                _justpressed = true;
            }

            // TODO: Add your update logic here
            
            if (State != GameState.Pause) { 
                // HERE WE UPDATE ALL SPRITES IN THE CURRENT LEVEL
                Level.Update(gameTime);

                // Check to see if ANYTHING collided
                CollisionHandler.Update(gameTime);
                
                //Check if we completed the level or failed it.
                CheckLevelDone(gameTime);
            }

            Text.Update(gameTime);
            if(ScreenShake != null) ScreenShake.Update();
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
            
           // if (State == GameState.Intro) particleEngine.Draw(_spriteBatch);
            

            //Draw the colorgradient background. Might want to change this to a big image??
            for (int i = 0; i < ScreenSize.X; i++)
            {
                _background.Position = new Vector2(1 * i, 0);
                _background.Draw(_spriteBatch);
            }
            if (State != GameState.Intro) { 
                 for (int i = 0; i < ScreenSize.X/64; i++)
                 {
                     _spriteBatch.Draw(texture: _blackbottom, position: new Vector2( i * _blackbottom.Width, GroundLevel+10), drawRectangle: null, sourceRectangle: null, origin: new Vector2(0, 0), rotation: 0f, scale: new Vector2(1, 5));
                 }
            }
            
            //HERE WE DRAW ALL SPRITES CONTAINED IN THE LISTS IN OUR CURRENT LEVEL
            Level.Draw(_spriteBatch);
            
            DrawHUD();
            Text.Draw(_spriteBatch);
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

       

        // This method is called whenever a condition requires the current level to be reset. For instance player death or too many children died.
        private void Restart()
        {
            //Reset player to starting position.
            Player.Position = new Vector2(Player.BoundingBox.Width,Player.BoundingBox.Height);
            Player.Ammo = 0;
            Player.DistanceTravelled = 0;
            ChildrenFed = 0;
            ChildrenDied = 0;
                       
            switch (State)
            {
                    case GameState.Level0A:
                    Text = new Text("LEVEL A:\nFeed the hungry child \nbefore the soldier reaches her...", 4000 + Time);
                    Level = LevelConstructor.Level0A();
                    ChildrenTotal = 1;
                    ChildrenFedGoal = 1;
                    Player.Ammo = 10;
                    break;
                    case GameState.Level0B:
                    Text = new Text("LEVEL B:\nDon't get killed by the helicopter...", 4000 + Time);
                    Level = LevelConstructor.Level0B();
                    ChildrenTotal = 0;
                    ChildrenFedGoal = 0;
                    Player.Ammo = 6;
                    break;
                    case GameState.Level1:
                    Text = new Text("LEVEL 1:\nFeed 2 hungry children...", 3000 + Time);
                    Level = LevelConstructor.Level1();
                    ChildrenTotal = 3;
                    ChildrenFedGoal = 2;
                    Player.Ammo = 20;
                    break;
                    case GameState.Level2:
                    Text = new Text("LEVEL 2:\nCows can be burgers...", 3000 + Time);
                    Level = LevelConstructor.Level2();
                    ChildrenTotal = 6;
                    ChildrenFedGoal = 5;
                    Player.Ammo = 5;
                    break;
                    case GameState.Level3:
                    Text = new Text("LEVEL 3:\nJust chill...", 3000 + Time);
                    Level = LevelConstructor.Level3();
                    ChildrenTotal = 0;
                    ChildrenFedGoal = 0;
                    Player.Ammo = 0;
                    break;
                    case GameState.Level4:
                    Text = new Text("Game Over, You won!\nNow contemplate\nthe meaning of life\nwithout any challenge...", 30000 + Time);
                    Level = LevelConstructor.Level4();
                    ChildrenTotal = 0;
                    ChildrenFedGoal = 0;
                    Player.Ammo = 0;
                    break;
            }
            CollisionHandler = new CollissionHandler(Level.LevelSprites);
            
        }

        

        public void CheckLevelDone(GameTime gameTime)
        {
            if (ChildrenTotal - ChildrenDied < ChildrenFedGoal && !_restarting)
            {
                Text = new Text("Not enough children left to complete level goal.", 2000 + Time);
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
                    case GameState.Level0A:
                    if (ChildrenFed >= 1)
                    {
                        State = GameState.Level0B;
                        Text = new Text("Well Done!", 3000 + Time);
                        _restarting = true;
                        _restartTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    break;
                    case GameState.Level0B:
                    if (Player.DistanceTravelled > Level.LevelLength)
                    {
                        State = GameState.Level1;
                        Text = new Text("Well Done!", 3000 + Time);
                        _restarting = true;
                        _restartTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    break;
                    case GameState.Level1:
                    if (ChildrenFed >= 2)
                    {
                        State = GameState.Level2;
                        Text = new Text("Well Done!", 3000 + Time);
                        _restarting = true;
                        _restartTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    break;
                    case GameState.Level2:
                    if (ChildrenFed >= 5)
                    {
                        State = GameState.Level3;
                        Text = new Text("Well Done!", 3000 + Time);
                        _restarting = true;
                        _restartTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    break;
                    case GameState.Level3:
                    if (Player.DistanceTravelled > Level.LevelLength)
                    {
                        State = GameState.Level4;
                        Text = new Text("Well Done!", 3000 + Time);
                        _restarting = true;
                        _restartTime = gameTime.TotalGameTime.TotalMilliseconds;
                        //goto level 4 or end game :)
                    }
                    break;
            }
        }

        
    }
}
