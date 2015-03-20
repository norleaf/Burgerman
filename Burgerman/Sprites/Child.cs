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
        private Game1 game;
        
        public Child(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
           // Scale = 0.5f;
            Name = "Child";

            game = Game1.Instance;
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
            switch (_state)
            {
                    case State.Leaving:
                    setAnimation(_walking);
                    MoveHorizontally(-1);
                    if (Position.X < -BoundingBox.Width)
                    {
                        game.Level.MarkDead(this);
                    }
                    break;
            }
        }

        public void CollideWith(Sprite other)
        {
            if (other is Soldier && _state != State.Leaving )
            {
                Die();
                game.Text = new Text("Child killed!", 2000 + game.Time);
            }

            if (other is Burger && !_fed)
            {
                _fed = true;
                _state = State.Leaving;
                game.ChildFedSound.Play(0.2f, 0f, 0f);
                game.Level.MarkDead(other);
                game.ChildrenFed++;
            }

            if (other is Bullet)
            {
                Die();
                game.Text = new Text("Child shot!", 2000 + game.Time);
                game.Level.MarkDead(other);
            }

            if (Position.X < -BoundingBox.Width)
            {
                game.ChildrenDied++;
                game.Level.MarkDead(this);
            }
            
        }

        public override void Die()
        {
            game.ChildrenDied++;
            if (_fed)
            {
                game.ChildrenFed--;
            }
            game.Level.MarkDead(this);
            game.ChildDead = true;
            AnimatedSprite corpse = new AnimatedSprite(game.ChildDeathTexture, Position);
            
            Animation fall = new Animation(corpse,200);
            fall.Loop = false;
            fall.Frames.Add(new Rectangle(0, 0, 47, 47));
            fall.Frames.Add(new Rectangle(47, 0, 47, 47));
            fall.Frames.Add(new Rectangle(94, 0, 47, 47));
            fall.Frames.Add(new Rectangle(141, 0, 47, 47));
            corpse.setAnimation(fall);
            game.Level.SpawnSpriteAtRuntime(corpse);
        }

        public override Sprite CloneAt(float x)
        {
            return new Child(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }

       
    }
}
