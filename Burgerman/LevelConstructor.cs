﻿using System;
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

            float LevelLength = width * 1.6f;

            var ChildProto = game.Child;
            var SoldierProto = game.Soldier;
            var HelicopterProto = game.Helicopter;
            var HutProto = game.Hut;
            var JetProto = game.Jet;
            var CowProto = game.Cow;
            
            sprites.Add(HutProto.CloneAt(width * 0.5f));
           // sprites.Add(ChildProto.CloneAt(width /2 +300));
          //  sprites.Add(CowProto.CloneAt(width * 0.9f));
            sprites.Add(HutProto.CloneAt(width * 0.9f));
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
        //  222
        //     2
        //    2
        //   2
        //  2222
        public static List<Sprite> Level2(Game1 game)
        {
            float width = game.ScreenSize.X;
            float height = game.ScreenSize.Y;
            List<Sprite> sprites = new List<Sprite>();

            float LevelLength = width * 1.6f;

            var ChildProto = game.Child;
            var SoldierProto = game.Soldier;
            var HelicopterProto = game.Helicopter;
            var HutProto = game.Hut;
            var JetProto = game.Jet;
            var CowProto = game.Cow;

            sprites.Add(HutProto.CloneAt(width * 0.5f));
            sprites.Add(HutProto.CloneAt(width * 0.6f));
            sprites.Add(HutProto.CloneAt(width * 0.65f));
       //     sprites.Add(ChildProto.CloneAt(width /4));
            sprites.Add(HutProto.CloneAt(width * 1.0f));
            sprites.Add(HutProto.CloneAt(width * 1.3f));
            sprites.Add(HutProto.CloneAt(width * 1.4f));
            sprites.Add(CowProto.CloneAt(width * 0.9f));
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

        //  333
        //     3
        //  333
        //     3
        //  333
        public static List<Sprite> Level3(Game1 game)
        {
            float width = game.ScreenSize.X;
            float height = game.ScreenSize.Y;
            List<Sprite> sprites = new List<Sprite>();

            float LevelLength = width*2f;

            var ChildProto = game.Child;
            var SoldierProto = game.Soldier;
            var HelicopterProto = game.Helicopter;
            var HutProto = game.Hut;
            var JetProto = game.Jet;
            var CowProto = game.Cow;

            sprites.Add(CowProto.CloneAt(width * 0.2f));
            sprites.Add(CowProto.CloneAt(width * 0.3f)); 
            sprites.Add(CowProto.CloneAt(width * 0.4f));
            sprites.Add(CowProto.CloneAt(width * 0.6f));
            sprites.Add(CowProto.CloneAt(width * 0.8f));
            sprites.Add(CowProto.CloneAt(width * 1.1f));
            sprites.Add(CowProto.CloneAt(width * 1.2f));
            sprites.Add(CowProto.CloneAt(width * 1.5f));
            sprites.Add(SoldierProto.CloneAt(width * 1.5f));
            sprites.Add(SoldierProto.CloneAt(width * 1.55f));
            sprites.Add(SoldierProto.CloneAt(width * 1.575f));
            sprites.Add(SoldierProto.CloneAt(width * 1.6f));
            sprites.Add(SoldierProto.CloneAt(width * 1.65f));
            sprites.Add(SoldierProto.CloneAt(width * 1.7f));
            sprites.Add(SoldierProto.CloneAt(width * 1.75f));
            sprites.Add(SoldierProto.CloneAt(width * 1.775f));
            sprites.Add(SoldierProto.CloneAt(width * 1.825f));
            sprites.Add(SoldierProto.CloneAt(width * 1.85f));
            sprites.Add(SoldierProto.CloneAt(width * 1.875f));
            sprites.Add(SoldierProto.CloneAt(width * 1.905f));
            sprites.Add(SoldierProto.CloneAt(width * 1.925f));
            //Add the player last, so it will be on top
            sprites.Add(game.Player);
            return sprites;
        }

        public static List<Sprite> Level4(Game1 game)
        {
            float width = game.ScreenSize.X;
            float height = game.ScreenSize.Y;
            List<Sprite> sprites = new List<Sprite>();

            var ChildProto = game.Child;
            var SoldierProto = game.Soldier;
            var HelicopterProto = game.Helicopter;
            var HutProto = game.Hut;
            var JetProto = game.Jet;




            //Add the player last, so it will be on top
            sprites.Add(game.Player);
            return sprites;
        }
    }

    
}
