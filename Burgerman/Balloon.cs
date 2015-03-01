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
        private readonly Animation _floating;
        //private Animation movingLeft;
        private const float SpeedMult = 1.8f;
        private bool _loaded = true;
        public override Vector2 Origin { get; set; }
        //private bool justPressed = true;

        public Balloon(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            Name = "Hero Ballooneer";
            _movingUp = new Animation(this, 100);
            _movingUp.Frames.Add(new Rectangle(100, 0, 100, 171));
            _movingUp.Frames.Add(new Rectangle(200, 0, 100, 171));
            _movingUp.Frames.Add(new Rectangle(300, 0, 100, 171));
            _movingUp.Frames.Add(new Rectangle(400, 0, 100, 171));
            _floating = new Animation(this, 200);
            _floating.Frames.Add(new Rectangle(0, 0, 100, 171));

            setAnimation(_floating);
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
                result = new Rectangle((int)PositionX+padding, (int)PositionY+padding, (int)(spritesize.X * Scale)-padding, (int)(spritesize.Y * Scale) - padding);
                return result;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
            {
                if (_loaded == true)
                {
                    _loaded = false;
                    ShootBurger();
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Z))
            {
                _loaded = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                setAnimation(_floating);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                PositionX-= SpeedMult;
                setAnimation(_floating);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PositionX += SpeedMult;
                setAnimation(_floating);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                PositionY -= SpeedMult;
                    setAnimation(_movingUp);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (PositionY + BoundingBox.Height < Game1.GroundLevel)
                {
                    PositionY += SpeedMult;
                    setAnimation(_floating);
                }
            }
            //if (Keyboard.GetState().IsKeyUp(Keys.Up))
            //{
            //    justPressed = true;
            //}

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.2f)
            {
                PositionX += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * SpeedMult;
                setAnimation(_floating);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.2f)
            {
                PositionX += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * SpeedMult;
                setAnimation(_floating);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.2f)
            {
                PositionY -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * SpeedMult;
                setAnimation(_movingUp);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.2f)
            {
                PositionY -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * SpeedMult;
                setAnimation(_floating);
            }
            if (PositionY+BoundingBox.Height < Game1.GroundLevel)
            {
                PositionY += 0.3f;
            }
        }

        private void ShootBurger()
        {
            Vector2 spawnpoint = Vector2.Add(Position,new Vector2(BoundingBox.Width/2,BoundingBox.Height-20));
            Burger burger = new Burger(Game1.Instance.BurgerTexture,spawnpoint);
            Game1.Instance.SpawnSpriteAtRuntime(burger);
        }

        public void CollideWith(Sprite other)
        {
            if (other is Bullet)
            {
                if (Vector2.Distance(Center,other.Center) < (BoundingBox.Height + BoundingBox.Width)/4f)
                {
                    Game1.Instance.MarkForRemoval(this);
                    Game1.Instance.MarkForRemoval(other);
                }
            }
            if (other is Jet)
            {
                if (Vector2.Distance(Center, other.Center) < (BoundingBox.Height + BoundingBox.Width) / 4f + (other.BoundingBox.Height + other.BoundingBox.Width) / 4f)
                {
                    Game1.Instance.MarkForRemoval(this);
                }
            }
        }

        public override Sprite CloneAt(float x, float y)
        {
            return new Balloon(SpriteTexture, new Vector2(x, y));
        }
    }
}
