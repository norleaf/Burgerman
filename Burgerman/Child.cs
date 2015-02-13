using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{

    class Child: Sprite, ICollidable
    {
        private Vector2 jumpVector;
        private Balloon player;
        public Child(Texture2D spriteTexture, Vector2 position, Balloon balloon)
            : base(spriteTexture, position)
        {
            player = balloon;
            jumpVector = new Vector2(0,0);
            //Origin = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (PositionX < player.PositionX)
            {
                PositionX+=0.5f;
            }
            else if (PositionX > player.PositionX)
            {
                PositionX-=0.5f;
            }
            else
            {
                Jump();
            }
            
        }

        public void Jump()
        {
            jumpVector = new Vector2(0,-0.7f);
            PositionY -= 2.3f;
        }

        public void CollideWith(Sprite other)
        {
            Scale = 0;
            //other.Die();
        }
    }
}
