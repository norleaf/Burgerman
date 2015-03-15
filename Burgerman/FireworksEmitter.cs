using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class FireworksEmitter : ParticleEngine
    {
        public FireworksEmitter(List<Texture2D> textures, Vector2 location) : base(textures, location)
        {
        }

        public override void Update()
        {
            base.Update();
            if (TTL <= 0)
            {
                EmitterLocation = new Vector2((float)random.NextDouble() * Game1.Instance.ScreenSize.X, (float)random.NextDouble() * Game1.Instance.ScreenSize.Y);
                TTL = 60;
            }
        }
    }
}
