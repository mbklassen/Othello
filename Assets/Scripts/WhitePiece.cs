using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitePiece : MonoBehaviour {

    private void Start() {
        // Set name based on position to make individual pieces easier to find
        this.name = $"WhitePiece {this.transform.position.x} {this.transform.position.z}";
    }
}
