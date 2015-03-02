using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Burgerman;

namespace Burgerman
{
    public class Balloon: AnimatedSprite, ICollidable
    {

        private readonly Animation _movingUp;
        private readonly Animation _descent;
        //private Animation movingLeft;
        private const float SpeedMult = 1.8f;
        private Vector2 _moveVector;
        private Vector2 _left = new Vector2(-1,0);
        private Vector2 _right = new Vector2(1,0);
        private Vector2 _up = new Vector2(0,-1);
        private Vector2 _down = new Vector2(0, 1);
        private Vector2 _drop = new Vector2(0, 0.3f);
        private bool _loaded = true;
        public int Ammo { get; set; }
        public override Vector2 Origin { get; set; }
        private Game1 game;
        //private bool justPressed = true;

        public Balloon(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            game = Game1.Instance;
            Name = "Hero Ballooneer";
            SlideSpeed = new Vector2(0,0);
            Ammo = 5;
            _movingUp = new Animation(this, 100);
            _movingUp.Frames.Add(new Rectangle(100, 0, 100, 171));
            _movingUp.Frames.Add(new Rectangle(200, 0, 100, 171));
            _movingUp.Frames.Add(new Rectangle(300, 0, 100, 171));
            _movingUp.Frames.Add(new Rectangle(400, 0, 100, 171));
            _descent = new Animation(this, 200);
            _descent.Frames.Add(new Rectangle(0, 0, 100, 171));

            setAnimation(_descent);
        }

        public override Rectangle BoundingBox
        {
            get
            {
                Rectangle result;
                Vector2 spritesize;

                if (SourceRectangle.IsEmpty)
                {
                    spritesize = new Vector2(SpriteTexture.Width, SpriteTexture.Height);
                }
                else
                {
                    spritesize = new Vector2(SourceRectangle.Width, SourceRectangle.Height);
                }
                int padding = 0;
                result = new Rectangle((int)Position.X+padding, (int)Position.Y+padding, (int)(spritesize.X * Scale)-padding, (int)(spritesize.Y * Scale) - padding);
                return result;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _moveVector = new Vector2(0,0);
            if (Keyboard.GetState().IsKeyDown(Keys.Z) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                if (_loaded && Ammo > 0)
                {
                    _loaded = false;
                    ShootBurger();
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Z) && GamePad.GetState(PlayerIndex.One).IsButtonUp(Buttons.A))
            {
                _loaded = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                setAnimation(_descent);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _moveVector = Vector2.Add(_moveVector, _left);
                setAnimation(_descent);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _moveVector = Vector2.Add(_moveVector, _right) ;
                setAnimation(_descent);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _moveVector = Vector2.Add(_moveVector, _up);
                setAnimation(_movingUp);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Position.Y + BoundingBox.Height < Game1.GroundLevel)
                {
                    _moveVector = Vector2.Add(_moveVector, _down) ;
                    setAnimation(_descent);
                }
            }


            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.2f || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.2f || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.2f)
            {
                _moveVector = new Vector2(x: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, y: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1);
                setAnimation(_descent);
            }
        
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.2f)
            {
                _moveVector = new Vector2(x: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, y: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1);
                setAnimation(_movingUp);
            }
         
            if (Position.Y+BoundingBox.Height + _moveVector.Y < Game1.GroundLevel)
            {
                Position = Vector2.Add(Position, _moveVector * SpeedMult);
                Position = Vector2.Add(Position, _drop);
            }
        }

        private void ShootBurger()
        {
            Vector2 spawnpoint = Vector2.Add(Position,new Vector2(BoundingBox.Width/2,BoundingBox.Height-20));
            Burger burger = new Burger(game.BurgerTexture,spawnpoint);
            Ammo--;
            game.SpawnSpriteAtRuntime(burger);
        }

        public void CollideWith(Sprite other)
        {
            if (other is Bullet)
            {
                if (Vector2.Distance(Center,other.Center) < (BoundingBox.Height + BoundingBox.Width)/4f)
                {
                    game.MarkForRemoval(this);
                    game.MarkForRemoval(other);
                    game.PlayerDead = true;
                }
            }
            if (other is Jet)
            {
                if (Vector2.Distance(Center, other.Center) < (BoundingBox.Height + BoundingBox.Width) / 4f + (other.BoundingBox.Height + other.BoundingBox.Width) / 4f)
                {
                    game.MarkForRemoval(this);
                    game.PlayerDead = true;
                }
            }
        }

        public override Sprite CloneAt(float x, float y)
        {
            return new Balloon(SpriteTexture, new Vector2(x, y));
        }

        public override void Die()
        {
            
        }
    }
}
