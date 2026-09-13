using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using System;

namespace IbexGame.GameObjects;

public enum flowerType
{
    NONE,
    COSMO,
    DAFFOFIL,
    DAISY,
    LAVENDER,
    ORCHID,
    PANSY,
    TULIP
}

public class Flower
{
    public Vector2 position;
    private readonly Vector2 VELOCITY_Y = new(0f, 4.0f);

    private flowerType _type;

    private Sprite flowerSprite;

    private static Texture2D _cosmoTexture;
    private static Texture2D _daffodilTexture;
    private static Texture2D _daisyTexture;
    private static Texture2D _lavenderTexture;
    private static Texture2D _orchidTexture;
    private static Texture2D _pansyTexture;
    private static Texture2D _tulipTexture;
    private static Random _flowerRand;

    public bool toRemove;

    private static SoundEffect _collectSound;

    public Flower(flowerType type)
    {
        _type = type;

        flowerSprite = _type switch
        {
            flowerType.COSMO => new Sprite(_cosmoTexture),
            flowerType.DAFFOFIL => new Sprite(_daffodilTexture),
            flowerType.DAISY => new Sprite(_daisyTexture),
            flowerType.LAVENDER => new Sprite(_lavenderTexture),
            flowerType.ORCHID => new Sprite(_orchidTexture),
            flowerType.PANSY => new Sprite(_pansyTexture),
            flowerType.TULIP => new Sprite(_tulipTexture),
            _ => null
        };

        flowerSprite?.CenterOrigin();

        flowerSprite.Scale = new Vector2(2.0f, 2.0f);

        toRemove = false;
    }

    static public Sprite GetSprite(flowerType type)
    {
        return type switch
        {
            flowerType.COSMO => new Sprite(_cosmoTexture),
            flowerType.DAFFOFIL => new Sprite(_daffodilTexture),
            flowerType.DAISY => new Sprite(_daisyTexture),
            flowerType.LAVENDER => new Sprite(_lavenderTexture),
            flowerType.ORCHID => new Sprite(_orchidTexture),
            flowerType.PANSY => new Sprite(_pansyTexture),
            flowerType.TULIP => new Sprite(_tulipTexture),
            _ => new Sprite(_cosmoTexture)
        };
    }

    static public void LoadContent()
    {
        _cosmoTexture = Core.Content.Load<Texture2D>("images/Flowers/CosmoOUTLINED");
        _daffodilTexture = Core.Content.Load<Texture2D>("images/Flowers/DaffodilOUTLINED");
        _daisyTexture = Core.Content.Load<Texture2D>("images/Flowers/DaisyOUTLINED");
        _lavenderTexture = Core.Content.Load<Texture2D>("images/Flowers/LavenderOUTLINED");
        _orchidTexture = Core.Content.Load<Texture2D>("images/Flowers/OrchidOUTLINED");
        _pansyTexture = Core.Content.Load<Texture2D>("images/Flowers/PansyOUTLINED");
        _tulipTexture = Core.Content.Load<Texture2D>("images/Flowers/TulipOUTLINED");

        _flowerRand = new Random();

        _collectSound = Core.Content.Load<SoundEffect>("audio/Sound effects/Fruit collect 1");
    }

    static public flowerType getRandomType()
    {
        int rand = _flowerRand.Next(0, 101);
        if(rand > 70)
        {
            return (flowerType)_flowerRand.Next((int)flowerType.COSMO, (int)flowerType.TULIP + 1);
        }
        return flowerType.NONE;
    }

    public void Update(GameTime gameTime)
    {
        position.Y += Platform.platformGravity;
        if(getBounds().Top > Core.GraphicsDevice.PresentationParameters.BackBufferHeight)
        {
            toRemove = true;
        }
    }

    public void Draw()
    {
        flowerSprite.Draw(Core.SpriteBatch, position);
    }

    public Rectangle getBounds()
    {
        if(flowerSprite == null)
        {
            return Rectangle.Empty;
        }

        return new Rectangle(
            (int)(position.X - flowerSprite.Width * 0.5f),
            (int)(position.Y - flowerSprite.Height * 0.5f),
            (int)flowerSprite.Width,
            (int)flowerSprite.Height
        );
    }

    public static void PlayCollectibleSound()
    {
        Core.Audio.PlaySoundEffect(_collectSound);
    }

}