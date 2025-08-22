using System.Collections.Generic;

public class BossData : ISubject
{
    private static BossData _uniqueInstance;
    private List<IObserver> _observers = new();

    public int HP { get; private set; }
    public int MaxHP { get; private set; }

    private BossData() { }

    public static BossData getInstance()
    {
        if (_uniqueInstance == null)
            _uniqueInstance = new BossData();
        return _uniqueInstance;
    }

    public void Initialize(int maxHP)
    {
        MaxHP = maxHP;
        HP = maxHP;
        NotifyObservers("BossHP", HP);
    }

    public void TakeDamage(int amount)
    {
        HP = System.Math.Max(0, HP - amount);
        NotifyObservers("BossHP", HP);
    }

    public void RegisterObserver(IObserver observer) => _observers.Add(observer);
    public void RemoveObserver(IObserver observer) => _observers.Remove(observer);

    public void NotifyObservers(string eventType, int value)
    {
        foreach (var observer in _observers)
            observer.Update(eventType, value);
    }
}
