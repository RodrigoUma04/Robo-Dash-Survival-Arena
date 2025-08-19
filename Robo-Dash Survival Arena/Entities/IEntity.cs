using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public enum CStates
{
    Idle,
    Walk,
    Jump,
    Hit,
    Duck,
    Climb
}

public interface IEntity
{
    Vector2 Position { get; }
    CStates CurrentState { get; }
    Dictionary<CStates, List<Texture2D>> Animations { get; }
    string SpawnType { get; }
    int Width { get; }
    int Height { get; }
    void LoadContent(ContentManager content);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
    void ChangeState(CStates newState);
}
