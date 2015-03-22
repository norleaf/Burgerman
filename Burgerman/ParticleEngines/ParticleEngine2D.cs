using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Burgerman.ParticleEngines;

namespace Burgerman
{
    public class ParticleEngine
    {
        protected Random random;
        public Vector2 EmitterLocation { get; set; }
        protected List<Particle> particles;
        public List<Texture2D> textures;
        public int TTL;
        protected Color color;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public virtual void Update()
        {
            TTL--;
            int total = 100;
            if (TTL >= 0)
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

        public virtual Particle GenerateNewParticle()
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
        }
    }
}