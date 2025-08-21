using System.Collections.Generic;

public class PlayerData : ISubject
{
    private static PlayerData _uniqueInstance;
    private List<IObserver> _observers = new();

    public int Coins { get; private set; } = 0;
    public int Lives { get; private set; } = 6;

    private PlayerData() { }

    public static PlayerData getInstance()
    {
        if (_uniqueInstance == null)
        {
            _uniqueInstance = new PlayerData();
        }
        return _uniqueInstance;
    }

    public void AddCoin()
    {
        Coins++;
        NotifyObservers("Coins", Coins);
    }

    public void LoseLife()
    {
        Lives -= 2;
        SoundManager.getInstance().Play("hurt");
        NotifyObservers("Lives", Lives);
    }

    public void LoseHalfLife()
    {
        Lives--;
        SoundManager.getInstance().Play("hurt");
        NotifyObservers("Lives", Lives);
    }

    public void RegisterObserver(IObserver observer) => _observers.Add(observer);
    public void RemoveObserver(IObserver observer) => _observers.Remove(observer);

    public void NotifyObservers(string eventType, int value)
    {
        foreach (var observer in _observers)
            observer.Update(eventType, value);
    }
}