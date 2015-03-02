using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Hut : Sprite
    {
        private bool _spawnedChild = false;
        private float _spawnpoint;

        public Hut(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Random ran = new Random();
            _spawnpoint = (float)ran.NextDouble()/3f + 0.4f;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            
            if (!_spawnedChild && Position.X < Game1.Instance.ScreenSize.X * _spawnpoint)
            {
                _spawnedChild = true;
                Sprite child = Game1.Instance.Child.CloneAt(Position.X + BoundingBox.Width / 4f);
                Game1.Instance.NewSprites.Add(child);
            }
            SlideLeft();
        }

        public override Sprite CloneAt(float x)
        {
            return new Hut(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }
    }
}
