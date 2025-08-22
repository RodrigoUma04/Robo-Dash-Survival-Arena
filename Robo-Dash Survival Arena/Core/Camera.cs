using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;

public class Camera
{
    private OrthographicCamera _camera;

    private Vector2 _shakeOffset = Vector2.Zero;
    private float _shakeTimer = 0f;
    private float _shakeMagnitude = 0f;
    private Random _rand = new Random();

    public Camera(GraphicsDevice graphicsDevice, Vector2 heroPos)
    {
        this._camera = new OrthographicCamera(graphicsDevice);
        _camera.LookAt(heroPos);
    }

    public void Shake(float magnitude, float duration)
    {
        _shakeMagnitude = magnitude;
        _shakeTimer = duration;
    }

    public void Update(Vector2 heroPos, TiledMap map, GameTime gameTime)
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            _shakeOffset = new Vector2(
                ((float)_rand.NextDouble() * 2f - 1f) * _shakeMagnitude,
                ((float)_rand.NextDouble() * 2f - 1f) * _shakeMagnitude
            );
        }
        else
        {
            _shakeOffset = Vector2.Zero;
        }

        float camX = MathHelper.Clamp(heroPos.X + _shakeOffset.X, 512, map.WidthInPixels - 512);
        float camY = MathHelper.Clamp(heroPos.Y + 50 + _shakeOffset.Y, 256, map.HeightInPixels - 256);

        _camera.LookAt(new Vector2(camX, camY));
    }

    public Matrix GetViewMatrix()
    {
        return _camera.GetViewMatrix();
    }
}
