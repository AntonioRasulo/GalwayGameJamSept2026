using Gum.Forms.DefaultVisuals.V3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace GameName.UI;

public class LoadButton : AnimatedButton
{
    private TextRuntime _textMoney;

    public bool isNewGame{get;set;}

    private AnimatedSprite _chestAnimation;
    private SpriteRuntime _chestSprite;

    private SpriteRuntime _harpoonSprite;
    private SpriteRuntime _speedSprite;
    private SpriteRuntime _livesSprite;

    private SpriteRuntime _collLivesSpriteIcon;
    private SpriteRuntime _collLivesSprite;

    private SpriteRuntime _invincibilitySpriteIcon;
    private SpriteRuntime _invincibilitySprite;

    private SpriteRuntime _bombSpriteIcon;
    private SpriteRuntime _bombSprite;

    private SpriteRuntime _freezeSpriteIcon;
    private SpriteRuntime _freezeSprite;

    public LoadButton(TextureAtlas atlas) : base(atlas)
    {
        InitializeButton();
    }

    private void InitializeButton()
    {
        // Access the visual
        ButtonVisual visual = (ButtonVisual)this.Visual;

        this.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
        this.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;

        TextRuntime textInstance = visual.TextInstance;
        textInstance.Text = "NewGame";
        textInstance.Anchor(Gum.Wireframe.Anchor.Top);
        textInstance.Y = 10;
        textInstance.Color = Color.Black;
        _textMoney = new GameText("");
        _textMoney.Color = Color.Gold;
        _textMoney.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _textMoney.X = 26.0f;
        _textMoney.Y = -4.0f;

        visual.Children.Insert(1, _textMoney);

        isNewGame = true;

        // Add event handler for mouse hover focus.
        visual.RollOn += HandleRollOn;
    }

    public void UpdateLoadButtonPowerUps()
    {
        ButtonVisual visual = (ButtonVisual)this.Visual;

        // (_harpoonSprite.Color, _) = PlayerStatsManager.GetPowerUpStatus(ShopItems.HARPOON);
        // (_speedSprite.Color, _) = PlayerStatsManager.GetPowerUpStatus(ShopItems.SPEED);
        // (_livesSprite.Color, _) = PlayerStatsManager.GetPowerUpStatus(ShopItems.LIVES);

        // UpdateSpriteIcon(ref _collLivesSprite, ref _collLivesSpriteIcon, ShopItems.COLL_LIVES);
        // UpdateSpriteIcon(ref _invincibilitySprite, ref _invincibilitySpriteIcon, ShopItems.INVINCIBILITY);
        // UpdateSpriteIcon(ref _bombSprite, ref _bombSpriteIcon, ShopItems.BOMB);
        // UpdateSpriteIcon(ref _freezeSprite, ref _freezeSpriteIcon, ShopItems.CLOCK);
    }

    public void setTextMoney(string textMoney)
    {
        _textMoney.Text = textMoney;
    }

    public void Update(GameTime gameTime)
    {
        if(_chestSprite != null)
        {
            _chestAnimation.Update(gameTime);
            TextureRegion currentChestFrame = _chestAnimation.GetCurrentFrame();
            _chestSprite.Texture = currentChestFrame.Texture;
            _chestSprite.SourceRectangle = currentChestFrame.SourceRectangle;
            _chestSprite.Width = currentChestFrame.SourceRectangle.Width;
            _chestSprite.Height = currentChestFrame.SourceRectangle.Height;
        }
    }

    private void CreateChestAnimation()
    {
        TextureAtlas chestAtlas = TextureAtlas.FromFile(Core.Content, "images/Coins/chest_atlas.xml");

        // Get the multi-frame chest animation from the atlas
        _chestAnimation = chestAtlas.CreateAnimatedSprite("chest-animation");
        _chestAnimation.Scale = new Vector2(4.0f, 4.0f);

        TextureRegion ChestFrame = _chestAnimation.GetCurrentFrame();

        CreateChestSprite(ChestFrame.Texture, ChestFrame.SourceRectangle);
    }

    private void CreateChestSprite(Texture2D texture, Rectangle sourceRectangle)
    {
        // Access the visual
        ButtonVisual visual = (ButtonVisual)this.Visual;

        // Create sprite
        _chestSprite = new SpriteRuntime
        {
            Texture = texture,
            SourceRectangle = sourceRectangle,
            WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
            Width = sourceRectangle.Width,
            Height = sourceRectangle.Height,
            TextureAddress = Gum.Managers.TextureAddress.Custom
        };

        _chestSprite.Anchor(Gum.Wireframe.Anchor.BottomLeft);
        _chestSprite.X = 10.0f;
        _chestSprite.Y = -5.0f;

        visual.Background.AddChild(_chestSprite);
    }

    // public void LoadButtonStats(PlayerStats pStats, Gum.Wireframe.Anchor anchor, float deleteX, float deleteY)
    // {
    //     // Access the visual
    //     ButtonVisual visual = (ButtonVisual)this.Visual;

    //     TextRuntime textInstance = visual.TextInstance;
    //     textInstance.Text = pStats.Name;
    //     textInstance.Anchor(Gum.Wireframe.Anchor.TopLeft);
    //     textInstance.X = 10.0f;
    //     textInstance.Y = 5.0f;
    //     setTextMoney(pStats.Money.ToString());
    //     isNewGame = false;

    //     if(_chestSprite == null)
    //     {
    //         CreateChestAnimation();
    //     }

    //     AddPowerUpSprite(ref _harpoonSprite, ShopItems.HARPOON, pStats, 65.0f, 3.5f);
    //     AddPowerUpSprite(ref _speedSprite, ShopItems.SPEED, pStats, 83.0f, 1.0f, 0.5f, 0.5f);
    //     AddPowerUpSprite(ref _livesSprite, ShopItems.LIVES, pStats, 105.0f, 4.5f, 1f, 0.6f);

    //     PowerUpButtonState collState = AddPowerUpSprite(ref _collLivesSprite, ShopItems.COLL_LIVES, pStats, 65.0f, 28.0f, 1f, 0.6f);
    //     _collLivesSpriteIcon = GetIconSprite(collState, 67.0f, 30.0f);
    //     if(_collLivesSpriteIcon != null)
    //     {
    //         visual.Background.AddChild(_collLivesSpriteIcon);
    //     }

    //     collState = AddPowerUpSprite(ref _invincibilitySprite, ShopItems.INVINCIBILITY, pStats, 86.0f, 28.0f, 0.05f, 0.05f);
    //     _invincibilitySpriteIcon = GetIconSprite(collState, 87.0f, 30.0f);
    //     if(_invincibilitySpriteIcon != null)
    //     {
    //         visual.Background.AddChild(_invincibilitySpriteIcon);
    //     }

    //     collState = AddPowerUpSprite(ref _bombSprite, ShopItems.BOMB, pStats, 106.0f, 27.0f, 0.6f, 0.6f);
    //     _bombSpriteIcon = GetIconSprite(collState, 107.0f, 30.0f);
    //     if(_bombSpriteIcon != null)
    //     {
    //         visual.Background.AddChild(_bombSpriteIcon);
    //     }

    //     collState = AddPowerUpSprite(ref _freezeSprite, ShopItems.CLOCK, pStats, 124.0f, 26.0f, 0.017f, 0.017f);
    //     _freezeSpriteIcon = GetIconSprite(collState, 127.0f, 30.0f);
    //     if(_freezeSpriteIcon != null)
    //     {
    //         visual.Background.AddChild(_freezeSpriteIcon);
    //     }
    // }

    public void CleanButton()
    {
        // Access the visual
        ButtonVisual visual = (ButtonVisual)this.Visual;
        TextRuntime textInstance = visual.TextInstance;
        textInstance.Text = "NewGame";
        textInstance.Anchor(Gum.Wireframe.Anchor.Top);
        textInstance.Y = 10;
        setTextMoney("");
        isNewGame = true;

        visual.Background.RemoveChild(_chestSprite);
        _chestSprite = null;

        visual.Background.RemoveChild(_harpoonSprite);
        _harpoonSprite = null;

        visual.Background.RemoveChild(_speedSprite);
        _speedSprite = null;

        visual.Background.RemoveChild(_livesSprite);
        _livesSprite = null;

        CleanSprite(ref _collLivesSprite, ref _collLivesSpriteIcon);
        CleanSprite(ref _invincibilitySprite, ref _invincibilitySpriteIcon);
        CleanSprite(ref _bombSprite, ref _bombSpriteIcon);
        CleanSprite(ref _freezeSprite, ref _freezeSpriteIcon);
    }

    private SpriteRuntime GetIconSprite(PowerUpButtonState state, float xCor, float yCor)
    {
        SpriteRuntime returnSprite = null;
        if(state == PowerUpButtonState.Level0)
        {
            returnSprite = PowerUpSpritesHandler.GetXSprite();
        }
        else if(state == PowerUpButtonState.Level3)
        {
            returnSprite = PowerUpSpritesHandler.GetVSprite();
        }

        if(returnSprite != null)
        {
            returnSprite.Anchor(Gum.Wireframe.Anchor.TopLeft);
            returnSprite.X = xCor;
            returnSprite.Y = yCor;
        }

        return returnSprite;
    }

    // private PowerUpButtonState AddPowerUpSprite(ref SpriteRuntime sprite, ShopItems shopItem, PlayerStats pStats, float xCor, float yCor, float scaleWidth = 1.0f, float scaleHeight = 1.0f)
    // {
    //     // Access the visual
    //     ButtonVisual visual = (ButtonVisual)this.Visual;

    //     PowerUpButtonState collState;
    //     (sprite, collState) = PowerUpSpritesHandler.GetSpriteRuntime(shopItem, pStats, scaleWidth, scaleHeight);
    //     sprite.Anchor(Gum.Wireframe.Anchor.TopLeft);
    //     sprite.X = xCor;
    //     sprite.Y = yCor;
    //     visual.Background.AddChild(sprite);
    //     return collState;
    // }

    // private void UpdateSpriteIcon(ref SpriteRuntime sprite, ref SpriteRuntime spriteIcon, ShopItems shopItem)
    // {
    //     // Access the visual
    //     ButtonVisual visual = (ButtonVisual)this.Visual;

    //     PowerUpButtonState state;
    //     (sprite.Color, state) = PlayerStatsManager.GetPowerUpStatus(shopItem);

    //     if(spriteIcon != null && state > PowerUpButtonState.Level0 && state < PowerUpButtonState.Level3)
    //     {
    //         visual.Background.RemoveChild(spriteIcon);
    //         spriteIcon = null;
    //     }
    //     else if(spriteIcon == null && state == PowerUpButtonState.Level3)
    //     {
    //         float spriteIconX = sprite.X + 2.0f;
    //         float spriteIconY = sprite.Y + 3.0f;
    //         spriteIcon = GetIconSprite(state, spriteIconX, spriteIconY);
    //         if (spriteIcon != null)
    //         {
    //             visual.Background.AddChild(spriteIcon);
    //         }
    //     }
    // }

    private void CleanSprite(ref SpriteRuntime sprite, ref SpriteRuntime spriteIcon)
    {
        // Access the visual
        ButtonVisual visual = (ButtonVisual)this.Visual;

        visual.Background.RemoveChild(sprite);
        sprite = null;

        if(spriteIcon != null)
        {
            visual.Background.RemoveChild(spriteIcon);
            spriteIcon = null;
        }
    }

}