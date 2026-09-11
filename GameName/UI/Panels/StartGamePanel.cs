using Gum.Forms.Controls;
using Gum.DataTypes;
using Gum.Managers;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using GameName.Scenes;
//using GameName.Config;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameName.UI;

public class StartGamePanel : PangPanel
{
    private AnimatedButton _startGameButton;
    private AnimatedButton _shopButton;
    private AnimatedButton _backButton;
    private AnimatedButton _deleteButton;
    private TextRuntime _text;

    private bool _focusDelete;

    public StartGamePanel()
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

        _text = new TextRuntime();
        _text.Anchor(Gum.Wireframe.Anchor.Top);
        _text.Text = "Ready to start?\n";
        _text.UseCustomFont = true;
        _text.FontScale = 0.25f;
        _text.CustomFontFile = @"fonts/04b_30.fnt";
        _text.IsEnabled = false;
        _text.Y = 5f;
        _panel.AddChild(_text);

        _startGameButton = new AnimatedButton(_GUIatlas);
        _startGameButton.Text = "START";
        _startGameButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _startGameButton.X = 9f;
        _startGameButton.Y = -9f;
        _startGameButton.Click += StartGame;
        _startGameButton.GotFocus += SetText;
        _startGameButton.KeyDown += FocusDelete;
        _startGameButton.LostFocus += LostFocus;
        _startGameButton.IsUsingLeftAndRightGamepadDirectionsForNavigation = false;
        _panel.AddChild(_startGameButton);

        _shopButton = new AnimatedButton(_GUIatlas);
        _shopButton.Text = "SHOP";
        _shopButton.Anchor(Gum.Wireframe.Anchor.Bottom);
        _shopButton.X = 0f;
        _shopButton.Y = -9f;
        _shopButton.Click += TitlePanelManager.GoToShopPanel;
        _shopButton.GotFocus += SetText;
        _shopButton.IsUsingLeftAndRightGamepadDirectionsForNavigation = false;
        _panel.AddChild(_shopButton);

        _backButton = new AnimatedButton(_GUIatlas);
        _backButton.Text = "BACK";
        _backButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _backButton.X = -9f;
        _backButton.Y = -9f;
        _backButton.Click += TitlePanelManager.HandleBackStartGameClicked;
        _backButton.GotFocus += SetText;
        _backButton.GotFocus += CheckFocus;
        _backButton.IsUsingLeftAndRightGamepadDirectionsForNavigation = false;
        _panel.AddChild(_backButton);

        _deleteButton = new AnimatedButton(_GUIatlas);
        _deleteButton.Text = "DELETE";
        _deleteButton.ChangeTextColor(Color.Red);
        _deleteButton.Anchor(Gum.Wireframe.Anchor.Bottom);
        _deleteButton.X = -100f;
        _deleteButton.IsVisible = false;
        _deleteButton.IsEnabled = false;
        _deleteButton.LostFocus += LostFocus;
        _deleteButton.KeyDown += FocusDelete;
        _deleteButton.IsUsingLeftAndRightGamepadDirectionsForNavigation = false;
        _deleteButton.Click += DeleteSaving;
        _deleteButton.GotFocus += SetText;
        _deleteButton.AddToRoot();

        _focusDelete = false;
    }

    private void StartGame(object sender, EventArgs e)
    {
        // Change to the game scene to start the game.
        //Core.ChangeScene(new GameScene(LevelConfig.STARTING_LEVEL));
        Core.ChangeScene(new GameScene(0));
    }

    private void SetText(object sender, EventArgs e)
    {
        if(sender == _startGameButton)
        {
            _text.Color = Color.White;
            _text.Text = "Ready to start?";
        }
        else if(sender == _shopButton)
        {
            _text.Color = Color.White;
            _text.Text = "You can find some interesting\n";
            _text.Text += "power up in the shop";
        }
        else if(sender == _backButton)
        {
            _text.Color = Color.White;
            _text.Text = "Go back to load another saving!";
        }
        else if(sender == _deleteButton)
        {
            _text.Color = Color.Red;
            _text.Text = "DANGER ZONE: Delete saving?";
        }
    }

    private void CheckFocus(object sender, EventArgs e)
    {
        if(sender == _backButton)
        {
            if(_focusDelete)
            {
                _backButton.IsFocused = false;
                _deleteButton.IsFocused = true;
                _focusDelete = false;
            }
        }
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _startGameButton.IsFocused = !isVisible;
        _backButton.IsFocused = !isVisible;
        _deleteButton.IsFocused = !isVisible;
        _deleteButton.IsVisible = isVisible;
        _deleteButton.IsEnabled = isVisible;
        _shopButton.IsFocused = isVisible;
    }

    private void FocusDelete(object sender, KeyEventArgs e)
    {
        if(sender == _startGameButton)
        {
            if(e.Key == Keys.Up)
            {   
                _focusDelete = true;
            }
        }
        else if(sender == _deleteButton)
        {
                _focusDelete = false;
                _deleteButton.IsFocused = false;
        }
    }

    private void LostFocus(object sender, EventArgs e)
    {
        if(sender == _startGameButton)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if(gamePadState.IsButtonDown(Buttons.DPadUp) || gamePadState.ThumbSticks.Left.Y > 0)
            {
                _focusDelete = true;
            }
        }
        else if(sender == _deleteButton)
        {
            _focusDelete = false;
            _deleteButton.IsFocused = false;
        }
    }

    private void DeleteSaving(object sender, EventArgs e)
    {
        // A UI interaction occurred, play the sound effect
        Core.Audio.PlaySoundEffect(TitlePanelManager.uiSoundEffect);

        SetIsVisible(false);
        TitlePanelManager.HandleDeleteGameClicked();
    }

}