using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Helicopter: AnimatedSprite, ICollidable
    {
        private Random random = new Random();
        private int _wait;
        private int _upOrDown;
        private double _millisecondsAtLastSalvo;
        private double _millisecondsAtLastShot;
        private int _firingDelay = 50;
        private int _salvoLength = 1000;
        private int _salvos = 3;
        private int _waitTime = 3000;
        
        private int _speed = 5;
        private Game1 game;
        private Texture2D bulletTexture;
        private Texture2D spriteTexture;
        
       

        public Helicopter(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Name = "Helicopter";
            this.spriteTexture = spriteTexture;
            SlideSpeed = new Vector2(0,0);
            game = Game1.Instance;
            Animation flying = new Animation(this, 200);
            flying.Delay = 100;
            flying.Frames.Add(new Rectangle(0, 0, 200, 74));
            flying.Frames.Add(new Rectangle(200, 0, 200, 74));
            flying.Frames.Add(new Rectangle(400, 0, 200, 74));
            flying.Frames.Add(new Rectangle(200, 0, 200, 74));
            //_millisecondsAtEntry = gameTime.TotalGameTime.TotalMilliseconds;
            setAnimation(flying);
            CurrentState = State.Entering;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (CurrentState)
            {
                case State.Entering: // The helicopter starts off screen and flies to its position
                    Enter(gameTime);
                    return;
                case State.Fighting: // From here the helicopter fires at the player for a given time
                    Fight(gameTime);
                    return;
                case State.Waiting: // The helicopter pauses for a second or two, then resumes firing
                    Wait(gameTime);
                    return;
                case State.Leaving: // The helicopter leaves out the top of the screen and is destroyed
                    Leave(gameTime);
                    return;
            }
        }

        private void Wait(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > _millisecondsAtLastSalvo + _waitTime)
            {
                _millisecondsAtLastSalvo = gameTime.TotalGameTime.TotalMilliseconds;
                CurrentState = State.Fighting;
            }
        }

        private void Enter(GameTime gameTime)
        {
            
            if (Position.X > game.ScreenSize.X*4/5)
            {
                //PositionX-=_speed;
                MoveHorizontally(-_speed);
            }
            else
            {
                CurrentState = State.Fighting;
                _millisecondsAtLastSalvo = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private void Leave(GameTime gameTime)
        {
            //PositionY--;
            MoveVertically(-1);
            if (Position.Y < -200)
            {
                game.Level.MarkDead(this);
            }
        }

        private void Fight(GameTime gameTime)
        {
            if (_salvos <= 0)  // Are the total amount of salvos fired then we leave
            {
                CurrentState = State.Leaving;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > _millisecondsAtLastShot + _firingDelay)
            {
                Bullet bullet = (Bullet) game.LevelConstructor.BulletProto.CloneBullet(Position.X + BoundingBox.Width/3f, Position.Y + SpriteTexture.Height/3*2, this);
                game.ShotSound.Play();
                game.Level.SpawnSpriteAtRuntime(bullet);
                _millisecondsAtLastShot = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (_wait < 0)
            {
                _upOrDown = random.Next(0, 3) - 1;
                _wait = 30;
            }
            if (Position.Y < 100)
            {
                _upOrDown = 1;
            }
            _wait--;
            //    Position.Y += _upOrDown * 0.2f;
            MoveVertically(_upOrDown * 0.2f);
            
            if (gameTime.TotalGameTime.TotalMilliseconds > _millisecondsAtLastSalvo + _salvoLength)
            {
                _millisecondsAtLastSalvo = gameTime.TotalGameTime.TotalMilliseconds;
                CurrentState = State.Waiting;
                _salvos--;
            }
        }



        public override Sprite CloneAt(float x, float y)
        {
            return new Helicopter(spriteTexture,new Vector2(x,y));
        }

        public void CollideWith(Sprite other)
        {
            

            if (other is Jet)
            {
                
                Die();
                
                
            }
            if (other is Bullet)
            {
                Bullet bullet = (Bullet) other;
                if (!bullet.Shooter.Equals(this))
                {

                   Die();
                }
            }


        }
        public override void Die()
        {
            AnimatedSprite explodingHelicopter = new AnimatedSprite(game.ExplosionTexture, Position);
            explodingHelicopter.persistentSprite = false;
            Animation exploding = new Animation(explodingHelicopter, 100);
            exploding.Loop = false;
            exploding.Frames.Add(new Rectangle(0, 0, 200, 200));
            exploding.Frames.Add(new Rectangle(200, 0, 200, 200));
            exploding.Frames.Add(new Rectangle(0, 0, 200, 200));
            exploding.Frames.Add(new Rectangle(200, 0, 200, 200));
            explodingHelicopter.setAnimation(exploding);
            game.Level.SpawnSpriteAtRuntime(explodingHelicopter);

            game.HelicopterExplosionSound.Play(0.2f, 0f, 0f);
            game.ScreenShake = new ScreenShake(30, 4);
            game.Level.MarkDead(this);
           
            game.Level.ParticleEngines.Add(game.LevelConstructor.HelicopterDebris.Clone(Center));
        }
    }
}
