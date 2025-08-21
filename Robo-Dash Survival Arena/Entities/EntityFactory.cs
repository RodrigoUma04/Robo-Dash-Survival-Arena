using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

public static class EntityFactory
{
    public static Entity Create(string type, Entity heroRef, Vector2 position, ContentManager content)
    {
        Entity entity = type switch
        {
            "hero" => new Hero(),
            "spike" => new Spike(heroRef, false),
            "g_spike" => new Spike(heroRef, true),
            "barnacle" => new Barnacle(heroRef, false),
            "g_barnacle" => new Barnacle(heroRef, true),
            "coin" => new Coin(heroRef),
            "finish" => new Flag(),
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