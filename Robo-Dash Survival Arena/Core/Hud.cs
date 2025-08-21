using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class HUD : IObserver
{
    private static HUD _uniqueInstance;
    private int _coins;
    private int _lives;

    private Texture2D _playerIcon;
    private Texture2D _livesIcon;
    private Texture2D _halfLivesIcon;
    private Texture2D _emptyLivesIcon;
    private Texture2D _coinsIcon;

    private Dictionary<int, Texture2D> _numbers = new();

    private HUD() { }

    public static HUD getInstance()
    {
        if (_uniqueInstance == null)
        {
            _uniqueInstance = new HUD();
        }
        return _uniqueInstance;
    }

    public void LoadContent(ContentManager content)
    {
        string basePath = "kenney_new-platformer-pack-1.0/Sprites/Tiles/Default/";

        _playerIcon = content.Load<Texture2D>(basePath + "hud_player_helmet_purple");
        _livesIcon = content.Load<Texture2D>(basePath + "hud_heart");
        _halfLivesIcon = content.Load<Texture2D>(basePath + "hud_heart_half");
        _emptyLivesIcon = content.Load<Texture2D>(basePath + "hud_heart_empty");
        _coinsIcon = content.Load<Texture2D>(basePath + "hud_coin");

        for (int i = 0; i < 10; i++)
            _numbers[i] = content.Load<Texture2D>(basePath + $"hud_character_{i}");
    }

    public void Update(string eventType, int value)
    {
        if (eventType == "Coins") _coins = value;
        if (eventType == "Lives") _lives = value;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int padding = 10;
        int iconSize = 64;
        int heartSpacing = 5;
        int numberSize = 50;
        int screenWidth = 1024;

        // Player icon
        spriteBatch.Draw(_playerIcon, new Rectangle(padding, padding, iconSize, iconSize), Color.White);

        // Hearts
        int maxHearts = 3;
        int fullHearts = _lives / 2;
        bool hasHalfHeart = (_lives % 2) == 1;

        for (int i = 0; i < maxHearts; i++)
        {
            Texture2D heartTexture = (i < fullHearts) ? _livesIcon :
                                     (i == fullHearts && hasHalfHeart) ? _halfLivesIcon :
                                     _emptyLivesIcon;

            int x = padding + iconSize + i * (iconSize + heartSpacing);
            spriteBatch.Draw(heartTexture, new Rectangle(x, padding, iconSize, iconSize), Color.White);
        }

        // Coins number
        string coinStr = _coins.ToString();
        int startX = screenWidth - padding - iconSize - coinStr.Length * numberSize;

        for (int i = 0; i < coinStr.Length; i++)
        {
            int digit = coinStr[i] - '0';
            int x = startX + i * numberSize;
            spriteBatch.Draw(_numbers[digit], new Rectangle(x, padding + 7, numberSize, numberSize), Color.White);
        }

        // Coin Icon
        spriteBatch.Draw(_coinsIcon, new Rectangle(screenWidth - padding - iconSize, padding, iconSize, iconSize), Color.White);
    }
}