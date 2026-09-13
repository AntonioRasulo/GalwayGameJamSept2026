using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using MonoGameLibrary.Content;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using GameName.UI;
using MonoGameGum;
using GameName.GameObjects;
using System.Linq;

namespace GameName.Scenes;

public class GameScene : Scene
{
    private enum GameState
    {
        Playing,
        Paused
    }

    private Goat _goat;

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

    private Texture2D _levelBackground;
    private List<Platform> _platforms;

    private float _timeToGenerateNewPlatform = 4.5f; // Generate a new platform every 4 seconds
    private float _platformGeneratorTimer = 0.0f;

    private float _timeToIncreasePlatformGravity = 10.0f; // Every 10 seconds increase platform gravity
    private float _platformGravityTimer = 0.0f;

    private Random _platformRand;

    private Vector2 lastGenPlatformCoord;

    private List<Flower> _flowers;
    private int _numFlowersPicked = 0;

    public GameScene(int startingLevel)
    {
        _currentLevelIndex = startingLevel;
        Platform.restoreGravity();
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
            Song theme = Content.Load<Song>("audio/Music/4-Winter2-night-zwinzlergames");
            Core.Audio.PlaySong(theme);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load theme music: {ex.Message}");
        }

        _goat = new Goat();

        Platform.LoadContent();
        Flower.LoadContent();

        _platforms = new List<Platform>();
        _flowers = new List<Flower>();

        float initialPlatformPosY = Core.GraphicsDevice.PresentationParameters.BackBufferHeight * 0.5f;
        float initialPlatformPosX = Core.GraphicsDevice.PresentationParameters.BackBufferWidth * 0.1f;
        Vector2 initialPlatformPos = new Vector2(initialPlatformPosX, initialPlatformPosY);
        _platforms.Add(new Platform(initialPlatformPos));
        _platforms.Add(new Platform(initialPlatformPos + new Vector2(250.0f, -100.0f)));
        _platforms.Add(new Platform(initialPlatformPos + new Vector2(400.0f, -250.0f)));

        _platformRand = new Random();

        lastGenPlatformCoord = initialPlatformPos + new Vector2(400.0f, -250.0f);

        _levelBackground = Core.Content.Load<Texture2D>("images/backgrounds/mountains/origbig");

        // Load the font
        _font = Content.Load<SpriteFont>("fonts/mountain_and_nature/Mountain_and_Nature_small");

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

        _goat.Update(gameTime);

        foreach(Platform platform in _platforms)
        {
            platform.Update(gameTime);
        }

        foreach(Flower flower in _flowers)
        {
            flower.Update(gameTime);
        }

        _platforms.RemoveAll(platform => platform.toRemove);

        CollisionChecks();

        _flowers.RemoveAll(flower => flower.toRemove);

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _platformGeneratorTimer += delta;

        if (_platformGeneratorTimer >= _timeToGenerateNewPlatform)
        {
            _platformGeneratorTimer = 0;
            GenerateNewPlatform();
        }

        _platformGravityTimer += delta;

        if(_platformGravityTimer >= _timeToIncreasePlatformGravity)
        {
            _platformGravityTimer = 0;
            Platform.platformGravity += 0.1f;
            _timeToGenerateNewPlatform -= 0.2f;
            if(_timeToGenerateNewPlatform <= 2)
            {
                _timeToGenerateNewPlatform = 2f;
                _goat.jumpStrength += 0.1f;
            }
        }
    }

    private void GenerateNewPlatform()
    {
        float lowerBoundX = lastGenPlatformCoord.X - Core.GraphicsDevice.PresentationParameters.BackBufferWidth* 0.3f;

        float upperBoundX = lastGenPlatformCoord.X + Core.GraphicsDevice.PresentationParameters.BackBufferWidth* 0.3f;

        int randomX = _platformRand.Next((int)lowerBoundX, (int)upperBoundX);

        int platformSize = 23*4;

        while((randomX > Core.GraphicsDevice.PresentationParameters.BackBufferWidth - platformSize) ||  (randomX < platformSize))
        {
            randomX = _platformRand.Next((int)lowerBoundX, (int)upperBoundX);
        }

        Vector2 newPlatformPos = new Vector2(randomX, 0);
        lastGenPlatformCoord = newPlatformPos;

        _platforms.Add(new Platform(newPlatformPos));

        flowerType type = Flower.getRandomType();
        if(type != flowerType.NONE)
        {
            Flower flower = new Flower(type);
            flower.position = newPlatformPos - new Vector2(0.0f, flower.getBounds().Height);
            _flowers.Add(flower);
        }
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
        Rectangle goatBounds = _goat.getBounds();

        List<float> bottoms = new List<float>();
        bottoms.Add(Core.GraphicsDevice.PresentationParameters.BackBufferHeight);

        /* Platform Goat Collision */
        foreach(Platform platform in _platforms)
        {
            Rectangle platformBounds = platform.getBounds();
            if(goatBounds.Bottom <= platformBounds.Top &&
                checkSetIntersection(goatBounds.Left, goatBounds.Right, platformBounds.Left, platformBounds.Right))
            {
                bottoms.Add(platformBounds.Top);
                if(!platform.jumped)
                {
                    platform.jumped = true;
                    _score++;
                    _ui.UpdateScoreText(_score);
                }
            }
        }

        _goat.bottomLimit = bottoms.Min();

        /* Goat-Flowers Collision */
        foreach(Flower flower in _flowers)
        {
            Rectangle flowerBounds = flower.getBounds();
            if((flower.toRemove == false) && (goatBounds.Intersects(flowerBounds)))
            {
                Flower.PlayCollectibleSound();
                _score += 5;
                _ui.UpdateScoreText(_score);
                flower.toRemove = true;
                _numFlowersPicked++;
                _ui.UpdateFlowerText(_numFlowersPicked);
            }
        }
    
        /* Game over condition */
        if(goatBounds.Bottom >= Core.GraphicsDevice.PresentationParameters.BackBufferHeight)
        {
            Goat.playGoatSoundEffect();
            Core.ChangeScene(new GameOver(_score));
        }

    }

    private bool checkSetIntersection(float l1, float r1, float l2, float r2)
    {
        if(l1 >= l2 && r1 <= r2)
        {
            return true;
        }
        if(l1 <= l2 && r1 >= l2)
        {
            return true;
        }
        if(l1 <= r2 && r2 <= r1)
        {
            return true;
        }
        return false;
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.White);

        if (_state != GameState.Playing)
        {
            // We are in a game over state, so apply the saturation parameter.
            _grayscaleEffect.SetParameter("Saturation", _saturation);

            // And begin the sprite batch using the grayscale effect.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp, effect: _grayscaleEffect.Effect);
        }
        else
        {
            // Begin the sprite batch to prepare for rendering.
            Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }
        Core.SpriteBatch.Draw(_levelBackground, Core.GraphicsDevice.PresentationParameters.Bounds, Color.White);

        foreach(Platform platform in _platforms)
        {
            platform.Draw();
        }

        foreach(Flower flower in _flowers)
        {
            flower.Draw();
        }

        _goat.Draw(Core.SpriteBatch);

        // Always end the sprite batch when finished.
        Core.SpriteBatch.End();

        // Draw the UI.
        _ui.Draw();

        base.Draw(gameTime);
    }

}
