using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals.V3;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameGum.GueDeriving;
using System;

namespace IbexGame.UI;

public class VolumeButton : Button
{
    private static TextureRegion volumeOn2DRegion;
    private static TextureRegion volumeOff2DRegion;

    private static bool muted;

    private SpriteRuntime _sprite;

    private static float _musicVolumeToRestore;
    private static float _sfxVolumeToRestore;

    private static bool _contentLoaded = false;

    public VolumeButton()
    {
        // Access the visual
        ButtonVisual visual = (ButtonVisual)this.Visual;

        // Remove default background (NineSlice)
        if (visual.Background != null)
        {
            visual.Children.Remove(visual.Background);
        }

        visual.TextInstance.Text = "";

        // Create sprite
        _sprite = new SpriteRuntime();
        UpdateSprite();

        _sprite.Anchor(Gum.Wireframe.Anchor.Center);

        this.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        this.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;

        this.Width = _sprite.Texture.Width;
        this.Height = _sprite.Texture.Height;

        visual.Children.Insert(0, _sprite);

        Click += ChangeVolume;
        
        GotFocus += MoveFocus;
        this.GamepadTabbingFocusBehavior = TabbingFocusBehavior.SkipOnTab;
    }

    public static void LoadContent()
    {
        if(!_contentLoaded)
        {
            Texture2D volumeOn2DTexture = Core.Content.Load<Texture2D>("images/UI/Volume4");
            volumeOn2DRegion = new TextureRegion(volumeOn2DTexture, 0, 0, volumeOn2DTexture.Width, volumeOn2DTexture.Height);

            Texture2D volumeOff2DTexture = Core.Content.Load<Texture2D>("images/UI/Volume1");
            volumeOff2DRegion = new TextureRegion(volumeOff2DTexture, 0, 0, volumeOff2DTexture.Width, volumeOff2DTexture.Height);

            muted = false;
            _contentLoaded = true;
        }
    }

    private void ChangeVolume(object sender, EventArgs args)
    {
        if (muted)
        {
            Core.Audio.SongVolume = _musicVolumeToRestore;
            Core.Audio.SoundEffectVolume = _sfxVolumeToRestore;
        }
        else
        {
            _musicVolumeToRestore = Core.Audio.SongVolume;
            _sfxVolumeToRestore = Core.Audio.SoundEffectVolume;
            Core.Audio.SongVolume = 0;
            Core.Audio.SoundEffectVolume = 0;
        }

        muted = !muted;

        var region = muted ? volumeOff2DRegion : volumeOn2DRegion;
        _sprite.Texture = region.Texture;
        _sprite.SourceRectangle = region.SourceRectangle;
        _sprite.Width = region.SourceRectangle.Width;
        _sprite.Height = region.SourceRectangle.Height;
    }

    public void UpdateSprite()
    {
        if(_sprite != null)
        {
            var region = muted ? volumeOff2DRegion : volumeOn2DRegion;
            _sprite.Texture = region.Texture;
            _sprite.SourceRectangle = region.SourceRectangle;
            _sprite.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
            _sprite.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;
            _sprite.Width = region.SourceRectangle.Width;
            _sprite.Height = region.SourceRectangle.Height;
            _sprite.TextureAddress = Gum.Managers.TextureAddress.Custom;
        }
    }

    public static void Unmute(bool isMusic)
    {
        if(muted)
        {
            if (isMusic)
            {
                Core.Audio.SoundEffectVolume = _sfxVolumeToRestore;
            }
            else
            {
                Core.Audio.SongVolume = _musicVolumeToRestore;
            }
        }
        muted = false;
    }

    private void MoveFocus(object sender, EventArgs args)
    {
        this.HandleKeyboardFocusUpdate();
    }

}