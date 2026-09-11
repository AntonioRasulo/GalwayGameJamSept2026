using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals.V3;
using Gum.Managers;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using System;
using System.Collections.Generic;

namespace GameName.UI;

public class NewGamePanel : PangPanel
{
    private Panel _newGameNamePanel;
    private TextRuntime _nameText;
    private AnimatedButton _newGameBackButton;
    private AnimatedButton _confirmButton;
    private List<AnimatedButton> _lettersBtn;
    private SpriteButton _backSpace;
    private const int MAX_TEXT_SIZE = 7;

    public NewGamePanel()
    {
        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        AddLetterButtons();

        _confirmButton = new AnimatedButton(_GUIatlas);
        _confirmButton.Text = "CONFIRM";
        _confirmButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        _confirmButton.X = 70;
        _confirmButton.Y = 130f;
        _confirmButton.Click += TitlePanelManager.handleConfirmNameClicked;
        ((ButtonVisual)_confirmButton.Visual).Height = 18f;
        _panel.AddChild(_confirmButton);

        _newGameNamePanel = CreateTextPanel();
        _panel.AddChild(_newGameNamePanel);

        _newGameBackButton = new AnimatedButton(_GUIatlas);
        _newGameBackButton.Text = "BACK";
        _newGameBackButton.Anchor(Gum.Wireframe.Anchor.TopLeft);
        _newGameBackButton.X = 140f;
        _newGameBackButton.Y = 130f;
        _newGameBackButton.Click += TitlePanelManager.HandleStartClicked;
        ((ButtonVisual)_newGameBackButton.Visual).Height = 18f;
        _panel.AddChild(_newGameBackButton);

        Texture2D arrow2DTexture = Core.Content.Load<Texture2D>("images/UI/Pixelart arrow icon pack 1.0");
        TextureRegion arrowRegion = new TextureRegion(arrow2DTexture, 50, 1, 10, 13);
        
        _backSpace = new SpriteButton(_GUIatlas, arrowRegion, 90);
        _backSpace.Text = "";
        _backSpace.Anchor(Gum.Wireframe.Anchor.TopLeft);
        _backSpace.X = 190f;
        _backSpace.Y = 130f;
        _backSpace.Click += EraseLastChar;
        ((ButtonVisual)_backSpace.Visual).Height = 18f;
        ((ButtonVisual)_backSpace.Visual).Width = 30f;
        _panel.AddChild(_backSpace);
    }

    public string GetNewGameTextBoxText()
    {
        return _nameText.Text;
    }

    public void ClearNewGameTextBox()
    {
        _nameText.Text = "";
    }

    private void AddLetterButtons()
    {
        _lettersBtn = new List<AnimatedButton>();
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "Z" };
        float yIndex = 0;
        float xIndex = 0;
        const int NUM_COLUMN = 6;
        for (int i = 0; i < letters.Length; i++)
        {
            yIndex = i / NUM_COLUMN;
            xIndex = i - NUM_COLUMN * yIndex;
            
            AnimatedButton btn = new AnimatedButton(_GUIatlas);
            btn.Text = letters[i];

            ((ButtonVisual)btn.Visual).Width = 30f;
            ((ButtonVisual)btn.Visual).Height = 18f;
            btn.Anchor(Gum.Wireframe.Anchor.TopLeft);
            btn.X = 40.0f + xIndex * 40.0f;
            btn.Y = 10.0f + yIndex * 30.0f;
            btn.Click += AddChar;
            _panel.AddChild(btn);
            _lettersBtn.Add(btn);
        }
    }

    private void AddChar(Object sender, EventArgs e)
    {
        if(_nameText.Text.Length < MAX_TEXT_SIZE)
        {
            _nameText.Text += ((AnimatedButton)sender).Text;
        }
    }

    private void EraseLastChar(Object sender, EventArgs e)
    {
        string text = _nameText.Text;
        if(text != "")
        {
            _nameText.Text = text.Remove(text.Length-1);
        }
    }

    private Panel CreateTextPanel()
    {
        Panel panel = new Panel();
        panel.Anchor(Gum.Wireframe.Anchor.Bottom);
        panel.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        panel.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        panel.Width = 200.0f;
        panel.Height = 25.0f;
        panel.IsVisible = true;

        TextureRegion backgroundRegion = _GUIatlas.GetRegion("panel-background");

        NineSliceRuntime background = new NineSliceRuntime();
        background.Dock(Gum.Wireframe.Dock.Fill);
        background.Texture = backgroundRegion.Texture;
        background.TextureAddress = TextureAddress.Custom;
        background.TextureHeight = backgroundRegion.Height;
        background.TextureWidth = backgroundRegion.Width;
        background.TextureTop = backgroundRegion.SourceRectangle.Top;
        background.TextureLeft = backgroundRegion.SourceRectangle.Left;
        panel.AddChild(background);

        _nameText = new TextRuntime();
        _nameText.Text = "";
        _nameText.UseCustomFont = true;
        _nameText.CustomFontFile = "fonts/04b_30.fnt";
        _nameText.FontScale = 0.25f;
        _nameText.Anchor(Gum.Wireframe.Anchor.Center);
        panel.AddChild(_nameText);

        return panel;
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _confirmButton.IsFocused = !isVisible;
        _newGameBackButton.IsFocused = isVisible;
        _lettersBtn.ForEach(btn => btn.IsFocused = !isVisible);
    }

}