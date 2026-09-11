using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals.V3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.GueDeriving;
using System;
using System.Diagnostics;

namespace GameName.UI;

class LinkButton : Button
{
    private string _link;

    public LinkButton(string link, Texture2D texture, Rectangle sourceRectangle, float spriteScale = 1.0f)
    {

        Initialize(link);

        AddSprite(texture, sourceRectangle, spriteScale);

        GotFocus += MoveFocus;
        this.GamepadTabbingFocusBehavior = TabbingFocusBehavior.SkipOnTab;
    }

    private void Initialize(string link)
    {
        ButtonVisual buttonVisual = (ButtonVisual)Visual;

        // Remove background
        if (buttonVisual.Background != null)
        {
            buttonVisual.Children.Remove(buttonVisual.Background);
        }

        TextRuntime textInstance = buttonVisual.TextInstance;
        textInstance.Text = "";

        _link = link;

        Click += GoToWebsite;
    }

    public void AddSprite(Texture2D texture, Rectangle sourceRectangle, float scale = 1.0f)
    {
        // Create sprite
        SpriteRuntime _sprite = new SpriteRuntime
        {
            Texture = texture,
            SourceRectangle = sourceRectangle,
            WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            Width = sourceRectangle.Width * scale,
            Height = sourceRectangle.Height * scale,
            TextureAddress = Gum.Managers.TextureAddress.Custom
        };

        this.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        this.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;

        this.Width = sourceRectangle.Width * scale;
        this.Height = sourceRectangle.Height * scale;

        _sprite.Anchor(Gum.Wireframe.Anchor.Center);

        this.AddChild(_sprite);
    }

    private void GoToWebsite(object sender, EventArgs args)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = _link,
            UseShellExecute = true
        });
    }

    private void MoveFocus(object sender, EventArgs args)
    {
        this.HandleKeyboardFocusUpdate();
    }

}