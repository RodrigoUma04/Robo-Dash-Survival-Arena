using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class Coin : Entity
{
    private Entity heroRef;
    private SoundEffect _soundEffect;

    public Coin(Entity hero)
    {
        heroRef = hero;
    }
    public override void LoadContent(ContentManager content)
    {
        Animations[CStates.Idle] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Tiles/Default/coin_gold_side"),
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Tiles/Default/coin_gold"),
        };

        _soundEffect = content.Load<SoundEffect>("kenney_new-platformer-pack-1.0/Sounds/sfx_coin");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (GetBoundingBox().Intersects(heroRef.GetBoundingBox()))
        {
            _soundEffect.Play();
            IsDestroyed = true;
        }
    }
}