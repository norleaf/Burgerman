using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class BloodFountain : ParticleEngine
    {
        public BloodFountain(List<Texture2D> textures, Vector2 location) : base(textures, location)
        {
        }

        public override void Update()
        {
            TTL--;
            int total = 100;
            if (TTL <= 0)
            {
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle());
                }
                color = Color.Red;
            }
            for (int particle = 0; particle < particles.Count; particle++)
            {
                Particle par = particles[particle];
                par.Update();
                par.Position = Vector2.Add(par.Position,Sprite.DefaultSlideSpeed);
            }
        }

        public override Particle GenerateNewParticle()
        {
            return base.GenerateNewParticle();
        }
    }
}
