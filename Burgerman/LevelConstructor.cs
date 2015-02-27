using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Burgerman
{
    class LevelConstructor
    {

        public static List<Sprite> Level1(List<Sprite> prototypes)
        {
            float width = Game1.getInstance().ScreenSize.X;
            float height = Game1.getInstance().ScreenSize.Y;
            List<Sprite> sprites = new List<Sprite>();
            sprites.Add(prototypes[0]);
            sprites.Add(prototypes[1].CloneAt(width / 2, 0));
            sprites.Add(prototypes[2].CloneAt(width *1.3f, height/10));
            sprites.Add(prototypes[3].CloneAt(width+50, 0));
            return sprites;

        }
    }

    
}
