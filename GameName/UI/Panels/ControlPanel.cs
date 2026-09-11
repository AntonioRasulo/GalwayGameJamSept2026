using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;

namespace GameName.UI;

public class ControlPanel: PangPanel
{
    private AnimatedButton _backButton;

    public ControlPanel()
    {
        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        GameText MovementText = InitializeText("Movement");
        MovementText.X = 10.0f;
        MovementText.Y = 50.0f;
        _panel.AddChild(MovementText);

        GameText ShootingText = InitializeText("Shooting");
        ShootingText.X = 10.0f;
        ShootingText.Y = 125.0f;
        _panel.AddChild(ShootingText);

        GameText KeyboardText = InitializeText("Keyboard");
        KeyboardText.X = 100.0f;
        KeyboardText.Y = 10.0f;
        _panel.AddChild(KeyboardText);

        GameText ControllerText = InitializeText("Controller");
        ControllerText.X = 200.0f;
        ControllerText.Y = 10.0f;
        _panel.AddChild(ControllerText);

        _backButton = new AnimatedButton(_GUIatlas);
        _backButton.Text = "BACK";
        _backButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _backButton.X = -28f;
        _backButton.Y = -10f;
        _backButton.Click += TitlePanelManager.HandleOptionsClicked;
        _panel.AddChild(_backButton);

        Texture2D keysTexture = Core.Content.Load<Texture2D>("images/UI/All keys and controller icons");

        SpriteRuntime spaceRegion = GenerateSprite(keysTexture, 2, 912, 92, 47, 0.5f);
        spaceRegion.Y = 117.0f;
        spaceRegion.X = 105.0f;
        _panel.AddChild(spaceRegion);

        SpriteRuntime xRegion = GenerateSprite(keysTexture, 602, 1008, 43, 48, 0.5f);
        xRegion.Y = 117.0f;
        xRegion.X = 230.0f;
        _panel.AddChild(xRegion);

        SpriteRuntime leftRegion = GenerateSprite(keysTexture, 696, 817, 46, 45, 0.5f);
        leftRegion.Y = 30.0f;
        leftRegion.X = 100.0f;
        _panel.AddChild(leftRegion);

        SpriteRuntime rightRegion = GenerateSprite(keysTexture, 889, 817, 46, 45, 0.5f);
        rightRegion.Y = 30.0f;
        rightRegion.X = 140.0f;
        _panel.AddChild(rightRegion);

        SpriteRuntime aRegion = GenerateSprite(keysTexture, 216, 624, 46, 45, 0.5f);
        aRegion.Y = 50.0f;
        aRegion.X = 100.0f;
        _panel.AddChild(aRegion);

        SpriteRuntime dRegion = GenerateSprite(keysTexture, 504, 624, 46, 45, 0.5f);
        dRegion.Y = 50.0f;
        dRegion.X = 140.0f;
        _panel.AddChild(dRegion);

        SpriteRuntime leftContrRegion = GenerateSprite(keysTexture, 1178, 962, 43, 43, 0.5f);
        leftContrRegion.Y = 40.0f;
        leftContrRegion.X = 210.0f;
        _panel.AddChild(leftContrRegion);

        SpriteRuntime rightContrRegion = GenerateSprite(keysTexture, 986, 962, 43, 43, 0.5f);
        rightContrRegion.Y = 40.0f;
        rightContrRegion.X = 250.0f;
        _panel.AddChild(rightContrRegion);

    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _backButton.IsFocused = isVisible;
    }

    private GameText InitializeText(string text)
    {
        GameText Text = new GameText(text);
        Text.FontScale = 0.27f;
        Text.Red = 0;
        Text.Blue = 0;
        Text.Green = 0;
        Text.Anchor(Gum.Wireframe.Anchor.TopLeft);
        return Text;
    }

    private SpriteRuntime GenerateSprite(Texture2D texture, int x, int y, int Width, int Height, float scale = 1.0f)
    {
        Rectangle rect = new Rectangle(x, y, Width, Height);
        SpriteRuntime sprite = new SpriteRuntime
        {
            Texture = texture,
            SourceRectangle = rect,
            WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            Width = Width * scale,
            Height = Height * scale,
            TextureAddress = Gum.Managers.TextureAddress.Custom
        };

        sprite.Anchor(Gum.Wireframe.Anchor.TopLeft);

        return sprite;
    }

}