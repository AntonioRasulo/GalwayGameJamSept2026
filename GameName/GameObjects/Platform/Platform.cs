using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace MonoGame_Super_Pang.GameObjects;

public enum PlatformType
{
    GRAY,
    BROWN,
    CARAMEL,
    GOLD,
    BREAKABLE_LARGE_HORIZONTAL_BLUE
};

public enum PlatformRotation
{
    VERTICAL,
    HORIZONTAL
};

abstract public class Platform
{
    protected Vector2 _position;

    protected PlatformType _platformType;

    protected const float SCALE = 4f;

    protected bool _breakable;

    protected static TextureRegion _grassPlatform;

    protected static List<TextureRegion> _horizontalBreakableBlueSprites;

    protected static SoundEffect _breakPlatformEffect;

    protected PlatformRotation _rotation;

    public bool toRemove = false;

    public bool jumped = false;

    static public float platformGravity = 0.5f;

    public Platform(Vector2 position, PlatformType platformType, PlatformRotation rotation)
    {
        LoadSprite();
        _position = position;
        _platformType = platformType;
        _rotation = rotation;
    }

    public abstract void Draw();

    public abstract Rectangle getBounds();

    public bool isBreakable()
    {
        return _breakable;
    }

    public static void LoadContent()
    {
        TextureAtlas terrainAtlas = TextureAtlas.FromFile(Core.Content, "images/platforms/terrain_atlas.xml");
        TextureAtlas platformAtlas = TextureAtlas.FromFile(Core.Content, "images/platforms/platform_atlas.xml");

        _grassPlatform = terrainAtlas.GetRegion("grassPlatform");

        _horizontalBreakableBlueSprites = new List<TextureRegion>();
        for(int indexPlatform = 1; indexPlatform<=3; indexPlatform++)
        {
            String spriteName = "Breakable"+indexPlatform;
            _horizontalBreakableBlueSprites.Add(platformAtlas.GetRegion(spriteName));
        }

        _breakPlatformEffect = Core.Content.Load<SoundEffect>("audio/Sound Effects/Block Break 1");

    }

    protected Rectangle RotatePlatform(Rectangle rect)
    {
        // 90° and 270° swap width and height, re-centered on the same point
        float cx = rect.X + rect.Width * 0.5f;
        float cy = rect.Y + rect.Height * 0.5f;

        return new Rectangle(
            (int)(cx - rect.Height / 2f),
            (int)(cy - rect.Width / 2f),
            rect.Height,
            rect.Width
        );
    }

    protected abstract void LoadSprite();

    public Vector2 GetPosition()
    {
        return _position;
    }

    public void Update(GameTime gameTime)
    {
        _position.Y += platformGravity;
        if(getBounds().Top > Core.GraphicsDevice.PresentationParameters.BackBufferHeight)
        {
            toRemove = true;
        }
    }

}