using System.Collections.Generic;

public class PlayerManager : ISubject
{
    private static PlayerManager _uniqueInstance;
    private List<IObserver> _observers = new();

    public int Coins { get; private set; } = 0;
    public int Lives { get; private set; } = 6;

    private PlayerManager() { }

    public static PlayerManager getInstance()
    {
        if (_uniqueInstance == null)
        {
            _uniqueInstance = new PlayerManager();
        }
        return _uniqueInstance;
    }

    public void AddCoin()
    {
        Coins++;
        if (Coins % 15 == 0)
        {
            RegainLife();
        }
        SoundManager.getInstance().Play("coin");
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

    public void RegainLife()
    {
        if (Lives == 5)
            Lives++;
        else if (Lives < 5)
            Lives += 2;
        SoundManager.getInstance().Play("life_up");
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