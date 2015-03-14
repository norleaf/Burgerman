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
        public float DistanceTravelled { get; set; }
        public Vector2 MoveVector { get; private set; }
        private Vector2 _left = new Vector2(-1f,0);
        private Vector2 _right = new Vector2(1f,0);
        private Vector2 _up = new Vector2(0,-1f);
        private Vector2 _down = new Vector2(0, 1f);
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
            MoveVector = new Vector2(0,0);
            DistanceTravelled += Sprite.defaultSlideSpeed;
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
                MoveVector = Vector2.Add(MoveVector, _left);
                setAnimation(_descent);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                MoveVector = Vector2.Add(MoveVector, _right) ;
                setAnimation(_descent);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                MoveVector = Vector2.Add(MoveVector, _up);
                setAnimation(_movingUp);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Position.Y + BoundingBox.Height < Game1.GroundLevel)
                {
                    MoveVector = Vector2.Add(MoveVector, _down) ;
                    setAnimation(_descent);
                }
            }

            if (Position.X + MoveVector.X + BoundingBox.Width < game.ScreenSize.X + BoundingBox.Width * 0.4f && Position.X + MoveVector.X > 0 - BoundingBox.Width * 0.4f)
            {
                Position = Vector2.Add(Position, new Vector2(MoveVector.X, 0) * SpeedMult);
            }

            if (Position.Y + BoundingBox.Height + MoveVector.Y < Game1.GroundLevel && Position.Y + MoveVector.Y > -0.8f * BoundingBox.Height)
            {
                Position = Vector2.Add(Position, new Vector2(0, MoveVector.Y)* SpeedMult);
                Position = Vector2.Add(Position, _drop);
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.2f || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.2f || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.2f)
            {
                MoveVector = new Vector2(x: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, y: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1f);
                setAnimation(_descent);
            }
        
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.2f)
            {
                MoveVector = new Vector2(x: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X, y: GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * -1f);
                setAnimation(_movingUp);
            }

            
        }

        private void ShootBurger()
        {
            Vector2 spawnpoint = Vector2.Add(Position,new Vector2(BoundingBox.Width/2,BoundingBox.Height-20));
            Burger burger = new Burger(game.BurgerTexture,spawnpoint);
            Ammo--;
            game.Level.SpawnSpriteAtRuntime(burger);
        }

        public void CollideWith(Sprite other)
        {
            if (other is Bullet)
            {
                //This is to make the balloon hit area be a bit smaller than its bounding box rect. It basically becomes a circle that is the average of the boundingbox sides in diameter. Oval shape might be better but math harder...
                if (Vector2.Distance(Center,other.Center) < (BoundingBox.Height + BoundingBox.Width)/4f)
                {
                    Die();
                    Game1.Instance.CreateTextMessage("You got shot! Restarting mission...", 2000);
                    game.Level.MarkDead(other);
                }
            }
            if (other is Jet)
            {
                //This is to make the balloon hit area be a bit smaller than its bounding box rect. It basically becomes a circle that is the average of the boundingbox sides in diameter. So does the jet.
                if (Vector2.Distance(Center, other.Center) < (BoundingBox.Height + BoundingBox.Width) / 4f + (other.BoundingBox.Height + other.BoundingBox.Width) / 4f)
                {
                    Die();
                    Game1.Instance.CreateTextMessage("Hit by air plane! Restarting mission...", 2000);
                }
            }
            if (other is Cow)
            {
                //The balloon collects the cow and gains 3 burgers
                game.Level.MarkDead(other);
                Ammo += 3;
            }
        }

        
        public override Sprite CloneAt(float x, float y)
        {
            return new Balloon(SpriteTexture, new Vector2(x, y));
        }

        public override void Die()
        {
            game.Level.MarkDead(this);
            game.PlayerDead = true;
            //create a burning balloon sprite to replace our balloon
            AnimatedSprite corpse = new BalloonCorpse(Game1.Instance.BalloonDeathTexture, Position);
            game.Level.SpawnSpriteAtRuntime(corpse);
        }
    }
}
