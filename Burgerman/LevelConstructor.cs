using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Burgerman
{
    class LevelConstructor
    {

        public static List<Sprite> Level1(Game1 game)
        {
            float width = game.ScreenSize.X;
            float height = game.ScreenSize.Y;
            List<Sprite> sprites = new List<Sprite>();

            var ChildProto = game.Child;
            var SoldierProto = game.Soldier;
            var HelicopterProto = game.Helicopter;
            var HutProto = game.Hut;
            var JetProto = game.Jet;
            
            
            sprites.Add(HutProto.CloneAt(width / 2));
           // sprites.Add(ChildProto.CloneAt(width /2 +300));
            sprites.Add(HutProto.CloneAt(width * 1.3f));
            sprites.Add(HutProto.CloneAt(width * 1.4f));
            sprites.Add(HelicopterProto.CloneAt(width * 1.3f, height / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 3.9f, height * 3 / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 7.3f, height * 2 / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 10.8f, height * 1 / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 11.3f, height * 3 / 10));
            sprites.Add(SoldierProto.CloneAt(width + 50));
            sprites.Add(SoldierProto.CloneAt(width * 1.5f + 50));
            sprites.Add(SoldierProto.CloneAt(width * 2.5f + 50));
            sprites.Add(JetProto.CloneAt(width * 2, height * 5 / 10));
            sprites.Add(JetProto.CloneAt(width * 5, height * 1 / 10));
            sprites.Add(JetProto.CloneAt(width * 6, height * 6 / 10));
            //Add the player last, so it will be on top
            sprites.Add(game.Player);
            return sprites;

        }

        public static List<Sprite> Level2(Game1 game)
        {
            float width = game.ScreenSize.X;
            float height = game.ScreenSize.Y;
            List<Sprite> sprites = new List<Sprite>();

            var ChildProto = game.Child;
            var SoldierProto = game.Soldier;
            var HelicopterProto = game.Helicopter;
            var HutProto = game.Hut;
            var JetProto = game.Jet;


            sprites.Add(HutProto.CloneAt(width * 0.5f));
            sprites.Add(HutProto.CloneAt(width * 0.6f));
            sprites.Add(HutProto.CloneAt(width * 0.65f));
       //     sprites.Add(ChildProto.CloneAt(width /4));
            sprites.Add(HutProto.CloneAt(width * 1.3f));
            sprites.Add(HutProto.CloneAt(width * 1.4f));
            sprites.Add(HelicopterProto.CloneAt(width * 1.3f, height / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 3.9f, height * 3 / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 7.3f, height * 2 / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 10.8f, height * 1 / 10));
            sprites.Add(HelicopterProto.CloneAt(width * 11.3f, height * 3 / 10));
            sprites.Add(SoldierProto.CloneAt(width + 50));
            sprites.Add(SoldierProto.CloneAt(width * 1.5f + 50));
            sprites.Add(SoldierProto.CloneAt(width * 2.5f + 50));
            sprites.Add(JetProto.CloneAt(width * 2, height * 5 / 10));
            sprites.Add(JetProto.CloneAt(width * 5, height * 1 / 10));
            sprites.Add(JetProto.CloneAt(width * 6, height * 6 / 10));
            //Add the player last, so it will be on top
            sprites.Add(game.Player);
            return sprites;

        }
    }

    
}
