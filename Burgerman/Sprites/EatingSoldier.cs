using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman.Sprites
{
    public class EatingSoldier : AnimatedSprite
    {
        private Game1 game;
        public EatingSoldier(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
            game = Game1.Instance;
        }

        public override void Die()
        {
            game.Level.MarkDead(this);
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

    }
}
