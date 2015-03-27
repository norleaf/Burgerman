using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class PalmTree : Sprite
    {
        private Texture2D spriteTexture;

        public PalmTree(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            this.spriteTexture = spriteTexture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SlideLeft();
            
            
            if (Position.X < -spriteTexture.Width)
            {
                Game1.Instance.Level.MarkDead(this);

            }
        }

        public PalmTree MakeNewTree(float scale, int offset)
        {
            Random ran = new Random();
            Game1 game = Game1.Instance;
            
            PalmTree tree = new PalmTree(SpriteTexture, new Vector2(x: offset, y: Game1.GroundLevel - SpriteTexture.Height * scale));
            tree.Scale = scale;
            return tree;
        }
    }
}
