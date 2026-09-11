using MonoGameLibrary;
using MonoGameLibrary.Graphics;
//using GameName.Config;
using System.Collections.Generic;
using MonoGameGum.GueDeriving;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameName.UI; 

public static class PowerUpSpritesHandler
{
    //private static Dictionary<ShopItems, TextureRegion> _textureRegions;
    private static TextureRegion _Xicon;
    private static TextureRegion _Vicon;

    public static void LoadContent()
    {
        //_textureRegions = new Dictionary<ShopItems, TextureRegion>();

        TextureAtlas book2Atlas = TextureAtlas.FromFile(Core.Content, "images/UI/Book2_atlas.xml");

        _Xicon = book2Atlas.GetRegion("x-icon");
        _Vicon = book2Atlas.GetRegion("v-icon");

        Texture2D harpoonTexture = Core.Content.Load<Texture2D>("images/PowerUps/Harpoon");
        TextureRegion _harpoonRegion = new TextureRegion(harpoonTexture, 0, 0, harpoonTexture.Width, harpoonTexture.Height);

        //_textureRegions.Add(ShopItems.HARPOON, _harpoonRegion);

        Texture2D speed2DTexture = Core.Content.Load<Texture2D>("images/UI/Bolt");
        TextureRegion speedRegion = new TextureRegion(speed2DTexture, 0, 0, speed2DTexture.Width, speed2DTexture.Height);

        //_textureRegions.Add(ShopItems.SPEED, speedRegion);

        Texture2D texture2DLives = Core.Content.Load<Texture2D>("images/PowerUps/lives");
        TextureRegion livesRegion = new TextureRegion(texture2DLives, 0, 0, texture2DLives.Width, texture2DLives.Height);

        // _textureRegions.Add(ShopItems.LIVES, livesRegion);
        // _textureRegions.Add(ShopItems.COLL_LIVES, livesRegion);

        // Texture2D texture2DShield = Core.Content.Load<Texture2D>("images/PowerUps/shield");
        // TextureRegion invincibilityRegion = new TextureRegion(texture2DShield, 0, 0, texture2DShield.Width, texture2DShield.Height);
        // _textureRegions.Add(ShopItems.INVINCIBILITY, invincibilityRegion);

        // Texture2D bombTexture2D = Core.Content.Load<Texture2D>("images/PowerUps/dynamite-pack");
        // TextureRegion bombTexture = new TextureRegion(bombTexture2D, 5, 2, 21, 27);
        // _textureRegions.Add(ShopItems.BOMB, bombTexture);

        // Texture2D texture2DClock = Core.Content.Load<Texture2D>("images/PowerUps/Clock");
        // TextureRegion clockRegion = new TextureRegion(texture2DClock, 0, 0, texture2DClock.Width, texture2DClock.Height);
        // _textureRegions.Add(ShopItems.CLOCK, clockRegion);
    }

    // public static TextureRegion GetTextureRegion(ShopItems shopItem)
    // {
    //     return _textureRegions[shopItem];
    // }

    // public static (SpriteRuntime, PowerUpButtonState) GetSpriteRuntime(ShopItems shopItem, PlayerStats pStats = null, float scaleWidth = 1.0f, float scaleHeight = 1.0f)
    // {
    //     PowerUpButtonState iconState;
    //     SpriteRuntime spriteReturn = new SpriteRuntime
    //     {
    //         Texture = _textureRegions[shopItem].Texture,
    //         SourceRectangle = _textureRegions[shopItem].SourceRectangle,
    //         WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
    //         HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
    //         Width = _textureRegions[shopItem].SourceRectangle.Width * scaleWidth,
    //         Height = _textureRegions[shopItem].SourceRectangle.Height * scaleHeight,
    //         TextureAddress = Gum.Managers.TextureAddress.Custom,
    //     };
    //     (spriteReturn.Color, iconState )= PlayerStatsManager.GetPowerUpStatus(shopItem, pStats);
    //     return (spriteReturn, iconState);
    // }

    public static SpriteRuntime GetXSprite()
    {
        Texture2D texture = _Xicon.Texture;
        Rectangle sourceRectangle = _Xicon.SourceRectangle;

        // Create sprite
        return new SpriteRuntime
        {
            Texture = texture,
            SourceRectangle = sourceRectangle,
            WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            Width = sourceRectangle.Width,
            Height = sourceRectangle.Height,
            TextureAddress = Gum.Managers.TextureAddress.Custom
        };
    }

    public static SpriteRuntime GetVSprite()
    {
        Texture2D texture = _Vicon.Texture;
        Rectangle sourceRectangle = _Vicon.SourceRectangle;

        // Create sprite
        return new SpriteRuntime
        {
            Texture = texture,
            SourceRectangle = sourceRectangle,
            WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            Width = sourceRectangle.Width,
            Height = sourceRectangle.Height,
            TextureAddress = Gum.Managers.TextureAddress.Custom
        };
    }
}