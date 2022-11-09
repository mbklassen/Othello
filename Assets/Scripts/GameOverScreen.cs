using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour {

    private bool _isSetActive;
    
    private GameObject _blackOverlay;
    private GameObject _gameOverText;
    private GameObject _winnerText;
    private GameObject _newGameButton;

    private GameObject _tile;

    private TextMeshProUGUI _winnerTextTMP;


    private void Start() {
        // Get children of GameOverScreen and set them as inactive
        _blackOverlay = transform.GetChild(0).gameObject;
        _blackOverlay.SetActive(false);
        _gameOverText = transform.GetChild(1).gameObject;
        _gameOverText.SetActive(false);
        _winnerText = transform.GetChild(2).gameObject;
        _winnerTextTMP = _winnerText.GetComponent<TextMeshProUGUI>();
        _winnerText.SetActive(false);
        _newGameButton = transform.GetChild(3).gameObject;
        _newGameButton.SetActive(false);

        _isSetActive = false;
    }
    private void Update() {
        // If game is over and this code hasn't been run yet...
        if (Global.gameOver && !_isSetActive) {
            // Activate all children of GameOverText
            _blackOverlay.SetActive(true);
            _gameOverText.SetActive(true);
            _winnerText.SetActive(true);
            _newGameButton.SetActive(true);

            // Deactive all tiles when GameOverScreen is active (to prevent hover effect in background)
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    _tile = GameObject.Find($"Tile {i} {j}");
                    _tile.SetActive(false);
                }
            }

            PrintWinnerText();
            // Above code only runs once per game
            _isSetActive = true;
        }
    }

    private void PrintWinnerText() {
        
        // Displays outcome of game (depends on the final score)
        if (Global.whiteScore > Global.blackScore) {
            _winnerTextTMP.text = $"White Wins {Global.whiteScore}-{Global.blackScore}";
        }
        else if (Global.whiteScore < Global.blackScore) {
            _winnerTextTMP.text = $"Black Wins {Global.blackScore}-{Global.whiteScore}";
        }
        else {
            _winnerTextTMP.text = "Tie Game";
        }
            
    }
}
