using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Scenes;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace GameName.Scenes;

public class GameOver : Scene
{
    private const string GAME_OVER_TEXT = "GAME OVER";

    private string SCORE_TEXT = "Score: ";

    private const string PRESS_ENTER_TEXT = "Press Confirm To Continue";

    // The font to use to render normal text.
    private SpriteFont _font;

    // The font used to render the title text.
    private SpriteFont _font5x;

    private Vector2 _gameOverTextPosition;

    private Vector2 _gameOverTextOrigin;

    private Vector2 _scoreTextPosition;

    private Vector2 _scoreTextOrigin;

    private Vector2 _pressEnterPosition;

    private Vector2 _pressEnterOrigin;

    private Texture2D _levelBackground;

    private Random _backgroundRand;

    public GameOver(int score)
    {
        SCORE_TEXT += score.ToString();
        //TitlePanelManager.AddScore(PlayerStatsManager.currentStats.Name, score.ToString());
    }

    public override void Initialize()
    {
        // LoadContent is called during base.Initialize().
        base.Initialize();

        // While on the game over screen, we can enable exit on escape so the player
        // can close the game by pressing the escape key.
        Core.ExitOnEscape = true;

        // Set the position and origin for the game over text.
        Vector2 size = _font5x.MeasureString(GAME_OVER_TEXT);
        _gameOverTextPosition = new Vector2(640, 220);
        _gameOverTextOrigin = size * 0.5f;

        // Set the position and origin for the score text.
        size = _font5x.MeasureString(SCORE_TEXT);
        _scoreTextPosition = new Vector2(640, 420);
        _scoreTextOrigin = size * 0.5f;

        // Set the position and origin for the press enter text.
        size = _font.MeasureString(PRESS_ENTER_TEXT);
        _pressEnterPosition = new Vector2(640, 630);
        _pressEnterOrigin = size * 0.5f;

    }

    public override void LoadContent()
    {
        // Load the background theme music
        try
        {
            Song theme = Content.Load<Song>("audio/Music/5-Spring2-anewday-zwinzlergames");
            Core.Audio.PlaySong(theme);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load theme music: {ex.Message}");
        }

        // Load the font for the standard text.
        _font = Core.Content.Load<SpriteFont>("fonts/mountain_and_nature/Mountain_and_Nature_small");

        // Load the font for the title text.
        _font5x = Content.Load<SpriteFont>("fonts/mountain_and_nature/Mountain_and_Nature");

        _backgroundRand = new Random();
        //int backgroundIndex = _backgroundRand.Next(0, LevelRegistry.AllLevels.Count);

        //List<string> backgroundList = LevelRegistry.AllLevels[backgroundIndex].backgroundStr;
        List<Texture2D> clouds = new List<Texture2D>();

        _levelBackground = Core.Content.Load<Texture2D>("images/backgrounds/fields/origbig");

    }

    public override void Update(GameTime gameTime)
    {
        // If the user presses enter, switch to the game scene.
        if (Core.Input.Keyboard.WasKeyJustReleased(Keys.Space) || Core.Input.Keyboard.WasKeyJustReleased(Keys.Enter) ||
            Core.Input.GamePads[(int)PlayerIndex.One].WasButtonJustReleased(Buttons.A))
        {
            Core.ChangeScene(new TitleScene());
        }
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        // Begin the sprite batch to prepare for rendering.
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Core.SpriteBatch.Draw(_levelBackground, Core.GraphicsDevice.PresentationParameters.Bounds, Color.White);

        // The color to use for the drop shadow text.
        Color dropShadowColor = Color.Black * 0.5f;

        // Draw the Dungeon text slightly offset from it is original position and
        // with a transparent color to give it a drop shadow.
        Core.SpriteBatch.DrawString(_font5x, GAME_OVER_TEXT, _gameOverTextPosition + new Vector2(10, 10), dropShadowColor, 0.0f, _gameOverTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        // Draw the Dungeon text on top of that at its original position.
        Core.SpriteBatch.DrawString(_font5x, GAME_OVER_TEXT, _gameOverTextPosition, Color.White, 0.0f, _gameOverTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        // Draw the Slime text slightly offset from it is original position and
        // with a transparent color to give it a drop shadow.
        Core.SpriteBatch.DrawString(_font5x, SCORE_TEXT, _scoreTextPosition + new Vector2(10, 10), dropShadowColor, 0.0f, _scoreTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        // Draw the Slime text on top of that at its original position.
        Core.SpriteBatch.DrawString(_font5x, SCORE_TEXT, _scoreTextPosition, Color.White, 0.0f, _scoreTextOrigin, 1.0f, SpriteEffects.None, 1.0f);

        // Draw the press enter text.
        Core.SpriteBatch.DrawString(_font, PRESS_ENTER_TEXT, _pressEnterPosition, Color.White, 0.0f, _pressEnterOrigin, 1.0f, SpriteEffects.None, 0.0f);

        // Always end the sprite batch when finished.
        Core.SpriteBatch.End();

    }

}