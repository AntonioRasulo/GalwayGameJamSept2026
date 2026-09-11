using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum;
using MonoGameLibrary;

namespace GameName.UI;

public class TitleScreenButtonsPanel : PangPanel
{

    private const string TITLE_TEXT = "Falls\n off\n the\nBalls";

    // The position to draw the monogame text at.
    private Vector2 _titleTextPos;

    // The origin to set for the monogame text.
    private Vector2 _titleTextOrigin;

    private AnimatedButton _optionsButton;
    private AnimatedButton _startButton;
    private AnimatedButton _creditsButton;

    // The font used to render the title text.
    private SpriteFont _font5x;

    public TitleScreenButtonsPanel()
    {
        // Create a container to hold all of our buttons
        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        _creditsButton = new AnimatedButton(_GUIatlas);
        _creditsButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _creditsButton.Width = 50;
        _creditsButton.Text = "Credits";
        _creditsButton.Y = -12;
        _creditsButton.Click += TitlePanelManager.HandleCreditsClicked;
        _panel.AddChild(_creditsButton);

        _startButton = new AnimatedButton(_GUIatlas);
        _startButton.Anchor(Gum.Wireframe.Anchor.Bottom);
        _startButton.Y = -12;
        _startButton.Width = 50;
        _startButton.Text = "Start";
        _startButton.Click += TitlePanelManager.HandleStartClicked;
        _panel.AddChild(_startButton);

        _optionsButton = new AnimatedButton(_GUIatlas);
        _optionsButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _optionsButton.Y = -12;
        _optionsButton.Width = 50;
        _optionsButton.Text = "Options";
        _optionsButton.Click += TitlePanelManager.HandleOptionsClicked;
        _panel.AddChild(_optionsButton);

        // Load the font for the title text.
        _font5x = Core.Content.Load<SpriteFont>("fonts/04B_30_5x");

        // Set the position and origin for the Title text.
        Vector2 size = _font5x.MeasureString(TITLE_TEXT);
        _titleTextPos = new Vector2(640, 300);
        _titleTextOrigin = size * 0.5f;
    }

    public void SetStartButtonFocus(bool IsFocused)
    {
        _startButton.IsFocused = IsFocused;
    }

    public void Draw()
    {
        if(_panel.IsVisible)
        {
            // The color to use for the drop shadow text.
            Color dropShadowColor = Color.Black * 0.5f;

            // Draw the TITLE_TEXT text slightly offset from it is original position and
            // with a transparent color to give it a drop shadow.
            Core.SpriteBatch.DrawString(_font5x, TITLE_TEXT, _titleTextPos + new Vector2(10, 10), dropShadowColor, 0.0f, _titleTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

            // Draw the TITLE_TEXT text on top of that at its original position.
            Core.SpriteBatch.DrawString(_font5x, TITLE_TEXT, _titleTextPos, Color.White, 0.0f, _titleTextOrigin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);

        _creditsButton.IsFocused = !isVisible;
        _optionsButton.IsFocused = !isVisible;
        _startButton.IsFocused = isVisible;
    }

}