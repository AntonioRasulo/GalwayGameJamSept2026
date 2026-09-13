using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework;

namespace GameName.GameObjects;

public class Platform
{
    private Sprite _sprite;

    protected Vector2 _position;

    protected const float SCALE = 4f;

    protected static TextureRegion _grassPlatform;

    public bool toRemove = false;

    public bool jumped = false;

    static public float platformGravity;

    public Platform(Vector2 position)
    {
        _sprite = new Sprite(_grassPlatform);
        _sprite.Scale = new Vector2(SCALE, SCALE);
        _sprite.CenterOrigin();
        _position = position;
    }

    public void Draw()
    {
        _sprite.Draw(Core.SpriteBatch, _position);
    }

    public Rectangle getBounds()
    {
        // Creating a bounding rectangle for the platform
        return new Rectangle(
            (int)(_position.X - _sprite.Width*0.5f),
            (int)(_position.Y - _sprite.Height*0.5f),
            (int)_sprite.Width,
            (int)_sprite.Height
        );
    }

    public static void LoadContent()
    {
        TextureAtlas terrainAtlas = TextureAtlas.FromFile(Core.Content, "images/platforms/terrain_atlas.xml");

        _grassPlatform = terrainAtlas.GetRegion("grassPlatform");
    }

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

    public static void restoreGravity()
    {
        platformGravity = 0.5f;
    }

}