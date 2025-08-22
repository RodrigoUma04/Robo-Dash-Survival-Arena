using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class SlimeBoss : Enemy
{
    private float _speed = 300f;
    private float _patrolDistance = 800f;
    private Vector2 _startPosition;
    private int _direction = 1;
    private float _sleepDuration = 3f;
    private float _guardDuration = 7f;
    private float _stateTimer;

    public SlimeBoss(Entity hero, bool isGiant) : base(hero, isGiant)
    {
        BossManager.getInstance().Initialize(100);
        Width = 512;
        Height = 512;

        ChangeState(CStates.Attack);
        IsAwake = true;
        _stateTimer = 0f;

        IsBoss = true;
    }

    public override void LoadContent(ContentManager content)
    {
        string giant = IsGiant ? "Double" : "Default";
        Scale = 4f;

        Animations[CStates.Idle] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_normal_rest")
        };

        Animations[CStates.Attack] = new List<Texture2D>
        {
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_normal_walk_a"),
            content.Load<Texture2D>($"kenney_new-platformer-pack-1.0/Sprites/Enemies/{giant}/slime_normal_walk_b")
        };

        _startPosition = Position;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _stateTimer += delta;

        Hero hero = HeroRef as Hero;

        switch (CurrentState)
        {
            case CStates.Idle:
                if (hero != null)
                {
                    hero.CanShake = true;
                    if (hero.HasShaken)
                    {
                        BossManager.getInstance().TakeDamage(10);
                        hero.HasShaken = false;
                        ChangeToGuarding();
                    }
                }

                if (_stateTimer >= _sleepDuration)
                {
                    ChangeToGuarding();
                }
                break;

            case CStates.Attack:
                if (hero != null)
                    hero.CanShake = false;
                Position += new Vector2(_direction * _speed * delta, 0);

                if (Position.X > _startPosition.X + _patrolDistance)
                    _direction = -1;
                else if (Position.X < _startPosition.X - _patrolDistance)
                    _direction = 1;

                IsFlipped = _direction == 1;

                if (_stateTimer >= _guardDuration)
                {
                    ChangeToSleeping();
                }
                break;
        }
    }

    private void ChangeToSleeping()
    {
        _stateTimer = 0f;
        ChangeState(CStates.Idle);
        IsAwake = false;
    }

    private void ChangeToGuarding()
    {
        _stateTimer = 0f;
        ChangeState(CStates.Attack);
        IsAwake = true;
    }

    public override Rectangle GetBoundingBox()
    {
        return new Rectangle((int)Position.X + 32, (int)Position.Y + 192, Width - 64, Height - 192);
    }
}