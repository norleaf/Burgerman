using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burgerman
{
    public class ScreenShake
    {
        private int TTL;
        private int strength;
        private Random ran;

        public ScreenShake(int ttl, int strength)
        {
            TTL = ttl;
            this.strength = strength;
            ran = new Random();
        }

        public void Update()
        {
            if (TTL >= 0)
            {
                TTL--;
                Sprite.Shake = new Vector2(x: ran.Next(strength) - (strength/2), y: ran.Next(strength) - (strength/2));
            }
            
        }
    }
}
