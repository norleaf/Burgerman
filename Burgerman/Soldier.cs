using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Soldier: AnimatedSprite
    {
        public Soldier(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
           // Scale *= 0.5f;
            Animation running = new Animation(this);
            running.Frames.Add(new Rectangle(0, 0, 64, 55));
            running.Frames.Add(new Rectangle(64, 0, 64, 55));
            running.Frames.Add(new Rectangle(128, 0, 64, 55));
            running.Frames.Add(new Rectangle(192, 0, 64, 55));
            setAnimation(running);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            PositionX-=0.5f;
            if (PositionX < -30)
            {
                PositionX = 1300;
            }
        }
    }
}
