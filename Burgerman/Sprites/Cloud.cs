using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Cloud : BackgroundSprite
    {
        public Cloud(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
        }

        public override Sprite CloneAt(float x, float y)
        {
            return new Cloud(SpriteTexture, new Vector2(x,y));
        }
    }
}
