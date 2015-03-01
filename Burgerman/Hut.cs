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
        private float _spawnpoint;

        public Hut(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Random ran = new Random();
            _spawnpoint = (float)ran.NextDouble()/3f + 0.4f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            
            if (!spawnedChild && PositionX < Game1.Instance.ScreenSize.X * _spawnpoint)
            {
                spawnedChild = true;
                Sprite child = Game1.Instance.Child.CloneAt(PositionX + BoundingBox.Width / 4f);
                Game1.Instance.NewSprites.Add(child);
            }
            Move();
        }

        public override Sprite CloneAt(float x)
        {
            return new Hut(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }
    }
}
