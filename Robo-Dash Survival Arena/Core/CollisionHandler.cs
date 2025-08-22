using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;

public enum CollisionType
{
    Wall,
    Ground,
    Platform,
    Killable,
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
    private GameStateManager _gameStateManager;

    public CollisionHandler(GameStateManager gameStateManager)
    {
        _gameStateManager = gameStateManager;
    }

    public void LoadFromMap(TiledMap map, ContentManager content)
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
                    SoundManager.getInstance().Play("bump");
                    entity.Velocity = new Vector2(0, entity.Velocity.Y);
                    break;
                case CollisionType.Ground:
                    ResolveGround(entity);
                    break;
                case CollisionType.Platform:
                    if (entity.Velocity.Y > 0)
                    {
                        if (bounds.Bottom <= obj.Bounds.Top + 5 &&
                            futureBounds.Bottom >= obj.Bounds.Top)
                        {
                            ResolveGround(entity);
                        }
                    }
                    break;
                case CollisionType.Killable:
                    PlayerManager.getInstance().LoseLife();
                    if (PlayerManager.getInstance().Lives > 0)
                        _gameStateManager.SetActiveGameState("Retry");
                    else
                        _gameStateManager.SetActiveGameState("GameOver");
                    break;
                case CollisionType.Finish:
                    _gameStateManager.SetActiveGameState("NextLevel");
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

    public IEnumerable<CollisionObject> GetCollisionObjects() => _collisionObjects;
}