using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman.Sprites
{
    public class Mountain : BackgroundSprite
    {
        public Mountain(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
        }

        public override Sprite CloneAt(float x)
        {
            return new Mountain(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }
    }
}
