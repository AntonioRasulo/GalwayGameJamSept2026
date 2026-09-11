using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using MonoGameGum;

namespace GameName.UI;

public class LeaderboardPanel: PangPanel
{
    private AnimatedButton _homeButton;

    private GameText _playerText;
    private GameText _scoreText;

    private List<(string, string)> _board;
    private List<GameText> _addedText;

    private const int BOARDSIZE = 10;

    public static readonly string SaveDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "GameName",
            "leaderboard"
        );

    private static readonly string SaveFilePath = Path.Combine(SaveDirectory, "leaderboard.json");

    public LeaderboardPanel()
    {
        _board = new List<(string, string)>();
        _addedText = new List<GameText>();

        LoadBoard();

        _panel.Dock(Gum.Wireframe.Dock.Fill);
        _panel.IsVisible = false;
        _panel.AddToRoot();

        _volumeButton.Anchor(Gum.Wireframe.Anchor.TopRight);
        _panel.AddChild(_volumeButton);

        _homeButton = new AnimatedButton(_GUIatlas);
        _homeButton.Text = "HOME";
        _homeButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        _homeButton.X = -28f;
        _homeButton.Y = -10f;
        _homeButton.Click += TitlePanelManager.HandleOptionsButtonBack;
        _panel.AddChild(_homeButton);

        _playerText = new GameText("Player");
        _playerText.X = 75.0f;
        _playerText.Y = 10.0f;
        _playerText.Color = Color.Black;
        _panel.AddChild(_playerText);

        _scoreText = new GameText("Score");
        _scoreText.X = 140.0f;
        _scoreText.Y = 10.0f;
        _scoreText.Color = Color.Black;
        _panel.AddChild(_scoreText);
    }

    public new void SetIsVisible(bool isVisible)
    {
        base.SetIsVisible(isVisible);
        _homeButton.IsFocused = isVisible;

        if(isVisible)
        {
            foreach(GameText text in _addedText)
            {
                _panel.RemoveChild(text);
            }
            _addedText.Clear();

            for(int i = 0; i < _board.Count; i++)
            {
                string name = _board[i].Item1;
                string score = _board[i].Item2;
                GameText nameText = new GameText(name);
                nameText.X = 75.0f;
                nameText.Y = 20.0f + i*10.0f;
                _panel.AddChild(nameText);
                _addedText.Add(nameText);

                GameText scoreText = new GameText(score);
                scoreText.X = 140.0f;
                scoreText.Y = 20.0f + i*10.0f;
                _panel.AddChild(scoreText);
                _addedText.Add(scoreText);
            }
        }
    }

    public void AddScore(string player, string score)
    {
        _board.Add((player, score));
        _board = _board.OrderByDescending(o => int.Parse(o.Item2)).ToList();
        if(_board.Count > BOARDSIZE)
        {
            _board = _board.Take(BOARDSIZE).ToList();
        }
        SaveBoard();
    }

    private void LoadBoard()
    {
        try
        {
            if (!File.Exists(SaveFilePath))
            {
                return;
            }

            string json = File.ReadAllText(SaveFilePath);
            List<LeaderboardEntry> entries = JsonSerializer.Deserialize<List<LeaderboardEntry>>(json);

            if (entries != null)
            {
                _board = entries
                    .Select(e => (e.Player, e.Score))
                    .ToList();
            }
        }
        catch (Exception ex)
        {
            // If the save file is missing, corrupted, or unreadable, fall back to an empty
            // leaderboard rather than crashing the game.
            System.Diagnostics.Debug.WriteLine($"Failed to load leaderboard: {ex.Message}");
            _board = new List<(string, string)>();
        }
    }

    private void SaveBoard()
    {
        try
        {
            Directory.CreateDirectory(SaveDirectory);

            List<LeaderboardEntry> entries = _board
                .Select(b => new LeaderboardEntry { Player = b.Item1, Score = b.Item2 })
                .ToList();

            string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SaveFilePath, json);
        }
        catch (Exception ex)
        {
            // Persistence failures shouldn't take down the game; the in-memory board still works
            // for the current session.
            System.Diagnostics.Debug.WriteLine($"Failed to save leaderboard: {ex.Message}");
        }
    }

    private class LeaderboardEntry
    {
        public string Player { get; set; }
        public string Score { get; set; }
    }
}