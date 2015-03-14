using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Level
    {
        public List<Sprite> LevelSprites { get; set; }
        public List<Sprite> BackgroundSprites { get; set; }
        public List<Sprite> DeadSprites { get; set; }
        public List<Sprite> NewSprites { get; set; }
        public List<ParticleEngine> ParticleEngines { get; set; }
        public float LevelLength { get; set; }
        private double _timeSinceLastTree;
        private int _treeDelay = 7000;
        private Random _ran;
        private Game1 game;

        public Level()
        {
            LevelSprites = new List<Sprite>();
            NewSprites = new List<Sprite>();
            BackgroundSprites = new List<Sprite>();
            DeadSprites = new List<Sprite>();
            ParticleEngines = new List<ParticleEngine>();

            _ran = new Random();
            game = Game1.Instance;
        }

        public void SpawnSpriteAtRuntime(Sprite sprite)
        {
            NewSprites.Add(sprite);
        }

        //This method takes the sprites from the temporary list and adds them to the main sprites list prior to running the update loop
        private void AddNewSprites()
        {
         
            foreach (Sprite sprite in NewSprites)
            {
                LevelSprites.Add(sprite);
                if (sprite is ICollidable)
                {
                    game.CollisionHandler.CollisionListenersList.Add((ICollidable)sprite);
                }
            }
            NewSprites.Clear();
        }

        public void MarkDead(Sprite sprite)
        {
            DeadSprites.Add(sprite);
        }

        public void RemoveDeadSprites()
        {
            foreach (Sprite sprite in DeadSprites)
            {
                LevelSprites.Remove(sprite);
                Game1.Instance.CollisionHandler.AllElements.Remove(sprite);
                if (sprite is ICollidable)
                {
                    Game1.Instance.CollisionHandler.CollisionListenersList.Remove((ICollidable)sprite);
                }
            }
            DeadSprites.Clear();
        }

        public void Update(GameTime gameTime)
        {
            //Start by removing sprites that died in the previous tick
            RemoveDeadSprites();

            //The we add all the sprites that were spawned in the previous tick
            AddNewSprites();

            //We update all background sprites
            foreach (Sprite backgroundSprite in BackgroundSprites)
            {
                backgroundSprite.Update(gameTime);
            }

            //Is it time to spawn a new tree?
            if (gameTime.TotalGameTime.TotalMilliseconds > _timeSinceLastTree + _treeDelay)
            {
                float scale = ((float)_ran.Next(7, 11) / 10);
                int offset = (int)game.ScreenSize.X + _ran.Next(200);
                BackgroundSprites.Add(game.LevelConstructor.PalmProto.MakeNewTree(scale, offset));
                _timeSinceLastTree = gameTime.TotalGameTime.TotalMilliseconds;
                _treeDelay = 3500 + _ran.Next(3500);
            }
            
            //Update all our foreground sprites
            foreach (Sprite sprite in LevelSprites)
            {
                sprite.Update(gameTime);
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in BackgroundSprites)
            {
                sprite.Draw(spriteBatch);
            }
            foreach (Sprite sprite in LevelSprites)
            {
                sprite.Draw(spriteBatch);
            }
        }




    }
}
