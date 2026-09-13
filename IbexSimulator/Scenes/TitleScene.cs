using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Content;
using MonoGameLibrary.Graphics;
using MonoGameLibrary.Scenes;
using Microsoft.Xna.Framework.Media;
using MonoGameGum;
using IbexGame.UI;
using IbexGame.GameObjects;

namespace IbexGame.Scenes;

public class TitleScene : Scene
{
    // The font to use to render normal text.
    private SpriteFont _font;

    private Texture2D _levelBackground;

    private Texture2D _goatHead;
    private Vector2 _goatHeadPosition;

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
        _font = Core.Content.Load<SpriteFont>("fonts/mountain_and_nature/Mountain_and_Nature_small");

        try
        {
            // Load the background theme music
            Song theme = Content.Load<Song>("audio/Music/3-Fall-evening-zwinzlergames");
            Core.Audio.PlaySong(theme);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load theme music: {ex.Message}");
        }

        _levelBackground = Core.Content.Load<Texture2D>("images/backgrounds/backgroundTitle/origbig");

        _goatHead = Core.Content.Load<Texture2D>("images/Title/dall-schaf-brown-m");
        _goatHeadPosition = new Vector2(
            Core.GraphicsDevice.Viewport.Width * 0.23f + 30.0f,
            Core.GraphicsDevice.Viewport.Height * 0.12f
        );

        // Load the 3d effect 
        _3dMaterial = Core.SharedContent.WatchMaterial("effects/3dEffect");
        _3dMaterial.IsDebugVisible = false;

        var camera = new SpriteCamera3d();
        _3dMaterial.SetParameter("MatrixTransform", camera.CalculateMatrixTransform());
        _3dMaterial.SetParameter("ScreenSize", new Vector2(Core.GraphicsDevice.Viewport.Width, Core.GraphicsDevice.Viewport.Height));

        Goat.LoadSoundEffects();
    }

    public override void Update(GameTime gameTime)
    {
        GumService.Default.Update(gameTime);

        _3dMaterial.Update();

        float spinAmount = -150;
        _3dMaterial.SetParameter("SpinAmount", spinAmount);
    }

    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(new Color(32, 40, 78, 255));

        // Draw the background
        Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        Core.SpriteBatch.Draw(_levelBackground, Core.GraphicsDevice.PresentationParameters.Bounds, Color.White);
        Core.SpriteBatch.End();

        // Begin the sprite batch to prepare for rendering.
         Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp,
                                 rasterizerState: RasterizerState.CullNone,
                                 effect: _3dMaterial.Effect);

        if(TitlePanelManager.IsTitlePanelVisible())
        {
            Core.SpriteBatch.Draw(_goatHead, _goatHeadPosition, Color.White);
        }

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

        TitlePanelManager.LoadContent();
    }

}