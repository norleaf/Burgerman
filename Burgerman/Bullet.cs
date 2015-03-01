using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Bullet : Sprite
    {
        private Vector2 _moveVector;
        private float _speed = 3.9f;
        private Game1 game;

        public Bullet(Texture2D spriteTexture, Vector2 position, Vector2 balloonPosition) : base(spriteTexture, position)
        {
            game = Game1.Instance;
            float x = balloonPosition.X - position.X;
            float y = balloonPosition.Y - position.Y;
            _moveVector = new Vector2(x,y);
            _moveVector = Vector2.Normalize(_moveVector);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position = Vector2.Add(Position, _moveVector*_speed);
            if (PositionX < -100 || PositionX > game.ScreenSize.X+100 || PositionY < -10 || PositionY > Game1.GroundLevel)
            {
                game.MarkForRemoval(this);
            }
        }
    }
}
