using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class Burger : AnimatedSprite
    {
        public Vector2 Velocity { get; set; }
        

        public Burger(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            Velocity = new Vector2(1.5f,0);
        }

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Velocity = Vector2.Add(Velocity, Game1.Gravity);
            NextPosition = Vector2.Add(Position, Velocity);
            if (NextPosition.Y+BoundingBox.Height > Game1.GroundLevel)
            {
                Game1.Instance.Level.MarkDead(this);
            }
            else
            {
                Position = NextPosition;
            }
        }
    }
}
