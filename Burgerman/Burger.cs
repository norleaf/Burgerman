using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Burger : AnimatedSprite
    {
        private Vector2 Velocity { get; set; }
        

        public Burger(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Velocity = new Vector2(0.5f,0);
        }

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Velocity = Vector2.Add(Velocity, Game1.Gravity);
            Position = Vector2.Add(Position, Velocity);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override Sprite CloneAt(float x, float y)
        {
            return base.CloneAt(x, y);
        }
    }
}
