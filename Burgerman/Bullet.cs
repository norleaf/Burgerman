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
        public Sprite Shooter { get; private set; }

        public Bullet(Texture2D spriteTexture, Vector2 position, Vector2 balloonPosition, Sprite shooter) : base(spriteTexture, position)
        {
            game = Game1.Instance;
            Shooter = shooter;
            float x = balloonPosition.X - position.X;
            float y = balloonPosition.Y - position.Y;
            _moveVector = new Vector2(x,y);
            _moveVector = Vector2.Normalize(_moveVector);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position = Vector2.Add(Position, _moveVector*_speed);
            if (Position.X < -100 || Position.X > game.ScreenSize.X+100 || Position.Y < -10 || Position.Y > Game1.GroundLevel)
            {
                game.MarkForRemoval(this);
            }
        }
    }
}
