﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Soldier: AnimatedSprite, ICollidable
    {
        private double _millisecondsAtLastShot;
        private int _firingDelay = 4000;
        private Game1 game;

        public Soldier(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
           // Scale *= 0.5f;
            Name = "Soldier";
            game = Game1.Instance;
            SlideSpeed = new Vector2(-1,0);
            Animation running = new Animation(this, 200);
            running.Frames.Add(new Rectangle(0, 0, 64, 55));
            running.Frames.Add(new Rectangle(64, 0, 64, 55));
            running.Frames.Add(new Rectangle(128, 0, 64, 55));
            running.Frames.Add(new Rectangle(192, 0, 64, 55));
            setAnimation(running);
            _state = State.Walking;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            if (Position.X < -30)
            {
                Game1.Instance.Level.MarkDead(this);
            }
            if (_state == State.Walking)
            {
                Shoot(gameTime);
            }
            
        }

        private void Shoot(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > _millisecondsAtLastShot + _firingDelay && Position.X < game.ScreenSize.X)
            {
                Bullet bullet = (Bullet)game.LevelConstructor.BulletProto.CloneBullet(Position.X + BoundingBox.Width / 3f, Position.Y + SpriteTexture.Height / 3 * 2, this);
                game.ShotSound.Play();
                game.Level.SpawnSpriteAtRuntime(bullet);
                _millisecondsAtLastShot = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public override Sprite CloneAt(float x)
        {
            return new Soldier(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }

        public void CollideWith(Sprite other)
        {
            if (other is Bullet )
            {
                Bullet bullet = (Bullet) other;
                bool soldiershot = bullet.Shooter is Soldier;
                if (!soldiershot)
                {
                    Game1.Instance.Level.MarkDead(this);
                    Game1.Instance.Level.MarkDead(other);
                }
            }
            if (other is Burger)
            {
                _state = State.Waiting;
                SlideSpeed = Sprite.DefaultSlideSpeed;
                Game1.Instance.Level.MarkDead(other);
            }
        }
    }
}