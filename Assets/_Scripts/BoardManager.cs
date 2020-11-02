using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode 
{
    idle,
    playing,
    levelEnd
}

public class BoardManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject[] boards;         // An array of the boards
    public Text uiTimerText;     // The text element of the UI for the timer
    public Button uiTestButton;

    [Header("Set Dynamically")]
    public int level;                   // The current level
    public int levelMax;                // The number of levels
    public GameObject board;            // The current board
    public Vector3 spawn;            // The current player spawnpoint
    public GameMode mode;
    public GameObject player;

    void Start() 
    {
        uiTestButton.onClick.AddListener(OnTestButtonClicked);

        mode = GameMode.idle;
        level = 0;
        levelMax = boards.Length;
        StartLevel();
    }

    void Update()
    {
        // Check for level end
        if ((mode == GameMode.playing) && GoodHoles.goodHoleMet)
        {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            Invoke("NextLevel", 1f);
        }
        if ((mode == GameMode.playing) && BadHoles.badHoleMet)
        {
            // restart the level
            StartLevel();
        }
    }

    public void SpawnPlayer()
    {
        // Destroy any old player objects
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }

        // Spawn the new player object
        Instantiate<GameObject>(player, spawn, Quaternion.identity);
    }

    void StartLevel()
    {
        //reset hole conditions
        GoodHoles.goodHoleMet = false;
        BadHoles.badHoleMet = false;

        // Destroy the old board if one exists
        if (board != null)
        {
            Destroy(board);
        }

        // Destroy the old player if one exists
        var livePlayer = GameObject.FindGameObjectWithTag("Player");
        if (livePlayer != null)
        {
            Destroy(livePlayer);
        }

        // Instantiate the new board
        board = Instantiate<GameObject>(boards[level]);

        // Calculate the new spawn point
        spawn = board.transform.GetChild(0).transform.position;

        // Change the game mode.
        mode = GameMode.playing;

        SpawnPlayer();
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    void OnTestButtonClicked()
    {
        NextLevel();
    }
}