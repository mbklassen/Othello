using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackScore : MonoBehaviour {
    private TextMeshProUGUI _score;

    void Start() {
        // Initialize BlackScore color and text
        _score = GetComponent<TextMeshProUGUI>();
        _score.color = new Color32(20, 20, 20, 255);
        _score.text = "Black Score: 2";
    }
}
