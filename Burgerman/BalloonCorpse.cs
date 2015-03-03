using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class BalloonCorpse : AnimatedSprite
    {
        private Animation ignite;
        private Animation fall;
        private Animation crash;
        private Animation burn;
        private Vector2 Velocity { get; set; } 

        public BalloonCorpse(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Velocity = new Vector2(Game1.Instance.Player.MoveVector.X, 0);
           // SlideSpeed = new Vector2(0, 0);
            
            ignite = new Animation(this, 100);
            ignite.Loop = false;
            ignite.Frames.Add(new Rectangle(0, 0, 100, 247));
            ignite.Frames.Add(new Rectangle(100, 0, 100, 247));
            ignite.Frames.Add(new Rectangle(300, 0, 100, 247));
         
            setAnimation(ignite);

            fall = new Animation(this, 100);
            fall.Frames.Add(new Rectangle(200, 0, 100, 247));
            fall.Frames.Add(new Rectangle(300, 0, 100, 247));
            fall.Frames.Add(new Rectangle(400, 0, 100, 247));

            crash = new Animation(this,200);
            crash.Loop = false;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Position.Y + BoundingBox.Height < Game1.GroundLevel)
            {
                Velocity = Vector2.Add(Velocity, Game1.Gravity);
                Position = Vector2.Add(Position, Velocity);
            }
            
        }

        public override void AnimationComplete()
        {
            if (Position.Y + BoundingBox.Height < Game1.GroundLevel)
            {
                setAnimation(fall);
            }
            else
            {
                setAnimation(burn);
            }
            
        }
    }
}
