using System;
using MonoGameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace GameName.GameObjects;

// Enum to track character state
public enum GoatState
{
    Idle,
    Walking,
    Jumping
}

public enum WalkingDir
{
    IDLE,
    LEFT,
    RIGHT
}

public class Goat
{
    private GoatState currentState = GoatState.Idle;
    private KeyboardState previousKeyboardState;
    private GamePadState previousGamePadState;

    // Tracks the position of the character.
    public static Vector2 _goatPosition { get; set; }

    private Vector2 _velocity;

    private Sprite _idleSprite;
    private AnimatedSprite _walkAnimation;
    private AnimatedSprite _jumpAnimation;

    private WalkingDir _goatDir;

    private readonly Vector2 SCALE = new Vector2(2.0f, 2.0f);

    private const float THUMBSTICK_DEADZONE = 0.2f;

    private const float SPEED = 10.0f;

    private const float GRAVITY = 5f;
    public float jumpStrength = 50f;

    private int _lives = 50;

    public float bottomLimit;

    private static List<SoundEffect> _goatSoundEffects;

    public Goat()
    {
        LoadContent();

        float windowHeight = Core.GraphicsDevice.PresentationParameters.BackBufferHeight;

        _goatPosition = new Vector2(
            _idleSprite.Width,
            windowHeight * 0.25f - _idleSprite.Height * 0.5f);

        _goatDir = WalkingDir.RIGHT;

        _velocity = new Vector2(0.0f, 0.0f);

        bottomLimit = Core.GraphicsDevice.PresentationParameters.BackBufferHeight;

        previousKeyboardState = Keyboard.GetState();
        previousGamePadState = GamePad.GetState(PlayerIndex.One);
    }

    private void LoadContent()
    {
        TextureAtlas goatWalkAtlas = TextureAtlas.FromFile(Core.Content, "images/Goat/goat_walking_atlas.xml");
        TextureAtlas goatJumpAtlas = TextureAtlas.FromFile(Core.Content, "images/Goat/goat_jumping_atlas.xml");

        _idleSprite = goatWalkAtlas.CreateSprite("walk1");
        _idleSprite.Scale = SCALE;
        _idleSprite.CenterOrigin();
        _idleSprite.Effects = SpriteEffects.FlipHorizontally;

        _walkAnimation = goatWalkAtlas.CreateAnimatedSprite("walk-animation");
        _walkAnimation.Scale = SCALE;
        _walkAnimation.CenterOrigin();

        _jumpAnimation = goatJumpAtlas.CreateAnimatedSprite("jump-animation");
        _jumpAnimation.Scale = SCALE;
        _jumpAnimation.CenterOrigin();
        _jumpAnimation.Effects = SpriteEffects.FlipHorizontally;

        TimeSpan speed = TimeSpan.Zero;

        speed = new TimeSpan(0, 0, 0, 0, 150);

        _walkAnimation.SetDelay(speed);

    }

    public static void LoadSoundEffects()
    {
        _goatSoundEffects =
        [
            Core.Content.Load<SoundEffect>("audio/Sound effects/GoatSound/goat1"),
            Core.Content.Load<SoundEffect>("audio/Sound effects/GoatSound/goat2"),
            Core.Content.Load<SoundEffect>("audio/Sound effects/GoatSound/goat3"),
            Core.Content.Load<SoundEffect>("audio/Sound effects/GoatSound/goat4"),
            Core.Content.Load<SoundEffect>("audio/Sound effects/GoatSound/goat5"),
        ];
    }

    public void Update(GameTime gameTime)
    {
        KeyboardState currentKeyboardState = Keyboard.GetState();
        GamePadState currentGamepadState = GamePad.GetState(PlayerIndex.One);

        Vector2 newPosition = _goatPosition;

        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _velocity.Y += GRAVITY;

        switch (currentState)
        {
            case GoatState.Jumping:
                _jumpAnimation.Update(gameTime);

                switch(_goatDir)
                {
                    case WalkingDir.LEFT:
                        _jumpAnimation.Effects = SpriteEffects.None;
                        _idleSprite.Effects = SpriteEffects.None;
                    break;

                    case WalkingDir.IDLE:
                    case WalkingDir.RIGHT:
                        _jumpAnimation.Effects = SpriteEffects.FlipHorizontally;
                        _idleSprite.Effects = SpriteEffects.FlipHorizontally;
                    break;
                }

                switch(IsCharacterWalking(currentKeyboardState, currentGamepadState))
                {
                    case WalkingDir.LEFT:
                        _goatDir = WalkingDir.LEFT;
                        _velocity.X = -SPEED;
                        break;
                    case WalkingDir.RIGHT:
                        _goatDir = WalkingDir.RIGHT;
                        _velocity.X = SPEED;
                        break;
                    case WalkingDir.IDLE:
                    break;
                }

            break;
            case GoatState.Idle:
                _velocity.X = 0;
                if (IsJumpingPressed(currentKeyboardState, currentGamepadState))
                {
                    playGoatSoundEffect();
                    currentState = GoatState.Jumping;
                    _velocity.Y = - jumpStrength;
                }
                else
                {
                    currentState = GetWalkingState(currentKeyboardState, currentGamepadState);
                }
                // In case there's an animation for Idle, call update for the nimation here
            break;

            case GoatState.Walking:

                _walkAnimation.Update(gameTime);

                switch(IsCharacterWalking(currentKeyboardState, currentGamepadState))
                {
                    case WalkingDir.LEFT:
                        _goatDir = WalkingDir.LEFT;
                        currentState = GoatState.Walking;
                        _velocity.X = -SPEED;
                        _walkAnimation.Effects = SpriteEffects.None;
                        _idleSprite.Effects = SpriteEffects.None;
                        break;
                    case WalkingDir.RIGHT:
                        _goatDir = WalkingDir.RIGHT;
                        currentState = GoatState.Walking;
                        _velocity.X = SPEED;
                        _walkAnimation.Effects = SpriteEffects.FlipHorizontally;
                        _idleSprite.Effects = SpriteEffects.FlipHorizontally;
                        break;
                    case WalkingDir.IDLE:
                        currentState = GoatState.Idle;
                        break;
                }
                if (IsJumpingPressed(currentKeyboardState, currentGamepadState))
                {
                    playGoatSoundEffect();
                    currentState = GoatState.Jumping;
                    _velocity.Y = - jumpStrength;
                }

            break;
        }
        previousKeyboardState = currentKeyboardState;
        previousGamePadState = currentGamepadState;

        // Create a bounding rectangle for the screen.
        Rectangle screenBounds = new Rectangle(
            0,
            0,
            Core.GraphicsDevice.PresentationParameters.BackBufferWidth,
            Core.GraphicsDevice.PresentationParameters.BackBufferHeight
        );

        Vector2 newCharPosition = _goatPosition + _velocity;
        Rectangle characterBounds = getBounds(newCharPosition);

        // Use distance based checks to determine if the character is within the
        // bounds of the game screen, and if it is outside that screen edge,
        // move it back inside.
        if (characterBounds.Right < screenBounds.Left)
        {
            newCharPosition.X = screenBounds.Right - getWidth() * 0.5f;
            newCharPosition.Y -= jumpStrength;
        }
        else if (characterBounds.Left > screenBounds.Right)
        {
            newCharPosition.X = screenBounds.Left + getWidth() * 0.5f;
            newCharPosition.Y -= jumpStrength;
        }

        if (characterBounds.Bottom > bottomLimit)
        {
            _velocity.Y = 0.0f;
            newCharPosition.Y = bottomLimit  - _idleSprite.Height * 0.5f;
            if(currentState == GoatState.Jumping)
            {
                currentState = GetWalkingState(currentKeyboardState, currentGamepadState);
            }
        }
        _goatPosition = newCharPosition;
    }

    public void Draw(SpriteBatch spriteBatch)
    {

        Sprite currentAnimation = currentState switch
        {
            GoatState.Jumping => _jumpAnimation,
            GoatState.Walking => _walkAnimation,
            GoatState.Idle => _idleSprite,
            _ => null
        };

        if (currentAnimation != null)
        {
            Vector2 drawingPosition = _goatPosition;
            if(currentAnimation == _jumpAnimation)
            {
                drawingPosition += new Vector2(0, 5);
            }

            currentAnimation.Draw(spriteBatch, drawingPosition);
        }

    }

    public Rectangle getBounds()
    {
        // Creating a bounding rectangle for the character
        Rectangle characterBounds = new Rectangle(
            (int)(_goatPosition.X - _idleSprite.Width*0.5f),
            (int)(_goatPosition.Y - _idleSprite.Height*0.5f),
            (int)_idleSprite.Width,
            (int)_idleSprite.Height
        );

        return characterBounds;
    }

    public Rectangle getBounds(Vector2 position)
    {
        // Creating a bounding rectangle for the character
        Rectangle Bounds = new Rectangle(
            (int)(position.X - _idleSprite.Width*0.5f),
            (int)(position.Y - _idleSprite.Height*0.5f),
            (int)_idleSprite.Width,
            (int)_idleSprite.Height
        );

        return Bounds;
    }

    public float getWidth()
    {
        return _idleSprite.Width;
    }

    public float getHeight()
    {
        return _idleSprite.Height;
    }

    public void increaseLives()
    {
        _lives++;
    }

    public int getLives()
    {
        return _lives;
    }

    private WalkingDir IsCharacterWalking(KeyboardState currentKeyboardState, GamePadState currentGamepadState)
    {
        bool IsWalkingLeft = currentKeyboardState.IsKeyDown(Keys.A) ||
                             currentKeyboardState.IsKeyDown(Keys.Left) ||
                             currentGamepadState.DPad.Left == ButtonState.Pressed ||
                             currentGamepadState.ThumbSticks.Left.X < -THUMBSTICK_DEADZONE;

        if(IsWalkingLeft)
            return WalkingDir.LEFT;

        bool IsWalkingRight = currentKeyboardState.IsKeyDown(Keys.D) ||
                              currentKeyboardState.IsKeyDown(Keys.Right) ||
                              currentGamepadState.DPad.Right == ButtonState.Pressed ||
                              currentGamepadState.ThumbSticks.Left.X > THUMBSTICK_DEADZONE;

        if(IsWalkingRight)
            return WalkingDir.RIGHT;

        return WalkingDir.IDLE;
    }

    private GoatState GetWalkingState(KeyboardState currentKeyboardState, GamePadState currentGamepadState)
    {
        switch (IsCharacterWalking(currentKeyboardState, currentGamepadState))
        {
            case WalkingDir.LEFT:
            case WalkingDir.RIGHT:
                return GoatState.Walking;
            case WalkingDir.IDLE:
                break;
        }
        return GoatState.Idle;
    }

    private bool IsJumpingPressed(KeyboardState currentKeyboardState, GamePadState currentGamePadstate)
    {
        bool isButtonPressedKeyBoard = currentKeyboardState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space);
        bool isButtonPressedGamePad = currentGamePadstate.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A);
        bool isButtonPressed = isButtonPressedKeyBoard || isButtonPressedGamePad;

        return isButtonPressed &&
            currentState != GoatState.Jumping;
    }

    public void GoOnPlatform(float platfY)
    {
        float newY = platfY - _idleSprite.Height * 0.5f;
        Vector2 currentPos = _goatPosition;
        currentPos.Y = newY;
        _goatPosition = currentPos;
        _velocity.Y = 0f;
        currentState = GoatState.Idle;
    }

    public bool isJumping()
    {
        if(currentState == GoatState.Jumping)
            return true;
        return false;
    }

    static public void playGoatSoundEffect()
    {
        var random = new Random();
        int index = random.Next(_goatSoundEffects.Count);
        Core.Audio.PlaySoundEffect(_goatSoundEffects[index]);
    }

}