using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class BoardManager : MonoBehaviour {

    static private BoardManager S;   // a private Singleton

    [Header("Set in Inspector")]
    public Vector3 boardPos;            // The place to put boards
    public GameObject[] boards;         // An array of the boards

    [Header("Set Dynamically")]
    public int level;                   // The current level
    public int levelMax;                // The number of levels
    public GameObject board;            // The current board
    public GameMode mode = GameMode.idle;
    public GameObject player;
    public GameObject cam;

    void Start() {
        S = this; // Define the Singleton
        level = 0;
        levelMax = boards.Length;
        cam = GameObject.Find("Main Camera");
        StartLevel();
    }
    void StartLevel() {
        //reset hole conditions
        GoodHoles.goodHoleMet = false;
        BadHoles.badHoleMet = false;
        // reactivate the camera behaviour
        cam.GetComponent<CameraBehaviour>().enabled = true;
        // Get rid of the old board if one exists
        if (board != null) {
            Destroy(board);
        }
        // Instantiate the new board
        board = Instantiate<GameObject>(boards[level]);
        board.transform.position = boardPos;
        mode = GameMode.playing;
        Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
    }
    void UpdateGUI() {
        // does nothing right now,,, AUSTIN HELP (or do something else)
    }

    void Update() {
        // Check for level end
        if ((mode == GameMode.playing) && GoodHoles.goodHoleMet)// Replace Goal with the good hole 
        {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 2f);
        }
        if ((mode == GameMode.playing) && BadHoles.badHoleMet) {
            // restart the level
            StartLevel();
        }
    }

    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
        }
        StartLevel();
    }
}