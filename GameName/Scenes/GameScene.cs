using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Content;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
// using GameName.GameObjects;
// using GameName.Config;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using GameName.UI;
using MonoGameGum;
using GameName.Backgrounds;

namespace GameName.Scenes;

public class GameScene : Scene
{
    private enum GameState
    {
        Playing,
        Paused
    }

    private Rectangle _roomBounds;

    // The SpriteFont Description used to draw text.
    private SpriteFont _font;

    // Tracks the players score.
    private int _score;

    private int _currentLevelIndex;

    private GameSceneUI _ui;

    private GameState _state;

    // The grayscale shader effect.
    private Material _grayscaleEffect;

    // The amount of saturation to provide the grayscale shader effect.
    private float _saturation = 1.0f;

    // The speed of the fade to grayscale effect.
    private const float FADE_SPEED = 0.02f;

    private const int PLATF_DESTR_SCORE = 10;
    private const int SCORE_LEVEL = 20;

    private Background _levelBackground;

    public GameScene(int startingLevel)
    {
        _currentLevelIndex = startingLevel;
    }

    public override void Initialize()
    {

        base.Initialize();

        // During the game scene, we want to disable exit on escape. Instead,
        // the escape key will be used to return back to the title screen
        Core.ExitOnEscape = false;

        _roomBounds = Core.GraphicsDevice.PresentationParameters.Bounds;

        // Create any UI elements from the root element created in previous
        // scenes.
        GumService.Default.Root.Children.Clear();

        // Initialize the user interface for the game scene.
        InitializeUI();

        // Initialize a new game to be played.
        InitializeNewGame();

    }

    private void InitializeUI()
    {
        // Clear out any previous UI element incase we came here
        // from a different scene.
        GumService.Default.Root.Children.Clear();

        // Create the game scene ui instance.
        _ui = new GameSceneUI();
        _ui.UpdateMoneyText();

        // Subscribe to the events from the game scene ui.
        _ui.ResumeButtonClick += OnResumeButtonClicked;
        _ui.RetryButtonClick += OnRetryButtonClicked;
        _ui.QuitButtonClick += OnQuitButtonClicked;
    }

    // TODO complete implementation
    private void InitializeNewGame()
    {
        _state = GameState.Playing;
    }

    private void OnResumeButtonClicked(object sender, EventArgs args)
    {
        // Change the game state back to playing.
        _state = GameState.Playing;
    }

    private void OnRetryButtonClicked(object sender, EventArgs args)
    {
        // Player has chosen to retry, so initialize a new game.
        //InitializeNewGame(); TODO
    }

    private void OnQuitButtonClicked(object sender, EventArgs args)
    {
        // Player has chosen to quit, so return back to the title scene.
        Core.ChangeScene(new TitleScene());
    }

    public override void LoadContent()
    {
        try
        {
            // Load the background theme music
            Song theme = Content.Load<Song>("audio/Music/16. Battle Theme III (loop)");
            Core.Audio.PlaySong(theme);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load theme music: {ex.Message}");
        }

        // Load the font
        _font = Content.Load<SpriteFont>("fonts/04B_30");

        // Load the grayscale effect.
        _grayscaleEffect = Content.WatchMaterial("effects/grayscaleEffect");
        _grayscaleEffect.IsDebugVisible = false;
    }

    public override void Update(GameTime gameTime)
    {
        // Update the grayscale effect if it was changed
        _grayscaleEffect.Update();

        // Ensure the UI is always updated.
        _ui.Update(gameTime);

        if (_state != GameState.Playing)
        {
            // The game is in either a paused or game over state, so
            // gradually decrease the saturation to create the fading grayscale.
            _saturation = Math.Max(0.0f, _saturation - FADE_SPEED);

        }

        // If the pause button is pressed, toggle the pause state. TODO implement GameController
        if(Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape) || Core.Input.GamePads[(int)PlayerIndex.One].WasButtonJustPressed(Buttons.Start))
        {
            TogglePause();
        }

        // At this point, if the game is paused, just return back early.
        if (_state == GameState.Paused)
        {
            return;
        }

        CollisionChecks();

        checkChangeScene();

        _levelBackground.Update(gameTime);

    }

    private void TogglePause()
    {
        if (_state == GameState.Paused)
        {
            // We're now unpausing the game, so hide the pause panel.
            _ui.HidePausePanel();

            // And set the state back to playing.
            _state = GameState.Playing;
        }
        else
        {
            // We're now pausing the game, so show the pause panel.
            _ui.ShowPausePanel();

            // And set the state to paused.
            _state = GameState.Paused;

            // Set the grayscale effect saturation to 1.0f
            _saturation = 1.0f;
        }
    }

    private void CollisionChecks()
    {
 
    }

    private void checkChangeScene()
    {

    }

    private bool areIntersecting(Circle circle, Rectangle rectangle)
    {
        int distanceX = Math.Abs(circle.X - rectangle.Center.X);
        int distanceY = Math.Abs(circle.Y - rectangle.Center.Y);

        float halfRectWidth = rectangle.Width * 0.5f;
        float halfRectHeight = rectangle.Height * 0.5f;

        if((distanceX > (halfRectWidth + circle.Radius)) ||
           (distanceY > (halfRectHeight + circle.Radius)))
        {
            return false;
        }

        if(distanceX <= halfRectWidth ||
           distanceY <= halfRectHeight)
        {
            return true;
        }

        double cornerDistanceSquare = Math.Pow(distanceX-halfRectWidth, 2) + Math.Pow(distanceY-halfRectHeight, 2);

        return cornerDistanceSquare <= Math.Pow(circle.Radius, 2);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.White);

        if (_state != GameState.Playing)
        {
            // We are in a game over state, so apply the saturation parameter.
            _grayscaleEffect.SetParameter("Saturation", _saturation);

            // Draw the background
            _levelBackground.Draw(_grayscaleEffect.Effect);

            // And begin the sprite batch using the grayscale effect.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: _grayscaleEffect.Effect);
        }
        else
        {
            // Draw the background
            _levelBackground.Draw();

            // Begin the sprite batch to prepare for rendering.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }

        // Always end the sprite batch when finished.
        Core.SpriteBatch.End();

        // Draw the UI.
        _ui.Draw();

        base.Draw(gameTime);
    }

    private void LoadLevel()
    {

    }

}
