using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    
    public class Child: AnimatedSprite, ICollidable
    {
        //private Vector2 _jumpVector;
        //private Balloon player;
        
        
        public Child(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
           // Scale = 0.5f;
            Name = "Child";
         //   player = Game1.Instance.Player;
        //    _jumpVector = new Vector2(0,0);
            _state = State.Waiting;
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("child updating!");
            switch (_state)
            {
                    case State.Leaving:
                    MoveHorizontally(-1);
                    if (Position.X < -BoundingBox.Width)
                    {
                        Game1.Instance.MarkForRemoval(this);
                    }
                    break;
            }
            Move();
        }

        public void CollideWith(Sprite other)
        {
            if (other is Soldier && _state != State.Leaving )
            {
                Game1.Instance.MarkForRemoval(this);
                Game1.Instance.ChildDead = true;
            }
            if (other is Burger)
            {
                _state = State.Leaving;
                Game1.Instance.MarkForRemoval(other);
                Game1.Instance.ChildrenFed = 1;
            }
            if (other is Bullet)
            {
                Game1.Instance.MarkForRemoval(this);
                Game1.Instance.MarkForRemoval(other);
                Game1.Instance.ChildDead = true;
            }
            
        }

        public override Sprite CloneAt(float x)
        {
            return new Child(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }

        //public override Sprite CloneAt(float x, float y)
        //{
        //    return new Child(SpriteTexture, new Vector2(x, Game1.groundLevel - SpriteTexture.Height));
        //}
    }
}
