using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    
    public class Child: AnimatedSprite, ICollidable
    {
        Animation _waiting;
        Animation _walking;
        private bool _fed;
        
        public Child(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
           // Scale = 0.5f;
            Name = "Child";
            
            
            _waiting = new Animation(this, 200);
            _waiting.Frames.Add(new Rectangle(0, 0, 30, 47));

            _walking = new Animation(this, 200);
            _walking.Frames.Add(new Rectangle(0, 0, 30, 47));
            _walking.Frames.Add(new Rectangle(30, 0, 30, 47));
            _walking.Frames.Add(new Rectangle(60, 0, 30, 47));
            _walking.Frames.Add(new Rectangle(90, 0, 30, 47));
            
            setAnimation(_waiting);
       
            _state = State.Waiting;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Console.WriteLine("child updating!");
            switch (_state)
            {
                    case State.Leaving:
                    setAnimation(_walking);
                    MoveHorizontally(-1);
                    if (Position.X < -BoundingBox.Width)
                    {
                        Game1.Instance.MarkForRemoval(this);
                    }
                    break;
            }
        }

        public void CollideWith(Sprite other)
        {
            if (other is Soldier && _state != State.Leaving )
            {
                Die();
            }
            if (other is Burger && !_fed)
            {
                _fed = true;
                _state = State.Leaving;
                Game1.Instance.MarkForRemoval(other);
                Game1.Instance.ChildrenFed = 1;
            }
            if (other is Bullet)
            {
                Die();
                Game1.Instance.MarkForRemoval(other);
            }
            
        }

        public void Die()
        {
            Game1.Instance.MarkForRemoval(this);
            Game1.Instance.ChildDead = true;
            AnimatedSprite corpse = new AnimatedSprite(Game1.Instance.ChildDeathTexture, Position);
            
            Animation fall = new Animation(corpse,200);
            fall.Loop = false;
            fall.Frames.Add(new Rectangle(0, 0, 47, 47));
            fall.Frames.Add(new Rectangle(47, 0, 47, 47));
            fall.Frames.Add(new Rectangle(94, 0, 47, 47));
            fall.Frames.Add(new Rectangle(141, 0, 47, 47));
            corpse.setAnimation(fall);
            Game1.Instance.SpawnSpriteAtRuntime(corpse);
        }

        public override Sprite CloneAt(float x)
        {
            return new Child(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }

       
    }
}
