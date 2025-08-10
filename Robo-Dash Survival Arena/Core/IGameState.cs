using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public interface IGameState
{
    void LoadContent(ContentManager content);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}