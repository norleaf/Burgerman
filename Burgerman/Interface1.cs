using Burgerman;

using Microsoft.Xna.Framework;

public interface ICollidable
{
    void CollideWith(Sprite other);

    Rectangle BoundingBox { get; }
}