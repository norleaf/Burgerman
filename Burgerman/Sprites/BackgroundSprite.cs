using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class BackgroundSprite : Sprite
    {
        private Texture2D spriteTexture;

        public BackgroundSprite(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            this.spriteTexture = spriteTexture;
            SlideSpeed = new Vector2(-0.25f,0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SlideLeft();
            if (Position.X < -spriteTexture.Width)
            {
                MoveHorizontally(Game1.Instance.ScreenSize.X + spriteTexture.Width);
            }
        }
    }
}
