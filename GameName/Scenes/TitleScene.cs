using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Content;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using Microsoft.Xna.Framework.Media;
using MonoGameGum;
using GameName.UI;
using GameName.Backgrounds;
//using GameName.Config;
using System.Collections.Generic;

namespace GameName.Scenes;

public class TitleScene : Scene
{
    // The font to use to render normal text.
    private SpriteFont _font;

    private Background _levelBackground;

    private Random _backgroundRand;

    private static bool _volumeInitialized = false;

    // The 3d material  
    private Material _3dMaterial;

    public override void Initialize()
    {
        // LoadContent is called during base.Initialize().
        base.Initialize();

        // While on the title screen, we can enable exit on escape so the player
        // can close the game by pressing the escape key.
        Core.ExitOnEscape = true;

        if(_volumeInitialized == false)
        {
            Core.Audio.SongVolume = 0.5f;
            Core.Audio.SoundEffectVolume = 0.5f;
            _volumeInitialized = true;
        }

        InitializeUI();
    }

    public override void LoadContent()
    {
        // Load the font for the standard text.
        _font = Core.Content.Load<SpriteFont>("fonts/04B_30");

        try
        {
            // Load the background theme music
            Song theme = Content.Load<Song>("audio/Music/14. Traveling the Sky");
            Core.Audio.PlaySong(theme);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load theme music: {ex.Message}");
        }

        _backgroundRand = new Random();
        //int backgroundIndex = _backgroundRand.Next(0, LevelRegistry.AllLevels.Count);

        //List<string> backgroundList = LevelRegistry.AllLevels[backgroundIndex].backgroundStr;
        List<Texture2D> clouds = new List<Texture2D>();

        // foreach(string backgroundStr in backgroundList)
        // {
        //     clouds.Add(Content.Load<Texture2D>(backgroundStr));
        // }

        _levelBackground = new Background(clouds);

        //PlayerStatsManager.LoadContent();

        PowerUpSpritesHandler.LoadContent();

        // Load the 3d effect 
        _3dMaterial = Core.SharedContent.WatchMaterial("effects/3dEffect");
        _3dMaterial.IsDebugVisible = false;

        var camera = new SpriteCamera3d();
        _3dMaterial.SetParameter("MatrixTransform", camera.CalculateMatrixTransform());
        _3dMaterial.SetParameter("ScreenSize", new Vector2(Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height));
    }

    public override void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);

        _levelBackground.Update(gameTime);

        _3dMaterial.Update();

        double totalSeconds = gameTime.TotalGameTime.TotalSeconds;
        float spinAmount = (float)Math.Sin(totalSeconds * 0.8) * 0.1f;
        _3dMaterial.SetParameter("SpinAmount", spinAmount);

        TitlePanelManager.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        // Draw the background
        _levelBackground.Draw();

        // Begin the sprite batch to prepare for rendering.
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp,
                                rasterizerState: RasterizerState.CullNone,
                                effect: _3dMaterial.Effect);

        TitlePanelManager.Draw();

        // Always end the sprite batch when finished.
        Core.SpriteBatch.End();

        GumService.Default.Draw();
    }

    private void InitializeUI()
    {
        // Clear out any previous UI in case we came here from
        // a different screen:
        GumService.Default.Root.Children.Clear();

        // Create the directory if it doesn't exist yet
        //Directory.CreateDirectory(PlayerStatsManager.saveDirectory);

        TitlePanelManager.LoadContent();
    }

}