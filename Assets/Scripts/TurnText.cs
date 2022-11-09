using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnText : MonoBehaviour {

    private TextMeshProUGUI _text;
    private void Start() {
        // Initialize TurnText color and text
        _text = GetComponent<TextMeshProUGUI>();
        _text.color = new Color32(20, 20, 20, 255);
        _text.text = "Black Turn";
    }
}
   