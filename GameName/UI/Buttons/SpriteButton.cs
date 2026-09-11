using Gum.Forms.DefaultVisuals.V3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.GueDeriving;
using MonoGameLibrary.Graphics;

namespace GameName.UI;

public class SpriteButton : AnimatedButton
{
    private SpriteRuntime _sprite;

    public SpriteButton(TextureAtlas atlas, TextureRegion icon, float degreeRotation = 0) : base(atlas)
    {
        Text = "";

        Texture2D texture = icon.Texture;
        Rectangle sourceRectangle = icon.SourceRectangle;

        // Access the visual
        ButtonVisual visual = (ButtonVisual)this.Visual;

        // Create sprite
        _sprite = new SpriteRuntime
        {
            Texture = texture,
            SourceRectangle = sourceRectangle,
            WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            Width = sourceRectangle.Width,
            Height = sourceRectangle.Height,
            TextureAddress = Gum.Managers.TextureAddress.Custom,
            Rotation = degreeRotation
        };

        _sprite.Anchor(Gum.Wireframe.Anchor.Center);

        this.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        this.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;

        this.Width = sourceRectangle.Width;
        this.Height = sourceRectangle.Height;

        visual.Background.AddChild(_sprite);
    }
}