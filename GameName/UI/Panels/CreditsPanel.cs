using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameGum;

namespace GameName.UI;

public class CreditsPanel : PangPanel
{
    AnimatedButton _backButton;

    public CreditsPanel()
    {
        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        _backButton = new AnimatedButton(_GUIatlas);
        _backButton.Text = "BACK";
        _backButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _backButton.X = -28f;
        _backButton.Y = -20f;
        _backButton.Click += TitlePanelManager.HandleOptionsButtonBack;
        _panel.AddChild(_backButton);

        Texture2D itchLogo = Core.Content.Load<Texture2D>("images/general/itch_logo");
        Texture2D linkedinLogo = Core.Content.Load<Texture2D>("images/general/linkedin_logo");
        Texture2D wwwLogo = Core.Content.Load<Texture2D>("images/general/www");

        /* Development */
        AddDescriptionText("Development: ", 5.0f, -110.0f);
        AddContributorText("MischievousCats", 5.0f);
        AddButton("https://mischievouscats.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 6.0f, 63.0f);
        AddButton("https://www.linkedin.com/in/antonio-rasulo-698513142/", linkedinLogo, linkedinLogo.Bounds, 0.0045f, 5.0f, 75.0f);
        AddButton("https://antoniorasulo.github.io/my_portfolio/", wwwLogo, wwwLogo.Bounds, 0.015f, 6.0f, 87.0f);

        /* Framework */
        Texture2D _monogameLogo = Core.Content.Load<Texture2D>("images/general/logo");
        Rectangle iconSourceRect = new Rectangle(0, 0, 128, 128);
        AddDescriptionText("Framework: ", 15.0f, -117.0f);
        AddContributorText("MonoGame", 15.0f);
        AddButton("https://monogame.net/", _monogameLogo, iconSourceRect, 0.08f, 15.0f, 40.0f);

        /* Music */
        AddDescriptionText("Music: ", 25.0f, -135.0f);
        AddContributorText("Zwinzler Games", 25.0f);
        AddButton("https://zwinzlergames.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 26.0f, 60.0f);

        /* Balloon sounds */
        // AddDescriptionText("Game sound effects: ", 35.0f, -82.0f);
        // AddContributorText("JDWasabi", 35.0f, 25.0f);
        // AddButton("https://jdwasabi.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 36.0f, 62.0f);

        /* Background */
        AddDescriptionText("Background: ", 35.0f, -114.0f);
        AddContributorText("Craftpix.net", 35.0f);
        AddButton("https://free-game-assets.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 36.0f, 50.0f);

        /* Goat design */
        AddDescriptionText("Goat design: ", 65.0f, -111.0f);
        AddContributorText("Sevarihk", 65.0f, 0.0f);
        AddButton("https://opengameart.org/users/sevarihk", wwwLogo, wwwLogo.Bounds, 0.015f, 66.0f, 41.0f);

        /* Platforms */
        AddDescriptionText("Platform sprites: ", 45.0f, -92.0f);
        AddContributorText("Pixel Frog", 45.0f, 10.0f);
        AddButton("https://pixelfrog-assets.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 46.0f, 55.0f);

        /* Flowers design */
        AddDescriptionText("Flower design: ", 75.0f, -103.0f);
        AddContributorText("JennPixel", 75.0f, 0.0f);
        AddButton("https://jennpixel.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 76.0f, 41.0f);

        /* UI Controls */
        AddDescriptionText("Controls Pixel Art: ", 55.0f, -84.0f);
        AddContributorText("AdamGDA", 55.0f, 20.0f);
        AddButton("https://adamgamer1111.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 56.0f, 55.0f);

    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _backButton.IsFocused = isVisible;
    }

    private void AddDescriptionText(string text, float yCoordinate, float xCoordinate = -100.0f)
    {
        GameText developmentText = new GameText(text);
        developmentText.FontScale = 0.27f;
        developmentText.Red = 0;
        developmentText.Blue = 0;
        developmentText.Green = 0;
        developmentText.Anchor(Gum.Wireframe.Anchor.Top);
        developmentText.X = xCoordinate;
        developmentText.Y = yCoordinate;
        _panel.AddChild(developmentText);
    }

    private void AddContributorText(string text, float yCoordinate, float xCoordinate = 0f)
    {
        GameText contributorText = new GameText(text);
        contributorText.FontScale = 0.27f;
        contributorText.Anchor(Gum.Wireframe.Anchor.Top);
        contributorText.Y = yCoordinate;
        contributorText.X = xCoordinate;
        _panel.AddChild(contributorText);
    }

    private void AddButton(string link, Texture2D logo, Rectangle rectangle, float scale, float yCoordinate, float xCoordinate)
    {
        LinkButton button = new LinkButton(link, logo, rectangle, scale);
        button.Anchor(Gum.Wireframe.Anchor.Top);
        button.Y = yCoordinate;
        button.X = xCoordinate;
        _panel.AddChild(button);
    }
}