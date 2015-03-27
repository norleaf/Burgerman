using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Jet: AnimatedSprite
    {
        public Jet(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            SlideSpeed = new Vector2(-8,0);
        }

        public override Sprite CloneAt(float x, float y)
        {
            return new Jet(SpriteTexture, new Vector2(x,y));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Position.X < -BoundingBox.Width)
            {
                Game1.Instance.Level.MarkDead(this);
            }
        }
    }
}
