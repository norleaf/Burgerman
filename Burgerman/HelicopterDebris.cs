using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class HelicopterDebris : ParticleEngine
    {
        public HelicopterDebris(List<Texture2D> textures, Vector2 location) : base(textures, location)
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
                color = new Color(
                    (float)(random.NextDouble() + 0.5f),
                    (float)(random.NextDouble() + 0.5f),
                    (float)(random.NextDouble() + 0.5f));
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
            velocity.Normalize();
            velocity *= (float)random.NextDouble() * 3.5f + 0.3f;
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

            float size = (float)random.NextDouble();
            int ttl = 60 + random.Next(140);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public ParticleEngine Clone(Vector2 center)
        {
            return new HelicopterDebris(textures,center);
        }
    }
}
