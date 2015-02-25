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
        public override Vector2 Origin { get; set; }

        public Balloon(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            //Scale = Scale*2.0f;
            //Origin = new Vector2(spriteTexture.Width/2,spriteTexture.Height/2);
            movingUp = new Animation(this);
            movingUp.Frames.Add(new Rectangle(128, 0, 128, 166));
           // movingUp.Frames.Add(new Rectangle(0, 0, 128, 166));
            movingRight = new Animation(this);
            movingRight.Frames.Add(new Rectangle(0, 0, 128, 166));

            setAnimation(movingRight);
            Origin = new Vector2(Origin.X - movingRight.Frames[0].Width/2,Origin.Y);
            // SourceRectangle = new Rectangle(0,0,128,166);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
                PositionY -= speed;
                setAnimation(movingUp);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                PositionY += speed;
                setAnimation(movingRight);
            }

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

        public void CollideWith(Sprite other)
        {
            if (other is Bullet)
            {
                Scale = 0;
            }
            
        }
    }
}
