﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Burgerman
{
    class ColissionSprite : Sprite
    {
        public ColissionSprite(Texture2D spriteTexture, Vector2 position) : base(spriteTexture, position)
        {
        }
    }
}
