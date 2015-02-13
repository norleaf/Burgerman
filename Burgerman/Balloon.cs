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
    class Balloon: AnimatedSprite
    {

        private Animation movingUp;
        private Animation movingRight;
        private Animation movingLeft;


        public Balloon(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            //Scale = Scale*0.5f;
            //Origin = new Vector2(spriteTexture.Width/2,spriteTexture.Height/2);
            movingUp = new Animation(this);
            movingUp.Frames.Add(new Rectangle(128, 0, 128, 166));
           // movingUp.Frames.Add(new Rectangle(0, 0, 128, 166));
            movingRight = new Animation(this);
            movingRight.Frames.Add(new Rectangle(0, 0, 128, 166));

            setAnimation(movingRight);
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
                PositionX--;
                setAnimation(movingRight);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                PositionX++;
                setAnimation(movingRight);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                PositionY--;
                setAnimation(movingUp);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                PositionY++;
                setAnimation(movingRight);
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.2f)
            {
                PositionX += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                setAnimation(movingRight);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.2f)
            {
                PositionX += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                setAnimation(movingRight);
            }

            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.2f)
            {

                PositionY -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
                setAnimation(movingUp);
            }
            if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.2f)
            {
                PositionY -= GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
                setAnimation(movingRight);
            }
            PositionY +=0.3f;
        }
    }
}
