using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Cloud : Sprite
    {
        public Cloud(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SlideLeft();
            if (Position.X < -SpriteTexture.Width)
            {
                MoveHorizontally(Game1.Instance.ScreenSize.X + SpriteTexture.Width);
            }
        }
    }
}
