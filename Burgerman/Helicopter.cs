using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Security.Principal;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Helicopter: AnimatedSprite
    {
        private Random random = new Random();
        private int _wait = 0;
        private int _upOrDown = 0;
        private double _millisecondsAtLastSalvo = 0;
        private double _millisecondsAtLastShot = 0;
        private int _firingDelay = 500;
        private int _salvoLength = 3000;
        private int _salvos = 3;
        private int _waitTime = 2000;
        private Game1 game;
        private Texture2D bulletTexture;
        private Texture2D spriteTexture;
        
       

        public Helicopter(Texture2D spriteTexture, Vector2 position, Texture2D bulletTexture) : base(spriteTexture, position)
        {
            this.spriteTexture = spriteTexture;
            this.bulletTexture = bulletTexture;
            game = Game1.getInstance();
            Animation flying = new Animation(this, 200);
            flying.Delay = 100;
            flying.Frames.Add(new Rectangle(0, 0, 200, 74));
            flying.Frames.Add(new Rectangle(200, 0, 200, 74));
            flying.Frames.Add(new Rectangle(400, 0, 200, 74));
            flying.Frames.Add(new Rectangle(200, 0, 200, 74));
            //_millisecondsAtEntry = gameTime.TotalGameTime.TotalMilliseconds;
            setAnimation(flying);
            _state = State.Entering;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            switch (_state)
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
                _state = State.Fighting;
            }
        }

        private void Enter(GameTime gameTime)
        {
            if (PositionX > game.ScreenSize.X*4/5)
            {
                PositionX-=3.0f;
            }
            else
            {
                _state = State.Fighting;
                _millisecondsAtLastSalvo = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private void Leave(GameTime gameTime)
        {
            PositionY--;
            if (PositionY < -200)
            {
                game.markForRemoval(this);
            }
        }

        private void Fight(GameTime gameTime)
        {
            if (_salvos <= 0)  // Are the total amount of salvos fired then we leave
            {
                _state = State.Leaving;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > _millisecondsAtLastShot + _firingDelay)
            {
                Vector2 target = game.getPlayer().Center;
                Bullet bullet = new Bullet(bulletTexture, new Vector2(PositionX + BoundingBox.Width / 3, PositionY + SpriteTexture.Height / 3 * 2), target);
                game.SpawnSpriteAtRuntime(bullet);
                _millisecondsAtLastShot = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (_wait < 0)
            {
                _upOrDown = random.Next(0, 3) - 1;
                _wait = 30;
            }
            if (PositionY < 100)
            {
                _upOrDown = 1;
            }
            _wait--;
            PositionY += _upOrDown * 0.2f;
            
            if (gameTime.TotalGameTime.TotalMilliseconds > _millisecondsAtLastSalvo + _salvoLength)
            {
                _millisecondsAtLastSalvo = gameTime.TotalGameTime.TotalMilliseconds;
                _state = State.Waiting;
                _salvos--;
            }
        }

        public Helicopter CloneAt(float x, float y)
        {
            return new Helicopter(spriteTexture,new Vector2(x,y), bulletTexture);
        }
    }
}
