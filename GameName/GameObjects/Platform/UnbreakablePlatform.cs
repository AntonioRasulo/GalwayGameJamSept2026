using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework;

namespace MonoGame_Super_Pang.GameObjects;

public class UnbreakablePlatform : Platform
{
    private Sprite _sprite;

    public UnbreakablePlatform(Vector2 position, PlatformType platformType, PlatformRotation rotation) : base(position, platformType, rotation)
    {
        _sprite.Scale = new Vector2(SCALE, SCALE);
        _sprite.CenterOrigin();
        if (_rotation == PlatformRotation.VERTICAL)
        {
            _sprite.Rotation = float.DegreesToRadians(90);
        }
        _breakable = false;
    }

    public override void Draw()
    {
        _sprite.Draw(Core.SpriteBatch, _position);
    }

    public override Rectangle getBounds()
    {
        // Creating a bounding rectangle for the platform
        Rectangle platformBounds = new Rectangle(
            (int)(_position.X - _sprite.Width*0.5f),
            (int)(_position.Y - _sprite.Height*0.5f),
            (int)_sprite.Width,
            (int)_sprite.Height
        );

        if(_rotation == PlatformRotation.VERTICAL)
        {
            platformBounds = RotatePlatform(platformBounds);
        }

        return platformBounds;
    }

    protected override void LoadSprite()
    {
        _sprite = new Sprite(_grassPlatform);
    }
}
