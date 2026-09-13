using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary;
using IbexGame.Scenes;
using System;
using IbexGame.GameObjects;

namespace IbexGame.UI;

public class TitlePanelManager
{
    private static TitleScreenButtonsPanel _titleScreenButtonsPanel;

    private static OptionsPanel _optionsPanel;
    private static CreditsPanel _creditsPanel;
    private static ControlPanel _controlPanel;

    public static SoundEffect uiSoundEffect = Core.Content.Load<SoundEffect>("audio/Sound effects/Confirm 1");

    public static void LoadContent()
    {
        PangPanel.LoadContent();
        VolumeButton.LoadContent();
        _titleScreenButtonsPanel = new TitleScreenButtonsPanel();
        _optionsPanel = new OptionsPanel();
        _creditsPanel = new CreditsPanel();
        _controlPanel = new ControlPanel();
        _titleScreenButtonsPanel.SetStartButtonFocus(true);
    }

    public static void HandleStartClicked(object sender, EventArgs e)
    {
        Core.ChangeScene(new GameScene());
    }

    public static void HandleOptionsClicked(object sender, EventArgs e)
    {
        // A UI interaction occurred, play the sound effect
        Goat.playGoatSoundEffect();

        // Set the title panel to be invisible.
        _titleScreenButtonsPanel.SetIsVisible(false);

        _controlPanel.SetIsVisible(false);

        // Set the options panel to be visible.
        _optionsPanel.SetIsVisible(true);

    }

    public static void HandleOptionsButtonBack(object sender, EventArgs e)
    {
        // A UI interaction occurred, play the sound effect
        Goat.playGoatSoundEffect();

        // Set the options panel to be invisible.
        _optionsPanel.SetIsVisible(false);

        _creditsPanel.SetIsVisible(false);

        // Set the title panel to be visible.
        _titleScreenButtonsPanel.SetIsVisible(true);

    }

    public static bool IsTitlePanelVisible()
    {
        return _titleScreenButtonsPanel.IsVisible();
    }

    public static void HandleCreditsClicked(object sender, EventArgs e)
    {
        _titleScreenButtonsPanel.SetIsVisible(false);
        _creditsPanel.SetIsVisible(true);
    }

    public static void HandleControl(object sender, EventArgs e)
    {
        _optionsPanel.SetIsVisible(false);
        _controlPanel.SetIsVisible(true);
    }

    public static void Draw()
    {
        _titleScreenButtonsPanel.Draw();
    }

}