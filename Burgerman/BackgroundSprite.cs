using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class BackgroundSprite : Sprite
    {
        private Texture2D spriteTexture;

        public BackgroundSprite(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            this.spriteTexture = spriteTexture;
            Speed = new Vector2(-0.15f,0);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Move();
            if (PositionX < -spriteTexture.Width)
            {
                PositionX += Game1.Instance.ScreenSize.X + spriteTexture.Width;

            }
        }
    }
}
