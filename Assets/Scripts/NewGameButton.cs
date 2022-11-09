using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour {

    private void OnMouseDown() {
        // Reset appropriate Global variables before reloading the scene
        Global.blackTurn = true;
        Global.whiteTurn = false;
        Global.gameOver = false;
        Global.blackScore = 2;
        Global.whiteScore = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
