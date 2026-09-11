using Gum.DataTypes;
using Gum.Managers;
using MonoGameLibrary.Graphics;
using MonoGameGum.GueDeriving;
//using GameName.Config;
using System;

namespace GameName.UI;

public class ShopFailedBuyPanel: PangPanel
{
    private AnimatedButton _okButton;
    public TextRuntime _text;

    public ShopFailedBuyPanel()
    {
        _panel.Anchor(Gum.Wireframe.Anchor.Center);
        _panel.WidthUnits = DimensionUnitType.Absolute;
        _panel.HeightUnits = DimensionUnitType.Absolute;
        _panel.Width = 264.0f;
        _panel.Height = 70.0f;
        _panel.IsVisible = false;

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
        _text.FontScale = 0.2f;
        _text.X = 10.0f;
        _text.Y = 10.0f;
        _panel.AddChild(_text);

        _okButton = new AnimatedButton(_GUIatlas);
        _okButton.Text = "OK";
        _okButton.Anchor(Gum.Wireframe.Anchor.Bottom);
        _okButton.X = 0f;
        _okButton.Y = -9.0f;

        _okButton.Click += OkButtonClicked;

        _panel.AddChild(_okButton);

    }

    private void OkButtonClicked(object sender, EventArgs e)
    {
        _panel.IsVisible = false;
        ShopPanel.setButtonsIsEnabled(true);
    }

    // public void SetText(ShopItems item, int prize = -1)
    // {
    //     if(prize != -1)
    //     {
    //         _text.Text = "You don't have enough money to upgrade\n" + ShopItemsConfig.itemsText[item] + ".\n";
    //         _text.Text += "The price of the item is " + prize.ToString();
    //     }
    //     else
    //     {
    //         _text.Text = "Reached maximum level for "+ShopItemsConfig.itemsText[item] + ".";
    //     }
    // }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _okButton.IsFocused = isVisible;
    }

}