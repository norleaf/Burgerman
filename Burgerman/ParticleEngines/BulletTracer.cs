using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman.ParticleEngines
{
    public class BulletTracer : ParticleEngine
    {
        public BulletTracer(List<Texture2D> textures, Vector2 location) : base(textures, location)
        {
            TTL = 1000;
            color = Color.White;
        }

        public override void Update()
        {
            TTL--;
            int total = 20;
            if (TTL >= 0)
            {
                for (int i = 0; i < total; i++)
                {
                    particles.Add(GenerateNewParticle());
                }
                //color = new Color(
                //    (float)(random.NextDouble() + 0.5f),
                //    (float)(random.NextDouble() + 0.5f),
                //    (float)(random.NextDouble() + 0.5f));
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public override Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                                    (float)(random.NextDouble() * 2 - 1),
                                    (float)(random.NextDouble() * 2 - 1));
            //velocity.Normalize();
           // velocity *= (float)random.NextDouble() * 3.5f + 0.3f;
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

            float size = (float)random.NextDouble();
            int ttl = 6 + random.Next(12);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
    }
}
