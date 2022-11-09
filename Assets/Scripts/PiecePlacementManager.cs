using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePlacementManager : MonoBehaviour {

    [SerializeField] private WhitePiece _whitePiece;
    [SerializeField] private BlackPiece _blackPiece;
    [SerializeField] private AudioClip[] _soundList;
    [SerializeField] private AudioSource _audioSource;

    private char _upLeft;
    private char _up;
    private char _upRight;
    private char _right;
    private char _downRight;
    private char _down;
    private char _downLeft;
    private char _left;

    private bool _canPlacePiece;
    private bool _whiteIsStuck;
    private bool _blackIsStuck;

    private char[,] _flagArray;

    public void playAudioClip() {
        // Selects a random clip from an array of sounds, provided through the Inspector, and plays it
        _audioSource.clip = _soundList[Random.Range(0, _soundList.Length)];
        _audioSource.Play();
    }

    public bool CheckMoveValidity(int xPos, int zPos, bool pseudoTest) {
        // i is used for looping through probeArray and pseudoProbeArray
        int i;
        _canPlacePiece = false;
        // Initialize the directions we will be checking for validity
        InitializeDirections(xPos, zPos);
        // _flagArray keeps track of where flags are being placed for valid moves
        // Whether probeArray or pseudoProbeArray is used depends on where CheckMoveValidity(...) is being called from
        // Need two separate arrays so that "pseduo" flags and real flags don't get mixed
        if (pseudoTest) {
            _flagArray = Global.pseudoProbeArray;
        }
        else {
            _flagArray = Global.probeArray;
        }
        // Handles the validity checks when it is white turn
        if (Global.whiteTurn) {
            
            // For each of the 8 directions, performs the following actions:
            // 1. While within the bounds of gridArray and the "line" of pieces in a given direction is marked 'b' for black, mark that line as 'y' for yes (flagged)
            // 2. If there is a piece at the end of the line of black pieces that is marked 'w' for white, then the move is valid and white can place the piece at (xPos, yPos)
            // 2.5. If this is a pseudoTest for validity (we are not actually placing a piece yet), then remove all 'y' flags that were placed in this line by labelling them 'n' for no
            // 3. Else if there is no black ('b') piece at the end of a given line, remove all flags in that line by changing 'y' to 'n'

            if (_upLeft == 'b') {
                i = 1;
                while ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'b')) {
                    _flagArray[xPos - i, zPos + i] = 'y';
                    i++;
                }

                if ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'b')) {
                            Global.pseudoProbeArray[xPos - i, zPos + i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'b')) {
                        _flagArray[xPos - i, zPos + i] = 'n';
                        i++;
                    }
                }
            }
            if (_up == 'b') {
                i = 1;
                while ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'b')) {
                    _flagArray[xPos, zPos + i] = 'y';
                    i++;
                }

                if ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'b')) {
                            Global.pseudoProbeArray[xPos, zPos + i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'b')) {
                        _flagArray[xPos, zPos + i] = 'n';
                        i++;
                    }
                }
            }
            if (_upRight == 'b') {
                i = 1;
                while ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'b')) {
                    _flagArray[xPos + i, zPos + i] = 'y';
                    i++;
                }

                if ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'b')) {
                            Global.pseudoProbeArray[xPos + i, zPos + i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'b')) {
                        _flagArray[xPos + i, zPos + i] = 'n';
                        i++;
                    }
                }
            }
            if (_right == 'b') {
                i = 1;
                while ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'b')) {
                    _flagArray[xPos + i, zPos] = 'y';
                    i++;
                }

                if ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'b')) {
                            Global.pseudoProbeArray[xPos + i, zPos] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'b')) {
                        _flagArray[xPos + i, zPos] = 'n';
                        i++;
                    }
                }
            }
            if (_downRight == 'b') {
                i = 1;
                while ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'b')) {
                    _flagArray[xPos + i, zPos - i] = 'y';
                    i++;
                }

                if ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'b')) {
                            Global.pseudoProbeArray[xPos + i, zPos - i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'b')) {
                        _flagArray[xPos + i, zPos - i] = 'n';
                        i++;
                    }
                }
            }
            if (_down == 'b') {
                i = 1;
                while ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'b')) {
                    _flagArray[xPos, zPos - i] = 'y';
                    i++;
                }

                if ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'b')) {
                            Global.pseudoProbeArray[xPos, zPos - i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'b')) {
                        _flagArray[xPos, zPos - i] = 'n';
                        i++;
                    }
                }
            }
            if (_downLeft == 'b') {
                i = 1;
                while ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'b')) {
                    _flagArray[xPos - i, zPos - i] = 'y';
                    i++;
                }

                if ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'b')) {
                            Global.pseudoProbeArray[xPos - i, zPos - i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'b')) {
                        _flagArray[xPos - i, zPos - i] = 'n';
                        i++;
                    }
                }
            }
            if (_left == 'b') {
                i = 1;
                while ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'b')) {
                    _flagArray[xPos - i, zPos] = 'y';
                    i++;
                }

                if ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'w')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'b')) {
                            Global.pseudoProbeArray[xPos - i, zPos] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'b')) {
                        _flagArray[xPos - i, zPos] = 'n';
                        i++;
                    }
                }
            }

            if (_canPlacePiece) {
                return true;
            }
            else {
                return false;
            }

        }
        // Handles the validity checks when it is black turn
        else if (Global.blackTurn) {

            // For each of the 8 directions, performs the following actions:
            // 1. While within the bounds of gridArray and the "line" of pieces in a given direction is marked 'w' for white, mark that line as 'y' for yes (flagged)
            // 2. If there is a piece at the end of the line of white pieces that is marked 'b' for white, then the move is valid and white can place the piece at (xPos, yPos)
            // 2.5. If this is a pseudoTest for validity (we are not actually placing a piece yet), then remove all 'y' flags that were placed in this line by labelling them 'n' for no
            // 3. Else if there is no white ('w') piece at the end of a given line, remove all flags in that line by changing 'y' to 'n'

            if (_upLeft == 'w') {
                i = 1;
                while ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'w')) {
                    _flagArray[xPos - i, zPos + i] = 'y';
                    i++;
                }

                if ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'w')) {
                            Global.pseudoProbeArray[xPos - i, zPos + i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos - i >= 0) && (zPos + i <= 7) && (Global.gridArray[xPos - i, zPos + i] == 'w')) {
                        _flagArray[xPos - i, zPos + i] = 'n';
                        i++;
                    }
                }
            }
            if (_up == 'w') {
                i = 1;
                while ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'w')) {
                    _flagArray[xPos, zPos + i] = 'y';
                    i++;
                }

                if ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'w')) {
                            Global.pseudoProbeArray[xPos, zPos + i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((zPos + i <= 7) && (Global.gridArray[xPos, zPos + i] == 'w')) {
                        _flagArray[xPos, zPos + i] = 'n';
                        i++;
                    }
                }
            }
            if (_upRight == 'w') {
                i = 1;
                while ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'w')) {
                    _flagArray[xPos + i, zPos + i] = 'y';
                    i++;
                }

                if ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'w')) {
                            Global.pseudoProbeArray[xPos + i, zPos + i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos + i <= 7) && (zPos + i <= 7) && (Global.gridArray[xPos + i, zPos + i] == 'w')) {
                        _flagArray[xPos + i, zPos + i] = 'n';
                        i++;
                    }
                }
            }
            if (_right == 'w') {
                i = 1;
                while ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'w')) {
                    _flagArray[xPos + i, zPos] = 'y';
                    i++;
                }

                if ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'w')) {
                            Global.pseudoProbeArray[xPos + i, zPos] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos + i <= 7) && (Global.gridArray[xPos + i, zPos] == 'w')) {
                        _flagArray[xPos + i, zPos] = 'n';
                        i++;
                    }
                }
            }
            if (_downRight == 'w') {
                i = 1;
                while ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'w')) {
                    _flagArray[xPos + i, zPos - i] = 'y';
                    i++;
                }

                if ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'w')) {
                            Global.pseudoProbeArray[xPos + i, zPos - i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos + i <= 7) && (zPos - i >= 0) && (Global.gridArray[xPos + i, zPos - i] == 'w')) {
                        _flagArray[xPos + i, zPos - i] = 'n';
                        i++;
                    }
                }
            }
            if (_down == 'w') {
                i = 1;
                while ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'w')) {
                    _flagArray[xPos, zPos - i] = 'y';
                    i++;
                }

                if ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'w')) {
                            Global.pseudoProbeArray[xPos, zPos - i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((zPos - i >= 0) && (Global.gridArray[xPos, zPos - i] == 'w')) {
                        _flagArray[xPos, zPos - i] = 'n';
                        i++;
                    }
                }
            }
            if (_downLeft == 'w') {
                i = 1;
                while ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'w')) {
                    _flagArray[xPos - i, zPos - i] = 'y';
                    i++;
                }

                if ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'w')) {
                            Global.pseudoProbeArray[xPos - i, zPos - i] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos - i >= 0) && (zPos - i >= 0) && (Global.gridArray[xPos - i, zPos - i] == 'w')) {
                        _flagArray[xPos - i, zPos - i] = 'n';
                        i++;
                    }
                }
            }
            if (_left == 'w') {
                i = 1;
                while ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'w')) {
                    _flagArray[xPos - i, zPos] = 'y';
                    i++;
                }

                if ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'b')) {
                    _canPlacePiece = true;
                    if (pseudoTest) {
                        i = 1;
                        while ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'w')) {
                            Global.pseudoProbeArray[xPos - i, zPos] = 'n';
                            i++;
                        }
                    }
                }
                else {
                    i = 1;
                    while ((xPos - i >= 0) && (Global.gridArray[xPos - i, zPos] == 'w')) {
                        _flagArray[xPos - i, zPos] = 'n';
                        i++;
                    }
                }
            }

            // If move is valid, than this function returns true, else false
            if (_canPlacePiece) {
                return true;
            }
            else {
                return false;
            }
        }

        // Prevents compiler error: not all code paths return a value
        else {
            return false;
        }
    }

    private void InitializeDirections(int xPos, int zPos) {
        // Set variable values to the pieces that exist one space away in each of the 8 directions for use is CheckMoveValidity(...)
        // If at the edge of a grid, set the variable to '~' for the invalid direction (ie. _left = '~' when at the far left of the grid)
        if (xPos != 0 && zPos != 7) {
            _upLeft = Global.gridArray[xPos - 1, zPos + 1];
        }
        else {
            _upLeft = '~';
        }

        if (zPos != 7) {
            _up = Global.gridArray[xPos, zPos + 1];
        }
        else {
            _up = '~';
        }

        if (xPos != 7 && zPos != 7) {
            _upRight = Global.gridArray[xPos + 1, zPos + 1];
        }
        else {
            _upRight = '~';
        }

        if (xPos != 7) {
            _right = Global.gridArray[xPos + 1, zPos];
        }
        else {
            _right = '~';
        }

        if (xPos != 7 && zPos != 0) {
            _downRight = Global.gridArray[xPos + 1, zPos - 1];
        }
        else {
            _downRight = '~';
        }

        if (zPos != 0) {
            _down = Global.gridArray[xPos, zPos - 1];
        }
        else {
            _down = '~';
        }

        if (xPos != 0 && zPos != 0) {
            _downLeft = Global.gridArray[xPos - 1, zPos - 1];
        }
        else {
            _downLeft = '~';
        }

        if (xPos != 0) {
            _left = Global.gridArray[xPos - 1, zPos];
        }
        else {
            _left = '~';
        }
    }

    public void PlaceBlackPiece(Vector3 newPiecePosition) {
        // Instantiate a black piece at a given postion
        // Update gridArray accordingly and increment blackScore
        Instantiate(_blackPiece, (newPiecePosition + new Vector3(0, 0.1f, 0)), Quaternion.identity);
        Global.gridArray[(int)newPiecePosition.x, (int)newPiecePosition.z] = 'b';
        Global.blackScore++;
    }

    public void PlaceWhitePiece(Vector3 newPiecePosition) {
        // Instantiate a black piece at a given postion
        // Update gridArray accordingly and increment blackScore
        Instantiate(_whitePiece, (newPiecePosition + new Vector3(0, 0.1f, 0)), Quaternion.identity);
        Global.gridArray[(int)newPiecePosition.x, (int)newPiecePosition.z] = 'w';
        Global.whiteScore++;
    }

    public void FlipPieces(float yPos) {
        // Loop through each element of probeArray to check whether a given position is flagged to "flip"
        // We are not actually "flipping" pieces in this case as there is no animation. Instead, we destroy the old piece and instantiate a new piece
        // The score of the destroyed color is decremented to account for the missing piece
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (Global.probeArray[i, j] == 'y') {
                    // Finds a given piece in the current scene
                    GameObject blackPiece = GameObject.Find($"BlackPiece {i} {j}");
                    GameObject whitePiece = GameObject.Find($"WhitePiece {i} {j}");

                    if (blackPiece != null) {
                        Destroy(blackPiece);
                        PlaceWhitePiece(new Vector3(i, yPos, j));
                        Global.blackScore--;
                    }
                    if (whitePiece != null) {
                        Destroy(whitePiece);
                        PlaceBlackPiece(new Vector3(i, yPos, j));
                        Global.whiteScore--;
                    }
                    // Mark the "line" of pieces as unflagged in probeArray by setting them equal to 'n' for no
                    Global.probeArray[i, j] = 'n';
                }
            }
        }
    }

    // This check is used for handling the case where player can not go
    public bool CheckForValidMoves() {
        bool validMovesExist = false;
        // Performs a "pseudo" check for each empty space in gridArray to see whether it is a valid move
        // We are not actually trying to place a piece here just check whether there are any valid moves for the given player
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (Global.gridArray[i, j] == '0') {
                    // Third argument is true because this is a pseudo check
                    if (CheckMoveValidity(i, j, true)) {
                        validMovesExist = true;
                        return validMovesExist;
                    }
                }
            }
        }
        return validMovesExist;
    }

    public void HandleCasePlayerStuck() {
        // If player is stuck than change global variables to reflect this and move to the next turn
        if (Global.whiteTurn) {
            Global.blackTurn = true;
            Global.whiteTurn = false;
            _whiteIsStuck = true;
            Global.newTurnStarted = true;
        }
        else if (Global.blackTurn) {
            Global.whiteTurn = true;
            Global.blackTurn = false;
            _blackIsStuck = true;
            Global.newTurnStarted = true;
        }
        // If both players are stuck then end the game
        if (_whiteIsStuck && _blackIsStuck) {
            Global.gameOver = true;
        }
    }
}
