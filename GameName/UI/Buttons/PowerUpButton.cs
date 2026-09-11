using Gum.Forms.DefaultVisuals.V3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameGum.GueDeriving;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;
//using GameName.Config;
using System;

namespace GameName.UI;

public enum PowerUpButtonState
{
    Level0,
    Level1,
    Level2,
    Level3,
    NULL
}

// public class PowerUpButton : AnimatedButton
// {
//     private SpriteRuntime _sprite;

//     private PowerUpButtonState _state;

//     //private ShopItems _item;

//     private const int WIDTH = 30;
//     private const int HEIGHT = 20;

//     private SpriteRuntime _Xsprite;
//     private SpriteRuntime _Vsprite;

//     // public PowerUpButton(TextureAtlas atlas, ShopItems item) : base(atlas)
//     // {
//     //     TextureRegion textureRegion = PowerUpSpritesHandler.GetTextureRegion(item);
//     //     CreateSprite(textureRegion.Texture, textureRegion.SourceRectangle);
//     //     SetSpriteColor();
//     //     Width = WIDTH;
//     //     Height = HEIGHT;
//     //     _item = item;
//     // }

//     private void CreateSprite(Texture2D texture, Rectangle sourceRectangle)
//     {
//         // Access the visual
//         ButtonVisual visual = (ButtonVisual)this.Visual;

//         // Create sprite
//         _sprite = new SpriteRuntime
//         {
//             Texture = texture,
//             SourceRectangle = sourceRectangle,
//             WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute,
//             HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute,
//             Width = sourceRectangle.Width,
//             Height = sourceRectangle.Height,
//             TextureAddress = Gum.Managers.TextureAddress.Custom
//         };

//         this.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
//         this.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;

//         this.Width = sourceRectangle.Width;
//         this.Height = sourceRectangle.Height;

//         TextRuntime textInstance = visual.TextInstance;
//         textInstance.Text = "";

//         _sprite.Anchor(Gum.Wireframe.Anchor.Center);
//         visual.Background.AddChild(_sprite);

//         // Add event handler for mouse hover focus.
//         GotFocus += ChangeDescriptionText;
//     }

//     public void SetScale(float scale)
//     {
//         var rect = _sprite.SourceRectangle;

//         // Scale sprite (visual)
//         _sprite.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
//         _sprite.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;
//         _sprite.Width = rect.Width * scale;
//         _sprite.Height = rect.Height * scale;

//     }

//     public void SetScale(float scaleWidth, float scaleHeight)
//     {
//         var rect = _sprite.SourceRectangle;

//         // Scale sprite (visual)
//         _sprite.WidthUnits = Gum.DataTypes.DimensionUnitType.Absolute;
//         _sprite.HeightUnits = Gum.DataTypes.DimensionUnitType.Absolute;
//         _sprite.Width = rect.Width * scaleWidth;
//         _sprite.Height = rect.Height * scaleHeight;
//     }

//     public void LevelUp()
//     {
//         switch (_state)
//         {
//             case PowerUpButtonState.Level0:
//                 _state++;
//                 ButtonVisual visual = (ButtonVisual)this.Visual;
//                 visual.Background.RemoveChild(_Xsprite);
//                 break;
//             case PowerUpButtonState.Level1:
//                 _state++;
//                 break;
//             case PowerUpButtonState.Level2:
//                 _state++;
//                 break;
//             case PowerUpButtonState.Level3:
//             default:
//                 break;
//         }
//         //PlayerStatsManager.SetPlayerStats(_state, _item);
//         SetSpriteColor();
//     }

//     public void SetState(PowerUpButtonState state)
//     {
//         _state = state;
//         SetSpriteColor();
//     }

//     private void SetSpriteColor()
//     {
//         //(_sprite.Color, _state) = PlayerStatsManager.GetPowerUpStatus(_item);
//         switch (_state)
//         {
//             case PowerUpButtonState.Level0:
//                 CreateXSprite();
//             break;
//             case PowerUpButtonState.Level3:
//                 CreateVSprite();
//             break;
//             default:
//             break;
//         }
//     }

//     public int GetPowerUpPrize()
//     {
//         //return ShopItemsConfig.prizes[_item][_state];
//         return 0;
//     }

//     public PowerUpButtonState GetState()
//     {
//         return _state;
//     }

//     // public ShopItems GetItem()
//     // {
//     //     return _item;
//     // }

//     private void CreateXSprite()
//     {
//         ButtonVisual visual = (ButtonVisual)this.Visual;

//         if(visual.Background.Children.Contains(_Xsprite))
//             return;

//         // Create sprite
//         _Xsprite = PowerUpSpritesHandler.GetXSprite();

//         _Xsprite.Anchor(Gum.Wireframe.Anchor.Center);
//         visual.Background.AddChild(_Xsprite);
//     }

//     private void CreateVSprite()
//     {
//         ButtonVisual visual = (ButtonVisual)this.Visual;

//         if(visual.Background.Children.Contains(_Vsprite))
//             return;

//         // Create sprite
//         _Vsprite = PowerUpSpritesHandler.GetVSprite();

//         _Vsprite.Anchor(Gum.Wireframe.Anchor.Center);
//         visual.Background.AddChild(_Vsprite);
//     }

//     private void ChangeDescriptionText(object sender, EventArgs e)
//     {
//         //DescriptionPowerPanel.SetText(_item);
//     }

//     public void SetSpriteYCor(float yCor)
//     {
//         _sprite.Y = yCor;
//     }

// }