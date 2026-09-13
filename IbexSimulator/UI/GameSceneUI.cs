using System;
using Gum.DataTypes;
using Gum.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGameGum;
using Gum.Forms.Controls;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
using IbexGame.GameObjects;

namespace IbexGame.UI;

public class GameSceneUI : ContainerRuntime
{

    // The string format to use when updating the text for the score display.
    private static readonly string s_scoreFormat = "SCORE: {0:D6}";

    // The string format to use when updating the text for the time display.
    private static readonly string s_timeFormat = "TIME: {0:D3}";

    // The string format to use when updating the text for the lives display.
    private static readonly string s_flowerFormat = "{0:D5}"; 

    // The sound effect to play for auditory feedback of the user interface.
    //private SoundEffect _uiSoundEffect;

    // The pause panel
    private Panel _pausePanel;

    // The resume button on the pause panel. Field is used to track reference so
    // focus can be set when the pause panel is shown.
    private AnimatedButton _resumeButton;

    private AnimatedButton _quitButton;

    // The game over panel.
    private Panel _gameOverPanel;

    // The retry button on the game over panel. Field is used to track reference
    // so focus can be set when the game over panel is shown.
    private AnimatedButton _retryButton;

    // The text runtime used to display the players score on the game screen.
    private TextRuntime _scoreText;

    // The text runtime used to display the timer on the game screen.
    private TextRuntime _timerText;

    // The text runtime used to display the lives on the game screen.
    //private TextRuntime _livesText;

    // The text runtime used to display the flowers amount on the pause screen.
    private TextRuntime _flowerText;

    // Number of seconds on the current level.
    private double _timer;

    /// <summary>
    /// Event invoked when the Resume button on the Pause panel is clicked.
    /// </summary>
    public event EventHandler ResumeButtonClick;

    /// <summary>
    /// Event invoked when the Quit button on either the Pause panel or the
    /// Game Over panel is clicked.
    /// </summary>
    public event EventHandler QuitButtonClick;

    /// <summary>
    /// Event invoked when the Retry button on the Game Over panel is clicked.
    /// </summary>
    public event EventHandler RetryButtonClick;

    private Sprite _flowerSprite;
    private Vector2 _flowerSpritePosition;

    public GameSceneUI()
    {
        // The game scene UI inherits from ContainerRuntime, so we set its
        // doc to fill so it fills the entire screen.
        Dock(Gum.Wireframe.Dock.Fill);

        // Add it to the root element.
        this.AddToRoot();

        // Get a reference to the content manager that was registered with the
        // GumService when it was original initialized.
        ContentManager content = GumService.Default.ContentLoader.XnaContentManager;

        // Use that content manager to load the sound effect and atlas for the
        // user interface elements
        TextureAtlas atlas = TextureAtlas.FromFile(content, "images/UI/GUI_atlas.xml");

        // Create the text that will display the players score and add it as
        // a child to this container.
        _scoreText = CreateScoreText();
        AddChild(_scoreText);

        // Create the text that will display the timer and add it as
        // a child to this container.
        _timerText = CreateTimerText();
        AddChild(_timerText);

        // Create the Pause panel that is displayed when the game is paused and
        // add it as a child to this container
        _pausePanel = CreatePausePanel(atlas);
        AddChild(_pausePanel.Visual);

        // Create the Game Over panel that is displayed when a game over occurs
        // and add it as a child to this container
        _gameOverPanel = CreateGameOverPanel(atlas);
        AddChild(_gameOverPanel.Visual);
    }

    private TextRuntime CreateScoreText()
    {
        TextRuntime text = new TextRuntime();
        text.Anchor(Gum.Wireframe.Anchor.TopLeft);
        text.WidthUnits = DimensionUnitType.RelativeToChildren;
        text.X = 20.0f;
        text.Y = 5.0f;
        text.UseCustomFont = true;
        text.CustomFontFile = @"fonts/04b_30.fnt";
        text.FontScale = 0.25f;
        text.Text = string.Format(s_scoreFormat, 0);

        return text;
    }

    private TextRuntime CreateTimerText()
    {   
        var screenWidth = GumService.Default.CanvasWidth;
        _timer = 0f;
        TextRuntime text = new TextRuntime();
        text.Anchor(Gum.Wireframe.Anchor.TopRight);
        text.WidthUnits = DimensionUnitType.RelativeToChildren;
        text.X = -20.0f;
        text.Y = 5.0f;
        text.UseCustomFont = true;
        text.CustomFontFile = @"fonts/04b_30.fnt";
        text.FontScale = 0.25f;
        text.Text = string.Format(s_timeFormat, (int)_timer);

        return text;
    }

    private Panel CreatePausePanel(TextureAtlas atlas)
    {
        Panel panel = new Panel();
        panel.Anchor(Gum.Wireframe.Anchor.Center);
        panel.WidthUnits = DimensionUnitType.Absolute;
        panel.HeightUnits = DimensionUnitType.Absolute;
        panel.Width = 264.0f;
        panel.Height = 70.0f;
        panel.IsVisible = false;

        TextureRegion backgroundRegion = atlas.GetRegion("panel-background");

        NineSliceRuntime background = new NineSliceRuntime();
        background.Dock(Gum.Wireframe.Dock.Fill);
        background.Texture = backgroundRegion.Texture;
        background.TextureAddress = TextureAddress.Custom;
        background.TextureHeight = backgroundRegion.Height;
        background.TextureWidth = backgroundRegion.Width;
        background.TextureTop = backgroundRegion.SourceRectangle.Top;
        background.TextureLeft = backgroundRegion.SourceRectangle.Left;
        panel.AddChild(background);

        TextRuntime text = new TextRuntime();
        text.Text = "PAUSED";
        text.UseCustomFont = true;
        text.CustomFontFile = "fonts/04b_30.fnt";
        text.FontScale = 0.5f;
        text.X = 10.0f;
        text.Y = 10.0f;
        panel.AddChild(text);

        _resumeButton = new AnimatedButton(atlas);
        _resumeButton.Text = "RESUME";
        _resumeButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _resumeButton.X = 9.0f;
        _resumeButton.Y = -9.0f;

        _resumeButton.Click += OnResumeButtonClicked;

        panel.AddChild(_resumeButton);

        _quitButton = new AnimatedButton(atlas);
        _quitButton.Text = "QUIT";
        _quitButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _quitButton.X = -9.0f;
        _quitButton.Y = -9.0f;

        _quitButton.Click += OnQuitButtonClicked;

        panel.AddChild(_quitButton);

        var screenWidth = GumService.Default.CanvasWidth;
        _flowerText = new TextRuntime();
        _flowerText.Anchor(Gum.Wireframe.Anchor.TopRight);
        _flowerText.WidthUnits = DimensionUnitType.RelativeToChildren;
        _flowerText.Y = 15.0f;
        _flowerText.X = -10.0f;
        _flowerText.UseCustomFont = true;
        _flowerText.CustomFontFile = @"fonts/04b_30.fnt";
        _flowerText.FontScale = 0.25f;
        _flowerText.Text = string.Format(s_flowerFormat, 0);

        panel.AddChild(_flowerText);

        float flowerX = Core.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.7f;
        float flowerY = Core.GraphicsDevice.PresentationParameters.BackBufferHeight *0.37f;

        _flowerSpritePosition = new Vector2(flowerX, flowerY);

        return panel;
    }

    private Panel CreateGameOverPanel(TextureAtlas atlas)
    {
        Panel panel = new Panel();
        panel.Anchor(Gum.Wireframe.Anchor.Center);
        panel.WidthUnits = DimensionUnitType.Absolute;
        panel.HeightUnits = DimensionUnitType.Absolute;
        panel.Width = 264.0f;
        panel.Height = 70.0f;
        panel.IsVisible = false;

        TextureRegion backgroundRegion = atlas.GetRegion("panel-background");

        NineSliceRuntime background = new NineSliceRuntime();
        background.Dock(Gum.Wireframe.Dock.Fill);
        background.Texture = backgroundRegion.Texture;
        background.TextureAddress = TextureAddress.Custom;
        background.TextureHeight = backgroundRegion.Height;
        background.TextureWidth = backgroundRegion.Width;
        background.TextureTop = backgroundRegion.SourceRectangle.Top;
        background.TextureLeft = backgroundRegion.SourceRectangle.Left;
        panel.AddChild(background);

        TextRuntime text = new TextRuntime();
        text.Text = "GAME OVER";
        text.WidthUnits = DimensionUnitType.RelativeToChildren;
        text.UseCustomFont = true;
        text.CustomFontFile = "fonts/04b_30.fnt";
        text.FontScale = 0.5f;
        text.X = 10.0f;
        text.Y = 10.0f;
        panel.AddChild(text);

        _retryButton = new AnimatedButton(atlas);
        _retryButton.Text = "RETRY";
        _retryButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _retryButton.X = 9.0f;
        _retryButton.Y = -9.0f;

        _retryButton.Click += OnRetryButtonClicked;

        panel.AddChild(_retryButton);

        AnimatedButton quitButton = new AnimatedButton(atlas);
        quitButton.Text = "QUIT";
        quitButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        quitButton.X = -9.0f;
        quitButton.Y = -9.0f;

        quitButton.Click += OnQuitButtonClicked;

        panel.AddChild(quitButton);

        return panel;
    }

    private void OnResumeButtonClicked(object sender, EventArgs args)
    {
        // Button was clicked, play the ui sound effect for auditory feedback.
        Goat.playGoatSoundEffect();

        // Since the resume button was clicked, we need to hide the pause panel.
        HidePausePanel();

        // Invoke the ResumeButtonClick event
        if (ResumeButtonClick != null)
        {
            ResumeButtonClick(sender, args);
        }
    }

    private void OnRetryButtonClicked(object sender, EventArgs args)
    {
        // Button was clicked, play the ui sound effect for auditory feedback.
        Goat.playGoatSoundEffect();

        // Since the retry button was clicked, we need to hide the game over panel.
        HideGameOverPanel();

        // Invoke the RetryButtonClick event.
        if (RetryButtonClick != null)
        {
            RetryButtonClick(sender, args);
        }
    }

    private void OnQuitButtonClicked(object sender, EventArgs args)
    {
        // Button was clicked, play the ui sound effect for auditory feedback.
        Goat.playGoatSoundEffect();

        // Both panels have a quit button, so hide both panels
        HidePausePanel();
        HideGameOverPanel();

        // Invoke the QuitButtonClick event.
        if (QuitButtonClick != null)
        {
            QuitButtonClick(sender, args);
        }
    }

    /// <summary>
    /// Updates the text on the score display.
    /// </summary>
    /// <param name="score">The score to display.</param>
    public void UpdateScoreText(int score)
    {
        _scoreText.Text = string.Format(s_scoreFormat, score);
    }

    /// <summary>
    /// Updates the text on the timer display.
    /// </summary>
    /// <param name="gametime">A snapshot of the timing values for the current update cycle.</param>
    private void UpdateTimerText(GameTime gameTime)
    {
        if(_pausePanel.IsVisible == false)
        {
            _timer += gameTime.ElapsedGameTime.TotalSeconds;
            _timerText.Text = string.Format(s_timeFormat, (int)_timer);
        }
    }

    public void UpdateFlowerText(int numFlowers)
    {
        _flowerText.Text = string.Format(s_flowerFormat, numFlowers);
    }

    /// <summary>
    /// Reset the timer.
    /// </summary>
    public void resetTimer()
    {
        _timer = 0f;
    }

    public int getTimer()
    {
        return (int)_timer;
    }

    /// <summary>
    /// Tells the game scene ui to show the pause panel.
    /// </summary>
    public void ShowPausePanel()
    {
        flowerType type = flowerType.NONE;
        while(type == flowerType.NONE)
        {
            type = Flower.getRandomType();
        }

        _flowerSprite = Flower.GetSprite(type);
        _flowerSprite.Scale = new Vector2(2.0f, 2.0f);

        _pausePanel.IsVisible = true;

        // Give the resume button focus for keyboard/gamepad input.
        _resumeButton.IsFocused = true;

        // Ensure the game over panel isn't visible.
        _gameOverPanel.IsVisible = false;
    }

    /// <summary>
    /// Tells the game scene ui to hide the pause panel.
    /// </summary>
    public void HidePausePanel()
    {
        // Give the resume button focus for keyboard/gamepad input.
        _resumeButton.IsFocused = false;

        _quitButton.IsFocused = false;

        _pausePanel.IsVisible = false;
    }

    /// <summary>
    /// Tells the game scene ui to show the game over panel.
    /// </summary>
    public void ShowGameOverPanel()
    {
        _gameOverPanel.IsVisible = true;

        // Give the retry button focus for keyboard/gamepad input.
        _retryButton.IsFocused = true;

        // Ensure the pause panel isn't visible.
        _pausePanel.IsVisible = false;
    }

    /// <summary>
    /// Tells the game scene ui to hide the game over panel.
    /// </summary>
    public void HideGameOverPanel()
    {
        _gameOverPanel.IsVisible = false;
    }

    /// <summary>
    /// Updates the game scene ui.
    /// </summary>
    /// <param name="gameTime">A snapshot of the timing values for the current update cycle.</param>
    public void Update(GameTime gameTime)
    {
        UpdateTimerText(gameTime);
        GumService.Default.Update(gameTime);
    }

    /// <summary>
    /// Draws the game scene ui.
    /// </summary>
    public void Draw()
    {
        GumService.Default.Draw();
        if (_pausePanel.IsVisible)
        {
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _flowerSprite.Draw(Core.SpriteBatch, _flowerSpritePosition);
            Core.SpriteBatch.End();
        }
    }

}
