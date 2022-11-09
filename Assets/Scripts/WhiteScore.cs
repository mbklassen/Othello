using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhiteScore : MonoBehaviour {

    private TextMeshProUGUI _score;

    void Start() {
        // Initialize BlackScore color and text
        _score = GetComponent<TextMeshProUGUI>();
        _score.color = new Color(255, 255, 255, 255);
        _score.text = "White Score: 2";
    }
}
