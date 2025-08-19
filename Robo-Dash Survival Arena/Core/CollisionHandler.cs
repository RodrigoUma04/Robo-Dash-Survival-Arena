using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;

public enum CollisionType
{
    Wall,
    Ground,
    Platform,
    Killable,
    Ladder,
    Finish
}

public class CollisionObject
{
    public Rectangle Bounds { get; }
    public CollisionType Type { get; }

    public CollisionObject(Rectangle bounds, CollisionType type)
    {
        Bounds = bounds;
        Type = type;
    }
}

public class CollisionHandler
{
    private readonly List<CollisionObject> _collisionObjects = new();

    public void LoadFromMap(TiledMap map)
    {
        _collisionObjects.Clear();

        var layer = map.GetLayer<TiledMapObjectLayer>("Collisions");
        if (layer == null) return;

        foreach (var obj in layer.Objects)
        {
            if (!obj.Properties.ContainsKey("type")) continue;

            string typeStr = obj.Properties["type"].ToString().ToLower();
            CollisionType type = typeStr switch
            {
                "wall" => CollisionType.Wall,
                "ground" => CollisionType.Ground,
                "platform" => CollisionType.Platform,
                "killable" => CollisionType.Killable,
                "ladder" => CollisionType.Ladder,
                "finish" => CollisionType.Finish,
                _ => CollisionType.Wall
            };

            var bounds = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)obj.Size.Width, (int)obj.Size.Height);
            _collisionObjects.Add(new CollisionObject(bounds, type));
        }
    }

    public void Resolve(Entity entity, float dt)
    {
        entity.Velocity += new Vector2(0, 1200f * dt);

        var bounds = entity.GetBoundingBox();
        Rectangle futureBounds = bounds;
        futureBounds.Offset((int)(entity.Velocity.X * dt), (int)(entity.Velocity.Y * dt));

        foreach (var obj in _collisionObjects)
        {
            if (!futureBounds.Intersects(obj.Bounds))
                continue;
            switch (obj.Type)
            {
                case CollisionType.Wall:
                    Console.WriteLine("Wall touched");
                    ResolveWall(entity);
                    break;
                case CollisionType.Ground:
                    Console.WriteLine("Ground touched");
                    ResolveGround(entity);
                    break;
                case CollisionType.Platform:
                    if (entity.Velocity.Y >= 0 && futureBounds.Bottom <= obj.Bounds.Top)
                    {
                        Console.WriteLine("Standing on a platform");
                        entity.Velocity = new Vector2(entity.Velocity.X, 0);
                    }
                    break;
                case CollisionType.Killable:
                    Console.WriteLine("Killable touched");
                    break;
                case CollisionType.Ladder:
                    Console.WriteLine("Ladder touched");
                    break;
                case CollisionType.Finish:
                    Console.WriteLine("Finish touched");
                    break;
            }
        }

        entity.Position += entity.Velocity * dt;
    }

    private void ResolveGround(Entity entity)
    {
        if (entity.Velocity.Y > 0)
        {
            entity.Velocity = new Vector2(entity.Velocity.X, 0);
            entity.IsGrounded = true;
        }
    }

    private void ResolveWall(Entity entity)
    {
        entity.Velocity = new Vector2(0, entity.Velocity.Y);
    }

    public IEnumerable<CollisionObject> GetCollisionObjects() => _collisionObjects;
}