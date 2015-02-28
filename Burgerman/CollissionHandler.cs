using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace Burgerman
{
    public class CollissionHandler : IUpdateable
    {
        private List<Sprite> _allElements;
        private List<ICollidable> _collisionListenersList;
 
        public CollissionHandler(List<Sprite> allElements)
        {
            _allElements = allElements;
            _collisionListenersList = new List<ICollidable>();
            foreach (Sprite sprite in _allElements)
            {
                if (sprite is ICollidable)
                {
                    _collisionListenersList.Add((ICollidable)sprite);
                }
            }
        }

        public List<Sprite> AllElements
        {
            get{ return _allElements; }
            set { _allElements = value; }
        }

        public List<ICollidable> CollisionListenersList
        {
            get { return _collisionListenersList; }
            set { _collisionListenersList = value; }
        }
 
        public void Update(GameTime gameTime)
        {
            foreach (var collidable in _collisionListenersList)
            {
                foreach (var element in _allElements)
                {
                    if (element.BoundingBox.Intersects(collidable.BoundingBox) && element != collidable)
                    {
                        collidable.CollideWith(element);
                    }
                }
            }
        }
 
        public bool Enabled { get; private set; }
        public int UpdateOrder { get; private set; }
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
    }
}