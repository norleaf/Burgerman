using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Burgerman.ParticleEngines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Bullet : AnimatedSprite
    {
        private Vector2 _moveVector;
        private float _speed = 10.5f;
        private int _spray = 50;
        private Game1 game;
        public Sprite Shooter { get; private set; }
        public BulletTracer Tracer { get; set; }

        public Bullet(Texture2D spriteTexture, Vector2 position, Sprite shooter) : base(spriteTexture, position)
        {
            Random ran = new Random();
            SlideSpeed = new Vector2(0,0);
            game = Game1.Instance;
            Shooter = shooter;
            List<Texture2D> tracerTex = new List<Texture2D>();
            tracerTex.Add(game.BulletTracer);
            Tracer = new BulletTracer(tracerTex, position);
            float x = game.Player.Center.X - position.X + (float)(ran.NextDouble() - 1) * _spray;
            float y = game.Player.Center.Y + -position.Y + (float)(ran.NextDouble() - 1) * _spray;
            _moveVector = new Vector2(x,y);
            _moveVector = Vector2.Normalize(_moveVector);
        }

        public override void Update(GameTime gameTime)
        {
            Position = Vector2.Add(Position, _moveVector *_speed);
            Tracer.EmitterLocation = Center;
            Tracer.Update();
            if (Position.X < -100 || Position.X > game.ScreenSize.X+100 || Position.Y < -10 || Position.Y > Game1.GroundLevel)
            {
                game.Level.MarkDead(this);
            }
        }

        public Sprite CloneBullet(float x, float y, Sprite shooter)
        {
            return new Bullet(SpriteTexture, new Vector2(x, y), shooter);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Tracer.Draw(spriteBatch);
        }
    }
}
