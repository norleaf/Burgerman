using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Burgerman
{
    class Animation : IUpdateable
    {
        private double _milisecondsSinceLastFrameUpdate = 0;
        private Sprite _sprite;
        private int _currentFrame = 0;
        private List<Rectangle> _frames = new List<Rectangle>();
        private bool _loop;
        private int _delay;

        public bool Loop
        {
            get { return _loop; }
            set { _loop = value; }
        }

        public int Delay
        {
            get { return _delay; }
            set { _delay = value; }
        }

        public List<Rectangle> Frames
        {
            get { return _frames; }
            set { _frames = value; }
        }

        public Animation(Sprite sprite)
        {
            _sprite = sprite;
            _delay = 200;
            _loop = true;
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > _milisecondsSinceLastFrameUpdate + Delay)
            {
                _sprite.SourceRectangle = NextFrame();
                _milisecondsSinceLastFrameUpdate = gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        private Rectangle NextFrame()
        {
            if (_currentFrame == _frames.Count - 1 && _loop) _currentFrame = 0;
            else if (_currentFrame < _frames.Count - 1) _currentFrame++;
            return Frames[_currentFrame];
        }

        public bool Enabled { get; private set; }
        public int UpdateOrder { get; private set; }
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
    }

    //public void ButtonADown(InputController.ButtonStates buttonStates)
    //{
    //    animation = new Animation(this);
    //    animation.Loop = false;
    //    animation.Frames.Add(new Rectangle(288, 96, 72, 96));
    //    animation.Frames.Add(new Rectangle(360, 96, 72, 96));
    //    animation.Frames.Add(new Rectangle(432, 96, 72, 96));
    //    _state = State.Jumping;

    //}

    //public void ButtonBDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonXDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonYDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonLeftShoulderDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonRightShoulderDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonStartDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonBackDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonLeftStickDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}

    //public void ButtonRightStickDown(InputController.ButtonStates buttonStates)
    //{
    //    throw new NotImplementedException();
    //}
}

