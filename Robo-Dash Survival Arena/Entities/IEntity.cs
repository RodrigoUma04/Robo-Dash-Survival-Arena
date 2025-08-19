using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System.Collections.Generic;

public enum CStates
{
    Idle,
    Walk,
    Jump,
    Hit,
    Duck
}

public interface IEntity
{
    Vector2 Position { get; }
    CStates CurrentState { get; }
    Dictionary<CStates, List<Texture2D>> Animations { get; }
    bool FacingRight { get; }
    string SpawnType { get; }
    int Width { get; }
    int Height { get; }
    void LoadContent(ContentManager content);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void ChangeState(CStates newState);
    void Spawn(TiledMap map, ContentManager content);
    Rectangle GetBoundingBox();
}
