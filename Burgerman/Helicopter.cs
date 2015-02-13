using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Helicopter: AnimatedSprite
    {
        private Random random = new Random();
        private int _wait = 0;
        private int _upOrDown = 0;
        public Helicopter(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Animation flying = new Animation(this);
            flying.Delay = 100;
            flying.Frames.Add(new Rectangle(0, 0, 200, 74));
            flying.Frames.Add(new Rectangle(200, 0, 200, 74));
            flying.Frames.Add(new Rectangle(400, 0, 200, 74));
            flying.Frames.Add(new Rectangle(200, 0, 200, 74));
            
            setAnimation(flying);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            

            if (_wait < 0)
            {
                _upOrDown = random.Next(0, 3) - 1;
                _wait = 30;
            }
            if (PositionY < 100)
            {
                _upOrDown = 1;
            }
            _wait--;
            PositionY += _upOrDown *0.2f;
        }
    }
}
