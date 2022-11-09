using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] private GameObject _highlight;

    private GameObject _placementManager;
    private PiecePlacementManager _placementManagerGO;

    private int _xPosition;
    private int _zPosition;


    void Start() {
        _xPosition = (int)this.transform.position.x;
        _zPosition = (int)this.transform.position.z;

        _placementManager = GameObject.Find("PiecePlacementManager");
        _placementManagerGO = _placementManager.GetComponent<PiecePlacementManager>();
    }

    private void OnMouseEnter() {
        // When mouse is hovering, highlight the tile
        _highlight.SetActive(true);
    }

    private void OnMouseExit() {
        // When mouse stops hovering, stop highlighting the tile
        _highlight.SetActive(false);
    }
    private void OnMouseDown() {
        // When mouse is clicked down on a tile, if the tile is empty and move is valid, perform the following actions:
        if (Global.gridArray[_xPosition, _zPosition] == '0') {
            // Third argument is false because this is not a "pseudo" check
            // We intend to place a piece if we can
            if (_placementManagerGO.CheckMoveValidity(_xPosition, _zPosition, false)) {

                _placementManagerGO.playAudioClip();

                // If it is white turn and game is not over, place a white piece, flip appropriate pieces, and start new turn (black turn)
                if (Global.whiteTurn && !Global.gameOver) {
                    _placementManagerGO.PlaceWhitePiece(this.transform.position);
                    Global.whiteTurn = false;
                    Global.blackTurn = true;
                    Global.newTurnStarted = true;
                    _placementManagerGO.FlipPieces(this.transform.position.y);

                }
                // Else if it is black turn and game is not over, place a black piece, flip appropriate pieces, and start new turn (white turn)
                else if (Global.blackTurn && !Global.gameOver) {
                    _placementManagerGO.PlaceBlackPiece(this.transform.position);
                    Global.blackTurn = false;
                    Global.whiteTurn = true;
                    Global.newTurnStarted = true;
                    _placementManagerGO.FlipPieces(this.transform.position.y);
                }
            }
            // Else if move is not valid, check whether there are valid moves
            // If there are no valid moves, call a function that handles when a player is stuck (can't move)
            else {
                bool validMovesExist = _placementManagerGO.CheckForValidMoves();
                if (!validMovesExist) {
                    _placementManagerGO.HandleCasePlayerStuck();
                }
            }
        }
    }
}



