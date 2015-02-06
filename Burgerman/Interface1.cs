using Burgerman;

using Microsoft.Xna.Framework;

interface ICollidable
{
    void CollideWith(Sprite other);

    Rectangle BoundingBox { get; }
}