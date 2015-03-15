using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Ground : BackgroundSprite
    {
        private Game1 game;

        public Ground(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            SlideSpeed = Sprite.DefaultSlideSpeed;
        }

        public override Sprite CloneAt(float x)
        {
            return new Ground(SpriteTexture, new Vector2(x, Game1.GroundLevel));
        }
    }
}
