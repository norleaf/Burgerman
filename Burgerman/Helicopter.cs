using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Helicopter: AnimatedSprite
    {
        public Helicopter(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Animation flying = new Animation(this);
            flying.Frames.Add(new Rectangle(0, 0, 200, 74));
            flying.Frames.Add(new Rectangle(200, 0, 200, 74));
            
            setAnimation(flying);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            int i = 0;
        }
    }
}
