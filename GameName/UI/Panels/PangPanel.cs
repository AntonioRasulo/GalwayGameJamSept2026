using Gum.Forms.Controls;
using MonoGameLibrary;
using MonoGameLibrary.Graphics;

namespace GameName.UI;

public abstract class PangPanel
{
    protected Panel _panel;

    protected static TextureAtlas _GUIatlas;

    protected VolumeButton _volumeButton;

    public PangPanel()
    {
        _panel = new Panel();
        _volumeButton = new VolumeButton();
    }

    public static void LoadContent()
    {
        // Load the texture atlas from the xml configuration file.
        _GUIatlas = TextureAtlas.FromFile(Core.Content, "images/UI/GUI_atlas.xml");
    }

    public bool IsVisible()
    {
        return _panel.IsVisible;
    }

    public void SetIsVisible(bool isVisible)
    {
        if(isVisible == true)
        {
            _volumeButton.UpdateSprite();
        }
        _panel.IsVisible = isVisible;
    }

}