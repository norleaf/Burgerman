using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class AnimatedSprite : Sprite
    {
        private Animation animation;
        public State CurrentState;
        public bool blink;
        public bool persistentSprite = true;

        public enum State
        {
            Waiting,
            Walking,
            Jumping,
            Entering,
            Fighting,
            Leaving,
            Eating
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
            if (!persistentSprite)
            {
                Game1.Instance.Level.MarkDead(this);
            }
            
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
                    spriteBatch.Draw(SpriteTexture, Position+Sprite.Shake, SourceRectangle, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0f);
                    if (blink) spriteBatch.Draw(SpriteTexture, Position + Sprite.Shake, SourceRectangle, Color.Black, Rotation, Origin, Scale, SpriteEffects.None, 0f);
                }
            }
        }

    }
}
