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

        private Animation movingUp;
        private Animation movingRight;
        private Animation movingLeft;
        private float speed = 1.8f;
        private bool loaded = true;
        public override Vector2 Origin { get; set; }
        //private bool justPressed = true;

        public Balloon(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            Name = "Hero Ballooneer";
            movingUp = new Animation(this, 100);
            movingUp.Frames.Add(new Rectangle(100, 0, 100, 171));
            movingUp.Frames.Add(new Rectangle(200, 0, 100, 171));
            movingUp.Frames.Add(new Rectangle(300, 0, 100, 171));
            movingUp.Frames.Add(new Rectangle(400, 0, 100, 171));
            movingRight = new Animation(this, 200);
            movingRight.Frames.Add(new Rectangle(0, 0, 100, 171));

            setAnimation(movingRight);
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
                if (loaded == true)
                {
                    loaded = false;
                    ShootBurger();
                }
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Z))
            {
                loaded = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                setAnimation(movingRight);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                PositionX-= speed;
                setAnimation(movingRight);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PositionX += speed;
                setAnimation(movingRight);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
              //  justPressed = false;
                PositionY -= speed;
                    setAnimation(movingUp);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                
                PositionY += speed;
                setAnimation(movingRight);
            }
            //if (Keyboard.GetState().IsKeyUp(Keys.Up))
            //{
            //    justPressed = true;
            //}

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.2f)
            {
                PositionX += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * speed;
                setAnimation(movingRight);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.2f)
            {
                PositionX += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X * speed;
                setAnimation(movingRight);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.2f)
            {
                PositionY -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * speed;
                setAnimation(movingUp);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.2f)
            {
                PositionY -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y * speed;
                setAnimation(movingRight);
            }
            PositionY +=0.3f;
        }

        private void ShootBurger()
        {
            Vector2 spawnpoint = Vector2.Add(Position,new Vector2(BoundingBox.Width/2,BoundingBox.Height-20));
            Burger burger = new Burger(Game1.Instance.burgerTexture,spawnpoint);
            Game1.Instance.SpawnSpriteAtRuntime(burger);
        }

        public void CollideWith(Sprite other)
        {
            if (other is Bullet)
            {
                Scale = 0;
            }
            
        }

        public override Sprite CloneAt(float x, float y)
        {
            return new Balloon(SpriteTexture, new Vector2(x, y));
        }
    }
}
