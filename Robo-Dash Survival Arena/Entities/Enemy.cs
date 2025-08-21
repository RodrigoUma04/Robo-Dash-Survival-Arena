using Microsoft.Xna.Framework;

public abstract class Enemy : Entity
{
    public float DetectionRange { get; set; } = 300f;
    public bool IsAwake { get; private set; } = false;

    public bool IsGiant { get; set; } = false;

    protected Entity HeroRef;

    public Enemy(Entity hero, bool isGiant)
    {
        HeroRef = hero;
        IsGiant = isGiant;

        if (IsGiant)
        {
            Width = 128;
            Height = 128;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!IsAwake && Vector2.Distance(Position, HeroRef.Position) < DetectionRange)
        {
            IsAwake = true;
            ChangeState(CStates.Attack);
        }

        if (IsAwake)
        {
            if (GetBoundingBox().Intersects(HeroRef.GetBoundingBox()))
            {
                OnHeroCollision();
            }
        }
    }

    protected virtual void OnHeroCollision()
    {
        if (HeroRef is Hero hero)
        {
            hero.TakeDamage();
        }
    }

    public override Rectangle GetBoundingBox()
    {
        if(IsGiant)
            return new Rectangle((int)Position.X, (int)Position.Y + 20, Width, Height - 20);

        return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }
}
