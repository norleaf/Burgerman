using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Burgerman;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class AnimatedSprite : Sprite
    {
        private Animation animation;
        protected State _state;

        public enum State
        {
            Waiting,
            Walking,
            Jumping,
            Entering,
            Fighting,
            Leaving
        }

        public AnimatedSprite(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            // set sourcerectangle
            SourceRectangle = new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height);
            
        }

        public void setAnimation(Animation anim)
        {
            animation = anim;
            
        }

        public virtual void AnimationComplete()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (animation != null)
            {
                animation.Update(gameTime);
            }

            SlideLeft();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Do we have a texture? If not then there is nothing to draw...
            if (SpriteTexture != null)
            {
                // Has a source rectangle been set?
                if (SourceRectangle.IsEmpty)
                {
                    // No, so draw the entire sprite texture
                    spriteBatch.Draw(SpriteTexture, Position, null, Color.White, Rotation, Origin, Scale,
                        SpriteEffects.None, 0f);
                }
                else
                {
                    // Yes, so just draw the specified SourceRect
                    spriteBatch.Draw(SpriteTexture, Position, SourceRectangle, Color.White, Rotation, Origin, Scale,
                        SpriteEffects.None, 0f);
                }
            }
        }

    }
}
