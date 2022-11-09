using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour {

    [SerializeField] private GridManager _gridManager;

    private bool _isSetActive;

    private GameObject _turnText;
    private GameObject _blackScore;
    private GameObject _whiteScore;

    private TextMeshProUGUI _turnTextTMP;
    private TextMeshProUGUI _blackScoreTMP;
    private TextMeshProUGUI _whiteScoreTMP;

    private void Start() {
        // Get children of GameOverScreen and set them as active
        _turnText.SetActive(true);
        _turnTextTMP = _turnText.GetComponent<TextMeshProUGUI>();

        _blackScore = transform.GetChild(1).gameObject;
        _blackScore.SetActive(true);
        _blackScoreTMP = _blackScore.GetComponent<TextMeshProUGUI>();

        _whiteScore = transform.GetChild(2).gameObject;
        _whiteScore.SetActive(true);
        _whiteScoreTMP = _whiteScore.GetComponent<TextMeshProUGUI>();

        _isSetActive = true;
    }
    private void Update() {
        // If game is over and this code hasn't been run yet...
        if (Global.gameOver && _isSetActive) {
            // Deactivate all children of UIText
            _turnText.SetActive(false);
            _blackScore.SetActive(false);
            _whiteScore.SetActive(false);
            _isSetActive = false;
            // Above code only runs once per game
        }
        // If a new turn has just started...
        if (Global.newTurnStarted) {
            // Display the turn and scores of black and white
            PrintTurnText();
            PrintBlackScore();
            PrintWhiteScore();
            // If the function from GridManager, CheckIfGridFull(), returns true then end game
            if (_gridManager.CheckIfGridFull()) {
                Global.gameOver = true;
            }
            // Above code only runs once per turn
            Global.newTurnStarted = false;
        }
    }

    private void PrintTurnText() {
        // Display whose turn it is
        if (Global.whiteTurn) {
            _turnTextTMP.color = new Color32(255, 255, 255, 255);
            _turnTextTMP.text = "White Turn";
        }
        else if (Global.blackTurn) {
            _turnTextTMP.color = new Color32(20, 20, 20, 255);
            _turnTextTMP.text = "Black Turn";
        }
    }

    private void PrintBlackScore() {
        // Display Black score
        _blackScoreTMP.text = $"Black Score: {Global.blackScore}";
    }

    private void PrintWhiteScore() {
        // Display white score
        _whiteScoreTMP.text = $"White Score: {Global.whiteScore}";
    }
}
