using System;
using Gum.DataTypes;
using Gum.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using MonoGameGum;
using Gum.Forms.Controls;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
// using GameName.Config;
// using GameName.GameObjects;

namespace GameName.UI;

public class GameSceneUI : ContainerRuntime
{

    // The string format to use when updating the text for the score display.
    private static readonly string s_scoreFormat = "SCORE: {0:D6}";

    // The string format to use when updating the text for the time display.
    private static readonly string s_timeFormat = "TIME: {0:D3}";

    // The string format to use when updating the text for the lives display.
    private static readonly string s_livesFormat = "X{0:D2}";

    // The string format to use when updating the text for the lives display.
    private static readonly string s_moneyFormat = "{0:D5}"; 

    // The sound effect to play for auditory feedback of the user interface.
    private SoundEffect _uiSoundEffect;

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
    private TextRuntime _livesText;

    // The text runtime used to display the money amount on the pause screen.
    private TextRuntime _moneyText;

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

    private AnimatedSprite _chestAnimation;
    private Vector2 _chestAnimationPosition;

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
        _uiSoundEffect = content.Load<SoundEffect>("audio/Sound effects/Confirm 1");
        TextureAtlas atlas = TextureAtlas.FromFile(content, "images/UI/GUI_atlas.xml");

        // Create the text that will display the players score and add it as
        // a child to this container.
        _scoreText = CreateScoreText();
        AddChild(_scoreText);

        // Create the text that will display the timer and add it as
        // a child to this container.
        _timerText = CreateTimerText();
        AddChild(_timerText);

        // Create the text that will display the lives and add it as
        // a child to this container.
        _livesText = CreateLivesText();
        AddChild(_livesText);

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
        text.Anchor(Gum.Wireframe.Anchor.TopLeft);
        text.WidthUnits = DimensionUnitType.RelativeToChildren;
        text.X = GumService.Default.CanvasWidth * 0.5f;
        text.Y = 5.0f;
        text.UseCustomFont = true;
        text.CustomFontFile = @"fonts/04b_30.fnt";
        text.FontScale = 0.25f;
        text.Text = string.Format(s_timeFormat, (int)_timer);

        return text;
    }

    private TextRuntime CreateLivesText()
    {
        var screenWidth = GumService.Default.CanvasWidth;
        TextRuntime text = new TextRuntime();
        text.Anchor(Gum.Wireframe.Anchor.TopRight);
        text.WidthUnits = DimensionUnitType.RelativeToChildren;
        text.Y = 5.0f;
        text.UseCustomFont = true;
        text.CustomFontFile = @"fonts/04b_30.fnt";
        text.FontScale = 0.25f;
        text.Text = string.Format(s_livesFormat, 0);

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
        _resumeButton.GotFocus += OnElementGotFocus;

        panel.AddChild(_resumeButton);

        _quitButton = new AnimatedButton(atlas);
        _quitButton.Text = "QUIT";
        _quitButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _quitButton.X = -9.0f;
        _quitButton.Y = -9.0f;

        _quitButton.Click += OnQuitButtonClicked;
        _quitButton.GotFocus += OnElementGotFocus;

        panel.AddChild(_quitButton);

        var screenWidth = GumService.Default.CanvasWidth;
        _moneyText = new TextRuntime();
        _moneyText.Anchor(Gum.Wireframe.Anchor.TopRight);
        _moneyText.WidthUnits = DimensionUnitType.RelativeToChildren;
        _moneyText.Y = 15.0f;
        _moneyText.X = -10.0f;
        _moneyText.UseCustomFont = true;
        _moneyText.CustomFontFile = @"fonts/04b_30.fnt";
        _moneyText.FontScale = 0.25f;
        _moneyText.Text = string.Format(s_moneyFormat, 0);

        panel.AddChild(_moneyText);

        TextureAtlas chestAtlas = TextureAtlas.FromFile(Core.Content, "images/Coins/chest_atlas.xml");

        // Get the multi-frame chest animation from the atlas
        _chestAnimation = chestAtlas.CreateAnimatedSprite("chest-animation");
        _chestAnimation.Scale = new Vector2(4.0f, 4.0f);

        float chestAnimationX = Core.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.7f;
        float chestAnimationY = Core.GraphicsDevice.PresentationParameters.BackBufferHeight *0.37f;

        _chestAnimationPosition = new Vector2(chestAnimationX, chestAnimationY);

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
        _retryButton.GotFocus += OnElementGotFocus;

        panel.AddChild(_retryButton);

        AnimatedButton quitButton = new AnimatedButton(atlas);
        quitButton.Text = "QUIT";
        quitButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        quitButton.X = -9.0f;
        quitButton.Y = -9.0f;

        quitButton.Click += OnQuitButtonClicked;
        quitButton.GotFocus += OnElementGotFocus;

        panel.AddChild(quitButton);

        return panel;
    }

    private void OnResumeButtonClicked(object sender, EventArgs args)
    {
        // Button was clicked, play the ui sound effect for auditory feedback.
        Core.Audio.PlaySoundEffect(_uiSoundEffect);

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
        Core.Audio.PlaySoundEffect(_uiSoundEffect);

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
        Core.Audio.PlaySoundEffect(_uiSoundEffect);

        // Both panels have a quit button, so hide both panels
        HidePausePanel();
        HideGameOverPanel();

        // Invoke the QuitButtonClick event.
        if (QuitButtonClick != null)
        {
            QuitButtonClick(sender, args);
        }
    }

    private void OnElementGotFocus(object sender, EventArgs args)
    {
        // A ui element that can receive focus has received focus, play the
        // ui sound effect for auditory feedback.
        Core.Audio.PlaySoundEffect(_uiSoundEffect);
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
        // if((_pausePanel.IsVisible == false) && (FreezeHandler.freezeTimer == 0))
        // {
        //     _timer += gameTime.ElapsedGameTime.TotalSeconds;
        //     _timerText.Text = string.Format(s_timeFormat, (int)_timer);
        // }
    }

    public void UpdateMoneyText()
    {
        //_moneyText.Text = string.Format(s_moneyFormat, PlayerStatsManager.currentStats.Money);
    }

    /// <summary>
    /// Updates the text on the lives display.
    /// </summary>
    /// <param name="lives">Number of lives of the character.</param>
    public void UpdateLivesText(int lives)
    {
        if(_pausePanel.IsVisible == false)
        {
            _livesText.Text = string.Format(s_livesFormat, lives);
        }
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
        if (_pausePanel.IsVisible)
        {
            _chestAnimation.Update(gameTime);
        }
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
            _chestAnimation.Draw(Core.SpriteBatch, _chestAnimationPosition);
            Core.SpriteBatch.End();
        }
    }

}
