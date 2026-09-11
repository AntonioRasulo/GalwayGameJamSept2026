using Microsoft.Xna.Framework;
//using GameName.Config;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using MonoGameGum;
using System;
using System.IO;

namespace GameName.UI;

public class LoadGamePanel : PangPanel
{
    private LoadButton _loadButton1;
    private LoadButton _loadButton2;
    private LoadButton _loadButton3;

    private LoadButton _newGameButton;

    private AnimatedButton _loadBackButton;

    public LoadGamePanel()
    {
        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        TextureAtlas book2Atlas = TextureAtlas.FromFile(Core.Content, "images/UI/Book2_atlas.xml");
        TextureRegion loadGamePaperRegion = book2Atlas.GetRegion("paper-tile-9");

        float screenHeight = Core.GraphicsDevice.PresentationParameters.BackBufferHeight;
        float screenWidth = Core.GraphicsDevice.PresentationParameters.BackBufferWidth;

        _loadButton1 = new(_GUIatlas);
        _loadButton2 = new(_GUIatlas);
        _loadButton3 = new(_GUIatlas);

        _loadButton1.Click += HandleLoadButton;
        _loadButton1.Anchor(Gum.Wireframe.Anchor.TopLeft);
        _loadButton1.X = 2;
        _loadButton1.Y = 10;
        _loadButton1.Width = 240;
        _loadButton1.Height = 50;

        // if(PlayerStatsManager.pStats1 != null)
        // {
        //     LoadButton(PlayerStatsManager.pStats1, _loadButton1, Gum.Wireframe.Anchor.TopRight, -65, 30);
        // }

        _loadButton2.Anchor(Gum.Wireframe.Anchor.TopLeft);
        _loadButton2.Click += HandleLoadButton;
        _loadButton2.X = 2;
        _loadButton2.Y = 65;
        _loadButton2.Width = 240;
        _loadButton2.Height = 50;

        // if(PlayerStatsManager.pStats2 != null)
        // {
        //     LoadButton(PlayerStatsManager.pStats2, _loadButton2, Gum.Wireframe.Anchor.Right, -65, 0);
        // }

        _loadButton3.Anchor(Gum.Wireframe.Anchor.TopLeft);
        _loadButton3.Click += HandleLoadButton;
        _loadButton3.X = 2;
        _loadButton3.Y = 120;
        _loadButton3.Width = 240;
        _loadButton3.Height = 50;

        // if(PlayerStatsManager.pStats3 != null)
        // {
        //     LoadButton(PlayerStatsManager.pStats3, _loadButton3, Gum.Wireframe.Anchor.BottomRight, -65, -30);
        // }

        _loadBackButton = new AnimatedButton(_GUIatlas);
        _loadBackButton.Text = "BACK";
        _loadBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _loadBackButton.X = -15f;
        _loadBackButton.Y = -5f;
        _loadBackButton.Click += TitlePanelManager.HandleOptionsButtonBack;

        _panel.AddChild(_loadBackButton);
        _panel.AddChild(_loadButton1);
        _panel.AddChild(_loadButton2);
        _panel.AddChild(_loadButton3);
    }

    public void handleConfirmNameClicked(string newGameText)
    {
        if (_newGameButton == _loadButton1)
        {
            // PlayerStatsManager.pStats1 = new PlayerStats(newGameText, PlayerStatsManager.PATH1);

            // PlayerStats.SaveGame(PlayerStatsManager.pStats1);
            // LoadButton(PlayerStatsManager.pStats1, _loadButton1, Gum.Wireframe.Anchor.TopRight, -65, 30);

            // PlayerStatsManager.SelectPlayerStats(1);
            // TitlePanelManager.GoToStartGamePanel();
        }
        // if (_newGameButton == _loadButton2)
        // {
        //     PlayerStatsManager.pStats2 = new PlayerStats(newGameText, PlayerStatsManager.PATH2);

        //     PlayerStats.SaveGame(PlayerStatsManager.pStats2);
        //     LoadButton(PlayerStatsManager.pStats2, _loadButton2, Gum.Wireframe.Anchor.Right, -65, 0);

        //     PlayerStatsManager.SelectPlayerStats(2);
        //     TitlePanelManager.GoToStartGamePanel();
        // }
        // if (_newGameButton == _loadButton3)
        // {
        //     PlayerStatsManager.pStats3 = new PlayerStats(newGameText, PlayerStatsManager.PATH3);

        //     PlayerStats.SaveGame(PlayerStatsManager.pStats3);
        //     LoadButton(PlayerStatsManager.pStats3, _loadButton3, Gum.Wireframe.Anchor.BottomRight, -65, -30);

        //     PlayerStatsManager.SelectPlayerStats(3);
        //     TitlePanelManager.GoToStartGamePanel();
        // }
    }

    // private void LoadButton(PlayerStats pStats, LoadButton loadButton, Gum.Wireframe.Anchor anchor, float deleteX, float deleteY)
    // {
    //     loadButton.LoadButtonStats(pStats, anchor, deleteX, deleteY);
    // }

    private void HandleLoadButton(object sender, EventArgs e)
    {
        //TODO load game implementation
        if(sender == _loadButton1)
        {
            if(_loadButton1.isNewGame)
            {
                HandleNewGameClicked(_loadButton1);
                return;
            }
            
            //PlayerStatsManager.SelectPlayerStats(1);
        }

        if(sender == _loadButton2)
        {
            if(_loadButton2.isNewGame)
            {
                HandleNewGameClicked(_loadButton2);
                return;
            }
            //PlayerStatsManager.SelectPlayerStats(2);
        }

        if(sender == _loadButton3)
        {
            if (_loadButton3.isNewGame)
            {
                HandleNewGameClicked(_loadButton3);
                return;
            }
            //PlayerStatsManager.SelectPlayerStats(3);
        }

        TitlePanelManager.GoToStartGamePanel();
    }

    private void HandleNewGameClicked(object sender)
    {
        _newGameButton = (LoadButton)sender;

        TitlePanelManager.HandleNewGameClicked();

    }

    public void handleConfirmDeleteGameClicked(object sender)
    {
        // string saveToDelete = PlayerStatsManager.currentStats.Path;

        // if(PlayerStatsManager.pStats1 != null && saveToDelete == PlayerStatsManager.pStats1.Path)
        // {
        //     _loadButton1.CleanButton();
        // }

        // if(PlayerStatsManager.pStats2 != null && saveToDelete == PlayerStatsManager.pStats2.Path)
        // {
        //     _loadButton2.CleanButton();
        // }

        // if(PlayerStatsManager.pStats3 != null && saveToDelete == PlayerStatsManager.pStats3.Path)
        // {
        //     _loadButton3.CleanButton();
        // }

        // var jsonFile = saveToDelete + ".json";
        // var bakFile = saveToDelete + ".bak";
        // File.Delete(jsonFile);
        // File.Delete(bakFile);
        // PlayerStatsManager.currentStats = null;
    }

    public void UpdateLoadButton()
    {
        // if(PlayerStatsManager.currentStats == PlayerStatsManager.pStats1)
        // {
        //     _loadButton1.setTextMoney(PlayerStatsManager.pStats1.Money.ToString());
        //     _loadButton1.UpdateLoadButtonPowerUps();
        // }

        // if(PlayerStatsManager.currentStats == PlayerStatsManager.pStats2)
        // {
        //     _loadButton2.setTextMoney(PlayerStatsManager.pStats2.Money.ToString());
        //     _loadButton2.UpdateLoadButtonPowerUps();
        // }

        // if(PlayerStatsManager.currentStats == PlayerStatsManager.pStats3)
        // {
        //     _loadButton3.setTextMoney(PlayerStatsManager.pStats3.Money.ToString());
        //     _loadButton3.UpdateLoadButtonPowerUps();
        // }
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _loadBackButton.IsFocused = isVisible;
    }

    public void Update(GameTime gametime)
    {
        _loadButton1.Update(gametime);
        _loadButton2.Update(gametime);
        _loadButton3.Update(gametime);
    }

}