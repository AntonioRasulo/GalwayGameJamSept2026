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
        AddContributorText("HydroGene", 25.0f);
        AddButton("https://hydrogene.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 26.0f, 43.0f);

        /* Balloon sounds */
        AddDescriptionText("Game sound effects: ", 35.0f, -82.0f);
        AddContributorText("JDWasabi", 35.0f, 25.0f);
        AddButton("https://jdwasabi.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 36.0f, 62.0f);

        /* Background */
        AddDescriptionText("Background: ", 45.0f, -114.0f);
        AddContributorText("Craftpix.net", 45.0f);
        AddButton("https://free-game-assets.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 46.0f, 50.0f);

        /* Coins and chests */
        AddDescriptionText("Treasures sprites: ", 55.0f, -88.0f);
        AddContributorText("greatdocbrown", 55.0f, 30.0f);
        AddButton("https://greatdocbrown.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 56.0f, 88.0f);

        /* Bat sprites */
        AddDescriptionText("Bat enemies sprites: ", 65.0f, -82f);
        AddContributorText("Segnah", 65.0f, 12.0f);
        AddButton("https://segnah.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 66.0f, 41.0f);

        /* Bat boss */
        AddDescriptionText("Bat boss sprite: ", 75.0f, -96.0f);
        AddContributorText("Mattz Art", 75.0f, 0.0f);
        AddButton("https://xzany.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 76.0f, 41.0f);

        /* Platforms */
        AddDescriptionText("Platform sprites: ", 85.0f, -92.0f);
        AddContributorText("Pixel Frog", 85.0f, 10.0f);
        AddButton("https://pixelfrog-assets.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 86.0f, 55.0f);

        /* UI arrow */
        AddDescriptionText("UI Pixel Art: ", 95.0f, -108.0f);
        AddContributorText("Ibin Games", 95.0f, 0.0f);
        AddButton("https://ibingames.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 96.0f, 41.0f);

        /* UI x and v buttons */
        AddDescriptionText("UI Pixel Art: ", 105.0f, -108.0f);
        AddContributorText("Sr.Toasty", 105.0f, 0.0f);
        AddButton("https://srtoasty.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 106.0f, 41.0f);

        /* UI Controls */
        AddDescriptionText("Controls Pixel Art: ", 115.0f, -84.0f);
        AddContributorText("AdamGDA", 115.0f, 20.0f);
        AddButton("https://adamgamer1111.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 116.0f, 55.0f);

        /* Pixel Art Clock */
        AddDescriptionText("Clock Pixel Art: ", 125.0f, -96.0f);
        AddContributorText("Bont", 125.0f, 0.0f);
        AddButton("https://bontt.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 126.0f, 20.0f);

        /* Pixel Art Dynamite */
        AddDescriptionText("Dynamite Pixel Art: ", 135.0f, -85.0f);
        AddContributorText("tumas81", 135.0f, 11.0f);
        AddButton("https://tumas81.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 136.0f, 43.0f);

        /* Balls Design */
        AddDescriptionText("Sphere Design: ", 145.0f, -104.0f);
        AddContributorText("GameSupplyGuy", 145.0f, 0.0f);
        AddButton("https://gamesupply.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 146.0f, 57.0f);

        /* Pixel Art Shield */
        AddDescriptionText("Shield Pixel Art: ", 155.0f, -94.0f);
        AddContributorText("havran", 155.0f, 11.0f);
        AddButton("https://havran.itch.io/", itchLogo, itchLogo.Bounds, 0.015f, 156.0f, 43.0f);

        /* Character Pixel Art */
        AddDescriptionText("Character Pixel Art: ", 165.0f, -80.0f);
        AddContributorText("Abby Sheehan", 165.0f, 45.0f);
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