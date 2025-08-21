using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

public class SoundManager : IObserver
{
    private static SoundManager _uniqueInstance;
    private Dictionary<string, SoundEffect> _sounds = new();
    private Dictionary<string, Song> _songs = new();

    private SoundManager() { }

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
        _sounds["bump"] = content.Load<SoundEffect>(basePath + "sfx_bump");
        _sounds["life_up"] = content.Load<SoundEffect>(basePath + "sfx_gem");
        _sounds["click_a"] = content.Load<SoundEffect>("UI/Sounds/click-a");
        _sounds["click_b"] = content.Load<SoundEffect>("UI/Sounds/click-b");
        _sounds["game_over"] = content.Load<SoundEffect>(basePath + "sfx_disappear");

        _songs["menu"] = content.Load<Song>("Music/Let_s Start - Daniel N. Martin - SoundLoadMate.com");
        _songs["level1"] = content.Load<Song>("Music/Ambient 2");
        _songs["level2"] = content.Load<Song>("Music/1 Scorched Sands - Blue Lava");
        _songs["boss"] = content.Load<Song>("Music/Alien Boss Battle Music Loop Pack for Game Developers - Taris Studios Video Game Music _ Audio Design - SoundLoadMate.com");
    }

    public void Play(string soundKey)
    {
        if (_sounds.ContainsKey(soundKey))
            _sounds[soundKey].Play();
    }

    public void PlaySong(string songKey, bool loop = true)
    {
    if (_songs.ContainsKey(songKey))
    {
        if (MediaPlayer.State == MediaState.Playing)
            MediaPlayer.Stop();

        MediaPlayer.IsRepeating = loop;
        MediaPlayer.Play(_songs[songKey]);
    }
    }

    public void Update(string eventType, int value)
    {
        if (eventType == "Lives")
            Play("hurt");
        else if (eventType == "Coins")
            Play("coin");
    }
}
