//using GameName.Config;
using MonoGameGum;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using System;

namespace GameName.UI;

class ShopPanel : PangPanel
{
    private static AnimatedButton _backButton;

    /* Character power up */
    // private static PowerUpButton harpoonButton;
    // private static PowerUpButton speedButton;
    // private static PowerUpButton livesButton;

    // /* Collectibles power up */
    // private static PowerUpButton collLivesButton;
    // private static PowerUpButton invincibilityButton;
    // private static PowerUpButton bombButton;
    // private static PowerUpButton clockButton;

    // private static PowerUpButton lastButtonPressed;

    private ConfirmUpgradeItemPanel _confirmUpgradePanel;
    private ShopFailedBuyPanel _shopFailedPanel;
    private static DescriptionPowerPanel _descriptionPanel;

    private const float BUTTONDISTANCE = 30.0f;

    public ShopPanel()
    {
        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        _backButton = new AnimatedButton(_GUIatlas);
        _backButton.Text = "BACK";
        _backButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _backButton.X = -28f;
        _backButton.Y = -10f;
        _backButton.Click += GoToStartGamePanel;
        _panel.AddChild(_backButton);

        // harpoonButton = new PowerUpButton(_GUIatlas, ShopItems.HARPOON);
        // harpoonButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        // harpoonButton.X = BUTTONDISTANCE;
        // harpoonButton.Y = BUTTONDISTANCE;
        // harpoonButton.Click += ShowConfirmPanel;
        // _panel.AddChild(harpoonButton);

        // speedButton = new PowerUpButton(_GUIatlas, ShopItems.SPEED);
        // speedButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        // speedButton.X = 2*BUTTONDISTANCE;
        // speedButton.Y = BUTTONDISTANCE;
        // speedButton.Click += ShowConfirmPanel;
        // speedButton.SetScale(0.5f);
        // speedButton.SetSpriteYCor(-1.5f);
        // _panel.AddChild(speedButton);

        // livesButton = new PowerUpButton(_GUIatlas, ShopItems.LIVES);
        // livesButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        // livesButton.X = 3*BUTTONDISTANCE;
        // livesButton.Y = BUTTONDISTANCE;
        // livesButton.SetScale(1f, 0.6f);
        // livesButton.Click += ShowConfirmPanel;
        // _panel.AddChild(livesButton);

        // collLivesButton = new PowerUpButton(_GUIatlas, ShopItems.COLL_LIVES);
        // collLivesButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        // collLivesButton.X = BUTTONDISTANCE;
        // collLivesButton.Y = 2*BUTTONDISTANCE;
        // collLivesButton.SetScale(1f, 0.6f);
        // collLivesButton.Click += ShowConfirmPanel;
        // _panel.AddChild(collLivesButton);

        // invincibilityButton = new PowerUpButton(_GUIatlas, ShopItems.INVINCIBILITY);
        // invincibilityButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        // invincibilityButton.X = 2*BUTTONDISTANCE;
        // invincibilityButton.Y = 2*BUTTONDISTANCE;
        // invincibilityButton.Click += ShowConfirmPanel;
        // invincibilityButton.SetScale(0.05f);
        // _panel.AddChild(invincibilityButton);

        // bombButton = new PowerUpButton(_GUIatlas, ShopItems.BOMB);
        // bombButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        // bombButton.X = 3*BUTTONDISTANCE;
        // bombButton.Y = 2*BUTTONDISTANCE;
        // bombButton.Click += ShowConfirmPanel;
        // bombButton.SetScale(0.6f);
        // _panel.AddChild(bombButton);

        // clockButton = new PowerUpButton(_GUIatlas, ShopItems.CLOCK);
        // clockButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        // clockButton.X = 4*BUTTONDISTANCE;
        // clockButton.Y = 2*BUTTONDISTANCE;
        // clockButton.SetScale(0.02f);
        // clockButton.Click += ShowConfirmPanel;
        // _panel.AddChild(clockButton);

        _confirmUpgradePanel = new ConfirmUpgradeItemPanel();
        _confirmUpgradePanel.confirmButton.Click += ConfirmBuy;
        _panel.AddChild(_confirmUpgradePanel.Visual());

        _shopFailedPanel = new ShopFailedBuyPanel();
        _panel.AddChild(_shopFailedPanel.Visual());

        _descriptionPanel = new DescriptionPowerPanel();
        _panel.AddChild(_descriptionPanel.Visual());
    }

    public static void InitializePowerUpButtons()
    {
        // harpoonButton.SetState(PlayerStatsManager.currentStatsLevels.harpoonLevel);
        // speedButton.SetState(PlayerStatsManager.currentStatsLevels.speedLevel);
        // livesButton.SetState(PlayerStatsManager.currentStatsLevels.livesLevel);
        // collLivesButton.SetState(PlayerStatsManager.currentStatsLevels.collLivesLevel);
        // invincibilityButton.SetState(PlayerStatsManager.currentStatsLevels.invincibilityLevel);
        // bombButton.SetState(PlayerStatsManager.currentStatsLevels.bombLevel);
        // clockButton.SetState(PlayerStatsManager.currentStatsLevels.clockLevel);
    }

    private void GoToStartGamePanel(object sender, EventArgs e)
    {
        TitlePanelManager.GoToStartGamePanel();
    }

    // private void ShowConfirmPanel(object sender, EventArgs e)
    // {
    //     lastButtonPressed = (PowerUpButton)sender;
    //     PowerUpButtonState buttonPressedState = lastButtonPressed.GetState();
    //     ShopItems buttonPressedItem = lastButtonPressed.GetItem();
    //     if(buttonPressedState < PowerUpButtonState.Level3)
    //     {
    //         int prize = lastButtonPressed.GetPowerUpPrize();
    //         if (prize <= PlayerStatsManager.currentStats.Money)
    //         {
    //             _confirmUpgradePanel.SetText(buttonPressedItem, prize);
    //             _confirmUpgradePanel.SetIsVisible(true);
    //         }
    //         else
    //         {
    //             _shopFailedPanel.SetText(buttonPressedItem, prize);
    //             _shopFailedPanel.SetIsVisible(true);
    //         }
    //         setButtonsIsEnabled(false);
    //     }
    //     else if(buttonPressedState == PowerUpButtonState.Level3)
    //     {
    //         _shopFailedPanel.SetText(buttonPressedItem);
    //         _shopFailedPanel.SetIsVisible(true);
    //         setButtonsIsEnabled(false);
    //     }
    // }

    private void ConfirmBuy(object sender, EventArgs e)
    {
        // int prize = lastButtonPressed.GetPowerUpPrize();
        // PlayerStatsManager.currentStats.Money -= prize;
        // lastButtonPressed.LevelUp();
    }

    public static void setButtonsIsEnabled(bool isEnabled)
    {
        // speedButton.IsEnabled = isEnabled;
        // harpoonButton.IsEnabled = isEnabled;
        // livesButton.IsEnabled = isEnabled;
        // _backButton.IsEnabled = isEnabled;
        // collLivesButton.IsEnabled = isEnabled;
        // invincibilityButton.IsEnabled = isEnabled;
        // bombButton.IsEnabled = isEnabled;
        // clockButton.IsEnabled = isEnabled;
        // lastButtonPressed.IsFocused = isEnabled;
        // _descriptionPanel.SetIsVisible(isEnabled);
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _backButton.IsFocused = isVisible;
    }
}