using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    public class Sprite
    {
        public string Name { get; set; }
        public Vector2 SlideSpeed { get; set; }
        public Random Ran { get; private set; }
        
        public Sprite(Texture2D spriteTexture, Vector2 position)
        {
            Ran = new Random();
            SlideSpeed = new Vector2(-0.5f,0);
            this.SpriteTexture = spriteTexture;
            this.Position = position;
            Scale = 1;
        }

        public virtual Vector2 Center
        {
            get { return new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y); }
        }

        public virtual float Scale{ get; set; }

        public Texture2D SpriteTexture { get; set; }
        public virtual Rectangle SourceRectangle { get; set; }

        public virtual float Rotation { get; set; }

        public Vector2 NextPosition { get; set; }

        public Vector2 Position { get; set; }

        public void MoveHorizontally(float distance)
        {
            Position = Vector2.Add(Position, new Vector2(distance, 0));
        }
        public void MoveVertically(float distance)
        {
            Position = Vector2.Add(Position, new Vector2(0, distance));
        }

        public virtual Vector2 Origin { get; set; }

        public virtual SpriteEffects SpriteEffect { get; set; }

        public virtual Rectangle BoundingBox
        {
            get
            {
                Rectangle result;
                Vector2 spritesize;

                if (SourceRectangle.IsEmpty)
                {
                    spritesize = new Vector2(SpriteTexture.Width, SpriteTexture.Height);
                }
                else
                {
                    spritesize = new Vector2(SourceRectangle.Width, SourceRectangle.Height);
                }
                result = new Rectangle((int)Position.X, (int)Position.Y, (int)(spritesize.X * Scale), (int)(spritesize.Y * Scale));
         //       result.Offset((int)(-Origin.X * Scale), (int)(-Origin.Y * Scale));
                return result;
            }
        }

        public virtual void Die() { }

        public virtual void Update(GameTime gameTime)
        {
            // sprite logic goes here...
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, null, Color.White, Rotation, Origin, new Vector2(Scale, Scale), SpriteEffect, 0f);
        }

        public virtual Sprite CloneAt(float x, float y)
        {
            return new Sprite(SpriteTexture,new Vector2(x,y));
        }

        public virtual Sprite CloneAt(float x)
        {
            return new Sprite(SpriteTexture, new Vector2(x, Game1.GroundLevel - BoundingBox.Height));
        }

        protected void SlideLeft()
        {
            Position = Vector2.Add(Position,SlideSpeed);
        }

    }
}