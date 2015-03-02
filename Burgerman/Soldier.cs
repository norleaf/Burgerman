using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Soldier: AnimatedSprite, ICollidable
    {
        public Soldier(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
           // Scale *= 0.5f;
            Name = "Soldier";
            SlideSpeed = new Vector2(-1,0);
            Animation running = new Animation(this, 200);
            running.Frames.Add(new Rectangle(0, 0, 64, 55));
            running.Frames.Add(new Rectangle(64, 0, 64, 55));
            running.Frames.Add(new Rectangle(128, 0, 64, 55));
            running.Frames.Add(new Rectangle(192, 0, 64, 55));
            setAnimation(running);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (Position.X < -30)
            {
                Game1.Instance.MarkForRemoval(this);
            }
        }

        public override Sprite CloneAt(float x)
        {
            return new Soldier(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }

        public void CollideWith(Sprite other)
        {
            if (other is Bullet || other is Burger)
            {
                Game1.Instance.MarkForRemoval(this);
                Game1.Instance.MarkForRemoval(other);
            }
        }
    }
}
