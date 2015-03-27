using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class FireworksEmitter : ParticleEngine
    {
        public int timeToNext;
        public FireworksEmitter(List<Texture2D> textures, Vector2 location) : base(textures, location)
        {
        }

        public override void Update()
        {
            base.Update();
            timeToNext--;
            if (timeToNext <= 0)
            {
                EmitterLocation = new Vector2((float)random.NextDouble() * Game1.Instance.ScreenSize.X, (float)random.NextDouble() * Game1.Instance.ScreenSize.Y * 0.6f);
                TTL = 2;
                timeToNext = 60;
            }
        }
    }
}
