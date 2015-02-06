using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Soldier: Sprite
    {
        public Soldier(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            Scale *= 0.3f;
        }

        public override void Update(GameTime gameTime)
        {
            PositionX--;
            if (PositionX < -30)
            {
                PositionX = 1300;
            }
        }
    }
}
