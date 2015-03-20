using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Burgerman.Sprites;
using Microsoft.Xna.Framework;

namespace Burgerman
{
    public class LevelConstructor
    {
        Level level;
        private Game1 game;
        private float width;
        private float height;
        private Random ran;

        public Child ChildProto { get; private set; }
        public Soldier SoldierProto { get; private set; }
        public Hut HutProto { get; private set; }
        public Jet JetProto { get; private set; }
        public Helicopter HelicopterProto { get; private set; }
        public Cow CowProto { get; private set; }
        public Cloud CloudProto { get; set; }
        public Mountain MountainProto { get; set; }
        public PalmTree PalmProto { get; set; }
        public Ground GroundProto { get; set; }
        public Bullet BulletProto { get; set; }

        public LevelConstructor(Child childProto, Soldier soldierProto, Hut hutProto, Jet jetProto, Helicopter helicopterProto,Cow cowProto, Cloud cloudProto, Mountain mountainProto,
            PalmTree palm, Ground ground, Bullet bullet)
        {
            game = Game1.Instance;
            level = new Level();
            ran = new Random();
            width = game.ScreenSize.X;
            height = game.ScreenSize.Y;

            ChildProto = childProto;
            SoldierProto = soldierProto;
            HutProto = hutProto;
            JetProto = jetProto;
            HelicopterProto = helicopterProto;
            CowProto = cowProto;
            CloudProto = cloudProto;
            MountainProto = mountainProto;
            PalmProto = palm;
            GroundProto = ground;
            BulletProto = bullet;
        }


        public void GenerateClouds()
        {
            //Generate clouds at beginning of level
            for (int i = 0; i < 8; i++)
            {
                float scale = ((float)ran.Next(3, 7) / 10);
                int offsetX = ran.Next((int)width + 100);
                int offsetY = ran.Next((int)height / 3);
                Sprite cloud = CloudProto.CloneAt(offsetX, offsetY);
                cloud.Scale = scale;
                cloud.SlideSpeed = new Vector2(-0.375f, 0);
                level.BackgroundSprites.Add(cloud);
            }
        }

        public void GenerateMountains()
        {
            int amount = ran.Next(3) + 1;
            for (int i = 0; i < amount; i++)
            {
                Sprite mountain = MountainProto.CloneAt(ran.Next((int) width));
                mountain.SlideSpeed = new Vector2(-0.25f,0);
                level.BackgroundSprites.Add(mountain);
            }
        }

        public void GenerateGround()
        {
            for (int i = 0; i < width / 30 + 3; i++)
            {
                Ground ground = (Ground) GroundProto.CloneAt(30*i);
                level.BackgroundSprites.Add(ground);
            }
        }

        public void GenerateTrees()
        {
            //Generate trees at beginning of level
            for (int i = 0; i < 15; i++)
            {
                float scale = ((float)ran.Next(7, 11) / 10);
                int offset = ran.Next((int)width + 100);
                level.BackgroundSprites.Add(PalmProto.MakeNewTree(scale, offset));
            }
        }

        public void CleanData()
        {
            level.BackgroundSprites.Clear();
            level.NewSprites.Clear();
            level.LevelSprites.Clear();
            level.DeadSprites.Clear();
        }

        
        public void NewLevelStart()
        {
            CleanData();
            GenerateGround();
            GenerateMountains();
            GenerateTrees();
            GenerateClouds();
            level.ParticleEngines.Clear();
        }

        public Level IntroScreen()
        {
            GenerateClouds();
            level.Palms = false;
            return level;
        }

        public Level Level0A()
        {
            NewLevelStart();
            level.LevelLength = width*1;

            level.LevelSprites.Add(ChildProto.CloneAt(width * 0.65f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 0.95f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.1f));
            //Add the player last, so it will be on top of everything
            level.LevelSprites.Add(game.Player);
            level.Palms = true;
            return level;
        }

        public Level Level0B()
        {
            NewLevelStart();
            level.LevelLength = width * 0.3f;

            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 1.3f, height / 10));
            //Add the player last, so it will be on top of everything
            level.LevelSprites.Add(game.Player);
            level.Palms = true;
            return level;
        }

        public Level Level1()
        {
            NewLevelStart();
            level.LevelLength = width * 1.6f;

            level.LevelSprites.Add(HutProto.CloneAt(width * 0.5f));
            level.LevelSprites.Add(HutProto.CloneAt(width * 0.9f));
            level.LevelSprites.Add(HutProto.CloneAt(width * 1.4f));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 1.3f, height / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 3.9f, height * 3 / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 7.3f, height * 2 / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 10.8f, height * 1 / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 11.3f, height * 3 / 10));
            level.LevelSprites.Add(SoldierProto.CloneAt(width + 50));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.5f + 50));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 2.5f + 50));
            level.LevelSprites.Add(JetProto.CloneAt(width * 2, height * 5 / 10));
            level.LevelSprites.Add(JetProto.CloneAt(width * 5, height * 1 / 10));
            level.LevelSprites.Add(JetProto.CloneAt(width * 6, height * 6 / 10));
            //Add the player last, so it will be on top
            level.LevelSprites.Add(game.Player);
            level.Palms = true;
            return level;

        }
        //  222
        //     2
        //    2
        //   2
        //  2222
        public Level Level2()
        {
            NewLevelStart();
            level.LevelLength = width * 1.6f;

            level.LevelSprites.Add(HutProto.CloneAt(width * 0.5f));
            level.LevelSprites.Add(HutProto.CloneAt(width * 0.6f));
            level.LevelSprites.Add(HutProto.CloneAt(width * 0.65f));
            level.LevelSprites.Add(HutProto.CloneAt(width * 1.0f));
            level.LevelSprites.Add(HutProto.CloneAt(width * 1.3f));
            level.LevelSprites.Add(HutProto.CloneAt(width * 1.4f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 0.9f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 1.45f));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 1.3f, height / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 3.9f, height * 3 / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 7.3f, height * 2 / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 10.8f, height * 1 / 10));
            level.LevelSprites.Add(HelicopterProto.CloneAt(width * 11.3f, height * 3 / 10));
            level.LevelSprites.Add(SoldierProto.CloneAt(width + 50));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.5f + 50));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 2.5f + 50));
            level.LevelSprites.Add(JetProto.CloneAt(width * 2, height * 5 / 10));
            level.LevelSprites.Add(JetProto.CloneAt(width * 5, height * 1 / 10));
            level.LevelSprites.Add(JetProto.CloneAt(width * 6, height * 6 / 10));
            //Add the player last, so it will be on top
            level.LevelSprites.Add(game.Player);
            return level;

        }

        //  333
        //     3
        //  333
        //     3
        //  333
        public Level Level3()
        {
            NewLevelStart();
            level.LevelLength = width;
            
            level.LevelSprites.Add(CowProto.CloneAt(width * 0.2f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 0.3f)); 
            level.LevelSprites.Add(CowProto.CloneAt(width * 0.4f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 0.6f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 0.8f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 1.1f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 1.2f));
            level.LevelSprites.Add(CowProto.CloneAt(width * 1.5f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.5f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.55f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.575f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.6f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.65f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.7f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.75f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.775f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.825f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.85f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.875f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.905f));
            level.LevelSprites.Add(SoldierProto.CloneAt(width * 1.925f));
            //Add the player last, so it will be on top
            level.LevelSprites.Add(game.Player);
            return level;
        }

        public Level Level4()
        {
            NewLevelStart();
            level.LevelLength = width * 2f;


            //Add the player last, so it will be on top
            level.LevelSprites.Add(game.Player);
            return level;
        }
    }

    
}
