using Gum.DataTypes;
using Gum.Managers;
using MonoGameLibrary.Graphics;
using MonoGameGum.GueDeriving;
//using GameName.Config;
using System;

namespace GameName.UI;

public class ConfirmUpgradeItemPanel : PangPanel
{
    public AnimatedButton confirmButton;
    public AnimatedButton cancelButton;
    private TextRuntime _text;

    public ConfirmUpgradeItemPanel()
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
        _text.FontScale = 0.25f;
        _text.X = 10.0f;
        _text.Y = 10.0f;
        _panel.AddChild(_text);

        confirmButton = new AnimatedButton(_GUIatlas);
        confirmButton.Text = "CONFIRM";
        confirmButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        confirmButton.X = 9.0f;
        confirmButton.Y = -9.0f;

        confirmButton.Click += OnConfirmButtonClicked;

        _panel.AddChild(confirmButton);

        cancelButton = new AnimatedButton(_GUIatlas);
        cancelButton.Text = "CANCEL";
        cancelButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        cancelButton.X = -9.0f;
        cancelButton.Y = -9.0f;

        cancelButton.Click += OnCancelButtonClicked;

        _panel.AddChild(cancelButton);

    }

    private void OnConfirmButtonClicked(object sender, EventArgs e)
    {
        _panel.IsVisible = false;
        ShopPanel.setButtonsIsEnabled(true);
    }

    private void OnCancelButtonClicked(object sender, EventArgs e)
    {
        _panel.IsVisible = false;
        ShopPanel.setButtonsIsEnabled(true);
    }

    // public void SetText(ShopItems item, int prize)
    // {
    //     _text.Text = "Do you want to upgrade\n" + ShopItemsConfig.itemsText[item] + " for "+prize.ToString()+ "?";
    // }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        confirmButton.IsFocused = !isVisible;
        cancelButton.IsFocused = isVisible;
    }
}