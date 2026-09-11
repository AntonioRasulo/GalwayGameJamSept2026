using Gum.DataTypes;
using Gum.Managers;
using MonoGameLibrary.Graphics;
using MonoGameGum.GueDeriving;
//using GameName.Config;

namespace GameName.UI;

public class DescriptionPowerPanel: PangPanel
{
    private static TextRuntime _text;

    public DescriptionPowerPanel()
    {
        _panel.Anchor(Gum.Wireframe.Anchor.Bottom);
        _panel.WidthUnits = DimensionUnitType.Absolute;
        _panel.HeightUnits = DimensionUnitType.Absolute;
        _panel.Width = 264.0f;
        _panel.Height = 50.0f;
        _panel.Y = -25f;
        _panel.IsVisible = true;

        TextureRegion backgroundRegion = _GUIatlas.GetRegion("panel-background");

        NineSliceRuntime background = new NineSliceRuntime();
        background.Dock(Gum.Wireframe.Dock.Fill);
        background.Texture = backgroundRegion.Texture;
        background.TextureAddress = TextureAddress.Custom;
        background.TextureHeight = backgroundRegion.Height;
        background.TextureWidth = backgroundRegion.Width;
        background.TextureTop = backgroundRegion.SourceRectangle.Top;
        background.TextureLeft = backgroundRegion.SourceRectangle.Left;
        _panel.AddChild(background);

        _text = new TextRuntime();
        _text.Text = "";
        _text.UseCustomFont = true;
        _text.CustomFontFile = "fonts/04b_30.fnt";
        _text.FontScale = 0.17f;
        _text.X = 10.0f;
        _text.Y = 10.0f;
        _panel.AddChild(_text);
    }

    // public static void SetText(ShopItems item)
    // {
    //     _text.Text = ShopItemsConfig.itemsDescriptions[item];
    // }

}