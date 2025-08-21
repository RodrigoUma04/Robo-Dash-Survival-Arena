using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

public class SoundManager : IObserver
{
    private static SoundManager _uniqueInstance;
    private Dictionary<string, SoundEffect> _sounds = new();

    private SoundManager(){}

    public static SoundManager getInstance()
    {
        if (_uniqueInstance == null)
        {
            _uniqueInstance = new SoundManager();
        }
        return _uniqueInstance;
    }

    public void LoadContent(ContentManager content)
    {
        string basePath = "kenney_new-platformer-pack-1.0/Sounds/";

        _sounds["jump"] = content.Load<SoundEffect>(basePath + "sfx_jump-high");
        _sounds["coin"] = content.Load<SoundEffect>(basePath + "sfx_coin");
        _sounds["hurt"] = content.Load<SoundEffect>(basePath + "sfx_hurt");
        _sounds["click_a"] = content.Load<SoundEffect>("UI/Sounds/click-a");
        _sounds["click_b"] = content.Load<SoundEffect> ("UI/Sounds/click-b");
    }

    public void Play(string soundKey)
    {
        if (_sounds.ContainsKey(soundKey))
            _sounds[soundKey].Play();
    }

    public void Update(string eventType, int value)
    {
        if (eventType == "Lives")
            Play("hurt");
        else if (eventType == "Coins")
            Play("coin");
    }
}
