using Gum.DataTypes;
using Gum.Managers;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using MonoGameLibrary.Graphics;
using System;

namespace GameName.UI;

public class DeleteGamePanel : PangPanel
{
    AnimatedButton _confirmButton;
    AnimatedButton _cancelButton;
    public DeleteGamePanel()
    {
        _panel.Anchor(Gum.Wireframe.Anchor.Center);
        _panel.IsVisible = false;
        _panel.AddToRoot();
        _panel.WidthUnits = DimensionUnitType.Absolute;
        _panel.HeightUnits = DimensionUnitType.Absolute;
        _panel.Width = 264.0f;
        _panel.Height = 70.0f;

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

        TextRuntime text = new TextRuntime();
        text.Anchor(Gum.Wireframe.Anchor.Top);
        text.Text = "Delete saving?";
        text.UseCustomFont = true;
        text.FontScale = 0.25f;
        text.CustomFontFile = @"fonts/04b_30.fnt";
        text.IsEnabled = false;
        text.Y = 5f;
        _panel.AddChild(text);

        _confirmButton = new AnimatedButton(_GUIatlas);
        _confirmButton.Text = "CONFIRM";
        _confirmButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _confirmButton.X = 9f;
        _confirmButton.Y = -9f;
        _confirmButton.Click += handleConfirmDeleteGameClicked;
        _panel.AddChild(_confirmButton);

        _cancelButton = new AnimatedButton(_GUIatlas);
        _cancelButton.Text = "CANCEL";
        _cancelButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _cancelButton.X = -9f;
        _cancelButton.Y = -9f;
        _cancelButton.Click += GoToStartGamePanel;
        _panel.AddChild(_cancelButton);
    }

    private void handleConfirmDeleteGameClicked(object sender, EventArgs e)
    {
        TitlePanelManager.handleConfirmDeleteGameClicked(sender);
    }

    private void GoToStartGamePanel(object sender, EventArgs e)
    {
        TitlePanelManager.GoToStartGamePanel();
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _confirmButton.IsFocused = !isVisible;
        _cancelButton.IsFocused = isVisible;
    }

}