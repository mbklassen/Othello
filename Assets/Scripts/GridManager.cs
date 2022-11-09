using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tile;
    [SerializeField] private Transform _cam;


    private const float TILE_Y_POS = 0.501f;
    private const float CAM_Y_POS = 10.0f;

    private void Start() {
        GenerateGrid();
    }

    private void GenerateGrid() {
        // Instantiate Tile prefab for each square on the board grid
        for (int x = 0; x < _width; x++) {
            for (int z = 0; z < _height; z++) {
                var spawnedTile = Instantiate(_tile, new Vector3(x, TILE_Y_POS, z), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {z}";
                // Initialize 2D gridArray to reflect a grid that is empty aside from the middle 4 squares
                // 'w' -> contains white piece, 'b' -> contains black piece, '0' -> is empty
                if (x == 3 && z == 3 || x == 4 && z == 4) {
                    Global.gridArray[x, z] = 'w';
                }
                else if  (x == 3 && z == 4 || x == 4 && z == 3) {
                    Global.gridArray[x, z] = 'b';
                }
                else {
                    Global.gridArray[x, z] = '0';
                }
            }
        }
        // Position camera in center of grid and above (in the y-direction)
        _cam.transform.position = new Vector3((float)_width/2 - 0.5f, CAM_Y_POS, (float)_height/2 - 0.5f);
    }

    // Check if grid is full (if there are no empty squares, UIText will set Global.gameOver to true)
    public bool CheckIfGridFull() {
        for (int x = 0; x < _width; x++) {
            for (int z = 0; z < _height; z++) {
                if (Global.gridArray[x, z] == '0') {
                    return false;
                }

            }
        }
        return true;
    }
}
