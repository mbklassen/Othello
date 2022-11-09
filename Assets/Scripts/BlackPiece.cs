using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPiece : MonoBehaviour {

    private void Start() {
        // Set name based on position to make individual pieces easier to find
        this.name = $"BlackPiece {this.transform.position.x} {this.transform.position.z}";
    }
}
