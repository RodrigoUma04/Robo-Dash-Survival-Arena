using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;

public class Camera
{
    private OrthographicCamera _camera;

    public Camera(GraphicsDevice graphicsDevice, Vector2 heroPos)
    {
        this._camera = new OrthographicCamera(graphicsDevice);
        _camera.LookAt(heroPos);
    }

    public void Update(Vector2 heroPos, TiledMap map)
    {
        float camX = MathHelper.Clamp(heroPos.X, 512, map.WidthInPixels - 512);
        float camY = MathHelper.Clamp(heroPos.Y + 50, 256, map.HeightInPixels - 256);

        _camera.LookAt(new Vector2(camX, camY));
    }

    public Matrix GetViewMatrix()
    {
        return _camera.GetViewMatrix();
    }
}
