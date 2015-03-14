using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Cow : AnimatedSprite
    {
        public Cow(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
        }

        public override Sprite CloneAt(float x)
        {
            return new Cow(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }
    }
}
