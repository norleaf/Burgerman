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
        private Vector2 _jumpVector;
        private Balloon player;
        private State _state;

        public enum State 
        {
            Waiting,
            Walking,
            Jumping
        }
        
        public Child(Texture2D spriteTexture, Vector2 position, Balloon balloon)
            : base(spriteTexture, position)
        {
            Scale = 0.5f;
            player = balloon;
            _jumpVector = new Vector2(0,0);
            _state = State.Walking;
        }

        public override void Update(GameTime gameTime)
        {
            if (PositionX < player.PositionX)
            {
                PositionX += 0.5f;
            }
            else if (PositionX > player.PositionX)
            {
                PositionX -= 0.5f;
            }
        }

        public void CollideWith(Sprite other)
        {
           // Scale = 0;
            
        }
    }
}
