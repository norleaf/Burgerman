using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class PalmTree : Sprite
    {
        private Texture2D spriteTexture;

        public PalmTree(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            this.spriteTexture = spriteTexture;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Scroll();
            
            
            if (PositionX < -spriteTexture.Width)
            {
                Game1.getInstance().markForRemoval(this);

            }
        }

        public static PalmTree MakeNewTree(Texture2D spriteTexture, float scale, int offset)
        {
            Random ran = new Random();
            Game1 game = Game1.getInstance();
            
            PalmTree tree = new PalmTree(spriteTexture, new Vector2(offset, game.ScreenSize.Y * 0.8f - spriteTexture.Height * scale));
            tree.Scale = scale;
            return tree;
        }
    }
}
