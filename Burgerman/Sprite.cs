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
        public Vector2 Speed { get; set; }
        public Random Ran { get; private set; }
        
        public Sprite(Texture2D spriteTexture, Vector2 position)
        {
            Ran = new Random();
            Speed = new Vector2(-0.3f,0);
            this.SpriteTexture = spriteTexture;
            this.Position = position;
            Scale = 1;
            //_bottom = spriteTexture.Height;
        }

       

        //private float _bottom;

        //public virtual float Bottom
        //{
        //    get { return _bottom + Position.Y; }
        //    set { _bottom = value; }
        //}


        public virtual Vector2 Center
        {
            get { return new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y); }
        }

        public virtual float Scale{ get; set; }

        public Texture2D SpriteTexture { get; set; }
        public virtual Rectangle SourceRectangle { get; set; }

        public virtual float Rotation { get; set; }

        public Vector2 NextPosition { get; set; }

        public Vector2 Position
        {
            get { return new Vector2(PositionX, PositionY); }
            set
            {
                PositionX = value.X;
                PositionY = value.Y;
            }
        }
        public float PositionY { get; set; }

        public float PositionX { get; set; }

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
                result = new Rectangle((int)PositionX, (int)PositionY, (int)(spritesize.X * Scale), (int)(spritesize.Y * Scale));
         //       result.Offset((int)(-Origin.X * Scale), (int)(-Origin.Y * Scale));
                return result;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            // sprite logic goes here...
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteTexture, Position, null, Color.White, Rotation, Origin, new Vector2(Scale, Scale), SpriteEffect, 0f);
        }

        public virtual Sprite CloneAt(float x, float y)
        {
            return new Sprite(SpriteTexture,new Vector2(x,y));
        }

        public virtual Sprite CloneAt(float x)
        {
            return new Sprite(SpriteTexture, new Vector2(x, Game1.groundLevel - BoundingBox.Height));
        }

        protected void Move()
        {
            Position = Vector2.Add(Position,Speed);
        }

    }
}