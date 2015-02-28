using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    
    class Child: AnimatedSprite, ICollidable
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
        
        public Child(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
           // Scale = 0.5f;
            Name = "Child";
            player = Game1.Instance.Player;
            _jumpVector = new Vector2(0,0);
            _state = State.Walking;
        }

        public override void Update(GameTime gameTime)
        {
            //if (Center.X < player.Center.X)
            //{
            //    PositionX += 0.5f;
            //}
            //else if (Center.X > player.Center.X)
            //{
            //    PositionX -= 0.5f;
            //}
            Scroll();
        }

        public void CollideWith(Sprite other)
        {
            if (other is Soldier)
            {
                Game1.Instance.MarkForRemoval(this);
            }
            
        }

        public override Sprite CloneAt(float x, float y)
        {
            return new Child(SpriteTexture, new Vector2(x, Game1.groundLevel - SpriteTexture.Height));
        }
    }
}
