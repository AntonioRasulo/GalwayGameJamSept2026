using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using MonoGameLibrary;
//using GameName.Config;
using GameName.Scenes;
using System;
using GameName.GameObjects;

namespace GameName.UI;

public class TitlePanelManager
{
    private static TitleScreenButtonsPanel _titleScreenButtonsPanel;
    private static LoadGamePanel _loadGamePanel;
    private static NewGamePanel _newGamePanel;
    private static OptionsPanel _optionsPanel;
    private static DeleteGamePanel _deleteGamePanel;
    private static StartGamePanel _startGamePanel;
    private static ShopPanel _shopPanel;
    private static CreditsPanel _creditsPanel;
    private static ControlPanel _controlPanel;
    private static LeaderboardPanel _leaderboardPanel;

    public static SoundEffect uiSoundEffect = Core.Content.Load<SoundEffect>("audio/Sound effects/Confirm 1");

    public static void LoadContent()
    {
        PangPanel.LoadContent();
        VolumeButton.LoadContent();
        _titleScreenButtonsPanel = new TitleScreenButtonsPanel();
        _loadGamePanel = new LoadGamePanel();
        _newGamePanel = new NewGamePanel();
        _optionsPanel = new OptionsPanel();
        _deleteGamePanel = new DeleteGamePanel();
        _startGamePanel = new StartGamePanel();
        _shopPanel = new ShopPanel();
        _creditsPanel = new CreditsPanel();
        _controlPanel = new ControlPanel();
        _leaderboardPanel = new LeaderboardPanel();
        _titleScreenButtonsPanel.SetStartButtonFocus(true);
    }

    public static void HandleStartClicked(object sender, EventArgs e)
    {
        Core.ChangeScene(new GameScene(0));
        // Set the title panel to be invisible.
        /*_titleScreenButtonsPanel.SetIsVisible(false);

        _newGamePanel.SetIsVisible(false);

        // Set the options panel to be visible.
        _optionsPanel.SetIsVisible(false);

        _deleteGamePanel.SetIsVisible(false);

        _newGamePanel.ClearNewGameTextBox();

        _loadGamePanel.SetIsVisible(true);*/

    }

    public static void HandleOptionsClicked(object sender, EventArgs e)
    {
        // A UI interaction occurred, play the sound effect
        //Core.Audio.PlaySoundEffect(uiSoundEffect);
        Goat.playGoatSoundEffect();

        // Set the title panel to be invisible.
        _titleScreenButtonsPanel.SetIsVisible(false);

        _loadGamePanel.SetIsVisible(false);

        _newGamePanel.SetIsVisible(false);

        _deleteGamePanel.SetIsVisible(false);

        _controlPanel.SetIsVisible(false);

        // Set the options panel to be visible.
        _optionsPanel.SetIsVisible(true);

    }

    public static void HandleOptionsButtonBack(object sender, EventArgs e)
    {
        // A UI interaction occurred, play the sound effect
        //Core.Audio.PlaySoundEffect(uiSoundEffect);
        Goat.playGoatSoundEffect();

        // Set the options panel to be invisible.
        _optionsPanel.SetIsVisible(false);

        _loadGamePanel.SetIsVisible(false);

        _newGamePanel.SetIsVisible(false);

        _deleteGamePanel.SetIsVisible(false);

        _creditsPanel.SetIsVisible(false);

        _leaderboardPanel.SetIsVisible(false);

        // Set the title panel to be visible.
        _titleScreenButtonsPanel.SetIsVisible(true);

    }

    public static void handleConfirmNameClicked(object sender, EventArgs e)
    {
        _loadGamePanel.handleConfirmNameClicked(_newGamePanel.GetNewGameTextBoxText());
        _newGamePanel.ClearNewGameTextBox();
    }

    public static bool IsTitlePanelVisible()
    {
        return _titleScreenButtonsPanel.IsVisible();
    }

    public static void HandleDeleteGameClicked()
    {
        _deleteGamePanel.SetIsVisible(true);
    }

    public static void HandleNewGameClicked()
    {
        // A UI interaction occurred, play the sound effect
        Core.Audio.PlaySoundEffect(uiSoundEffect);

        // Set the title panel to be invisible.
        _titleScreenButtonsPanel.SetIsVisible(false);

        _loadGamePanel.SetIsVisible(false);

        // Set the options panel to be visible.
        _optionsPanel.SetIsVisible(false);

        _newGamePanel.SetIsVisible(true);

    }

    public static void handleConfirmDeleteGameClicked(object sender)
    {
        _loadGamePanel.handleConfirmDeleteGameClicked(sender);

        _deleteGamePanel.SetIsVisible(false);

        _loadGamePanel.SetIsVisible(true);
    }

    public static void HandleBackStartGameClicked(object sender, EventArgs e)
    {
        _startGamePanel.SetIsVisible(false);
        _loadGamePanel.SetIsVisible(true);
        _loadGamePanel.UpdateLoadButton();
        //PlayerStatsManager.SelectPlayerStats(4);
    }

    public static void GoToStartGamePanel()
    {
        _titleScreenButtonsPanel.SetIsVisible(false);
        _newGamePanel.SetIsVisible(false);
        _optionsPanel.SetIsVisible(false);
        _deleteGamePanel.SetIsVisible(false);
        _shopPanel.SetIsVisible(false);
        _loadGamePanel.SetIsVisible(false);
        _startGamePanel.SetIsVisible(true);
    }

    public static void GoToShopPanel(object sender, EventArgs e)
    {
        _loadGamePanel.SetIsVisible(false);
        _startGamePanel.SetIsVisible(false);
        _shopPanel.SetIsVisible(true);
    }

    public static void GoToLeaderboard(object sender, EventArgs e)
    {
        _optionsPanel.SetIsVisible(false);
        _leaderboardPanel.SetIsVisible(true);
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

    public static void Update(GameTime gametime)
    {
        _loadGamePanel.Update(gametime);
    }

    public static void AddScore(string player, string score)
    {
        _leaderboardPanel.AddScore(player, score);
    }

}