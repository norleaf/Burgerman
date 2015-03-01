using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Hut : Sprite
    {
        private bool spawnedChild = false;

        public Hut(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!spawnedChild && PositionX < Game1.Instance.ScreenSize.X*3/4)
            {
                spawnedChild = true;
                Sprite child = Game1.Instance.Child.CloneAt(PositionX + BoundingBox.Width / 2);
                Game1.Instance.NewSprites.Add(child);
            }
            Move();
        }

        public override Sprite CloneAt(float x)
        {
            return new Hut(SpriteTexture, new Vector2(x, Game1.groundLevel - BoundingBox.Height));
        }
    }
}
