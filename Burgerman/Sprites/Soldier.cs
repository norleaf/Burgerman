using Burgerman.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Soldier : AnimatedSprite, ICollidable
    {
        private double _millisecondsAtLastShot;
        private int _firingDelay = 4000;
        private Game1 game;

        private Texture2D walkingSoldierTexture;

        private Animation running;

        private int eatingTime;

        public Soldier(Texture2D spriteTexture, Vector2 position)
            : base(spriteTexture, position)
        {
            // Scale *= 0.5f;
            Name = "Soldier";
            game = Game1.Instance;
            walkingSoldierTexture = spriteTexture;
            SlideSpeed = new Vector2(-1, 0);
            running = new Animation(this, 200);
            running.Frames.Add(new Rectangle(0, 0, 64, 55));
            running.Frames.Add(new Rectangle(64, 0, 64, 55));
            running.Frames.Add(new Rectangle(128, 0, 64, 55));
            running.Frames.Add(new Rectangle(192, 0, 64, 55));
            setAnimation(running);
            CurrentState = State.Walking;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Position.X < -30)
            {
                Game1.Instance.Level.MarkDead(this);
            }
            if (CurrentState == State.Walking)
            {
                Shoot(gameTime);
            }
            if (CurrentState == State.Eating)
            {
                eatingTime--;
                if (eatingTime <= 0)
                {
                    CurrentState = State.Walking;
                    SpriteTexture = walkingSoldierTexture;
                    setAnimation(running);
                    Position = new Vector2(Position.X, Game1.GroundLevel - BoundingBox.Height - 11);
                    SlideSpeed = new Vector2(-1, 0);
                }
            }
        }

        private void Shoot(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > _millisecondsAtLastShot + _firingDelay && Position.X < game.ScreenSize.X
                && game.Player.Position.X < Position.X)
            {
                Vector2 spawnpoint = new Vector2(Position.X + BoundingBox.Width / 3f, Position.Y + SpriteTexture.Height / 3 * 2);
                Bullet bullet = (Bullet)game.LevelConstructor.BulletProto.CloneBullet(spawnpoint.X, spawnpoint.Y, this);
                game.ShotSound.Play();
                game.Level.SpawnSpriteAtRuntime(bullet);
                _millisecondsAtLastShot = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public override Sprite CloneAt(float x)
        {
            return new Soldier(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }

        public void ReceiveBurger()
        {
            SpriteTexture = game.SoldierEatingTexture;

            Animation eating = new Animation(this, 100);

            eating.Loop = true;
            eating.Frames.Add(new Rectangle(0, 45, 59, 45));
            eating.Frames.Add(new Rectangle(59, 45, 59, 45));
            eating.Frames.Add(new Rectangle(118, 45, 59, 45));
            eating.Frames.Add(new Rectangle(59, 45, 59, 45));
            eating.Frames.Add(new Rectangle(0, 45, 59, 45));
            eating.Frames.Add(new Rectangle(0, 0, 59, 45));
            eating.Frames.Add(new Rectangle(59, 0, 59, 45));
            eating.Frames.Add(new Rectangle(118, 0, 59, 45));
            eating.Frames.Add(new Rectangle(0, 0, 59, 45));
            eating.Frames.Add(new Rectangle(59, 0, 59, 45));
            eating.Frames.Add(new Rectangle(118, 0, 59, 45));
            eating.Frames.Add(new Rectangle(0, 0, 59, 45));
            eating.Frames.Add(new Rectangle(59, 0, 59, 45));
            eating.Frames.Add(new Rectangle(118, 0, 59, 45));
            eating.Frames.Add(new Rectangle(0, 0, 59, 45));
            eating.Frames.Add(new Rectangle(59, 0, 59, 45));
            eating.Frames.Add(new Rectangle(118, 0, 59, 45));
            eating.Frames.Add(new Rectangle(0, 0, 59, 45));
            eating.Frames.Add(new Rectangle(59, 0, 59, 45));
            eating.Frames.Add(new Rectangle(118, 0, 59, 45));
            eating.Frames.Add(new Rectangle(0, 0, 59, 45));
            eating.Frames.Add(new Rectangle(59, 0, 59, 45));
            eating.Frames.Add(new Rectangle(118, 0, 59, 45));

            setAnimation(eating);

            eatingTime = 300;

            CurrentState = State.Eating;
            SlideSpeed = new Vector2(-0.5f, 0);
            Position = new Vector2(Position.X, Game1.GroundLevel - 45);
        }

        public override void Die()
        {
            //If statements to correct groundlevel for death animations when coming from various states.
            game.Level.MarkDead(this);
            if (CurrentState == State.Eating)
            {
                Position = new Vector2(Position.X, Position.Y - 7);
            }
            if (CurrentState == State.Walking)
            {
                Position = new Vector2(Position.X, Position.Y + 3);
            }

            AnimatedSprite corpse = new AnimatedSprite(game.SoldierDeathTexture, Position);

            Animation fall = new Animation(corpse, 200);
            fall.Loop = false;
            fall.Frames.Add(new Rectangle(0, 0, 73, 58));
            fall.Frames.Add(new Rectangle(73, 0, 73, 58));
            fall.Frames.Add(new Rectangle(146, 0, 73, 58));
            fall.Frames.Add(new Rectangle(219, 0, 146, 58));
            corpse.setAnimation(fall);
            game.Level.SpawnSpriteAtRuntime(corpse);
            game.SoldierDeathSound.Play(0.2f, 0f, 0f);
        }

        public void CollideWith(Sprite other)
        {
            if (other is Bullet)
            {
                Bullet bullet = (Bullet)other;
                bool soldiershot = bullet.Shooter is Soldier;
                if (!soldiershot)
                {
                    Game1.Instance.Level.MarkDead(other);
                    Die();
                }
            }
            if (other is Burger)
            {
                Game1.Instance.Level.MarkDead(other);
                ReceiveBurger();
            }
            if (other is Soldier)
            {
                Soldier otherSoldier = (Soldier)other;
                if (otherSoldier.CurrentState == State.Eating)
                {

                    other.Die();
                    ReceiveBurger();
                }
                
            }
        }
    }
}
