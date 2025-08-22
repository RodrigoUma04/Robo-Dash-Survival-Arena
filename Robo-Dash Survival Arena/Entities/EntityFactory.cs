using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

public static class EntityFactory
{
    public static Entity Create(string type, Entity heroRef, Vector2 position, ContentManager content)
    {
        Entity entity = type switch
        {
            "hero" => Hero.getInstance(),
            "spike" => new Spike(heroRef, false),
            "g_spike" => new Spike(heroRef, true),
            "bee" => new Bee(heroRef, false),
            "g_fire_slime" => new FireSlime(heroRef, true),
            "saw" => new Saw(heroRef, false),
            "coin" => new Coin(heroRef),
            "finish" => new Flag(),
            "slime_boss" => new SlimeBoss(heroRef, true),
            _ => null
        };

        if (entity != null)
        {
            entity.Position = position;
            entity.LoadContent(content);
        }

        return entity;
    }
}