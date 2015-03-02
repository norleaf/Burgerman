using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Ground : Sprite
    {
        private Game1 game;

        public Ground(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            game = Game1.Instance;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SlideLeft();
            if (Position.X < -30)
            {
                MoveHorizontally(game.ScreenSize.X + 30);
            }
        }
    }
}
