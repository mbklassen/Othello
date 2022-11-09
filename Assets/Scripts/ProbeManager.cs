using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeManager : MonoBehaviour
{
    private void Start() {
        // Initialize probeArray (for determining which pieces will "flip" when the current player places a piece)
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Global.probeArray[i, j] = 'n';
            }
        }
        // Initialize pseudoProbe array (for checking whether the next player will be able to move)
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                Global.pseudoProbeArray[i, j] = 'n';
            }
        }
    }
}
