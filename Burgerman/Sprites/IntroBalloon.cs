using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Burgerman
{
    public class IntroBalloon : AnimatedSprite
    {
        private Vector2 velocity = new Vector2(0,-1);
        private int vertspeed = -1;
        private Random ran;
        private int width;
        private int height;

        public IntroBalloon(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            SlideSpeed = new Vector2(0,0);
            ran = new Random();
            width = (int)Game1.Instance.ScreenSize.X;
            height = (int)Game1.Instance.ScreenSize.Y;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down))
            Position = Vector2.Add(Position,velocity);
            if ((Position.Y + BoundingBox.Height) < 0 || Position.Y > height)
            {
                Position = new Vector2(ran.Next(width - BoundingBox.Width),Position.Y);
                vertspeed *= -1;
                velocity = new Vector2(0,vertspeed);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position = Vector2.Add(Position, new Vector2(-1,0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position = Vector2.Add(Position, new Vector2(1,0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Position = Vector2.Add(Position, new Vector2(0, -1));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position = Vector2.Add(Position, new Vector2(0, 1));
            }



        }
    }
}
