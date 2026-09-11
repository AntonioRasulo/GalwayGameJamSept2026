using Gum.Forms.Controls;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using System;

namespace GameName.UI;

public class OptionsPanel: PangPanel
{
    private OptionsSlider sfxSlider;
    private OptionsSlider musicSlider;
    private AnimatedButton _optionsBackButton;
    private AnimatedButton _controlButton;
    private AnimatedButton _leaderButton;

    public OptionsPanel()
    {
        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        TextRuntime optionsText = new TextRuntime();
        optionsText.X = 10;
        optionsText.Y = 10;
        optionsText.Text = "OPTIONS";
        optionsText.UseCustomFont = true;
        optionsText.FontScale = 0.5f;
        optionsText.CustomFontFile = @"fonts/04b_30.fnt";
        optionsText.IsEnabled = false;
        _panel.AddChild(optionsText);

        musicSlider = new OptionsSlider(_GUIatlas);
        musicSlider.Name = "MusicSlider";
        musicSlider.Text = "MUSIC";
        musicSlider.Anchor(Gum.Wireframe.Anchor.Top);
        musicSlider.Y = 30f;
        musicSlider.Minimum = 0;
        musicSlider.Maximum = 1;
        musicSlider.Value = Core.Audio.SongVolume;
        musicSlider.SmallChange = .1;
        musicSlider.LargeChange = .2;
        musicSlider.ValueChanged += HandleMusicSliderValueChanged;
        musicSlider.ValueChangeCompleted += HandleMusicSliderValueChangeCompleted;
        _panel.AddChild(musicSlider);

        sfxSlider = new OptionsSlider(_GUIatlas);
        sfxSlider.Name = "SfxSlider";
        sfxSlider.Text = "SFX";
        sfxSlider.Anchor(Gum.Wireframe.Anchor.Top);
        sfxSlider.Y = 93;
        sfxSlider.Minimum = 0;
        sfxSlider.Maximum = 1;
        sfxSlider.Value = Core.Audio.SoundEffectVolume;
        sfxSlider.SmallChange = .1;
        sfxSlider.LargeChange = .2;
        sfxSlider.ValueChanged += HandleSfxSliderChanged;
        sfxSlider.ValueChangeCompleted += HandleSfxSliderChangeCompleted;
        _panel.AddChild(sfxSlider);

        _optionsBackButton = new AnimatedButton(_GUIatlas);
        _optionsBackButton.Text = "BACK";
        _optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _optionsBackButton.X = -28f;
        _optionsBackButton.Y = -10f;
        _optionsBackButton.Click += TitlePanelManager.HandleOptionsButtonBack;
        _panel.AddChild(_optionsBackButton);

        _leaderButton = new AnimatedButton(_GUIatlas);
        _leaderButton.Text = "LEADERBOARD";
        _leaderButton.Anchor(Gum.Wireframe.Anchor.Bottom);
        _leaderButton.Y = -10.0f;
        _leaderButton.X = 10.0f;
        _leaderButton.Click += TitlePanelManager.GoToLeaderboard;
        _panel.AddChild(_leaderButton);

        _controlButton = new AnimatedButton(_GUIatlas);
        _controlButton.Text = "CONTROLS";
        _controlButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _controlButton.X = 28f;
        _controlButton.Y = -10f;
        _controlButton.Click += TitlePanelManager.HandleControl;
        _panel.AddChild(_controlButton);
    }

    private void HandleMusicSliderValueChanged(object sender, EventArgs args)
    {
        // Intentionally not playing the UI sound effect here so that it is not
        // constantly triggered as the user adjusts the slider's thumb on the
        // track.

        // Get a reference to the sender as a Slider.
        var slider = (Slider)sender;

        // Set the global song volume to the value of the slider.
        Core.Audio.SongVolume = (float)slider.Value;

        VolumeButton.Unmute(true);
        _volumeButton.UpdateSprite();
    }

    private void HandleMusicSliderValueChangeCompleted(object sender, EventArgs args)
    {
        // A UI interaction occurred, play the sound effect
        Core.Audio.PlaySoundEffect(TitlePanelManager.uiSoundEffect);
    }

    private void HandleSfxSliderChanged(object sender, EventArgs args)
    {
        // Intentionally not playing the UI sound effect here so that it is not
        // constantly triggered as the user adjusts the slider's thumb on the
        // track.

        // Get a reference to the sender as a Slider.
        var slider = (Slider)sender;

        // Set the global sound effect volume to the value of the slider.;
        Core.Audio.SoundEffectVolume = (float)slider.Value;

        VolumeButton.Unmute(false);
        _volumeButton.UpdateSprite();
    }

    private void HandleSfxSliderChangeCompleted(object sender, EventArgs e)
    {
        // Play the UI Sound effect so the player can hear the difference in audio.
        Core.Audio.PlaySoundEffect(TitlePanelManager.uiSoundEffect);
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _optionsBackButton.IsFocused = isVisible;
        _controlButton.IsFocused = !isVisible;
        _leaderButton.IsFocused = !isVisible;
    }

}